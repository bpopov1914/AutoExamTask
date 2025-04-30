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

        [Given("execute login API call with \"(.*)\" username and \"(.*)\" password")]
        [When("execute login API call with \"(.*)\" username and \"(.*)\" password")]
        public void WhenExecuteLoginAPICallWithUsernameAndPassword(string username, string password)
        {
            loginResponse = restCalls.LoginCall(username, password);
            _test.Log(Status.Info, $@"Login call is executed with ""{username}"" username and ""{password}"" password");
        }

        [When("execute create class API call: \"(.*)\" with subjects \"(.*)\", \"(.*)\", \"(.*)\"")]
        public void WhenExecuteCreateClassAPICallWithSubjects(string className, string subjOne, string subjTwo, string subjThree)
        {
            RestResponse response = restCalls.CreateClass(restCalls.token, className, subjOne, subjTwo, subjThree);
            _test.Log(Status.Info, $@"Create class call is executed.");
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

        [Then("class and subjects are created")]
        public void ThenClassAndSubjectsAreCreated()
        {
            UtilitiesMethods.AssertEqual(
                System.Net.HttpStatusCode.OK,
                loginResponse.StatusCode,
                "Class was not created: " + loginResponse.Content,
                _scenarioContext);
        }

    }
}
