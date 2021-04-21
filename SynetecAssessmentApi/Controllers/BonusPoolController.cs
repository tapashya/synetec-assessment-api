using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services;
using System;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolService _bonusPoolService;
        public BonusPoolController (IBonusPoolService bonusPoolService)
        {
            _bonusPoolService = bonusPoolService;
        }

        [HttpGet]
        [Route("Employees")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bonusPoolService.GetEmployeesAsync());
        }

        [HttpGet]
        [Route("Employee")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var employee = await _bonusPoolService.GetEmployeeAsync(id);
                return Ok(employee);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusRequest request)
        {
            if (request != null)
            {
                try
                {
                    return Ok(await _bonusPoolService.CalculateAsync(
                     request.TotalBonusPoolAmount,
                     request.SelectedEmployeeId));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
