﻿using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(string email, string password)
        {
            return Ok(await _userService.GetUser(email,password));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<User>>>> CreateEmployee(User User)
        {
            var result = await _userService.CreateEmployee(User);
            if (result.Sucesso == true)

                return Ok(result);
            else
                return BadRequest(result);
        }
        [HttpGet("sendEmail")]
        public async Task<ActionResult<ServiceResponse<string>>> SendEmail(string email)
        {
            var result = await _userService.SendEmail(email);
            if(result.Sucesso == true)
            
                return Ok(result);
            else
                return BadRequest(result);
               
        }


    }
}
