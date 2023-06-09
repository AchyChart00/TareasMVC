using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json.Serialization;
using TareasMVC.Servicios;
using TareasMVC.Servicios.IServices;

namespace TareasMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //agregamos un filtro para que solo acepte usuarios autenticados
            var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();


            // Add services to the container.
            builder.Services.AddControllersWithViews(opciones =>
            {
                opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(opciones =>
            {
                //anotaciones de datos para traducciones
                opciones.DataAnnotationLocalizerProvider = (_, factoria) => factoria.Create(typeof(RecursoCompartido));
            }).AddJsonOptions(opciones =>
            {
                //Esto ayuda a solucionar el problema de referencias ciclicas que producen las entidades de PASO y TAREA
                opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services
                .AddDbContext<ApplicationDbContext>(
                    opciones => opciones.UseSqlServer("name=DefaultConnection")
                    );

            //Agrego el servicio de autenticación para que el usuario se pueda logear
            builder.Services.AddAuthentication().AddMicrosoftAccount(opciones =>
            {
                opciones.ClientId = builder.Configuration["MicrosoftClientId"];
                opciones.ClientSecret = builder.Configuration["MicrosoftSecretId"];
            });

            //Agergo los servicios de identity, puedo pasar una clase personalizada o unas clases por defecto 
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
            {
                //No Se requiere una cuenta confirmada para que le usuario pueda logearse
                opciones.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opciones =>
                {
                    opciones.LoginPath = "/usuarios/login";
                    opciones.AccessDeniedPath = "/usuarios/login";

                });

            //localización
            builder.Services.AddLocalization(opciones =>
            {
                opciones.ResourcesPath = "Recursos";
            });

            //inyección de dependensia
            builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();
           //Inyección de automapper
            builder.Services.AddAutoMapper(typeof(Program));
            //inyección almacenador archivo azure
            //builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();
            //inyección almacenador archivos local
            builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();

            var app = builder.Build();

            //seleccionar idioma manualmente
            //var CulturasUISoportadas = new[] { "es", "en" };

            app.UseRequestLocalization(opciones =>
            {
                opciones.DefaultRequestCulture = new RequestCulture("es");
                opciones.SupportedUICultures = Constantes.CulturasUISoportadas.Select(cultura => new CultureInfo(cultura.Value)).
                ToList();
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}