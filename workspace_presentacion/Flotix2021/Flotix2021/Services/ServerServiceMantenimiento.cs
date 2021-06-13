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
    class ServerServiceMantenimiento
    {
        private readonly string MANTENIMIENTO = "mantenimiento/";

        /// <summary>
        /// Devuelve los datos con los filtros: Tipo y Matricula
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="matricula"></param>
        /// <returns>ServerResponseMantenimiento</returns>
        public ServerResponseMantenimiento GetAllFilter(string tipo, string matricula)
        {
            ServerResponseMantenimiento serverResponseMantenimiento;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + MANTENIMIENTO + "allFilter/" + tipo + "/" + matricula;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseMantenimiento = JsonSerializer.Deserialize<ServerResponseMantenimiento>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseMantenimiento = new ServerResponseMantenimiento();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseMantenimiento.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseMantenimiento = new ServerResponseMantenimiento();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseMantenimiento.error = error;
            }

            return serverResponseMantenimiento;
        }

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseMantenimiento</returns>
        public ServerResponseMantenimiento GetAll()
        {
            ServerResponseMantenimiento serverResponseMantenimiento;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + MANTENIMIENTO + "all";

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseMantenimiento = JsonSerializer.Deserialize<ServerResponseMantenimiento>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseMantenimiento = new ServerResponseMantenimiento();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseMantenimiento.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseMantenimiento = new ServerResponseMantenimiento();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseMantenimiento.error = error;
            }

            return serverResponseMantenimiento;
        }

        /// <summary>
        /// Devuelve los datos con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseMantenimiento</returns>
        public ServerResponseMantenimiento Find(string id)
        {
            ServerResponseMantenimiento serverResponseMantenimiento;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + MANTENIMIENTO + "find/" + id;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseMantenimiento = JsonSerializer.Deserialize<ServerResponseMantenimiento>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseMantenimiento = new ServerResponseMantenimiento();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseMantenimiento.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseMantenimiento = new ServerResponseMantenimiento();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseMantenimiento.error = error;
            }

            return serverResponseMantenimiento;
        }

        /// <summary>
        /// Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el objeto de la BD
        /// </summary>
        /// <param name="mantenimientoDTO">objeto de BD</param>
        /// <param name="id"></param>
        /// <returns>ServerResponseMantenimiento</returns>
        public ServerResponseMantenimiento Save(MantenimientoDTO mantenimientoDTO, string id)
        {
            ServerResponseMantenimiento serverResponseMantenimiento;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + MANTENIMIENTO + "save/" + id;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "POST";

                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;
                    httpRequest.ContentType = "application/json";

                    Mantenimiento mantenimiento = transformMantenimientoDTOToMantenimiento(mantenimientoDTO);

                    if (null != mantenimiento)
                    {
                        var data = JsonSerializer.Serialize<Mantenimiento>(mantenimiento);

                        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                        {
                            streamWriter.Write(data);
                        }

                        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();

                            serverResponseMantenimiento = JsonSerializer.Deserialize<ServerResponseMantenimiento>(result);
                        }

                        //Console.WriteLine(httpResponse.StatusCode);
                    }
                    else
                    {
                        serverResponseMantenimiento = new ServerResponseMantenimiento();

                        ErrorBean error = new ErrorBean();
                        error.code = MessageExceptions.SERVER_ERROR;
                        error.message = MessageExceptions.MSSG_SERVER_ERROR;

                        serverResponseMantenimiento.error = error;
                    }
                }
                else
                {
                    serverResponseMantenimiento = new ServerResponseMantenimiento();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseMantenimiento.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseMantenimiento = new ServerResponseMantenimiento();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseMantenimiento.error = error;
            }

            return serverResponseMantenimiento;
        }

        /// <summary>
        /// Parseo de DTO a Modelo
        /// </summary>
        /// <param name="mantenimientoDTO"></param>
        /// <returns></returns>
        private Mantenimiento transformMantenimientoDTOToMantenimiento(MantenimientoDTO mantenimientoDTO)
        {
            Mantenimiento mantenimiento = new Mantenimiento();

            try
            {
                mantenimiento.idTipoMantenimiento = mantenimientoDTO.idTipoMantenimiento;
                mantenimiento.idVehiculo = mantenimientoDTO.idVehiculo;
                mantenimiento.kmMantenimiento = mantenimientoDTO.kmMantenimiento;
                mantenimiento.proximoMantenimiento = mantenimientoDTO.proximoMantenimiento;
                mantenimiento.ultimoMantenimiento = mantenimientoDTO.ultimoMantenimiento;
            }
            catch (System.Exception)
            {
                mantenimiento = null;
            }

            return mantenimiento;
        }
    }
}
