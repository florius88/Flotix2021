
namespace Flotix2021.Model
{
    /// <summary>
    /// Objeto Alquiler para mandar al servidor
    /// </summary>
    class Alquiler
    {
        public string idVehiculo { get; set; }
        public string idCliente { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public int km { get; set; }
        public string tipoKm { get; set; }
        public string importe { get; set; }
        public string tipoImporte { get; set; }
	}
}
