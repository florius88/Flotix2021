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
    /// <summary>
    /// Controlador que gestiona los usuarios
    /// </summary>
    class ServerServiceUsuario
    {
        private readonly string USUARIO = "usuario/";

        /// <summary>
        /// Devuelve los datos con los filtros: Tipo, Cliente y Matricula
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ServerResponseUsuario GetLogin(string email, string password)
        {
            ServerResponseUsuario serverResponseUsuario;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                var url = Constantes.SERVIDOR + USUARIO + "login/" + email + "/" + password;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    serverResponseUsuario = JsonSerializer.Deserialize<ServerResponseUsuario>(result);
                }

                //Console.WriteLine(httpResponse.StatusCode);

            }
            catch (System.Exception)
            {
                serverResponseUsuario = new ServerResponseUsuario();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseUsuario.error = error;
            }

            return serverResponseUsuario;
        }

    }
}
