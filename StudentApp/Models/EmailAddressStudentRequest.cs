
using StudentApp.Controllers.Validations;

namespace StudentApp.Models;
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

    public StudentEmailAddress ToEmailStudent() => new StudentEmailAddress { EmailAddress = EmailAddress };
}
