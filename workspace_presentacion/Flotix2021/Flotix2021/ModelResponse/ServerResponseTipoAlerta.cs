using Flotix2021.ModelDTO;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    class ServerResponseTipoAlerta
    {
        private List<TipoAlertaDTO> _listaTipoAlerta;

        public List<TipoAlertaDTO> listaTipoAlerta
        {
            get { return _listaTipoAlerta; }
            set { _listaTipoAlerta = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
