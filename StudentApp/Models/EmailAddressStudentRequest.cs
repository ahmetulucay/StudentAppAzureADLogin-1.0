
using StudentApp.Controllers.Validations;

namespace StudentApp.Models
{
    public class EmailAddressStudentRequest
    {
        public EmailAddressStudentRequest()
        {
        }
        public EmailAddressStudentRequest(StudentEmailAddress studentEmailAddress)
        {
            EmailAddress = studentEmailAddress.EmailAddress;
        }
        [IsNotNullOrEmpty] public string EmailAddress { get; private set; }

        public virtual StudentEmailAddress ToEmailStudent(EmailAddressStudentRequest emailAddressStudentRequest)
        {
            return new StudentEmailAddress
            {
                EmailAddress = emailAddressStudentRequest.EmailAddress
            };
        }
    }
}
