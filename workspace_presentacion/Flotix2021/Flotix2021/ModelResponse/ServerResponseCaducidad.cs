using Flotix2021.ModelDTO;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    class ServerResponseCaducidad
    {
        private string _idCaducidad;

        public string idCaducidad
        {
            get { return _idCaducidad; }
            set { _idCaducidad = value; }
        }

        private List<CaducidadDTO> _listaCaducidad;

        public List<CaducidadDTO> listaCaducidad
        {
            get { return _listaCaducidad; }
            set { _listaCaducidad = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
