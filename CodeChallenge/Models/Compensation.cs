using System;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public int Id { get; set; }  
        public string EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
