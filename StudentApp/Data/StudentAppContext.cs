
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;

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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StudentApp.Data;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>()
                .HasOne(s => s.Address)
                .WithOne(ad => ad.Students)
                .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);
        }

        public DbSet<Students> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }


        //public StudentAppContext(DbSet<Students> students, DbSet<StudentAddress> studentAddresses)
        //{
        //    Students = students;
        //    StudentAddresses = studentAddresses;
        //}
        //DbContextOptions<StudentAppContext> dbContextOptions { get; set; } 

        ////public studentappcontext()
        //public studentappcontext(dbcontextoptions<studentappcontext> options)
        //    : base(options) { }
        //public dbset<studentapp.models.students> student { get; set; }
        //public object students { get; set; }
    }
}
