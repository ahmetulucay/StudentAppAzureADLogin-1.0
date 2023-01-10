using System.ComponentModel.DataAnnotations;
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

        [RequiredAttribute] [IsValid] public string UserName { get; set; }
        [RequiredAttribute] [IsValid] public string FirstName { get; set; }
        public string? SecondName { get; set; }
        [RequiredAttribute] [IsValid] public string LastName { get; set; }

        [RequiredAttribute] [IsValid] public string Address { get; set; }
        [RequiredAttribute] [IsValid] public string TlfNo { get; set; }
        [RequiredAttribute] [IsValid] public string School { get; set; }
        [RequiredAttribute] [ValidJoinDate] public DateTime RegistrationDate { get; set; }

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
