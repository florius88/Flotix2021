using System;
using System.Collections.Generic;
using System.Text;

namespace Flotix2021.ModelDTO
{
    public class TipoAlertaDTO
    {
        private string _id;

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
