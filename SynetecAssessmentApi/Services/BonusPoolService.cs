﻿using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class BonusPoolService: IBonusPoolService
    {
        private readonly AppDbContext _dbContext;

        public BonusPoolService(AppDbContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public async Task<EmployeeDto> GetEmployeeAsync(int selectedEmployeeId)
        {
            Employee employee = await _dbContext      
                .Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);

            if(employee != null)
            {
                return new EmployeeDto
                {
                    Id = employee.Id,
                    Fullname = employee.Fullname,
                    JobTitle = employee.JobTitle,
                    Salary = employee.Salary,
                    Department = new DepartmentDto
                    {
                        Title = employee.Department.Title,
                        Description = employee.Department.Description
                    }
                };
            }
            else
            {
                throw new KeyNotFoundException("Employee not found");
            }
        }    
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();

            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                result.Add(
                    new EmployeeDto
                    {
                        Id = employee.Id,
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    });
            }

            return result;
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            Employee employee = await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);
            if (employee != null)
            {

                //get the total salary budget for the company
                int totalSalary = (int)_dbContext.Employees.Sum(item => item.Salary);

                //calculate the bonus allocation for the employee
                decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
                int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);

                return new BonusPoolCalculatorResultDto
                {
                    Employee = new EmployeeDto
                    {
                        Id = employee.Id,
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    },

                    Amount = bonusAllocation
                };
            }
            else
            {
                throw new KeyNotFoundException("Employee not found");
            }
        }
    }
}
