using Microsoft.EntityFrameworkCore;
using WebForm2.Models.Domain;

namespace WebForm2.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
