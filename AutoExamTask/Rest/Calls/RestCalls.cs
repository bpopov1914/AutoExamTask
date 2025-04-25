using RestSharp;

namespace AutoExamTask.Rest.Calls
{
    public class RestCalls
    {
        public RestResponse LoginCall(string url, string username, string password, bool rememberMe = false)
        {
            RestClientOptions options = new RestClientOptions(url)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };

            RestClient client = new RestClient(options);

            RestRequest request = new RestRequest("/users/login", Method.Post);

            request.AddHeader("Content-Type", "application/json");

            string body = @"{""usernameOrEmail"":""" + username + @""",""password"":""" + password + @""",""rememberMe"":""" + rememberMe.ToString().ToLower() + @"""}";

            request.AddStringBody(body, DataFormat.Json);

            RestResponse response = client.Execute(request);

            return response;
        }

    }
}
