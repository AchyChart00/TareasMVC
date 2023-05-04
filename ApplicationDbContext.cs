using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Entidades;

namespace TareasMVC
{
    //public class ApplicationDbContext : DbContext nos define que tablas vamos a utilizar
    //IdentityDbContext nos trae unas tablas por defecto para trabajar con usuarios
    public class ApplicationDbContext : IdentityDbContext
    {
        //applicationDbContext es arbitrario puedo usar cualquier nombre, el dbContext
        //Podemos configurar las tablas de nuestras bases de datos y hacer consultas o cualquier tipo de operación.
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        //api fluente, para configuraciones como alternativa. si no funcionan o tenemos probleams con los atributos en nuestras clases tipo entidad
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Es equivalente a 
            //atributo de configuración para mi tabla
            //[StringLength(250)]
            //[Required]
            //public string Titulo { get; set; }
            //modelBuilder.Entity<Tarea>().Property(t=>t.Titulo).HasMaxLength(250).IsRequired();
        }
        //Configuro nuestra clase Tarea como una entidad
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Paso> Pasos { get; set; }
        public DbSet<ArchivoAdjunto> ArchivosAdjuntos { get; set; } 
        //Migraciones, es una representación en código de los cambios que va a ocurrir en la BD
        //es un paso intermedio, ya que podemos ver que exactamente va a pasar con nuestra base de datos
        //Para eso utilizamos el package manager console

        //Para aplicar la migración debemos de abrir el Package Manager Console
        //Ejecutamos el comando "Add-Migration Tareas"
        //Después ejecutamos el comando Update-Database para aplicar los cambios a la base de datos


    }
}
