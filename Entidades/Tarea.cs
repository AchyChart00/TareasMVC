using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TareasMVC.Entidades
{
    //Creación de una entidad
    //Entidad es una representación de una tabla en C#
    public class Tarea
    {
        //configuraciones
        public int Id { get; set; }
        //atributo de configuración para mi tabla
        [StringLength(250)]
        [Required]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacionId { get; set; }//relación de tareas a tabla usuarios
        public IdentityUser UsuarioCreacion { get; set; }//propiedad de navegación
        //propiedad de navegación
        //una tarea le corresponden muchos pasos
        public List<Paso> Pasos { get; set; }
        //Propiedad de navegación
        public List<ArchivoAdjunto> ArchivosAdjuntos { get; set; }
    }
}
