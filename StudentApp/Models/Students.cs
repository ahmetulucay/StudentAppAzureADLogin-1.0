
using StudentApp.Controllers.Validations;

namespace StudentApp.Models
{
    public class Students
    {
        internal int id;

        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [ValidateNotNullOrEmptyAttribute] public string? Address { get; set; }
        public string? TlfNo { get; set; }
        public string? School { get; set; }

    }
}
