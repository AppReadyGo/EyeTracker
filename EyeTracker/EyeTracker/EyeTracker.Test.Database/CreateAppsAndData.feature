Feature: create N apps and add mock data

Background: 
  Given I have cleared the relevant tables

@mytag
Scenario: Create N apps and add mock data
	Given I have created a DB connection
	Then I have added 10 apps
	And I have added a screen for each app with height=1280 and width=600
	And I have added a page view for each app with client height=1280 and client width=600
	And I have created 50 touches for each page view
#	And I have created 20 scrolls for each page view
	And I have created 10 viewparts for each page view
