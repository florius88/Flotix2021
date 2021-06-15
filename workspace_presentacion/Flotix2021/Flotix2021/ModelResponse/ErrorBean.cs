using System;
using System.Collections.Generic;
using System.Text;

namespace Flotix2021.ModelResponse
{
    public class ErrorBean
    {
        private int _code;

        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _message;

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

    }
}
