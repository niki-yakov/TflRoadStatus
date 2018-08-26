Feature: CorridorStatusDisplayName
		Retrieve DisplayName, Severity and 
		SeverityDescription when correct road is passed

@statusNameDisplay
Scenario: Display Name should be displayed
Given a valid road ID A2 is specified
When the client is run
Then the road ‘displayName’ should be displayed

@statusSeverityDisplay
Scenario: Display Severity
Given a valid road ID A2 is specified
When the client is run
Then the road ‘statusSeverity’ should be displayed as ‘Road Status’

@statusSeverityDescriptionDisplay
Scenario: Display Severity Description
Given a valid road ID A2 is specified
When the client is run
Then the road ‘statusSeverityDescription’ should be displayed as ‘Road Status Description’