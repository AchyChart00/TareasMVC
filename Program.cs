using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddLocalization();

            var app = builder.Build();

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