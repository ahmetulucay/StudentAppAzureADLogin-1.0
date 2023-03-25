namespace StudentApp.Models;

public class ImageStudentRequest
{

    public ImageStudentRequest() {}

    public ImageStudentRequest(StudentImage studentImage)
    {
        ImageName = studentImage.ImageName;
        Path = studentImage.Path;
    }

    public string ImageName { get; set; }
    public string Path { get; set; }

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