
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models
{
    public class Students
    {
        internal int id;

        public int Id { get; set; }
        [Required] public string UserName { get; set; }
        [Required] public string FirstName { get; set; }
        public string SecondName { get; set; }
        [Required] public string LastName { get; set; }

        [Required] public string Address { get; set; }
        [Required] public string TlfNo { get; set; }
        [Required] public string School { get; set; }
        [Required] public DateTime RegistrationDate { get; internal set; }
    }
}
