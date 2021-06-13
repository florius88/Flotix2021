
namespace Flotix2021.Model
{
    /// <summary>
    /// Objeto Cliente para mandar al servidor
    /// </summary>
    class Cliente
    {
        public string nif { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string poblacion { get; set; }
        public string personaContacto { get; set; }
        public string tlfContacto { get; set; }
        public string email { get; set; }
        public string idMetodoPago { get; set; }
        public string cuentaBancaria { get; set; }
        public bool baja { get; set; }
	}
}
