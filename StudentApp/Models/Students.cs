
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models
{
    public class Students
    {
        internal int id;
        private readonly Students Student;

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string TlfNo { get; set; }
        public string School { get; set; }
        public DateTime RegistrationDate { get; internal set; }

        public StudentAddress Address { get; set; }
    }

    public class StudentAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PostNumber { get; set; }
        public string Country { get; set; }
        public int StudentsId { get; set; }
        public Students Students { get; set; }

    }
}
