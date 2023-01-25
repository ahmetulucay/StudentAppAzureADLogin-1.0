
namespace StudentApp.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string School { get; set; }
        public DateTime RegistrationDate { get; internal set; }

        public StudentPhoneNo PhoneStudent { get; set; }
        public StudentEmailAddress EmailAddressStudent { get; set; }
        public StudentAddress AddressStudent { get; set; }
    }

    public class StudentPhoneNo
    {
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public int StudentsId { get; set; }
        public Students Students { get; set; }
    }

    public class StudentEmailAddress
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public int StudentsId { get; set; }
        public Students Students { get; set; }

    }
    public class StudentAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PostNumber { get; set; }
        public string Country { get; set; }
        public int StudentsId { get; set; }
        public Students Students { get; set; }
    }
}
