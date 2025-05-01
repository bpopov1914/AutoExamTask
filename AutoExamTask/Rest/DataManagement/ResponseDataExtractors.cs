using Newtonsoft.Json.Linq;

namespace AutoExamTask.Rest.DataManagement
{
    public class ResponseDataExtractors
    {

        public string ExtractLoggedInToken(string jsonResponse, string jsonIdentfier = "access_token")
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject[jsonIdentfier]?.ToString();
        }

        public string ExtractStudentId(string jsonResponse)
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject["student_id"]?.ToString();

        }

        public string ExtractStockMessage(string jsonResponse)
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject["message"]?.ToString();

        }
    }
}
