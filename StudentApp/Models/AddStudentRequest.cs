﻿
using StudentApp.Controllers.Validations;

namespace StudentApp.Models;
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
        School= students.School;

        RegistrationDate = students.RegistrationDate;
        PhoneStudent = students.PhoneStudent.Select(p => new PhoneStudentRequest(p)).ToList();
        EmailAddressStudent = students.EmailAddressStudent.Select(p => new EmailAddressStudentRequest(p)).ToList();
        AddressStudent = students.AddressStudent.Select(p => new AddressStudentRequest(p)).ToList();
        ImageStudent = students.ImageStudent.Select(p => new ImageStudentRequest(p)).ToList();
    }

    [IsNotNullOrEmpty] public string UserName { get; set; }
    [IsNotNullOrEmpty] public string FirstName { get; set; }
    [IsNotNullOrEmpty] public string SecondName { get; set; }
    [IsNotNullOrEmpty] public string LastName { get; set; }
    [IsNotNullOrEmpty] public string School { get; set; }
    [ValidateAddJoinDate] public DateTime RegistrationDate { get; set; }
    [IsNotNullOrEmpty] public ICollection<PhoneStudentRequest> PhoneStudent { get; set; }
    [IsNotNullOrEmpty] public ICollection<EmailAddressStudentRequest> EmailAddressStudent { get; set; }
    [IsNotNullOrEmpty] public ICollection<AddressStudentRequest> AddressStudent { get; set; }
    [IsNotNullOrEmpty] public ICollection<ImageStudentRequest> ImageStudent { get; set; }

    public virtual Students ToStudent(AddStudentRequest addStudentRequest)
    {
        return new Students
        {
            UserName = addStudentRequest.UserName,
            FirstName = addStudentRequest.FirstName,
            SecondName = addStudentRequest.SecondName,
            LastName = addStudentRequest.LastName,
            School = addStudentRequest.School,
            RegistrationDate = addStudentRequest.RegistrationDate,
            PhoneStudent = addStudentRequest.PhoneStudent.Select(p => p.ToPhoneStudent()).ToList(),
            EmailAddressStudent = addStudentRequest.EmailAddressStudent.Select(e => e.ToEmailStudent()).ToList(),
            AddressStudent = addStudentRequest.AddressStudent.Select(p => p.ToAddressStudent()).ToList(),
            ImageStudent = addStudentRequest.ImageStudent.Select(p => p.ToImageStudent()).ToList()
        };
    }
}
