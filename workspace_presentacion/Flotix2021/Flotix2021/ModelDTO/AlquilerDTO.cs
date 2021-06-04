
namespace Flotix2021.ModelDTO
{
    public class AlquilerDTO
    {
        private string _id;

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _idVehiculo;

        public string idVehiculo
        {
            get { return _idVehiculo; }
            set { _idVehiculo = value; }
        }

        private VehiculoDTO _vehiculo;

        public VehiculoDTO vehiculo
        {
            get { return _vehiculo; }
            set { _vehiculo = value; }
        }

        private string _idCliente;

        public string idCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        private ClienteDTO _cliente;

        public ClienteDTO cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }


        private string _fechaInicio;

        public string fechaInicio
        {
            get { return _fechaInicio; }
            set { _fechaInicio = value; }
        }

        private string _fechaFin;

        public string fechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }

        private int _km;

        public int km
        {
            get { return _km; }
            set { _km = value; }
        }

        private string _tipoKm;

        public string tipoKm
        {
            get { return _tipoKm; }
            set { _tipoKm = value; }
        }

        private double _importe;

        public double importe
        {
            get { return _importe; }
            set { _importe = value; }
        }

        private string _tipoImporte;

        public string tipoImporte
        {
            get { return _tipoImporte; }
            set { _tipoImporte = value; }
        }

    }
}
