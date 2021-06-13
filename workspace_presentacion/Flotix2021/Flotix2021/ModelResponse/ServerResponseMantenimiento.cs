using Flotix2021.ModelDTO;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    class ServerResponseMantenimiento
    {
        private string _idMantenimiento;

        public string idMantenimiento
        {
            get { return _idMantenimiento; }
            set { _idMantenimiento = value; }
        }

        private List<MantenimientoDTO> _listaMantenimiento;

        public List<MantenimientoDTO> listaMantenimiento
        {
            get { return _listaMantenimiento; }
            set { _listaMantenimiento = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
