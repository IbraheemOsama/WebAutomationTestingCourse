Feature: Submit Tax
	As a PO
	I want to verify submit tax page is working fine
	So users can submit the taxes and gov. can claim it

Scenario: Submit Valid Tax Form
	Given login with user
	| email         | password |
	| test@test.com | P@ssw0rd |
	And I provide the following tax details
	| year | totalIncome | charityPaidAmount | numberOfChildren |
	| 2006 | 10000   | 10              | 1              |
	When submit the data
	Then Tax due amount should be 
	| taxDueAmount |
    | 599.4 |
