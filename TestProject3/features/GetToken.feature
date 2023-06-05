Feature: GetToken

As a user
I want to obtain an authentication token
So that I can access protected functionalities

@positive @smoke @regression @integration @JIRA-1003
Scenario: Get Auth Token
	Given I have a valid username and password
	When  I send a POST request
	Then  I expect a valid token response

@negative @smoke @regression @integration @JIRA-1004
Scenario: Get Forbidden Http response
	Given I have an invalid username and password
	When  I send a POST request
	Then  I expect an forbiden error response
