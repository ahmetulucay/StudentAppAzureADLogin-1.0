
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models
{
    public class Students
    {
        internal int id;
        private readonly Students students;

        public int Id { get; set; }
        [Required] public string UserName { get; set; }
        [Required] public string FirstName { get; set; }
        public string SecondName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string TlfNo { get; set; }
        [Required] public string School { get; set; }
        [Required] public DateTime RegistrationDate { get; internal set; }

        [Required] public StudentAddress Address { get; set; }
    }

    public class StudentAddress
    {
        [Required] public int StudentAddressId { get; set; }
        [Required] public string address { get; set; }
        [Required] public string City { get; set; }
        [Required] public int PostNumber { get; set; }
        [Required] public string Country { get; set; }
        [Required] public int AddressOfStudentId { get; set; }
        [Required] public Students Students { get; set; }

    }
}
