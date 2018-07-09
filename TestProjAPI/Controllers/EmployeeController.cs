using System;
using Microsoft.AspNetCore.Mvc;
using Models;
using BLL;
using DAL;
using Microsoft.AspNetCore.Http;

namespace TestProjAPI.Controllers
{
    [Route("[Controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeBll _objBll = new EmployeeBll(new EmployeeDll());

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (!_objBll.ValidateModel(employee))
                    return StatusCode(StatusCodes.Status400BadRequest);

                return Ok(_objBll.PostEmployeeDllDetal(employee));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}