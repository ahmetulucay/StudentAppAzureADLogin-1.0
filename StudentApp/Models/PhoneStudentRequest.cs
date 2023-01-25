
using StudentApp.Controllers.Validations;

namespace StudentApp.Models
{
    public class PhoneStudentRequest
    {
        public PhoneStudentRequest()
        {
        }
        public PhoneStudentRequest(StudentPhoneNo studentPhone)
        {
            PhoneNo = studentPhone.PhoneNo;
        }
        [IsNotNullOrEmpty] public string PhoneNo { get; private set; }

        public virtual StudentPhoneNo ToPhoneStudent(PhoneStudentRequest phoneStudentRequest)
        {
            return new StudentPhoneNo
            {
                    PhoneNo = phoneStudentRequest.PhoneNo,
            };
        }
    }
}
