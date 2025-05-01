using RestSharp;
using AutoExamTask.Rest.DataManagement;
using AutoExamTask.Utilities;
using System.Diagnostics;
using System.Reactive.Subjects;

namespace AutoExamTask.Rest.Calls
{
    public class RestCalls
    {
        ResponseDataExtractors responseDataExtractors = new ResponseDataExtractors();
        private string baseUrl = "https://schoolprojectapi.onrender.com";
        private string token = "";
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
            RestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Something went wrong. The response content was: " + response.Content);
            }

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

        public RestResponse CreateClass(string className, string subjectOne, string subjectTwo, string subjectThree)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest($"/classes/create?class_name={className}&subject_1={subjectOne}&subject_2={subjectTwo}&subject_3={subjectThree}", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            RestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Something went wrong. The response content was: " + response.Content);
            }

            return response;
        }

        public RestResponse AddStudentToClass(string studentName, string classId)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest($"/classes/add_student?name={studentName}&class_id={classId}", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            RestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Something went wrong. The response content was: " + response.Content);
            }

            return response;
        }

        public RestResponse CreateUser(string username, string password, string role)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);

            username = $"{username}_{DateTime.Now:yyyyMMdd_HHmmss}";

            RestRequest request = new RestRequest($"/users/create?username={username}&password={password}&role={role}", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            RestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Something went wrong. The response content was: " + response.Content);
            }

            return response;
        }

        public RestResponse ConnectStudent(string parentName, string studentId)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest($"/users/connect_parent?parent_username={parentName}&student_id={studentId}", Method.Put);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            RestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Something went wrong. The response content was: " + response.Content);
            }

            return response;
        }

        public RestResponse AddGrade(string studentId, string subjectName, int grade)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest($"/grades/add?student_id={studentId}&subject={subjectName}&grade={grade}", Method.Put);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            RestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Something went wrong. The response content was: " + response.Content);
            }

            return response;
        }

        public RestResponse ViewGrades(string studentId)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest($"/grades/student/{studentId}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept", "application/json");
            RestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Something went wrong. The response content was: " + response.Content);
            }

            return response;
        }

    }
}
