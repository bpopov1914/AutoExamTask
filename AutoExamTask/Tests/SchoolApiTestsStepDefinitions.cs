using AutoExamTask.Rest.Calls;
using AutoExamTask.Rest.DataManagement;
using AutoExamTask.Utilities;
using AventStack.ExtentReports;
using NUnit.Framework;
using Reqnroll;
using RestSharp;
using System.Text.RegularExpressions;

namespace AutoExamTask.Tests
{
    [Binding]
    public class SchoolApiTestsStepDefinitions
    {
        private RestCalls restCalls = new RestCalls();
        private ResponseDataExtractors extractResponseData = new ResponseDataExtractors();
        private RestResponse loginResponse;
        private RestResponse createClassResponse;
        private RestResponse addStudentResponse;
        private RestResponse createParentResponse;
        private RestResponse connectStudentResponse;
        private RestResponse addGradeResponse;
        private RestResponse viewGradesResponse;
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
            createClassResponse = restCalls.CreateClass(className, subjOne, subjTwo, subjThree);
            _test.Log(Status.Info, $@"Create class call is executed.");
        }

        [When("execute Add Student API call with student name \"(.*)\" and class id \"(.*)\"")]
        public void WhenExecuteAddStudentAPICallWithStudentNameAndClassId(string studentName, string classId)
        {
            addStudentResponse = restCalls.AddStudentToClass(studentName, classId);
            _test.Log(Status.Info, $@"Add Student to class call is executed.");
        }

        [When("create parent with \"(.*)\" username and \"(.*)\" password and connect to student")]
        public void WhenCreateParentWithUsernameAndPassword(string parentName, string password)
        {
            createParentResponse = restCalls.CreateUser(parentName, password, "parent");
            _test.Log(Status.Info, $@"Create user with Parent role was executed.");
            string studentId = extractResponseData.ExtractStudentId(addStudentResponse.Content);
            connectStudentResponse = restCalls.ConnectStudent(parentName, studentId);
            _test.Log(Status.Info, $@"Connect Student to Parent call is executed.");
        }

        [When("execute Add Grade API call \"(.*)\", \"(.*)\", (.*)")]
        public void WhenExecuteAddGradeAPICall(string studentId, string subjectName, int grade)
        {
            addGradeResponse = restCalls.AddGrade(studentId, subjectName, grade);
            _test.Log(Status.Info, $@"Add grade call was executed.");
        }

        [When("execute View Grades API call for student \"(.*)\"")]
        public void WhenExecuteViewGradesAPICall(string studentId)
        {
            viewGradesResponse = restCalls.ViewGrades(studentId);
            _test.Log(Status.Info, $@"View grades call was executed.");
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
                createClassResponse.StatusCode,
                "Class was not created: " + createClassResponse.Content,
                _scenarioContext);

            UtilitiesMethods.AssertEqual(
                "Class created",
                extractResponseData.ExtractStockMessage(createClassResponse.Content),
                "Class was not created: " + createClassResponse.Content,
                _scenarioContext);
        }

        [Then("student is added to class and connected to parent")]
        public void ThenStudentIsAddedToClassAndConnectedToParent()
        {
            UtilitiesMethods.AssertEqual(
                System.Net.HttpStatusCode.OK,
                addStudentResponse.StatusCode,
                "Student was not added: " + addStudentResponse.Content,
                _scenarioContext);

            UtilitiesMethods.AssertEqual(
                System.Net.HttpStatusCode.OK,
                createParentResponse.StatusCode,
                "Parent was not created: " + createParentResponse.Content,
                _scenarioContext);

            UtilitiesMethods.AssertEqual(
                System.Net.HttpStatusCode.OK,
                connectStudentResponse.StatusCode,
                "Student was not connected to parent: " + connectStudentResponse.Content,
                _scenarioContext);

            UtilitiesMethods.AssertEqual(
                "Student added",
                extractResponseData.ExtractStockMessage(addStudentResponse.Content),
                "Actual: " + addStudentResponse.Content,
            _scenarioContext);

            string expectedMessage = $"parent 'BpParent_\\d{{8}}_\\d{{6}}' created successfully"; 
            Assert.That(Regex.IsMatch(extractResponseData.ExtractStockMessage(createParentResponse.Content), expectedMessage), "The message does not contain the expected 'BpParent_' followed by a timestamp.");
            
            UtilitiesMethods.AssertEqual(
                "Parent linked to student",
                extractResponseData.ExtractStockMessage(connectStudentResponse.Content),
                "Actual: " + connectStudentResponse.Content,
                _scenarioContext);
        }

        [Then("student is assigned grade")]
        public void ThenStudentIsAssignedGrade()
        {
            UtilitiesMethods.AssertEqual(
                System.Net.HttpStatusCode.OK,
                addGradeResponse.StatusCode,
                "Grade was not assigned: " + addGradeResponse.Content,
                _scenarioContext);

            UtilitiesMethods.AssertEqual(
                "Grade updated",
                extractResponseData.ExtractStockMessage(addGradeResponse.Content),
                "Actual: " + addGradeResponse.Content,
                _scenarioContext);
        }

        [Then("grades only for student linked to the parent are returned")]
        public void ThenGradesOnlyForStudentLinkedToTheParentAreReturned()
        {
            UtilitiesMethods.AssertEqual(
                System.Net.HttpStatusCode.OK,
                viewGradesResponse.StatusCode,
                "Grades were not returned: " + viewGradesResponse.Content,
                _scenarioContext);
        }

    }
}
