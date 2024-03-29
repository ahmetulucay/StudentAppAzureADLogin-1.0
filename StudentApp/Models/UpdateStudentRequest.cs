﻿
using StudentApp.Controllers.Validations;

namespace StudentApp.Models;
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
        School = students.School;
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
    [ValidateUpdateJoinDate] public DateTime RegistrationDate { get; set; }
    [IsNotNullOrEmpty] public ICollection<PhoneStudentRequest> PhoneStudent { get; set; }
    [IsNotNullOrEmpty] public ICollection<EmailAddressStudentRequest> EmailAddressStudent { get; set; }
    [IsNotNullOrEmpty] public ICollection<AddressStudentRequest> AddressStudent { get; set; }
    [IsNotNullOrEmpty] public ICollection<ImageStudentRequest> ImageStudent { get; set; }


    public virtual Students ToUpdateStudent(UpdateStudentRequest updateStudentRequest)
    {
        return new Students
        {
            UserName = updateStudentRequest.UserName,
            FirstName = updateStudentRequest.FirstName,
            SecondName = updateStudentRequest.SecondName,
            LastName = updateStudentRequest.LastName,
            School = updateStudentRequest.School,
            RegistrationDate = updateStudentRequest.RegistrationDate,
            PhoneStudent = updateStudentRequest.PhoneStudent.Select(p => p.ToPhoneStudent()).ToList(),
            EmailAddressStudent = updateStudentRequest.EmailAddressStudent.Select(p => p.ToEmailStudent()).ToList(),
            AddressStudent = updateStudentRequest.AddressStudent.Select(p => p.ToAddressStudent()).ToList(),
            ImageStudent = updateStudentRequest.ImageStudent.Select(p => p.ToImageStudent()).ToList()
        };
    }
}
