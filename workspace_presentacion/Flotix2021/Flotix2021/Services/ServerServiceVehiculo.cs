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

                if (null != oauthToken)
                {
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
                else
                {
                    serverResponseVehiculo = new ServerResponseVehiculo();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseVehiculo.error = error;
                }
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

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseVehiculo</returns>
        public ServerResponseVehiculo GetAll()
        {
            ServerResponseVehiculo serverResponseVehiculo;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {

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
                }
                else
                {
                    serverResponseVehiculo = new ServerResponseVehiculo();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseVehiculo.error = error;
                }
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

        /// <summary>
        /// Devuelve los datos con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseVehiculo</returns>
        public ServerResponseVehiculo Find(string id)
        {
            ServerResponseVehiculo serverResponseVehiculo;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {

                    var url = Constantes.SERVIDOR + VEHICULO + "find/" + id;

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
                else
                {
                    serverResponseVehiculo = new ServerResponseVehiculo();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseVehiculo.error = error;
                }
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

        /// <summary>
        /// Obtiene todos los Vehiculos que NO tienen mantenimientos
        /// </summary>
        /// <returns>ServerResponseVehiculo</returns>
        public ServerResponseVehiculo GetAllNoMantenimiento()
        {
            ServerResponseVehiculo serverResponseVehiculo;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {

                    var url = Constantes.SERVIDOR + VEHICULO + "findnomantenimiento";

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
                else
                {
                    serverResponseVehiculo = new ServerResponseVehiculo();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseVehiculo.error = error;
                }
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

        /// <summary>
        /// Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el objeto de la BD
        /// </summary>
        /// <param name="vehiculoDTO"></param>
        /// <param name="id"></param>
        /// <returns>ServerResponseVehiculo</returns>
        public ServerResponseVehiculo Save(VehiculoDTO vehiculoDTO, string id)
        {
            ServerResponseVehiculo serverResponseVehiculo = null;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + VEHICULO + "save/" + id;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "POST";

                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;
                    httpRequest.ContentType = "application/json";

                    Vehiculo vehiculo = transformVehiculoDTOToVehiculo(vehiculoDTO);

                    if (null != vehiculo)
                    {
                        var data = JsonSerializer.Serialize<Vehiculo>(vehiculo);

                        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                        {
                            streamWriter.Write(data);
                        }

                        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();

                            serverResponseVehiculo = JsonSerializer.Deserialize<ServerResponseVehiculo>(result);
                        }

                        //Console.WriteLine(httpResponse.StatusCode);
                    }
                    else
                    {
                        serverResponseVehiculo = new ServerResponseVehiculo();

                        ErrorBean error = new ErrorBean();
                        error.code = MessageExceptions.SERVER_ERROR;
                        error.message = MessageExceptions.MSSG_SERVER_ERROR;

                        serverResponseVehiculo.error = error;
                    }
                }
                else
                {
                    serverResponseVehiculo = new ServerResponseVehiculo();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseVehiculo.error = error;
                }
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

        /// <summary>
        /// Elimina el dato con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseVehiculo</returns>
        public ServerResponseVehiculo Delete(string id)
        {
            ServerResponseVehiculo serverResponseVehiculo;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + VEHICULO + "delete/" + id;

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
                else
                {
                    serverResponseVehiculo = new ServerResponseVehiculo();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseVehiculo.error = error;
                }
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

        /// <summary>
        /// Parseo de DTO a Modelo
        /// </summary>
        /// <param name="vehiculoDTO"></param>
        /// <returns>Vehiculo</returns>
        private Vehiculo transformVehiculoDTOToVehiculo(VehiculoDTO vehiculoDTO)
        {
            Vehiculo vehiculo = new Vehiculo();

            vehiculo.capacidad = vehiculoDTO.capacidad;
            vehiculo.fechaMatriculacion = vehiculoDTO.fechaMatriculacion;
            vehiculo.km = vehiculoDTO.km;
            vehiculo.matricula = vehiculoDTO.matricula;
            vehiculo.modelo = vehiculoDTO.modelo;
            vehiculo.plazas = vehiculoDTO.plazas;

            return vehiculo;
        }
    }
}