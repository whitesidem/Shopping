Feature: ShoppingList
	In order to to calculate the total shopping price
	As a checkout till
	I want to be told the sum of products


@mytag
Scenario: Total of 1 simple Item
	Given I have 1 Product 'A' with price 10.00
	When I calculate the total
	Then the total price should be 10.00

@mytag
Scenario: Total of 2 simple Items
	Given I have 2 Product 'A' with price 10.00
	When I calculate the total
	Then the total price should be 20.00

Scenario: Total of Multiple Simple Items
	Given I have the following items:
	    | Sku   | Price | 
		| "Apple" | 10.0   |
		| "Banana" | 30.0  |
	When I calculate the total
	Then the total price should be 40.00


