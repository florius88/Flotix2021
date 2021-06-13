
namespace Flotix2021.Model
{
    /// <summary>
    /// Objeto Mantenimiento para mandar al servidor
    /// </summary>
    class Mantenimiento
    {
        public string ultimoMantenimiento { get; set; }
        public string proximoMantenimiento { get; set; }
        public int kmMantenimiento { get; set; }
        public string idVehiculo { get; set; }
        public string idTipoMantenimiento { get; set; }
        public bool baja { get; set; }
    }
}
