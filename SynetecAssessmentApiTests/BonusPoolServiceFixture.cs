using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Services;
using Moq;
using SynetecAssessmentApi.Domain;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SynetecAssessmentApi.Dtos;
using System;

namespace SynetecAssessmentApiTests
{
    [TestClass]
    public class BonusPoolServiceFixture
    {
        private IBonusPoolService _bonusPoolService;
        private AppDbContext _appDbContext;

        [TestInitialize]
        public void Setup()
        {
            SetupData(); 
        }

        [TestCleanup]
        public void TearDown()
        {
            _appDbContext.Database.EnsureDeleted();
            _appDbContext.Dispose();
        }

        [TestMethod]
        public void GetEmplyee_ThrowsExceptionWhenEmployeeDoesNotExist()
        {
            int employeeId = 7;            
            var exceptionMessage = "";
            try
            {
                var result = _bonusPoolService.GetEmployeeAsync(employeeId).Result;
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
            }
            Assert.IsTrue(exceptionMessage.Contains("Employee not found"));
        }

        [TestMethod]
        public void CalculateBonusForEmployee_UsesTheCorrectPercentageOfTotalWageBudget()
        {
            int employeeId = 1;
            int bonusPoolAmount = 123456;
            EmployeeDto employee = _bonusPoolService.GetEmployeeAsync(employeeId).Result;
            IEnumerable<EmployeeDto> employees = _bonusPoolService.GetEmployeesAsync().Result;

            int totalSalary = employees.Sum(e => e.Salary);
            decimal expectedPercentage = (decimal)employee.Salary / (decimal)totalSalary;
            int expectedBonusAllocation = (int)(expectedPercentage * bonusPoolAmount);

            var actualResult = _bonusPoolService.CalculateAsync(bonusPoolAmount, employeeId).Result;
            Assert.AreEqual(expectedBonusAllocation, actualResult.Amount);
        }

        [TestMethod]       
        public void CalculateBonusForEmployee_ThrowsExceptionWhenEmployeeDoesNotExist()
        {
            int employeeId = 6;
            int bonusPoolAmount = 123456;
            var exceptionMessage = "";
            try
            {
                var result = _bonusPoolService.CalculateAsync(bonusPoolAmount, employeeId).Result;
            } catch(Exception ex)
            {
                exceptionMessage = ex.Message;
            }
            Assert.IsTrue(exceptionMessage.Contains("Employee not found"));
        }

        public void SetupData()
        {
            var dept1 = new Department(1, "Finance", "The finance department for the company");
            var dept2 = new Department(2, "Human Resources", "The Human Resources department for the company");
            var dept3 = new Department(3, "IT", "The IT support department for the company");
            var dept4 = new Department(4, "Marketing", "The Marketing department for the company");

            var departments = new List<Department> { dept1, dept2, dept3, dept4 };

            var emp1 = new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1);
            emp1.Department = dept1;
            var emp2 = new Employee(2, "Janet Jones", "HR Director", 90000, 2);
            emp2.Department = dept2;
            var emp3 = new Employee(3, "Robert Rinser", "IT Director", 95000, 3);
            emp3.Department = dept3;
            var emp4 = new Employee(4, "Jilly Thornton", "Marketing Manager (Senior)", 55000, 4);
            emp4.Department = dept4;

            var employees = new List<Employee> {emp1, emp2, emp3, emp4};

            var serviceProvider = new ServiceCollection()
            .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDbContext>();

            builder.UseInMemoryDatabase(databaseName: "HrDb");

            _appDbContext = new AppDbContext(builder.Options);
            _appDbContext.Employees.AddRange(employees);
            _appDbContext.Departments.AddRange(departments);
            _appDbContext.SaveChanges();

            _bonusPoolService = new BonusPoolService(_appDbContext);
        }

    }
}
