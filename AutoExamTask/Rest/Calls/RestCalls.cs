using RestSharp;
using AutoExamTask.Rest.DataManagement;
using System.Security.Policy;
using AutoExamTask.Utilities;

namespace AutoExamTask.Rest.Calls
{
    public class RestCalls
    {
        ResponseDataExtractors responseDataExtractors = new ResponseDataExtractors();
        private string baseUrl = "https://schoolprojectapi.onrender.com";
        public string token = "";
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
            try
            {
                token = responseDataExtractors.ExtractLoggedInToken(response.Content);
            }
            catch (Exception ex) 
            {
                Logger.Log.Error("The token could not be extracted. The response content was: " + response.Content);
            }

            return response;
        }

        public RestResponse CreateClass(string token, string className, string subjectOne, string subjectTwo, string subjectThree)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest($"/classes/create?class_name={className}&subject_1={subjectOne}&subject_2={subjectTwo}&subject_3={subjectThree}", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            RestResponse response = client.Execute(request);

            return response;
        }

    }
}
