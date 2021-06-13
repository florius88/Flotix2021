using Flotix2021.Model;

namespace Flotix2021.ModelResponse
{
    public class ServerResponseImagenVehiculo
    {
        private string _idImagenVehiculo;

        public string idImagenVehiculo
        {
            get { return _idImagenVehiculo; }
            set { _idImagenVehiculo = value; }
        }

        private ImagenVehiculo _imagenVehiculo;

        public ImagenVehiculo imagenVehiculo
        {
            get { return _imagenVehiculo; }
            set { _imagenVehiculo = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
