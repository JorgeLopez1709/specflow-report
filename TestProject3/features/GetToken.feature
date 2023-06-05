Feature: GetToken

A short summary of the feature

@tag1
Scenario: Get Auth Token
	Given I have a valid username and password
	When  I send a POST request
	Then  I expect a valid token response
