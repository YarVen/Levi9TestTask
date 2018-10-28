Feature: Exchange Rates
	In order to avoid troubles with Exchange Rates service
	I want to run these tests

	Background: 
		Given I have opened Main Page

Scenario: Current Exchange Rates checking
	When I received current Exchange Rates
	Then I can see purchase is less than selling for each received Exchange Rate

Scenario: Currency converter selling checking
	Given I have wanted to sale 1000 USD
	When I received an amount converted to UAH
	Then I can see UAH converted seiling amount is correct

Scenario Outline: Currency converter purchasing checking
	Given I have switched to purchasing converter tab
		And I have wanted to purchase <Sum> USD
	When I received an amount converted to UAH
	Then I can see UAH converted purchasing amount is correct for <Sum> USD
	Examples: 
	| Sum  |
	| 1000 |
	| 1500 |

Scenario: Currency converter invalid value checking
	When I tried to set invalid value to the Amount field
	Then I can see the Amount field is empty