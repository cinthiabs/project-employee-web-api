using project_employee_web_api.Models;

namespace project_employee_web_api.Service.EmployeeService
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<List<EmployeeModel>>> GetEmployee();
        Task<ServiceResponse<List<EmployeeModel>>> CreateEmployee(EmployeeModel newEmployee);
        Task<ServiceResponse<EmployeeModel>> GetEmployeeById(int id);
        Task<ServiceResponse<List<EmployeeModel>>> UpdateEmployee(EmployeeModel newEmployee);
        Task<ServiceResponse<List<EmployeeModel>>> DeleteEmployee(int id);
        Task<ServiceResponse<List<EmployeeModel>>> InactiveEmployee(int id);
    }
}
