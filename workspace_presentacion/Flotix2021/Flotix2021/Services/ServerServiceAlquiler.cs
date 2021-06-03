using Flotix2021.Helpers;
using Flotix2021.ModelResponse;
using Flotix2021.Utils;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Flotix2021.Services
{
    class ServerServiceAlquiler
    {
        private readonly string ALQUILER = "alquiler/";

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

    }
}
