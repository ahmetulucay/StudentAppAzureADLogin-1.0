
using StudentApp.Controllers.Validations;

namespace StudentApp.Models
{
    public class UpdateStudentRequest {
        public UpdateStudentRequest()
        {
        }
        public UpdateStudentRequest(Students students)
        {
            UserName = students?.UserName??"Test";
            FirstName = students?.FirstName;
            SecondName =students?.SecondName;
            LastName = students?.LastName;
            Address = students?.Address;
            TlfNo = students?.TlfNo;
            School = students?.School;
            RegistrationDate = students.RegistrationDate;
        }

        [ValidateNotNullOrEmpty] public string? UserName { get; set; }
        [ValidateNotNullOrEmpty] public string? FirstName { get; set; }
        [ValidateNotNullOrEmpty] public string? SecondName { get; set; }
        [ValidateNotNullOrEmpty] public string? LastName { get; set; }
        [ValidateNotNullOrEmpty] public string? Address { get; set; }
        [ValidateNotNullOrEmpty] public string? TlfNo { get; set; }
        [ValidateNotNullOrEmpty] public string? School { get; set; }
        [ValidJoinDate] public DateTime RegistrationDate { get; internal set; }

        public virtual Students UpdateStudent(UpdateStudentRequest updateStudentRequest)
        {
            return new Students
            {
                UserName = updateStudentRequest.UserName,
                FirstName = updateStudentRequest.FirstName,
                SecondName = updateStudentRequest.SecondName,
                LastName = updateStudentRequest.LastName,
                Address = updateStudentRequest.Address,
                TlfNo = updateStudentRequest.TlfNo,
                School = updateStudentRequest.School,
                RegistrationDate = updateStudentRequest.RegistrationDate
            };
        }
    }
}
