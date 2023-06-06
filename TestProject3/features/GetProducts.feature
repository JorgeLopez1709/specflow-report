Feature: Get products

As a user
I want to be able to retrieve product information
So that I can perform operations related to them


@positive @smoke @regression @integration @JIRA-1001
Scenario: Get products 
	Given I have the valid products endpoint
	When I send a get request
	Then I expect a valid Get Http code response
	And I get a list of products in the Body Response

@positive @smoke @regression @integration @JIRA-1002
Scenario: Get products by id 
	Given I have an id with a valid value
	When I send a get request
	Then I expect a valid Get Http code response
	And I get the product according to the ID requested product in the Body Response




