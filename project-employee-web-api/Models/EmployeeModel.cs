using project_employee_web_api.Enums;
using System.ComponentModel.DataAnnotations;

namespace project_employee_web_api.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DepartmentEnum Departamento { get; set; }
        public bool Ativo { get; set; }
        public string Turno { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
    }
}
