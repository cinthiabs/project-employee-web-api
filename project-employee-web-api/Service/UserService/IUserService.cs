using project_employee_web_api.Models;

namespace project_employee_web_api.Service.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> GetUser(string email, string password);
        Task<ServiceResponse<List<User>>> GetUserAll();
        Task<ServiceResponse<User>> Create(User User);
        Task<ServiceResponse<User>> Update(User User);
        Task<ServiceResponse<string>> SendEmail(string email);

    }
}
