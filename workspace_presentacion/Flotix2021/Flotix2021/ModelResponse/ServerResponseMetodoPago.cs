using Flotix2021.ModelDTO;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    class ServerResponseMetodoPago
    {
        private List<MetodoPagoDTO> _listaMetodoPago;

        public List<MetodoPagoDTO> listaMetodoPago
        {
            get { return _listaMetodoPago; }
            set { _listaMetodoPago = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
