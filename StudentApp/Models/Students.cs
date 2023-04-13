
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models;
public class Students
{
    [Key]
    public int StudentId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string LastName { get; set; }
    public string School { get; set; }
    public DateTime RegistrationDate { get; internal set; }
    public ICollection<StudentPhoneNo> PhoneStudent { get; set; }
    public ICollection<StudentEmailAddress> EmailAddressStudent { get; set; }
    public ICollection<StudentAddress> AddressStudent { get; set; }
    public ICollection<StudentImage> ImageStudent { get; set; }
}

public class StudentPhoneNo
{
    [Key]
    public int PhoneId { get; set; }
    public string PhoneNo { get; set; }
    [ForeignKey("Students")]
    public int StudentsId { get; set; }
    public Students Students { get; set; }
}
public class StudentEmailAddress
{
    [Key]
    public int EmailId { get; set; }
    public string EmailAddress { get; set; }
    [ForeignKey("Students")]
    public int StudentsId { get; set; }
    public Students Students { get; set; }

}
public class StudentAddress
{
    [Key]
    public int AddressId { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int PostNumber { get; set; }
    public string Country { get; set; }
    [ForeignKey("Students")]
    public int StudentsId { get; set; }
    public Students Students { get; set; }
}
public class StudentImage
{
    [Key]
    public int ImageId { get; set; }
    public string Path { get; set; }
    public string ImageName { get; set; }

    [ForeignKey("Students")]
    public int StudentsId { get; set; }
    public Students Students { get; set; }
}
