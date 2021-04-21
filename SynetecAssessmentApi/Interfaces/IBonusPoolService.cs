using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public interface IBonusPoolService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<EmployeeDto> GetEmployeeAsync(int selectedEmployeeId);
        Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId);
        
    }
}
