using Flotix2021.Helpers;
using Flotix2021.ModelResponse;
using Flotix2021.Utils;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Flotix2021.Services
{
    public class ServerServiceVehiculo
    {
        private readonly string VEHICULO = "vehiculo/";

        /// <summary>
        /// Devuelve los datos con los filtros: matricula, plazas, capacidad y dispoiblilidad
        /// </summary>
        /// <param name="matricula"></param>
        /// <param name="plazas"></param>
        /// <param name="capacidad"></param>
        /// <param name="dispoiblilidad"></param>
        /// <returns>ServerResponseAlerta</returns>
        public ServerResponseVehiculo GetAllFilter(string matricula, string plazas, string capacidad, string dispoiblilidad)
        {
            ServerResponseVehiculo serverResponseVehiculo;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                var url = Constantes.SERVIDOR + VEHICULO + "allFilter/" + matricula + "/" + plazas + "/" + capacidad + "/" + dispoiblilidad;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    serverResponseVehiculo = JsonSerializer.Deserialize<ServerResponseVehiculo>(result);
                }

                //Console.WriteLine(httpResponse.StatusCode);

            }
            catch (System.Exception)
            {
                serverResponseVehiculo = new ServerResponseVehiculo();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseVehiculo.error = error;
            }

            return serverResponseVehiculo;
        }

        public ServerResponseVehiculo GetAll()
        {
            ServerResponseVehiculo serverResponseVehiculo = null;

            OauthToken oauthToken = ServerService.obtenerToken();

            var url = Constantes.SERVIDOR + VEHICULO + "all";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                serverResponseVehiculo = JsonSerializer.Deserialize<ServerResponseVehiculo>(result);
            }

            //Console.WriteLine(httpResponse.StatusCode);

            return serverResponseVehiculo;
        }

    }
}
