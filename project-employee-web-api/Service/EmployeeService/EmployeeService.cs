using Microsoft.EntityFrameworkCore;
using project_employee_web_api.DataContext;
using project_employee_web_api.Models;

namespace project_employee_web_api.Service.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<EmployeeModel>>> CreateEmployee(EmployeeModel newEmployee)
        {
            ServiceResponse<List<EmployeeModel>> serviceResponse = new ServiceResponse<List<EmployeeModel>>();
            try
            {
                if(newEmployee == null)
                     serviceResponse.Mensagem = "Informar dados!";
                    

                _context.Add(newEmployee);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Employee.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeeModel>>> DeleteEmployee(int id)
        {
            ServiceResponse<List<EmployeeModel>> serviceResponse = new ServiceResponse<List<EmployeeModel>>();
            try
            {
                EmployeeModel employee = _context.Employee.FirstOrDefault(x => x.Id == id);
                if (employee == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;
                }

                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Employee.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeeModel>>> GetEmployee()
        {
            ServiceResponse<List<EmployeeModel>> serviceResponse = new ServiceResponse<List<EmployeeModel>>();

            try
            {
                serviceResponse.Dados = _context.Employee.ToList();
                if (serviceResponse.Dados.Count == 0)
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";

            }catch(Exception ex) 
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeeModel>> GetEmployeeById(int id)
        {
            ServiceResponse<EmployeeModel> serviceResponse = new ServiceResponse<EmployeeModel>();
            try
            {
                EmployeeModel employee =  _context.Employee.FirstOrDefault(x => x.Id == id);
                if (employee == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;
                }
                   
                serviceResponse.Dados = employee;
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeeModel>>> InactiveEmployee(int id)
        {
            ServiceResponse<List<EmployeeModel>> serviceResponse = new ServiceResponse<List<EmployeeModel>>();
            try
            {
                EmployeeModel employee = _context.Employee.FirstOrDefault(x => x.Id == id);
                if (employee == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;
                }
                employee.Ativo = false;
                employee.DataDeAlteracao = DateTime.Now.ToLocalTime();
               
                _context.Employee.Update(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Employee.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeeModel>>> UpdateEmployee(EmployeeModel newEmployee)
        {
            ServiceResponse<List<EmployeeModel>> serviceResponse = new ServiceResponse<List<EmployeeModel>>();
            try
            {
                EmployeeModel employee = _context.Employee.AsNoTracking().FirstOrDefault(x => x.Id == newEmployee.Id);
                if (employee == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;
                }
                employee.DataDeAlteracao = DateTime.Now.ToLocalTime();

                _context.Employee.Update(newEmployee);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Employee.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }
    }
}
