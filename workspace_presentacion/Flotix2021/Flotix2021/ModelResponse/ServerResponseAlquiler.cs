using Flotix2021.ModelDTO;
using System;
using System.Collections.Generic;

namespace Flotix2021.ModelResponse
{
    class ServerResponseAlquiler
    {
        private string _idAlquiler;

        public string idAlquiler
        {
            get { return _idAlquiler; }
            set { _idAlquiler = value; }
        }

        private List<AlquilerDTO> _listaAlquiler;

        public List<AlquilerDTO> listaAlquiler
        {
            get { return _listaAlquiler; }
            set { _listaAlquiler = value; }
        }

        private ErrorBean _error;

        public ErrorBean error
        {
            get { return _error; }
            set { _error = value; }
        }

    }
}
