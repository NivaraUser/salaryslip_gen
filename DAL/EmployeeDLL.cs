using System;
using Models;

namespace DAL
{
    public class EmployeeDll : IEmployee
    {
        public EmployeeModel PostEmployeeSalaryDetail(Employee employee)
        {
            var employeeModel = new EmployeeModel
            {
                Name = string.Format("{0} {1}", employee.FirstName, employee.LastName),
                Payperiod = string.Format("{0}-{1}", employee.Startdate, employee.EndDate)
            };

            var result = GetCalculation(employee, employeeModel);

            return result;
        }

        private static EmployeeModel GetCalculation(Employee employee, EmployeeModel employeeModel)
        {
            employeeModel.Grossincome = employee.AnnualSalary > 0 ? employee.AnnualSalary / 12 : 0;
            employee.SupperRate = employee.SupperRate < 0 ? 0 : employee.SupperRate;

            employeeModel.Incometax = employee.AnnualSalary > 0 && employee.AnnualSalary <= 18200
                ? 0
                : (
                    employee.AnnualSalary >= 18201 && employee.AnnualSalary <= 37000
                        ? Math.Round(Convert.ToDouble((employee.AnnualSalary - 18200) * 19 / 100 / 12))
                        : (
                            employee.AnnualSalary >= 37001 && employee.AnnualSalary <= 87000
                                ? Math.Round(
                                    Convert.ToDouble((3572 + (employee.AnnualSalary - 37000) * 32.5m / 100) / 12))
                                : (
                                    employee.AnnualSalary >= 87001 && employee.AnnualSalary < 180000
                                        ? Math.Round(
                                            Convert.ToDouble((19822 + (employee.AnnualSalary - 87000) * 37 / 100) / 12))
                                        : (
                                            employee.AnnualSalary >= 180001
                                                ? Math.Round(Convert.ToDouble(
                                                    (54232 + (employee.AnnualSalary - 180000) * 45 / 100) / 12))
                                                : 0))));

            employeeModel.Netincome = employeeModel.Grossincome - employeeModel.Incometax;
            employeeModel.Superamount = employeeModel.Grossincome * employee.SupperRate / 100;

            return employeeModel;
        }
    }
}