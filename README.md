# Completed Synetec Basic .Net API assessement- TM

This is Synetec's basic API developer assessment. This repo has been forked from the main assessment repo.

## Completed as part of this exercise

1. Added a new Angular 11 client application project to the solution to consume the .NET Core Controller methods in the backend and display employee information and their bonus allocation. There are 2 main components- EmployeesComponent and the EmployeeBonusComponent. The former displays a list of employees and the latter allows you to input the total bonus pot and calculate the individual employee's bonus allocation. This front end app contains its own sevice layer and front end models and uses dependency injection.
![EmployeesDashboard](/Screenshots/EmployeesDashboard.png)
![ValidBonusAllocation](/Screenshots/ValidBonusAllocation.png)
---
2. Refactored the .NET Core controllers and services to follow SOLID principles (dependency injection etc). Added additional methods to the controller to retrieve individual employees and also relevant interfaces. Modified Startup.cs to allow CORS so the angular client could consume the API project. Relevant messages are shown when an employee id doesnt exist both in the front end and while using the Swagger api directly.
![ValidationBonusPot](/Screenshots/ValidationBonusPot.png)
---
3. Added a new unit tests project to the solution and added test fixture with various test methods for the back end service.

## How to run the application

1. Use `git clone https://github.com/tapashya/synetec-assessment-api.git` to download the forked repo to your local environment
2. Open SynetecAssessmentApi.sln in Visual Studio
2. Ensure Node.js command prompt and angular cli are installed.
3. Use the IISExpress option in Visual Studio or *F5* to start the SynetecAssessmentApi project (which is set as the startup project so the api is available and listening)
3. Open *Node.js* command prompt and *cd* to the `..EmployeeBonusClientApp\src\app` within the solution
4. Use `ng serve` to see the application in action. Open browser on http://localhost:4200
5. Use the *Calculate Bonus* button or click directly on the employee name which takes you to the bonus calculator page. You can also directly navigate to a specific employee using http://localhost:4200
![EmployeesNotFound](/Screenshots/EmployeesNotFound.png)
