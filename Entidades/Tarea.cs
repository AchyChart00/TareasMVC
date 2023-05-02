namespace TareasMVC.Entidades
{
    //Creación de una entidad
    //Entidad es una representación de una tabla en C#
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
