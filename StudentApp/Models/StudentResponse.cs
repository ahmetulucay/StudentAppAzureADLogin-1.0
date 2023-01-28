
namespace StudentApp.Models;

public class StudentResponse : AddStudentRequest
{
    public StudentResponse(Students students) : base(students)
    {
        StudentId = students.StudentId;
    }

    public int StudentId { get; set;}
}
