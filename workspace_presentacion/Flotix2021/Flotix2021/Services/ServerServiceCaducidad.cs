using Flotix2021.Helpers;
using Flotix2021.Model;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Utils;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Flotix2021.Services
{
    /// <summary>
    /// Controlador que gestiona las caducidades
    /// </summary>
    class ServerServiceCaducidad
    {
        private readonly string CADUCIDAD = "caducidad/";

        // ___________________________________________________ TODO Filtro: VARIABLE: Matricula
        public ServerResponseCaducidad GetAllFilter(string matricula)
        {
            ServerResponseCaducidad serverResponseCaducidad = null;

            return serverResponseCaducidad;
        }

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseCaducidad</returns>
        public ServerResponseCaducidad GetAll() 
        {
            ServerResponseCaducidad serverResponseCaducidad = null;

            OauthToken oauthToken = ServerService.obtenerToken();

            var url = Constantes.SERVIDOR + CADUCIDAD + "all";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                serverResponseCaducidad = JsonSerializer.Deserialize<ServerResponseCaducidad>(result);
            }

            //Console.WriteLine(httpResponse.StatusCode);

            return serverResponseCaducidad;
        }

        /// <summary>
        /// Devuelve los datos con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseCaducidad</returns> 
        public ServerResponseCaducidad Find(string id)
        {
            ServerResponseCaducidad serverResponseCaducidad = null;

            return serverResponseCaducidad;
        }

        /// <summary>
        /// Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el objeto de la BD
        /// </summary>
        /// <param name="caducidad">objeto de BD</param>
        /// <param name="id"></param>
        /// <returns>ServerResponseCaducidad</returns>
        public ServerResponseCaducidad Save(Caducidad caducidad, string id)
        {
            ServerResponseCaducidad serverResponseCaducidad = null;

            return serverResponseCaducidad;
        }

        /* _______________________________________ TODO BAJA LOGICA?  ___________________________________________________ */
        public ServerResponseCaducidad Delete(string id)
        {
            ServerResponseCaducidad serverResponseCaducidad = null;

            return serverResponseCaducidad;
        }

        private Caducidad transformCaducidadDTOToCaducidad(CaducidadDTO caducidadDTO)
        {
            Caducidad caducidad = new Caducidad();
            // TODO

            return caducidad;
        }
    }
}
