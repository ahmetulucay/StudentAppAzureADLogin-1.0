
using StudentApp.Controllers.Validations;

namespace StudentApp.Models
{
    public class AddStudentRequest
    {
        public AddStudentRequest()
        {
        }
        public AddStudentRequest(Students students)
        {
            UserName= students.UserName??"Test";
            FirstName= students.FirstName;
            SecondName = students.SecondName;
            LastName= students.LastName;   
            School= students.School;
            RegistrationDate = students.RegistrationDate;
            PhoneStudent = new PhoneStudentRequest(students.PhoneStudent);
            EmailAddressStudent = new EmailAddressStudentRequest(students.EmailAddressStudent);
            AddressStudent = new AddressStudentRequest(students.AddressStudent);
        }

        [IsNotNullOrEmpty] public string UserName { get; set; }
        [IsNotNullOrEmpty] public string FirstName { get; set; }
        public string SecondName { get; set; }
        [IsNotNullOrEmpty] public string LastName { get; set; }
        [IsNotNullOrEmpty] public string School { get; set; }
        [ValidateAddJoinDate] public DateTime RegistrationDate { get; set; }
        [IsNotNullOrEmpty] public PhoneStudentRequest PhoneStudent { get; set; }
        [IsNotNullOrEmpty] public EmailAddressStudentRequest EmailAddressStudent { get; set; }
        [IsNotNullOrEmpty] public AddressStudentRequest AddressStudent { get; set; }

        public virtual Students ToStudent(AddStudentRequest addStudentRequest)
        {
            return new Students
            {
                UserName = addStudentRequest.UserName,
                FirstName = addStudentRequest.FirstName,
                SecondName = addStudentRequest.SecondName,
                LastName = addStudentRequest.LastName,
                School = addStudentRequest.School,
                RegistrationDate = addStudentRequest.RegistrationDate,
                PhoneStudent = (ICollection<StudentPhoneNo>)new PhoneStudentRequest().ToPhoneStudent(addStudentRequest.PhoneStudent),
                EmailAddressStudent = (ICollection<StudentEmailAddress>)new EmailAddressStudentRequest().ToEmailStudent(addStudentRequest.EmailAddressStudent),
                AddressStudent = (ICollection<StudentAddress>)new AddressStudentRequest().ToAddressStudent(addStudentRequest.AddressStudent)
            };
        }
    }
}
