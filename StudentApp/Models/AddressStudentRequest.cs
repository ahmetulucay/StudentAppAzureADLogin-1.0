
using StudentApp.Controllers.Validations;

namespace StudentApp.Models
{
    public class AddressStudentRequest
    {
        public AddressStudentRequest()
        {
        }
        public AddressStudentRequest(StudentAddress studentAddress)
        {
            Address = studentAddress.Address;
            City = studentAddress.City;
            PostNumber = studentAddress.PostNumber;
            Country= studentAddress.Country;
        }

        [IsNotNullOrEmpty] public string Address{ get; set; }
        [IsNotNullOrEmpty] public string City { get; set; }
        [IsNotNullOrEmpty] public int PostNumber { get; set; }
        [IsNotNullOrEmpty] public string Country { get; set; }

        public virtual StudentAddress ToAddressStudent(AddressStudentRequest addressStudentRequest)
        {
            return new StudentAddress
            {
                    Address= addressStudentRequest.Address,
                    City = addressStudentRequest.City,
                    PostNumber = addressStudentRequest.PostNumber,
                    Country = addressStudentRequest.Country
            };
        }
    }
}
