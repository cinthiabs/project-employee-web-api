using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_employee_web_api.Migrations;
using project_employee_web_api.Models;
using project_employee_web_api.Service.EmployeeService;
using project_employee_web_api.Service.UserService;

namespace project_employee_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetUserAll")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUserAll()
        {
            var result = await _userService.GetUserAll();
            return result.Sucesso ? Ok(result) : NotFound(result);
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(string email, string password)
        {
            var result = await _userService.GetUser(email, password);
            return result.Sucesso ? Ok(result) : NotFound(result);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<User>>>> Create(User user)
        {
            var result = await _userService.Create(user);
            return result.Sucesso ? Ok(result) : NotFound(result);
        }
        [HttpPost("Update")]
        public async Task<ActionResult<ServiceResponse<User>>> Update(User user)
        {
            var result = await _userService.Update(user);
            return result.Sucesso ? Ok(result) : NotFound(result);
        }
        [HttpPost("SendEmail")]
        public async Task<ActionResult<ServiceResponse<string>>> SendEmail(string email)
        {
            var result = await _userService.SendEmail(email);
            return result.Sucesso ? Ok(result) : NotFound(result);
        }
    }
}
