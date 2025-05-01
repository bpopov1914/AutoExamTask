Feature: SchoolApiTests

Feature file containing all scenarios covered for the School API

@login
Scenario: Login with Admin role
	When execute login API call with "admin7" username and "admin129" password
	Then valid JWT token is returned

@createClass
Scenario: Teacher create class
	Given execute login API call with "teacher11" username and "teacher11" password
	When execute create class API call: "Class BP" with subjects "Subject 1", "Subject 2", "Subject 3"
	Then class and subjects are created 

@addStudentToClass
Scenario: Add Student to Class
	Given execute login API call with "teacher11" username and "teacher11" password
	When execute Add Student API call with student name "BP Student" and class id "eb050d07-a296-4bf9-9939-5c6ee902d6c8" 
	When execute login API call with "admin7" username and "admin129" password
	When create parent with "BpParent" username and "BpParent" password and connect to student
	Then student is added to class and connected to parent

@addGradeForStudent
Scenario: Add Grade for Student
	Given execute login API call with "teacher11" username and "teacher11" password
	When execute Add Grade API call "e8c3daff-6bc4-4a6b-a0d8-32f61afb206d", "Subject 2", 5
	Then student is assigned grade

@parentCanSeeStudentGrades
Scenario: Parent can see student grades
	Given execute login API call with "BpParent" username and "BpParent" password
	When execute View Grades API call for student "e8c3daff-6bc4-4a6b-a0d8-32f61afb206d"
	Then grades only for student linked to the parent are returned

