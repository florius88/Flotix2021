using Flotix2021.Helpers;
using Flotix2021.Model;
using Flotix2021.ModelResponse;
using Flotix2021.Utils;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Flotix2021.Services
{
    /// <summary>
    /// Controlador que gestiona los alquileres
    /// </summary>
    class ServerServiceAlquiler
    {
        private readonly string ALQUILER = "alquiler/";

        /// <summary>
        /// Devuelve los datos con los filtros: Cliente, Matricula y Periodo
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="matricula"></param>
        /// <param name="periodo"></param>
        /// <returns>ServerResponseAlquiler</returns>
        public ServerResponseAlquiler GetAllFilter(string cliente, string matricula, string periodo)
        {
            ServerResponseAlquiler serverResponseAlquiler = null;

            return serverResponseAlquiler;
        }

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseAlquiler</returns>
        public ServerResponseAlquiler GetAll()
        {
            ServerResponseAlquiler serverResponseAlquiler = null;

            OauthToken oauthToken = ServerService.obtenerToken();

            var url = Constantes.SERVIDOR + ALQUILER + "all";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                serverResponseAlquiler = JsonSerializer.Deserialize<ServerResponseAlquiler>(result);
            }

            //Console.WriteLine(httpResponse.StatusCode);

            return serverResponseAlquiler;
        }

        /// <summary>
        /// Devuelve los datos con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseAlquiler</returns>
        public ServerResponseAlquiler Find(string id)
        {
            ServerResponseAlquiler serverResponseAlquiler = null;

            return serverResponseAlquiler;
        }

        /// <summary>
        /// Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el objeto de la BD
        /// </summary>
        /// <param name="alquiler">objeto de BD</param>
        /// <param name="id"></param>
        /// <returns>ServerResponseAlquiler</returns>
        public ServerResponseAlquiler Save(Alquiler alquiler, string id)
        {
            ServerResponseAlquiler serverResponseAlquiler = null;

            return serverResponseAlquiler;
        }

    }
}
