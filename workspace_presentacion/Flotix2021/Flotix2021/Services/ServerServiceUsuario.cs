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
    /// Controlador que gestiona los usuarios
    /// </summary>
    class ServerServiceUsuario
    {
        private readonly string USUARIO = "usuario/";

        /// <summary>
        /// Devuelve los datos con los filtros: Email y Password
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

                if (null != oauthToken)
                {

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
                else
                {
                    serverResponseUsuario = new ServerResponseUsuario();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseUsuario.error = error;
                }

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

        /// <summary>
        /// Devuelve los datos con los filtros: Nombre, Email y Rol
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="email"></param>
        /// <param name="rol"></param>
        /// <returns>ServerResponseUsuario</returns>
        public ServerResponseUsuario GetAllFilter(string nombre, string email, string rol)
        {
            ServerResponseUsuario serverResponseUsuario;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + USUARIO + "allFilter/" + nombre + "/" + email + "/" + rol;

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
                else
                {
                    serverResponseUsuario = new ServerResponseUsuario();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseUsuario.error = error;
                }
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

        /// <summary>
        /// Devuelve todos los datos
        /// </summary>
        /// <returns>ServerResponseUsuario</returns>
        public ServerResponseUsuario GetAll()
        {
            ServerResponseUsuario serverResponseUsuario;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + USUARIO + "all";

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
                else
                {
                    serverResponseUsuario = new ServerResponseUsuario();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseUsuario.error = error;
                }
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

        /// <summary>
        /// Devuelve los datos con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseUsuario</returns>
        public ServerResponseUsuario Find(string id)
        {
            ServerResponseUsuario serverResponseUsuario;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + USUARIO + "find/" + id;

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
                else
                {
                    serverResponseUsuario = new ServerResponseUsuario();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseUsuario.error = error;
                }
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

        /// <summary>
        /// Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el objeto de la BD
        /// </summary>
        /// <param name="UsuarioDTO">objeto de BD</param>
        /// <param name="id"></param>
        /// <returns>ServerResponseUsuario</returns>
        public ServerResponseUsuario Save(UsuarioDTO usuarioDTO, string id)
        {
            ServerResponseUsuario serverResponseUsuario;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + USUARIO + "save/" + id;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "POST";

                    httpRequest.Headers["Authorization"] = "Bearer " + oauthToken.access_token;
                    httpRequest.ContentType = "application/json";

                    Usuario usuario = transformUsuarioDTOToUsuario(usuarioDTO);

                    if (null != usuario)
                    {
                        var data = JsonSerializer.Serialize<Usuario>(usuario);

                        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                        {
                            streamWriter.Write(data);
                        }

                        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();

                            serverResponseUsuario = JsonSerializer.Deserialize<ServerResponseUsuario>(result);
                        }

                        //Console.WriteLine(httpResponse.StatusCode);
                    }
                    else
                    {
                        serverResponseUsuario = new ServerResponseUsuario();

                        ErrorBean error = new ErrorBean();
                        error.code = MessageExceptions.SERVER_ERROR;
                        error.message = MessageExceptions.MSSG_SERVER_ERROR;

                        serverResponseUsuario.error = error;
                    }
                }
                else
                {
                    serverResponseUsuario = new ServerResponseUsuario();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseUsuario.error = error;
                }
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

        /// <summary>
        /// Cambia la contrasenia de un usuario
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="pwd"></param>
        /// <param name="newpwd"></param>
        /// <param name="confirmpwd"></param>
        /// <returns></returns>
        public ServerResponseUsuario ChangePwd(string nombre, string pwd, string newpwd, string confirmpwd)
        {
            ServerResponseUsuario serverResponseUsuario;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + USUARIO + "changepwd/" + nombre + "/" + pwd + "/" + newpwd + "/" + confirmpwd;

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "POST";

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
                else
                {
                    serverResponseUsuario = new ServerResponseUsuario();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseUsuario.error = error;
                }
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

        /// <summary>
        /// Elimina el dato con un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServerResponseUsuario</returns>
        public ServerResponseUsuario Delete(string id)
        {
            ServerResponseUsuario serverResponseUsuario;

            try
            {
                OauthToken oauthToken = ServerService.obtenerToken();

                if (null != oauthToken)
                {
                    var url = Constantes.SERVIDOR + USUARIO + "delete/" + id;

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
                else
                {
                    serverResponseUsuario = new ServerResponseUsuario();

                    ErrorBean error = new ErrorBean();
                    error.code = MessageExceptions.SERVER_ERROR;
                    error.message = MessageExceptions.MSSG_SERVER_ERROR;

                    serverResponseUsuario.error = error;
                }
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

        /// <summary>
        /// Parseo de DTO a Modelo
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns>Usuario</returns>
        private Usuario transformUsuarioDTOToUsuario(UsuarioDTO usuarioDTO)
        {
            Usuario usuario = new Usuario();

            try
            {
                usuario.email = usuarioDTO.email;
                usuario.idRol = usuarioDTO.idRol;
                usuario.nombre = usuarioDTO.nombre;
                usuario.pwd = usuarioDTO.pwd;
            }
            catch (System.Exception)
            {
                usuario = null;
            }

            return usuario;
        }
    }
}
