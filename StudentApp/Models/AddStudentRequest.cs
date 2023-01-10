
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
            Address= students.Address;  
            TlfNo= students.TlfNo; 
            School= students.School;
            RegistrationDate = students.RegistrationDate;
        }

        [IsNotNullOrEmpty] public string UserName { get; set; }
        [IsNotNullOrEmpty] public string FirstName { get; set; }
        public string SecondName { get; set; }
        [IsNotNullOrEmpty] public string LastName { get; set; }

        [IsNotNullOrEmpty] public string Address { get; set; }
        [IsNotNullOrEmpty] public string TlfNo { get; set; }
        [IsNotNullOrEmpty] public string School { get; set; }
        [ValidateAddJoinDate] public DateTime RegistrationDate { get; set; }

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
