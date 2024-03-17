using Microsoft.EntityFrameworkCore;
using project_employee_web_api.Models;

namespace project_employee_web_api.DataContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<EmployeeModel> Employee { get; set; }

    }
}
