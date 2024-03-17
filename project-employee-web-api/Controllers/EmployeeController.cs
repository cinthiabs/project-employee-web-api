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
            return Ok(await _employeeService.GetEmployee());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<EmployeeModel>>> GetEmployeeById(int id)
        {
            return Ok(await _employeeService.GetEmployeeById(id));  
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> CreateEmployee(EmployeeModel newEmployee)
        {
            return Ok(await _employeeService.CreateEmployee(newEmployee));
        }

        [HttpPut("InactiveEmployee")]
        public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> InactiveEmployee(int id)
        {
            return Ok(await _employeeService.InactiveEmployee(id));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> UpdateEmployee(EmployeeModel newEmployee)
        {
            return Ok(await _employeeService.UpdateEmployee(newEmployee));
        }
        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> DeleteEmployee(int id)
        {
           /// ServiceResponse<List<EmployeeModel>> serviceResponse = await _employeeService.DeleteEmployee(id);
            return Ok(await _employeeService.DeleteEmployee(id));
        }
    }
}
