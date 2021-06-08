using Flotix2021.Helpers;
using Flotix2021.ModelResponse;
using Flotix2021.Utils;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Flotix2021.Services
{
    class ServerServiceTipoAlerta
    {
        private readonly string TIPO_ALERTA = "tipoalerta/";

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseTipoAlerta</returns>
        public ServerResponseTipoAlerta GetAll()
        {
            ServerResponseTipoAlerta serverResponseTipoAlerta;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + TIPO_ALERTA + "all";

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseTipoAlerta = JsonSerializer.Deserialize<ServerResponseTipoAlerta>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseTipoAlerta = new ServerResponseTipoAlerta();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseTipoAlerta.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseTipoAlerta = new ServerResponseTipoAlerta();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseTipoAlerta.error = error;
            }

            return serverResponseTipoAlerta;
        }
    }
}
