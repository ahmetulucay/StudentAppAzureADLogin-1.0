
namespace StudentApp.Models;

public class StudentResponse : AddStudentRequest
{
    public StudentResponse(Students students) : base(students)
    {
        StudentId = students.Id;
    }

    public int StudentId { get; set;}
}
