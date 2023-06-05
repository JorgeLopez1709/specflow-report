Feature: Post Categories

As a user
I want to create product categories
So that I can categorize products effectively

@positive @smoke @regression @integration @JIRA-1005 
Scenario: Create a new category 
	Given I have a valid token authorization
	And  I have a valid Category Json body
	When I send a post request
	Then I expect a valid Post Http code response



