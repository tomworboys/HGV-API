Feature: ClientTests
	Proof on concept test for setting up the API test framework

@mytag
Scenario: Post test. Generate a token for logged in user
	Given a user with valid credentials
	When the api/Tokens/Request is submitted
	Then the api/Tokens response is returned

Scenario: Get test. Get dashboard totals
	Given a valid token
	When the api/dashboards/totals request is submitted
	Then the api/dashboards/total response is returned

Scenario: Put test. Put Mileage
	Given a valid token and mileage history record
	When the api/mileage request is submitted
	Then the api/mileage response is returned

Scenario: Delete test. Delete a user session
	Given a valid token and session
	When the api/sessions delete request is submitted
	Then the session is deleted