
using StudentApp.Controllers.Validations;

namespace StudentApp.Models;
public class PhoneStudentRequest
{
    public PhoneStudentRequest()
    {
    }
    public PhoneStudentRequest(StudentPhoneNo studentPhone)
    {
        PhoneNo = studentPhone.PhoneNo;
    }
    [IsNotNullOrEmpty] public string PhoneNo { get; set; }

    public StudentPhoneNo ToPhoneStudent() => new StudentPhoneNo { PhoneNo = PhoneNo };
}
