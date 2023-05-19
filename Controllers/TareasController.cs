using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Entidades;
using TareasMVC.Models;
using TareasMVC.Servicios.IServices;

namespace TareasMVC.Controllers
{
    //controllerBase no soporte vistas y es para webAPI
    //No es necesario agregar la palabra api
    [Route("api/tareas")]
    public class TareasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicioUsuarios servicioUsuarios;

        public TareasController(
                ApplicationDbContext context,
                IServicioUsuarios servicioUsuarios
            )
        {
            this.context = context;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        //public async Task<List<Tarea>> Get()
        //public async Task<IActionResult> Get()
        //Agregamos un DTO para evitar errores al momento de regresar algo en el metodo
        public async Task<List<TareaDTO>> Get()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tareas = await context.Tareas.Where(t=>t.UsuarioCreacionId== usuarioId)
                .OrderBy(t=>t.Orden)
                .Select(t=> new TareaDTO
                {
                    //Tenemos un problema si necesitamos mapear manuelmente más de 5 campos
                    Id = t.Id,
                    Titulo =t.Titulo
                })
                .ToListAsync();    
            return tareas;  
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> Post([FromBody] string titulo)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            var existenTareas = await context.Tareas.AnyAsync(t => t.UsuarioCreacionId == usuarioId);

            var ordenMayor = 0;

            if (existenTareas)
            {
                ordenMayor = await context.Tareas.Where(t => t.UsuarioCreacionId == usuarioId)
                    .Select(t=>t.Orden).MaxAsync();
            }

            var tarea = new Tarea
            {
                Titulo = titulo,    
                UsuarioCreacionId = usuarioId,  
                FechaCreacion = DateTime.UtcNow,
                Orden = ordenMayor + 1
            };    

            context.Add( tarea );

            await context.SaveChangesAsync();
            
            return tarea;


        }
    }
}
