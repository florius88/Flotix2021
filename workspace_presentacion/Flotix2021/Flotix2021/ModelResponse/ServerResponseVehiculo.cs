using Flotix2021.Helpers;
using Flotix2021.ModelDTO;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    public class ServerResponseVehiculo
    {
        private string _idVehiculo;

        public string idVehiculo
        {
            get { return _idVehiculo; }
            set { _idVehiculo = value; }
        }

        private List<VehiculoDTO> _listaVehiculo;

        public List<VehiculoDTO> listaVehiculo
        {
            get { return _listaVehiculo; }
            set { _listaVehiculo = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }

    }
}
