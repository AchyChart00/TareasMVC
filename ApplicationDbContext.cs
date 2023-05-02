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

    }
}
