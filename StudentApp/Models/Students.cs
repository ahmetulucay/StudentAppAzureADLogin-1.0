
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models
{
    public class Students
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username must be specified")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Firstname must be specified")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Secondname must be specified")]
        public string? SecondName { get; set; }

        [Required(ErrorMessage = "Lastname must be specified")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Address must be specified")]
        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }

        [Range(1, 10, ErrorMessage = "TlfNo must have upto 10 digits")]
        public string TlfNo { get; set; }

        [Required(ErrorMessage = "School must be specified")]
        public string School { get; set; }

        [Required(ErrorMessage = "RegistrationDate is required")]
        public DateTime RegistrationDate { get; set; }

    }
}
