using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees
                .Include(e => e.DirectReports)
                .ThenInclude(dr => dr.DirectReports)
                .SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }

        public ReportingStructure GetReportingStructure(string employeeId)
        {
            var employee = _employeeContext.Employees
                .Include(e => e.DirectReports)
                .ThenInclude(dr => dr.DirectReports)
                .SingleOrDefault(e => e.EmployeeId == employeeId);

            if (employee == null) return null;

            var numberOfReports = CountReports(employee);

            return new ReportingStructure
            {
                employee = employee,
                numberOfReports = numberOfReports
            };
        }

        private int CountReports(Employee employee)
        {
            if (employee.DirectReports == null || !employee.DirectReports.Any())
                return 0;

            int totalReports = 0;

                for (int i = 0; i < employee.DirectReports.Count; i++)
                {
                    totalReports++; 
                    totalReports += CountReports(employee.DirectReports[i]);
                }

            return totalReports;
        }
    }
}

