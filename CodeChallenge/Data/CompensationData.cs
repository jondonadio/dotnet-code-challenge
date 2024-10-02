using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CodeChallenge.Data
{
    public class CompensationData
    {
        private readonly EmployeeContext _context;

        public CompensationData(EmployeeContext context)
        {
            _context = context;
        }

        public Compensation CreateCompensation(Compensation compensation)
        {
            _context.Compensations.Add(compensation);
            _context.SaveChanges();
            return compensation;
        }

        public Compensation GetCompensationByEmployeeId(string employeeId)
        {
            return _context.Compensations
                   .Include(c => c.Employee)
                   .SingleOrDefault(c => c.EmployeeId == employeeId);
        }
    }
}
