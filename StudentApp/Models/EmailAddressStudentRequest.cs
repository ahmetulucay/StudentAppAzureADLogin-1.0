
using StudentApp.Controllers.Validations;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models
{
    public class EmailAddressStudentRequest
    {
        public EmailAddressStudentRequest()
        {
        }

        public EmailAddressStudentRequest(ICollection<StudentEmailAddress> emailAddressStudent)
        {
        }
        public EmailAddressStudentRequest(StudentEmailAddress studentEmailAddress)
        {
            EmailAddress = studentEmailAddress.EmailAddress;
        }
        [IsNotNullOrEmpty] public string EmailAddress { get; set; }

        public virtual StudentEmailAddress ToEmailStudent(EmailAddressStudentRequest emailAddressStudentRequest)
        {
            return new StudentEmailAddress
            {
                EmailAddress = emailAddressStudentRequest.EmailAddress
            };
        }
    }
}
