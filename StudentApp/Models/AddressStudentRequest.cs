
using StudentApp.Controllers.Validations;

namespace StudentApp.Models
{
    public class AddressStudentRequest
    {
        public AddressStudentRequest()
        {
        }
        public AddressStudentRequest(Students students)
        {
            Address = students.Address.Address;
            City = students.Address.City;
            PostNumber = students.Address.PostNumber;
            Country= students.Address.Country;
        }

        [IsNotNullOrEmpty] public string Address{ get; set; }
        [IsNotNullOrEmpty] public string City { get; set; }
        [IsNotNullOrEmpty] public int PostNumber { get; set; }
        [IsNotNullOrEmpty] public string Country { get; set; }

        public virtual Students AddressStudent(AddressStudentRequest addressStudentRequest)
        {
            return new Students
            {
                Address = new StudentAddress
                {
                    Address= addressStudentRequest.Address,
                    City = addressStudentRequest.City,
                    PostNumber = addressStudentRequest.PostNumber,
                    Country = addressStudentRequest.Country
                }
            };
        }
    }
}
