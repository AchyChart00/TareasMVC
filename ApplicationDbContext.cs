using Microsoft.EntityFrameworkCore;
using TareasMVC.Entidades;

namespace TareasMVC
{
    public class ApplicationDbContext : DbContext
    {
        //applicationDbContext es arbitrario puedo usar cualquier nombre, el dbContext
        //Podemos configurar las tablas de nuestras bases de datos y hacer consultas o cualquier tipo de operación.
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        //Configuro nuestra clase Tarea como una entidad
        public DbSet<Tarea> Tareas { get; set; }
        //Migraciones, es una representación en código de los cambios que va a ocurrir en la BD
        //es un paso intermedio, ya que podemos ver que exactamente va a pasar con nuestra base de datos
        //Para eso utilizamos el package manager console

        //Para aplicar la migración debemos de abrir el Package Manager Console
        //Ejecutamos el comando "Add-Migration Tareas"
        //Después ejecutamos el comando Update-Database para aplicar los cambios a la base de datos


    }
}
