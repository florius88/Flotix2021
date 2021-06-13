using Flotix2021.ModelDTO;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    class ServerResponseTipoMantenimiento
    {
        private List<TipoMantenimientoDTO> _listaTipoMantenimiento;

        public List<TipoMantenimientoDTO> listaTipoMantenimiento
        {
            get { return _listaTipoMantenimiento; }
            set { _listaTipoMantenimiento = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
