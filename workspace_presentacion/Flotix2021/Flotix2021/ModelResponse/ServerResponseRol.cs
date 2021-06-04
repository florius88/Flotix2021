using Flotix2021.ModelDTO;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    class ServerResponseRol
    {
        private List<RolDTO> _listaRol;

        public List<RolDTO> listaRol
        {
            get { return _listaRol; }
            set { _listaRol = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
