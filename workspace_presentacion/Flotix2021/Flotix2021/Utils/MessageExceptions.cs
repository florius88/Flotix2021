using System;
using System.Collections.Generic;
using System.Text;

namespace Flotix2021.Utils
{
    public class MessageExceptions
    {
		public static readonly int OK_CODE = 200;
		public static readonly int NOT_FOUND_CODE = 300;
		public static readonly int GENERIC_ERROR_CODE = 500;
		public static readonly int SERVER_ERROR = 600;

		public static readonly string MSSG_SERVER_ERROR = "No se ha podido conectar con el servidor, contacte con el servicio técnico.";
    }
}
