
using StudentApp.Controllers.Validations;

namespace StudentApp.Models
{
    public class PhoneStudentRequest
    {
        public PhoneStudentRequest()
        {
        }

        public PhoneStudentRequest(ICollection<StudentPhoneNo> phoneStudent) 
        { 
        }
        public PhoneStudentRequest(StudentPhoneNo studentPhone)
        {
            PhoneNo = studentPhone.PhoneNo;
        }
        [IsNotNullOrEmpty] public string PhoneNo { get; set; }

        public virtual StudentPhoneNo ToPhoneStudent(PhoneStudentRequest phoneStudentRequest)
        {
            return new StudentPhoneNo
            {
                    PhoneNo = phoneStudentRequest.PhoneNo,
            };
        }

        public StudentPhoneNo ToPhoneStudent() => new StudentPhoneNo { PhoneNo = PhoneNo };
    }
}
