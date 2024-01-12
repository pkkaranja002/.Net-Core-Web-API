using APIDemo.Model;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.Data
{
    public class ApplicationDbContext :DbContext
    {

        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<StudentEntity> StudentsRegister { get; set; }
    }
}
