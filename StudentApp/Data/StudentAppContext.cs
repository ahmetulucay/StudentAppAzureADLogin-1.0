
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
        public DbSet<Students> Student { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StudentApp.Data;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>()
                .Property(s => s.Id)
                .HasColumnName("id")
                .HasDefaultValue(0)
                .IsRequired();
        }
    }
}
