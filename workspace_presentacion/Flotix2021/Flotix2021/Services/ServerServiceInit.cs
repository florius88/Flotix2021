using Flotix2021.Helpers;
using Flotix2021.ModelResponse;
using Flotix2021.Utils;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Flotix2021.Services
{
    class ServerServiceInit
    {
        private readonly string INIT = "init/";

        /// <summary>
        /// Carga inicial de la aplicacion
        /// </summary>
        /// <returns>ServerResponseInit</returns>
        public ServerResponseInit Load()
        {
            ServerResponseInit serverResponseInit;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + INIT + "load";

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "POST";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = oauthToken.token_type + " " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseInit = JsonSerializer.Deserialize<ServerResponseInit>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseInit = new ServerResponseInit();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseInit.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseInit = new ServerResponseInit();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseInit.error = error;
            }

            return serverResponseInit;
        }
    }
}