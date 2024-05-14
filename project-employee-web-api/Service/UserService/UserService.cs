using Microsoft.EntityFrameworkCore;
using project_employee_web_api.DataContext;
using project_employee_web_api.Models;
using System.Net.Mail;
using System.Net;


namespace project_employee_web_api.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

        public async Task<ServiceResponse<string>> SendEmail(string userEmailDestinatario)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == userEmailDestinatario);
            if (user == null)
            {
                response.Sucesso = false;
                response.Mensagem = "Email não cadastrado!";
                return response;
            }
            else
            {
                string smtpServer = _configuration["EmailSettings:SmtpServer"];
                int port = int.Parse(_configuration["EmailSettings:Port"]);
                string userNameOrigem = _configuration["EmailSettings:UserNameOrigem"];
                string password = _configuration["EmailSettings:EncryptedPassword"];


                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(userNameOrigem);
                mail.To.Add(new MailAddress(userEmailDestinatario));
                mail.Subject = "Redefinição de senha";
                mail.Body = $"Olá, \n\nVocê solicitou uma redefinição de senha. Para continuar, clique no link a seguir: http://localhost:4200/reset/{userEmailDestinatario} \n\nSe você não solicitou esta alteração, ignore este e-mail.\n\nAtenciosamente, ";

                SmtpClient smtpClient = new SmtpClient(smtpServer, port);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(userNameOrigem, password);
                smtpClient.EnableSsl = true;

                try
                {
                    await smtpClient.SendMailAsync(mail);
                    response.Sucesso = true;
                    response.Mensagem = "E-mail enviado com sucesso!";
                }
                catch (Exception ex)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Erro ao enviar o e-mail: " + ex.Message;
                }
            }
            return response;
        }

        public Task<ServiceResponse<List<User>>> UpdateEmployee(User User)
        {
            throw new NotImplementedException();
        }

        
    }
}
