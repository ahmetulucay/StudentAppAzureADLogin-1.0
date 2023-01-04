
using Microsoft.EntityFrameworkCore;

namespace StudentApp.Data
{
    public class StudentAppContext : DbContext
    {
        public StudentAppContext()
        {
        }

        public StudentAppContext(DbContextOptions<StudentAppContext> options)
            : base(options)
        {
        }

        public DbSet<StudentApp.Models.Students> Student { get; set; }
        public object Students { get; set; }
    }
}
