using Flotix2021.ModelDTO;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    class ServerResponseCliente
    {
        private string _idCliente;

        public string idCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        private List<ClienteDTO> _listaCliente;

        public List<ClienteDTO> listaCliente
        {
            get { return _listaCliente; }
            set { _listaCliente = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
