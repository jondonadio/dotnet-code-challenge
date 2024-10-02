using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompensationController : ControllerBase
    {
        private readonly CompensationData _compensationData;

        public CompensationController(CompensationData compensationData)
        {
            _compensationData = compensationData;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            if (compensation == null)
            {
                return BadRequest("Compensation data is null.");
            }

            var createdCompensation = _compensationData.CreateCompensation(compensation);
            return CreatedAtAction(nameof(GetCompensationByEmployeeId), new { employeeId = createdCompensation.EmployeeId }, createdCompensation);
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetCompensationByEmployeeId(string employeeId)
        {
            var compensation = _compensationData.GetCompensationByEmployeeId(employeeId);

            if (compensation == null)
            {
                return NotFound($"Compensation for employee with ID {employeeId} not found.");
            }

            return Ok(compensation);
        }
    }
}
