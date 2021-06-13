using Flotix2021.Helpers;
using Flotix2021.ModelResponse;
using Flotix2021.Utils;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Flotix2021.Services
{
    class ServerServiceMetodoPago
    {
        private readonly string METODO_PAGO = "metodopago/";

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseMetodoPago</returns>
        public ServerResponseMetodoPago GetAll()
        {
            ServerResponseMetodoPago serverResponseMetodoPago;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + METODO_PAGO + "all";

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseMetodoPago = JsonSerializer.Deserialize<ServerResponseMetodoPago>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseMetodoPago = new ServerResponseMetodoPago();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseMetodoPago.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseMetodoPago = new ServerResponseMetodoPago();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseMetodoPago.error = error;
            }

            return serverResponseMetodoPago;
        }
    }
}
