
using StudentApp.Controllers.Validations;

namespace StudentApp.Models;

public class ImageStudentRequest
{

    public ImageStudentRequest() {}

    public ImageStudentRequest(StudentImage studentImage)
    {
        ImageId = studentImage.ImageId;
        ImageName = studentImage.ImageName;
        Path = studentImage.Path;
    }

    [IsNotNullOrEmpty] public int ImageId { get; set; }
    [IsNotNullOrEmpty] public string ImageName { get; set; }
    [IsNotNullOrEmpty] public string Path { get; set; }

    public virtual StudentImage ToImageStudent(ImageStudentRequest imageStudentRequest)
    {
        return new StudentImage
        {
                ImageName = imageStudentRequest.ImageName,
                Path = imageStudentRequest.Path
        };
    }

    public StudentImage ToImageStudent()
    {
        return new StudentImage
        {
                ImageName = ImageName,
                Path = Path
        };
    }
}