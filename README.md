# **AutoExamTask**

## **Technologies Used**

- **C# / .NET**: The core programming language and framework for the project.
- **RestSharp**: A simple and powerful library for making HTTP requests and interacting with APIs.
- **Reqnroll**: BDD/Gherkin.

## **Project Structure**

The project is organized into several key components for improved modularity and maintainability:

### 1. **RestCalls**
- **Purpose**: Centralized class responsible for making all API calls to the system under test.
- **Details**: Contains methods for handling the communication between the test suite and the APIs.

### 2. **ResponseDataExtractors**
- **Purpose**: Helper methods to extract specific data from JSON responses.
- **Details**: Includes various methods for parsing and extracting relevant information from API responses, aiding in verification and assertions.

### 3. **SchoolApiTests**
- **Purpose**: Contains all test scenarios and their corresponding definitions.
- **Details**: These are the test case files that outline different API interactions and verify expected outcomes based on various inputs and conditions.

### 4. **Utilities**
- **Purpose**: Contains supporting classes for setup, reporting, and logging.
- **Details**: Includes all the necessary setup and teardown procedures for the tests, as well as custom logging and reporting utilities to track and display results effectively.

## **Disclaimer**

- ⚠️ **Hardcoded Parameters**: Some test parameters are currently hardcoded (specifically in the Feature file). These should be refactored for better reusability and flexibility.
- ⚠️ **Error Handling**: There is a need to implement additional error handling to ensure all API calls are executed as intended and failures are properly captured.
- ⚠️ **Negative Test Scenarios**: Additional negative test cases should be added, such as:
  - A parent attempting to view the grades of a student who is not connected to them.
  - Other edge cases where expected behavior fails under abnormal conditions.
Test
