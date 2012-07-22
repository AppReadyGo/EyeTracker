Feature: create N apps and add mock data

Background: 
  Given I have cleared the relevant tables

@mytag
Scenario: Create N apps and add mock data
	Given I have created a DB connection
	Then I have added 10 apps
	And I have created 50 touches for each app
