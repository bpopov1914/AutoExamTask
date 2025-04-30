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
	When execute Add Student API call 
	When execute login API call with "admin7" username and "admin129" password
	When execute Connect Parent API call
	Then subjects are inherited

@addGradeForStudent
Scenario: Add Grade for Student
	Given execute login API call with "teacher11" username and "teacher11" password
	When execute Add Grade API call "student", "subject", "grade"
	Then student is graded

@parentCanSeeStudentGrades
Scenario: Parent can see student grades
	Given execute login API call with "parent" username and "parent" password
	When execute View Grades API call
	Then grades only for student linked to the parent are returned

