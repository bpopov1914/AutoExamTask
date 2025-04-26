using RestSharp;
using System.Security.Policy;

namespace AutoExamTask.Rest.Calls
{
    public class RestCalls
    {
        private string baseUrl = "https://schoolprojectapi.onrender.com";

        public RestResponse LoginCall(string username, string password)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest("/auth/login", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            RestResponse response = client.Execute(request);

            return response;
        }

    }
}
