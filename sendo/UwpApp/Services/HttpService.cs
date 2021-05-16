using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Sendo.UwpApp.Services
{
    class HttpService
    {
        private readonly string _userAgent = "SendoUWP/1.0";
        private readonly string _serverUrl;

        private string SessionToken
        {
            get
            {
                var token = ApplicationData.Current.LocalSettings.Values["SessionToken"];
                if (token != null)
                {
                    return token.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public HttpService()
        {
            _serverUrl = ApplicationData.Current.LocalSettings.Values["ServerUrl"].ToString();
        }

        public async Task<IRestResponse> GetAsync(string route, Dictionary<string, string> queryParameters)
        {
            var client = new RestClient(_serverUrl);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Authenticator = new SessionAuthenticator(SessionToken ?? "");
            return await client.ExecuteAsync(new RestRequest($"http://{_serverUrl}/{route}{ConstructQueryString(queryParameters)}", Method.GET));
        }

        public async Task<IRestResponse> PostAsync<T>(string route, T content, Dictionary<string, string> queryParameters = null)
        {
            var client = new RestClient(_serverUrl);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Authenticator = new SessionAuthenticator(SessionToken ?? "");
            var request = new RestRequest($"http://{_serverUrl}/{route}{ConstructQueryString(queryParameters)}", Method.POST);
            request.AddJsonBody(content);
            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> PutAsync(string route, string content, Dictionary<string, string> queryParameters)
        {
            var client = new RestClient(_serverUrl);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Authenticator = new SessionAuthenticator(SessionToken ?? "");
            var request = new RestRequest($"http://{_serverUrl}/{route}{ConstructQueryString(queryParameters)}", Method.PUT);
            request.AddJsonBody(content);
            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> DeleteAsync(string route, Dictionary<string, string> queryParameters)
        {
            var client = new RestClient(_serverUrl);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Authenticator = new SessionAuthenticator(SessionToken ?? "");
            return await client.ExecuteAsync(new RestRequest($"http://{_serverUrl}/{route}{ConstructQueryString(queryParameters)}", Method.DELETE));
        }

        private string ConstructQueryString(Dictionary<string, string> queryParameters)
        {
            if (queryParameters == null || queryParameters.Count == 0)
            {
                return "";
            }

            var sb = new StringBuilder('?');

            foreach (var kvp in queryParameters)
            {
                sb.Append($"{kvp.Key}={kvp.Value}&");
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
    }
}
