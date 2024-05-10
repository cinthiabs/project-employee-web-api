﻿using project_employee_web_api.Models;

namespace project_employee_web_api.Service.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> GetUser(string email, string password);
        Task<ServiceResponse<List<User>>> CreateEmployee(User User);
        Task<ServiceResponse<List<User>>> UpdateEmployee(User User);
        Task<ServiceResponse<List<User>>> SendEmail(User User);
    }
}