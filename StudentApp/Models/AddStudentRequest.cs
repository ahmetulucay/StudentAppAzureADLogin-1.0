using NuGet.Protocol;

namespace StudentApp.Models
{
    public class AddStudentRequest
    {
        public AddStudentRequest()
        {
        }
        public AddStudentRequest(Students students)
        {
            UserName= students.UserName;
            FirstName= students.FirstName;
            SecondName = students.SecondName; 
            LastName= students.LastName;   
            Address= students.Address;  
            TlfNo= students.TlfNo; 
            School= students.School;
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

        public virtual Students ToStudent(AddStudentRequest addStudentRequest)
        {
            return new Students 
            {
                UserName = addStudentRequest.UserName,
                FirstName = addStudentRequest.FirstName,
                SecondName = addStudentRequest.SecondName,
                LastName = addStudentRequest.LastName,
                Address = addStudentRequest.Address,
                TlfNo = addStudentRequest.TlfNo,
                School = addStudentRequest.School,
                RegistrationDate = addStudentRequest.RegistrationDate
            };
        }
    }
}
