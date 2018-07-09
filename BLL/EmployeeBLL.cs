using Models;

namespace BLL
{
    public class EmployeeBll
    {
        private readonly IEmployee _emp;
        public EmployeeBll(IEmployee client)
        {
            _emp = client;
        }

        public EmployeeModel PostEmployeeDllDetal(Employee employee)
        {
            return _emp.PostEmployeeSalaryDetail(employee);
        }

        public bool ValidateModel(Employee employee)
        {
            if (employee.AnnualSalary <= 0 || employee.SupperRate <= 0)
                return false;

            return true;
        }

    }
}
