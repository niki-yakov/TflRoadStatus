Feature: HttpNotFoundError And ExitCode
			When non existing road is passed
			The applica[tion returns HttpStatusCode 404

@error404
Scenario: HttpError 404
Given an invalid road ID A233 is specified
When the client is run
Then the application should return an informative error

@errorEXitCode
Scenario: Non Zero Exit Code
Given an invalid road ID A233 is specified
When the client is run
Then the application should exit with a non-zero System Error code