Feature: SchoolApiTests

Feature file containing all scenarios covered for the School API

@login
Scenario: Login with Admin role
	When execute login API call with "admin7" username and "admin129" password
	Then valid JWT token is returned

Scenario: TBD checking push
