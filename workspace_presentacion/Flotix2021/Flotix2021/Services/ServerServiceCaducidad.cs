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

        /// <summary>
        /// Devuelve los datos con el filtro Matricula
        /// </summary>
        /// <param name="matricula"></param>
        /// <returns></returns>
        public ServerResponseCaducidad GetAllFilter(string matricula)
        {
            ServerResponseCaducidad serverResponseCaducidad;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + CADUCIDAD + "allFilter/" + matricula;

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
                }
                else
                {
                    serverResponseCaducidad = new ServerResponseCaducidad();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseCaducidad.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseCaducidad = new ServerResponseCaducidad();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseCaducidad.error = error;
            }

            return serverResponseCaducidad;
        }

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseCaducidad</returns>
        public ServerResponseCaducidad GetAll()
        {
            ServerResponseCaducidad serverResponseCaducidad;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
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
                }
                else
                {
                    serverResponseCaducidad = new ServerResponseCaducidad();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseCaducidad.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseCaducidad = new ServerResponseCaducidad();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseCaducidad.error = error;
            }

            return serverResponseCaducidad;
        }

        /// <summary>
        /// Devuelve los datos con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseCaducidad</returns> 
        public ServerResponseCaducidad Find(string id)
        {
            ServerResponseCaducidad serverResponseCaducidad;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + CADUCIDAD + "find/" + id;

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
                }
                else
                {
                    serverResponseCaducidad = new ServerResponseCaducidad();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseCaducidad.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseCaducidad = new ServerResponseCaducidad();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseCaducidad.error = error;
            }
            return serverResponseCaducidad;
        }

        /// <summary>
        /// Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el objeto de la BD
        /// </summary>
        /// <param name="caducidad">objeto de BD</param>
        /// <param name="id"></param>
        /// <returns>ServerResponseCaducidad</returns>
        public ServerResponseCaducidad Save(CaducidadDTO caducidadDTO, string id)
        {
            ServerResponseCaducidad serverResponseCaducidad;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + CADUCIDAD + "save/" + id;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "POST";

                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;
                    httpRequest.ContentType = "application/json";

                    Caducidad caducidad = transformCaducidadDTOToCaducidad(caducidadDTO);

                    if (null != caducidad)
                    {
                        var data = JsonSerializer.Serialize<Caducidad>(caducidad);

                        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                        {
                            streamWriter.Write(data);
                        }

                        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();

                            serverResponseCaducidad = JsonSerializer.Deserialize<ServerResponseCaducidad>(result);
                        }

                        //Console.WriteLine(httpResponse.StatusCode);
                    }
                    else
                    {
                        serverResponseCaducidad = new ServerResponseCaducidad();

                        ErrorBean error = new ErrorBean();
                        error.code = MessageExceptions.SERVER_ERROR;
                        error.message = MessageExceptions.MSSG_SERVER_ERROR;

                        serverResponseCaducidad.error = error;
                    }
                }
                else
                {
                    serverResponseCaducidad = new ServerResponseCaducidad();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseCaducidad.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseCaducidad = new ServerResponseCaducidad();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseCaducidad.error = error;
            }
            return serverResponseCaducidad;
        }

        /// <summary>
        /// Parseo de DTO a Modelo
        /// </summary>
        /// <param name="caducidadDTO"></param>
        /// <returns></returns>
        private Caducidad transformCaducidadDTOToCaducidad(CaducidadDTO caducidadDTO)
        {
            Caducidad caducidad = new Caducidad();

            try
            {
                caducidad.idVehiculo = caducidadDTO.idVehiculo;
                caducidad.proximaITV = caducidadDTO.proximaITV;
                caducidad.ultimaITV = caducidadDTO.ultimaITV;
                caducidad.vencimientoVehiculo = caducidadDTO.vencimientoVehiculo;
                caducidad.baja = caducidadDTO.baja;

            }
            catch (System.Exception)
            {
                caducidad = null;
            }

            return caducidad;
        }
    }
}
