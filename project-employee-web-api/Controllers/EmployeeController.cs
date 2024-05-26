using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_employee_web_api.Models;
using project_employee_web_api.Service.EmployeeService;

namespace project_employee_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employee)
        {
            _employeeService = employee;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> GetEmployee()
        {
            var result = await _employeeService.GetEmployee();
            return result.Sucesso ? Ok(result) : NotFound(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<EmployeeModel>>> GetEmployeeById(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);
            return result.Sucesso ? Ok(result) : NotFound(result);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> CreateEmployee(EmployeeModel newEmployee)
        {
            var result = await _employeeService.CreateEmployee(newEmployee);
            return result.Sucesso ? Ok(result) : NotFound(result);
        }

        [HttpPut("InactiveEmployee")]
        public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> InactiveEmployee(int id)
        {
            var result = await _employeeService.InactiveEmployee(id);
            return result.Sucesso ? Ok(result) : NotFound(result);
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> UpdateEmployee(EmployeeModel newEmployee)
        {
            var result = await _employeeService.UpdateEmployee(newEmployee);
            return result.Sucesso ? Ok(result) : NotFound(result);
        }
        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            return result.Sucesso ? Ok(result) : NotFound(result);

        }
    }
}
