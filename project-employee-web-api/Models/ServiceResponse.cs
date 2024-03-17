using project_employee_web_api.Enums;

namespace project_employee_web_api.Models
{
    public class ServiceResponse<T>
    {
       public T? Dados { get; set; }
       public string Mensagem { get; set; }   = string.Empty;
       public bool Sucesso { get; set; }    
    }
}
