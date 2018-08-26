# Project Title

TfL Coding Challenge

## Getting Started

The document is aimed to describe how to use and run Unit tests, IntegrationTests, Begaviour Tests and Command line execution

### Prerequisites

Install Visual Studio Community Edition 2017 and open the solution. Because all the packages are deleted, first build the solution to automatically download missing packages and build the solution.

Here is the link to download Visual Studio Community Edition from Microsott - https://imagine.microsoft.com/en-us/Catalog/Product/530

Installing PowerShell please read and install from the link - https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell?view=powershell-6

### Installing

1. Install Visual Studio all default settings
2. Clone the solution from Git
3. Open the solution file RoadStatus.sln
4. Build solution
5. Install Powershell
6. Create deploy folder in 'C:\'
	a) Open PowerShell
	b) execute 'pwd' and check if you are in c: drive
	c) if c: drive is the current drive go to e)
	d) execute 'c:'
	e) execute 'cd \' and make 'C:' root current folder
	f) execute 'md deploy'

Updating app_id and app_key

There are app.congig files int the below list of projects and both keys should be updatd.
```
Projects to update:
RoadStatus.Behaviour.Tests
RoadStatus.e2e.Tests
RoadStatus.Integration.Tests
RoadStatus
```

Please update app_id and app_key appropriately

## Running the tests

- Open Test Explorer in Visual Studio Community Edition 2017 

```
After the Test Explorer is opened three test projects available are:
- RoadStatus.Behaviour.Tests - Behavioural SpecFlow Tests (BDD)
- RoadStatus.e2e.Tests - Contains all End-To-End testing
- RoadStatus.Integration.Tests - Integration Tests to test integration with TFL url and app.config file
- RoadStatus.Unit.Tests - Moq and Unit Testing the implementation by decoupling WebRequests and app.config
```

Clicking 'Run All' will run all tests together and also clicking on each project separately and right click 'Context Menu' will appear -> 'Run Selected Tests'.

That command will run all the tests implemented in the project TDD/BDD

## Running from PowerShell

1. Open PowerShell
2. The 'deploy' folder is already created and from PowerShell prompt execute
	a) execute 'c:'
	b) execute 'cd \'
	c) execute 'cd deploy'
3.	Run 'RoadStatus A2' 

```
The expected result is:
PS C:\deploy> .\RoadStatus.exe A2
The status of the A2 is as follows
        Road Status is Good
        Road Status Description is No Exceptional Delays
```
##Assumptions

For the better project quality and decoupling from the external URI where it may not be available for any particular reason I created :

```
RoadStatus.Behaviour.Tests 
RoadStatus.e2e.Tests
```

RoadStatus.Behaviour.Tests is aimed to BDD the application but mocking external URI and the configuration
RoadStatus.e2e.Tests is e2e solution using production Tfl URI and tokens

Additionally to remove dependencies between the projects I added separate app.config instead of a link to RoadStatus project app.config

## Authors

* **Nikolay Yakov** - *Tfl Codding Challange* - [RoadStatusValidator](https://github.com/PurpleBooth)