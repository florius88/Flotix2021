﻿using Flotix2021.Helpers;
using Flotix2021.ModelResponse;
using Flotix2021.Utils;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Flotix2021.Services
{
    /// <summary>
    /// Controlador que gestiona las alertas
    /// </summary>
    class ServerServiceAlerta
    {
        private readonly string ALERTA = "alerta/";

        /// <summary>
        /// Devuelve los datos con los filtros: Tipo, Cliente y Matricula
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="cliente"></param>
        /// <param name="matricula"></param>
        /// <returns>ServerResponseAlerta</returns>
        public ServerResponseAlerta GetAllFilter(string tipo, string cliente, string matricula)
        {
            ServerResponseAlerta serverResponseAlerta;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                var url = Constantes.SERVIDOR + ALERTA + "allFilter/" + tipo + "/" + cliente + "/" + matricula;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    serverResponseAlerta = JsonSerializer.Deserialize<ServerResponseAlerta>(result);
                }

                //Console.WriteLine(httpResponse.StatusCode);

            }
            catch (System.Exception)
            {
                serverResponseAlerta = new ServerResponseAlerta();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseAlerta.error = error;
            }

            return serverResponseAlerta;
        }

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseAlerta</returns>
        public ServerResponseAlerta GetAll()
        {
            ServerResponseAlerta serverResponseAlerta = null;

            OauthToken oauthToken = ServerService.obtenerToken();

            var url = Constantes.SERVIDOR + ALERTA + "all";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                serverResponseAlerta = JsonSerializer.Deserialize<ServerResponseAlerta>(result);
            }

            //Console.WriteLine(httpResponse.StatusCode);

            return serverResponseAlerta;
        }

        /// <summary>
        /// Devuelve los datos con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseAlerta</returns>
        public ServerResponseAlerta Find(string id)
        {
            ServerResponseAlerta serverResponseAlerta = null;

            return serverResponseAlerta;
        }

        /// <summary>
        /// Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el objeto de la BD
        /// </summary>
        /// <param name="alerta">objeto de BD</param>
        /// <param name="id"></param>
        /// <returns>ServerResponseAlerta</returns>
        public ServerResponseAlerta Save(Alerta alerta, string id)
        {
            ServerResponseAlerta serverResponseAlerta = null;

            return serverResponseAlerta;
        }

        // _______________________________________ TODO METODO DE SERVIDOR ___________________________________________________ 
        public ServerResponseAlerta Delete(string id)
        {
            ServerResponseAlerta serverResponseAlerta = null;

            return serverResponseAlerta;
        }
    }
}
