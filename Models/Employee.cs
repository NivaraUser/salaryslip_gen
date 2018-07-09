using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public int AnnualSalary { get; set; }
        [Required]
        public int SupperRate { get; set; }
        public string Startdate { get; set; }
        public string EndDate { get; set; }
    }
}
