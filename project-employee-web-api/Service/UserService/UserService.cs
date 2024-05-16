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
        public async Task<ServiceResponse<List<User>>> GetUserAll()
        {
            ServiceResponse<List<User>> serviceResponse = new ServiceResponse<List<User>>();
            try
            {
                serviceResponse.Dados =  await _context.User.ToListAsync();
                if (serviceResponse.Dados.Count == 0)
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<User>> Create(User user)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            try
            {
                if (user == null)
                {
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                User existingUser = await _context.User.FirstOrDefaultAsync(x => x.Email == user.Email);
                if (existingUser != null)
                {
                    serviceResponse.Mensagem = "E-mail já cadastrado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }
                _context.Add(user);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = user;
                serviceResponse.Sucesso = true;
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
                User user = await _context.User.FirstOrDefaultAsync(x => x.Email == email && x.Senha == password);
                serviceResponse.Dados = user;

                if (user == null)
                {
                    serviceResponse.Mensagem = "Usuário não localizado! Verifique seu email e senha!";
                    serviceResponse.Sucesso = false;
                }
                else
                {
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
        public async Task<ServiceResponse<User>> Update(User user)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            try
            {
                if (user == null)
                {
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                User existingUser = await _context.User.FirstOrDefaultAsync(x => x.Email == user.Email);
                if (existingUser != null)
                {
                    existingUser.Senha = user.Senha;
                    existingUser.ConfirmarSenha = user.ConfirmarSenha;
                    _context.Update(existingUser);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = user;
                    serviceResponse.Sucesso = true;
                }
                else
                {
                    serviceResponse.Mensagem = "Email não cadastrado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
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
    }
}
