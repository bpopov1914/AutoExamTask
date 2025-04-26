using AutoExamTask.Rest.Calls;
using AutoExamTask.Rest.DataManagement;
using AutoExamTask.Utilities;
using AventStack.ExtentReports;
using Reqnroll;
using RestSharp;

namespace AutoExamTask.Tests
{
    [Binding]
    public class SchoolApiTestsStepDefinitions
    {
        private RestCalls restCalls = new RestCalls();
        private ResponseDataExtractors extractResponseData = new ResponseDataExtractors();
        private RestResponse loginResponse;
        private readonly ScenarioContext _scenarioContext;
        private ExtentTest _test;

        public SchoolApiTestsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _test = scenarioContext.Get<ExtentTest>("ExtentTest");
        }

        [When("execute login API call with \"(.*)\" username and \"(.*)\" password")]
        public void WhenExecuteLoginAPICallWithUsernameAndPassword(string username, string password)
        {
            loginResponse = restCalls.LoginCall(username, password);
            _test.Log(Status.Info, $@"Login call is executed with ""{username}"" username and ""{password}"" password");
        }

        [Then("valid JWT token is returned")]
        public void ThenValidJWTTokenIsReturned()
        {
            UtilitiesMethods.AssertEqual(
                System.Net.HttpStatusCode.OK,
                loginResponse.StatusCode,
                "User is not logged in: " + loginResponse.Content,
                _scenarioContext);
            
        }
    }
}
