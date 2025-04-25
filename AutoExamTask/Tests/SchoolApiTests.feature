Feature: SchoolApiTests

Feature file containing all scenarios covered for the School API

@login
Scenario: Login with Admin role
	Given login data is prepared
	When execute login API call
	Then valid JWT token is returned

Scenario: TBD
