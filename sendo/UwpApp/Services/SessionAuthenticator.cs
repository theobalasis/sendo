using RestSharp;
using RestSharp.Authenticators;

namespace Sendo.UwpApp.Services
{
    class SessionAuthenticator : IAuthenticator
    {
        private readonly string _sessionToken = "";

        public SessionAuthenticator(string sessionToken)
        {
            _sessionToken = sessionToken;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("Authorization", $"Session {_sessionToken}");
        }
    }
}
