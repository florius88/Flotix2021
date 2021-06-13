
namespace Flotix2021.Model
{
    /// <summary>
    /// Objeto Caducidad para mandar al servidor
    /// </summary>
    class Caducidad
    {
        public string idVehiculo { get; set; }
        public string ultimaITV { get; set; }
        public string proximaITV { get; set; }
        public string vencimientoVehiculo { get; set; }
        public bool baja { get; set; }
    }
}