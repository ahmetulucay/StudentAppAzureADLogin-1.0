
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
            TlfNo = students.TlfNo;
            School = students.School;
            RegistrationDate = students.RegistrationDate;
            addressStudent = new AddressStudentRequest(students.AddressStudent);
        }

        [IsNotNullOrEmpty] public string UserName { get; set; }
        [IsNotNullOrEmpty] public string FirstName { get; set; }
        public string SecondName { get; set; }
        [IsNotNullOrEmpty] public string LastName { get; set; }
        [IsNotNullOrEmpty] public string TlfNo { get; set; }
        [IsNotNullOrEmpty] public string School { get; set; }
        [ValidateUpdateJoinDate] public DateTime RegistrationDate { get; set; }
        [IsNotNullOrEmpty] public AddressStudentRequest addressStudent { get; set; }


        public virtual Students ToUpdateStudent(UpdateStudentRequest updateStudentRequest)
        {
            return new Students
            {
                UserName = updateStudentRequest.UserName,
                FirstName = updateStudentRequest.FirstName,
                SecondName = updateStudentRequest.SecondName,
                LastName = updateStudentRequest.LastName,
                TlfNo = updateStudentRequest.TlfNo,
                School = updateStudentRequest.School,
                RegistrationDate = updateStudentRequest.RegistrationDate,
                AddressStudent = new AddressStudentRequest().ToAddressStudent(updateStudentRequest.addressStudent)
            };
        }
    }
}
