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

        //public int ExtractUserId(string jsonResponse)
        //{
        //    var jsonObject = JObject.Parse(jsonResponse);
        //    return jsonObject["user"]?["id"]?.Value<int>() ?? 0;
        //}

        public string ExtractStockMessage(string jsonResponse)
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject["message"]?.ToString();

        }
    }
}
