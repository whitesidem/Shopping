Feature: ShoppingList
	In order to to calculate the total shopping price for scanned items
	As a checkout till's printout
	I want to be told the sum of products after applying dicount rules
	And I wnt an itemised list including the discounts

Rules:
 Item   Unit      Special
         Price     Price
  --------------------------
    A     50       3 for 130
    B     30       2 for 45   - i.e. buy 1 get one half price
    C     20       2 for 20   - i.e  buy 1 get on free
    D     15


Background:
    Given the following pricing rules
    | Sku | Price	 | Rule    |
    | A     | 50     | 3 > 130 |
    | B     | 30     | 2 > 45  |
    | C     | 20     | 2 > 20  |
    | D     | 15     |         |


Scenario: Total of 1 simple Item That does not qualify for any discount
	Given I have 1 Product 'D' 
	When I calculate the total
	Then the total price should be 15.00

Scenario: Total of 2 same simple Items That do not qualify for any discount
	Given I have 2 Product 'D' 
	When I calculate the total
	Then the total price should be 30.00

Scenario: Order single item which qualify for buy 1 get 1 free
	Given I have the following items:
	    | Sku	|  
		| C	| 
	When I calculate the total
	Then the total price should be 20.00

Scenario: Order two items which qualify for buy 1 get 1 free
	Given I have the following items:
	    | Sku	|  
		| C	|
		| C	| 
	When I calculate the total
	Then the total price should be 20.00
	And will contain 3 receipt items
	And the following will be output:
	| Sku	| Desc    | Price	|
	| C		| C       | 20		|
	| C		| C       | 20		|
	| C		| 2 > 20  | -20		|

Scenario: Order four items which qualify for buy 1 get 1 free
	Given I have the following items:
	    | Sku	|  
		| C	|
		| C	| 
		| C	|
		| C	| 
	When I calculate the total
	Then the total price should be 40.00
	And will contain 6 receipt items
	And the following will be output:
	| Sku	| Desc    | Price	|
	| C		| C       | 20		|
	| C		| C       | 20		|
	| C		| 2 > 20  | -20		|
	| C		| C       | 20		|
	| C		| C       | 20		|
	| C		| 2 > 20  | -20		|

Scenario: Order five items which qualify for buy 1 get 1 free
	Given I have the following items:
	    | Sku	|  
		| C	|
		| C	| 
		| C	|
		| C	| 
		| C	| 
	When I calculate the total
	Then the total price should be 60.00


Scenario: Order single item which qualify for buy 1 get 1 half price
	Given I have the following items:
	    | Sku	|  
		| B	|
	When I calculate the total
	Then the total price should be 30.00


Scenario: Order two items which qualify for buy 1 get 1 half price
	Given I have the following items:
	    | Sku	|  
		| B	|
		| B	| 
	When I calculate the total
	Then the total price should be 45.00
	And will contain 3 receipt items
	And the following will be output:
	| Sku	| Desc    | Price	|
	| B		| B       | 30		|
	| B		| B       | 30		|
	| B		| 2 > 45  | -15		|

Scenario: Order four items which qualify for buy 1 get 1 half price
	Given I have the following items:
	    | Sku	|  
		| B	|
		| B	| 
		| B	|
		| B	| 
	When I calculate the total
	Then the total price should be 90.00

Scenario: Order five items which qualify for buy 1 get 1 half price
	Given I have the following items:
	    | Sku	|  
		| B	|
		| B	| 
		| B	|
		| B	| 
		| B	| 
	When I calculate the total
	Then the total price should be 120.00

Scenario: Order two items which qualify for buy 3 for £130
	Given I have the following items:
	    | Sku	|  
		| A	|
		| A	| 
	When I calculate the total
	Then the total price should be 100.00

Scenario: Order three items which qualify for buy 3 for £130
	Given I have the following items:
	    | Sku	|  
		| A	|
		| A	| 
		| A	| 
	When I calculate the total
	Then the total price should be 130.00

Scenario: Order 6 items which qualify for buy 3 for £130
	Given I have the following items:
	    | Sku	|  
		| A	|
		| A	| 
		| A	| 
		| A	|
		| A	| 
		| A	| 
	When I calculate the total
	Then the total price should be 260.00
	And will contain 8 receipt items
	And the following will be output:
	| Sku	| Desc    | Price	|
	| A		| A       | 50	|
	| A		| A       | 50	|
	| A		| A       | 50	|
	| A		| 3 > 130 | -20	|
	| A		| A       | 50	|
	| A		| A       | 50	|
	| A		| A       | 50	|
	| A		| 3 > 130 | -20	|


Scenario: Order multiple items that do not qualify for any special discounts results in receipt with all items and prices
	Given I have the following items:
	    | Sku	|  
		| A	| 
		| B	| 
		| C	| 
	When I request receipt items
	Then will contain 3 receipt items
	And the following will be output:
	| Sku	| Desc | Price	|
	| A		| A    | 50		|
	| B		| B    | 30		|
    | C		| C    | 20		| 

Scenario: Order multiple items that qualify for multiple special discounts results in receipt with all items and prices and discounts
	Given I have the following items:
	    | Sku	|  
		| A	| 
		| B	| 
		| C	| 
		| A	| 
		| B	| 
		| C	| 
		| A	| 
		| B	| 
		| C	| 
	When I request receipt items
	Then will contain 12 receipt items
	And the following will be output:
	| Sku	| Desc | Price	|
	| A		| A    | 50		|
	| B		| B    | 30		|
    | C		| C    | 20		| 
	| A		| A    | 50		|
	| B		| B    | 30		|
	| B		| 2 > 45  | -15		|
    | C		| C    | 20		| 
	| C		| 2 > 20  | -20		|
	| A		| A    | 50		|
	| A		| 3 > 130 | -20	|
	| B		| B    | 30		|
    | C		| C    | 20		| 

