
using StudentApp.Controllers.Validations;

namespace StudentApp.Models
{
    public class UpdateStudentRequest {
        public UpdateStudentRequest()
        {
        }
        public UpdateStudentRequest(Students students)
        {
            UserName = students.UserName ?? "Test";
            FirstName = students.FirstName;
            SecondName = students.SecondName;
            LastName = students.LastName;
            School = students.School;
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
        [ValidateUpdateJoinDate] public DateTime RegistrationDate { get; set; }
        [IsNotNullOrEmpty] public PhoneStudentRequest PhoneStudent { get; set; }
        [IsNotNullOrEmpty] public EmailAddressStudentRequest EmailAddressStudent { get; set; }
        [IsNotNullOrEmpty] public AddressStudentRequest AddressStudent { get; set; }


        public virtual Students ToUpdateStudent(UpdateStudentRequest updateStudentRequest)
        {
            return new Students
            {
                UserName = updateStudentRequest.UserName,
                FirstName = updateStudentRequest.FirstName,
                SecondName = updateStudentRequest.SecondName,
                LastName = updateStudentRequest.LastName,
                School = updateStudentRequest.School,
                RegistrationDate = updateStudentRequest.RegistrationDate,
                PhoneStudent = (ICollection<StudentPhoneNo>)new PhoneStudentRequest().ToPhoneStudent(updateStudentRequest.PhoneStudent),
                EmailAddressStudent = (ICollection<StudentEmailAddress>)new EmailAddressStudentRequest().ToEmailStudent(updateStudentRequest.EmailAddressStudent),
                AddressStudent = (ICollection<StudentAddress>)new AddressStudentRequest().ToAddressStudent(updateStudentRequest.AddressStudent)
            };
        }
    }
}
