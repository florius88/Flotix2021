using Flotix2021.ModelDTO;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    class ServerResponseAlerta
    {
        private string _idAlerta;

        public string idAlerta
        {
            get { return _idAlerta; }
            set { _idAlerta = value; }
        }

        private List<AlertaDTO> _listaAlerta;

        public List<AlertaDTO> listaAlerta
        {
            get { return _listaAlerta; }
            set { _listaAlerta = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
