
namespace Flotix2021.Model
{
    /// <summary>
    /// Objeto Vehiculo para mandar al servidor
    /// </summary>
    class Vehiculo
    {
        public string matricula { get; set; }
        public string fechaMatriculacion { get; set; }
        public string modelo { get; set; }
        public int plazas { get; set; }
        public int capacidad { get; set; }
        public int km { get; set; }
        public bool disponibilidad { get; set; }
        public bool baja { get; set; }
        public string nombreImagen { get; set; }
        public string nombreImagenPermiso { get; set; }
    }
}
