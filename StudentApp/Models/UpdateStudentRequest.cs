
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
            SecondName = students?.SecondName;
            LastName = students?.LastName;
            Address = students?.Address;
            TlfNo = students?.TlfNo;
            School = students?.School;
            RegistrationDate = students.RegistrationDate;
        }

        public string? UserName { get; set; }
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }
        public string? LastName { get; set; }

        public string? Address { get; set; }
        public string? TlfNo { get; set; }
        public string? School { get; set; }

        public DateTime RegistrationDate { get; set; }


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
