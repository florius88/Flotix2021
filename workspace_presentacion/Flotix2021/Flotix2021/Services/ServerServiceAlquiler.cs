using Flotix2021.Helpers;
using Flotix2021.Model;
using Flotix2021.ModelDTO;
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
            ServerResponseAlquiler serverResponseAlquiler;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + ALQUILER + "allFilter/" + cliente + "/" + matricula + "/" + periodo;

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
                }
                else
                {
                    serverResponseAlquiler = new ServerResponseAlquiler();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseAlquiler.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseAlquiler = new ServerResponseAlquiler();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseAlquiler.error = error;
            }

            return serverResponseAlquiler;
        }

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseAlquiler</returns>
        public ServerResponseAlquiler GetAll()
        {
            ServerResponseAlquiler serverResponseAlquiler;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
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
                }
                else
                {
                    serverResponseAlquiler = new ServerResponseAlquiler();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseAlquiler.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseAlquiler = new ServerResponseAlquiler();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseAlquiler.error = error;
            }

            return serverResponseAlquiler;
        }

        /// <summary>
        /// Devuelve los datos con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseAlquiler</returns>
        public ServerResponseAlquiler Find(string id)
        {
            ServerResponseAlquiler serverResponseAlquiler;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + ALQUILER + "find/" + id;

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
                }
                else
                {
                    serverResponseAlquiler = new ServerResponseAlquiler();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseAlquiler.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseAlquiler = new ServerResponseAlquiler();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseAlquiler.error = error;
            }

            return serverResponseAlquiler;
        }

        /// <summary>
        /// Devuelve los datos con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseAlquiler</returns>
        public ServerResponseAlquiler FindByVehiculo(string id)
        {
            ServerResponseAlquiler serverResponseAlquiler;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + ALQUILER + "findvehiculo/" + id;

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
                }
                else
                {
                    serverResponseAlquiler = new ServerResponseAlquiler();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseAlquiler.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseAlquiler = new ServerResponseAlquiler();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseAlquiler.error = error;
            }

            return serverResponseAlquiler;
        }

        /// <summary>
        /// Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el objeto de la BD
        /// </summary>
        /// <param name="alquiler">objeto de BD</param>
        /// <param name="id"></param>
        /// <returns>ServerResponseAlquiler</returns>
        public ServerResponseAlquiler Save(AlquilerDTO alquilerDTO, string id)
        {
            ServerResponseAlquiler serverResponseAlquiler;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + ALQUILER + "save/" + id;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "POST";

                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;
                    httpRequest.ContentType = "application/json";

                    Alquiler alquiler = transformAlquilerDTOToAlquiler(alquilerDTO);

                    if (null != alquiler)
                    {
                        var data = JsonSerializer.Serialize<Alquiler>(alquiler);

                        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                        {
                            streamWriter.Write(data);
                        }

                        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();

                            serverResponseAlquiler = JsonSerializer.Deserialize<ServerResponseAlquiler>(result);
                        }

                        //Console.WriteLine(httpResponse.StatusCode);
                    }
                    else
                    {
                        serverResponseAlquiler = new ServerResponseAlquiler();

                        ErrorBean error = new ErrorBean();
                        error.code = MessageExceptions.SERVER_ERROR;
                        error.message = MessageExceptions.MSSG_SERVER_ERROR;

                        serverResponseAlquiler.error = error;
                    }
                }
                else
                {
                    serverResponseAlquiler = new ServerResponseAlquiler();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseAlquiler.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseAlquiler = new ServerResponseAlquiler();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseAlquiler.error = error;
            }

            return serverResponseAlquiler;
        }

        /// <summary>
        /// Parseo de DTO a Modelo
        /// </summary>
        /// <param name="alquilerDTO"></param>
        /// <returns></returns>
        private Alquiler transformAlquilerDTOToAlquiler(AlquilerDTO alquilerDTO)
        {
            Alquiler alquiler = new Alquiler();
            try
            {
                alquiler.idVehiculo = alquilerDTO.idVehiculo;
                alquiler.idCliente = alquilerDTO.idCliente;
                alquiler.fechaInicio = alquilerDTO.fechaInicio;
                alquiler.fechaFin = alquilerDTO.fechaFin;
                alquiler.km = alquilerDTO.km;
                alquiler.tipoKm = alquilerDTO.tipoKm;
                alquiler.importe = alquilerDTO.importe;
                alquiler.tipoImporte = alquilerDTO.tipoImporte;
            }
            catch (System.Exception)
            {
                alquiler = null;
            }

            return alquiler;
        }
    }
}