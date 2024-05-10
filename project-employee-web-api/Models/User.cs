using project_employee_web_api.Enums;
using System.ComponentModel.DataAnnotations;

namespace project_employee_web_api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? ConfirmarSenha { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
    }
}
