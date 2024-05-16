using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(await _userService.GetUserAll());
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(string email, string password)
        {
            return Ok(await _userService.GetUser(email,password));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<User>>>> Create(User User)
        {
            return Ok(await _userService.Create(User));

        }
        [HttpPost("Update")]
        public async Task<ActionResult<ServiceResponse<User>>> Update(User user)
        {
            return Ok(await _userService.Update(user));
        }
        [HttpPost("SendEmail")]
        public async Task<ActionResult<ServiceResponse<string>>> SendEmail(string email)
        {
            return Ok(await _userService.SendEmail(email));
           
        }


    }
}
