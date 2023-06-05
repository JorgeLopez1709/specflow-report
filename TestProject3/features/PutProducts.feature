Feature: Put Products

A short summary of the feature

@positive @smoke @regression @integration @JIRA-1111 
Scenario: Put by id 
	Given I have an id with a valid value 17
	When I send a put request
	Then I expected a valid HTTP code response



