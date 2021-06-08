using Flotix2021.Helpers;
using Flotix2021.ModelResponse;
using Flotix2021.Utils;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Flotix2021.Services
{
    class ServerServiceTipoMantenimiento
    {
        private readonly string TIPO_MANTENIMIENTO = "tipomantenimiento/";

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseTipoMantenimiento</returns>
        public ServerResponseTipoMantenimiento GetAll()
        {
            ServerResponseTipoMantenimiento serverResponseTipoMantenimiento;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + TIPO_MANTENIMIENTO + "all";

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseTipoMantenimiento = JsonSerializer.Deserialize<ServerResponseTipoMantenimiento>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseTipoMantenimiento = new ServerResponseTipoMantenimiento();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseTipoMantenimiento.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseTipoMantenimiento = new ServerResponseTipoMantenimiento();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseTipoMantenimiento.error = error;
            }

            return serverResponseTipoMantenimiento;
        }

    }
}
