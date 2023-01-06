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
            UserName= students.UserName;
            FirstName= students.FirstName;
            SecondName = students.SecondName;
            LastName= students.LastName;   
            Address= students.Address;  
            TlfNo= students.TlfNo; 
            School= students.School;
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
                RegistrationDate= addStudentRequest.RegistrationDate,
            };
        }
    }
}
