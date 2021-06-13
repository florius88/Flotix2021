using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Flotix2021.Helpers
{
    public static class ServerService
    {

        public static OauthToken obtenerToken()
        {
            OauthToken oauthToken = new OauthToken();

            try
            {
                var url = "http://florius8.ddns.net:6969/flotix/oauth/token";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = "Basic dXNlckZsb3RpeDpwYXNzd29yZEZsb3RpeA==";
                httpRequest.ContentType = "application/x-www-form-urlencoded";

                var data = "grant_type=password&username=administrador&password=a_secret";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    oauthToken = JsonSerializer.Deserialize<OauthToken>(result);

                }

                Console.WriteLine(httpResponse.StatusCode);

            }
            catch (System.Exception)
            {
                oauthToken = null;
            }

            return oauthToken;
        }

    }
}
