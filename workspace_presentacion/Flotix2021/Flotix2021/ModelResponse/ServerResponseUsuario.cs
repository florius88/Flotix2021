using Flotix2021.ModelDTO;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    class ServerResponseUsuario
    {
        private string _idUsuario;

        public string idUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        private List<UsuarioDTO> _listaUsuario;

        public List<UsuarioDTO> listaUsuario
        {
            get { return _listaUsuario; }
            set { _listaUsuario = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
