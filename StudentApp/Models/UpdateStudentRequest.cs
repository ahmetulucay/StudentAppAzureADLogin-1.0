
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
            Address = students.Address.Address;
            City = students.Address.City;
            PostNumber = students.Address.PostNumber;
            Country = students.Address.Country;
        }

        [IsNotNullOrEmpty] public string UserName { get; set; }
        [IsNotNullOrEmpty] public string FirstName { get; set; }
        public string SecondName { get; set; }
        [IsNotNullOrEmpty] public string LastName { get; set; }
        [IsNotNullOrEmpty] public string TlfNo { get; set; }
        [IsNotNullOrEmpty] public string School { get; set; }
        [ValidateAddJoinDate] public DateTime RegistrationDate { get; set; }
        [IsNotNullOrEmpty] public string Address { get; set; }
        [IsNotNullOrEmpty] public string City { get; set; }
        [IsNotNullOrEmpty] public int PostNumber { get; set; }
        [IsNotNullOrEmpty] public string Country { get; set; }


        public virtual Students UpdateStudent(UpdateStudentRequest updateStudentRequest)
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
                Address = new StudentAddress
                {
                    Address = updateStudentRequest.Address,
                    City = updateStudentRequest.City,
                    PostNumber = updateStudentRequest.PostNumber,
                    Country = updateStudentRequest.Country
                }
            };
        }
    }
}
