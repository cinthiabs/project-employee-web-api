using Microsoft.EntityFrameworkCore;
using project_employee_web_api.DataContext;
using project_employee_web_api.Models;
using System.Security.Cryptography;
using System.Text;

namespace project_employee_web_api.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<User>>> CreateEmployee(User User)
        {
            ServiceResponse<List<User>> serviceResponse = new ServiceResponse<List<User>>();
            try
            {
                if (User == null)
                {
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }
                //string hashedPassword = Hash(User.Senha);
                //User.Senha = hashedPassword;

                User user = await _context.User.FirstOrDefaultAsync(x => x.Email == User.Email);
                if(user != null)
                {
                    serviceResponse.Mensagem = "E-mail já cadastrado!";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    _context.Add(User);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = _context.User.ToList();
                    serviceResponse.Sucesso = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<User>> GetUser(string email, string password)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            try
            {
                User user =  await _context.User.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }
                else
                {
                    //string senha = Decrypt(user.Senha);
                   // user.Senha = senha;
                    serviceResponse.Dados = user;
                    serviceResponse.Sucesso = true;
                }


            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public Task<ServiceResponse<List<User>>> SendEmail(User User)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<User>>> UpdateEmployee(User User)
        {
            throw new NotImplementedException();
        }

        private string Hash(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
        public static string Decrypt(string encryptedText)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Convert.FromBase64String(encryptedText);
                byte[] originalBytes = sha256.ComputeHash(bytes);
                string originalSenha = Encoding.UTF8.GetString(originalBytes);
                return originalSenha;
            }
        }


    }
}
