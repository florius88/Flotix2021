using Flotix2021.Helpers;
using Flotix2021.ModelResponse;
using Flotix2021.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Flotix2021.Services
{
    public class ServerServiceVehiculo
    {
        private readonly string VEHICULO = "vehiculo/";

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
