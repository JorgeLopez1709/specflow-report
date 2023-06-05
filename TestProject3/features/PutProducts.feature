Feature: Put Products

As a user
I want to be able to update information of existing products
So that I can maintain data accuracy

@positive @smoke @regression @integration @JIRA-1006
Scenario: Put by id 
	Given I have a valid Id
	And  I have a valid Json body
	When I send a put request
	Then I expect a valid Put Http code response



