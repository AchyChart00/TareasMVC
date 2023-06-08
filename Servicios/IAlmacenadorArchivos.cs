using TareasMVC.Models;

namespace TareasMVC.Servicios
{
    public interface IAlmacenadorArchivos
    {
        Task Borrar(string ruta, string contenedor);
        Task<AlmacenarArchivoResultado[]> Almacenar(
            string contenedor, 
            //IFormFile es el tipo de dato que utilizamos en ASP .NET CORE para representar un archivo
            IEnumerable<IFormFile> archivos 
            );
    }
}
