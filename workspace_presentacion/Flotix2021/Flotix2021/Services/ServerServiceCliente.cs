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
    class ServerServiceCliente
    {
        private readonly string CLIENTE = "cliente/";

        /// <summary>
        /// Devuelve los datos con los filtros: Nif y Empresa
        /// </summary>
        /// <param name="nif"></param>
        /// <param name="empresa"></param>
        /// <returns>ServerResponseCliente</returns>
        public ServerResponseCliente GetAllFilter(string nif, string empresa)
        {
            ServerResponseCliente serverResponseCliente;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + CLIENTE + "allFilter/" + nif + "/" + empresa;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseCliente = JsonSerializer.Deserialize<ServerResponseCliente>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseCliente = new ServerResponseCliente();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseCliente.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseCliente = new ServerResponseCliente();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseCliente.error = error;
            }

            return serverResponseCliente;
        }

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseCliente</returns>
        public ServerResponseCliente GetAll()
        {
            ServerResponseCliente serverResponseCliente;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + CLIENTE + "all";

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseCliente = JsonSerializer.Deserialize<ServerResponseCliente>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseCliente = new ServerResponseCliente();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseCliente.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseCliente = new ServerResponseCliente();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseCliente.error = error;
            }

            return serverResponseCliente;
        }

        /// <summary>
        /// Devuelve los datos con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseCliente</returns>
        public ServerResponseCliente Find(string id)
        {
            ServerResponseCliente serverResponseCliente;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + CLIENTE + "find/" + id;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseCliente = JsonSerializer.Deserialize<ServerResponseCliente>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseCliente = new ServerResponseCliente();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseCliente.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseCliente = new ServerResponseCliente();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseCliente.error = error;
            }

            return serverResponseCliente;
        }

        /// <summary>
        /// Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el objeto de la BD
        /// </summary>
        /// <param name="clienteDTO">objeto de BD</param>
        /// <param name="id"></param>
        /// <returns>ServerResponseCliente</returns>
        public ServerResponseCliente Save(ClienteDTO clienteDTO, string id)
        {
            ServerResponseCliente serverResponseCliente;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + CLIENTE + "save/" + id;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "POST";

                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;
                    httpRequest.ContentType = "application/json";

                    Cliente cliente = transformClienteDTOToCliente(clienteDTO);

                    if (null != cliente)
                    {
                        var data = JsonSerializer.Serialize<Cliente>(cliente);

                        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                        {
                            streamWriter.Write(data);
                        }

                        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();

                            serverResponseCliente = JsonSerializer.Deserialize<ServerResponseCliente>(result);
                        }

                        //Console.WriteLine(httpResponse.StatusCode);
                    }
                    else
                    {
                        serverResponseCliente = new ServerResponseCliente();

                        ErrorBean error = new ErrorBean();
                        error.code = MessageExceptions.SERVER_ERROR;
                        error.message = MessageExceptions.MSSG_SERVER_ERROR;

                        serverResponseCliente.error = error;
                    }
                }
                else
                {
                    serverResponseCliente = new ServerResponseCliente();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseCliente.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseCliente = new ServerResponseCliente();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseCliente.error = error;
            }

            return serverResponseCliente;
        }

        /// <summary>
        /// Elimina el dato con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseCliente</returns>
        public ServerResponseCliente Delete(string id)
        {
            ServerResponseCliente serverResponseCliente;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + CLIENTE + "delete/" + id;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";

                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        serverResponseCliente = JsonSerializer.Deserialize<ServerResponseCliente>(result);
                    }

                    //Console.WriteLine(httpResponse.StatusCode);
                }
                else
                {
                    serverResponseCliente = new ServerResponseCliente();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseCliente.error = error;
                }
            }
            catch (System.Exception)
            {
                serverResponseCliente = new ServerResponseCliente();

                ErrorBean error = new ErrorBean();
                error.code = MessageExceptions.SERVER_ERROR;
                error.message = MessageExceptions.MSSG_SERVER_ERROR;

                serverResponseCliente.error = error;
            }

            return serverResponseCliente;
        }

        /// <summary>
        /// Parseo de DTO a Modelo
        /// </summary>
        /// <param name="clienteDTO"></param>
        /// <returns></returns>
        private Cliente transformClienteDTOToCliente(ClienteDTO clienteDTO)
        {
            Cliente cliente = new Cliente();

            try
            {
                cliente.cuentaBancaria = clienteDTO.cuentaBancaria;
                cliente.direccion = clienteDTO.direccion;
                cliente.email = clienteDTO.email;
                cliente.idMetodoPago = clienteDTO.idMetodoPago;
                cliente.nif = clienteDTO.nif;
                cliente.nombre = clienteDTO.nombre;
                cliente.personaContacto = clienteDTO.personaContacto;
                cliente.poblacion = clienteDTO.poblacion;
                cliente.tlfContacto = clienteDTO.tlfContacto;
            }
            catch (System.Exception)
            {
                cliente = null;
            }
            return cliente;
        }
    }
}
