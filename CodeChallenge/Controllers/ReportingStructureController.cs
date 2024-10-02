using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ReportingStructureController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("{employeeId}")]
        public ActionResult<ReportingStructure> GetReportingStructure(string employeeId)
        {
            var reportingStructure = _employeeRepository.GetReportingStructure(employeeId);

            if (reportingStructure == null)
            {
                return NotFound();
            }

            return Ok(reportingStructure);
        }
    }
}
