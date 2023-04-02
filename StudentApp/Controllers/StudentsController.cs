
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Models;
using StudentApp.Services;
using System.Text;

namespace StudentApp.Controllers;

//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
//[Authorize]
[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IService _service;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly StudentAppContext _context;
    public StudentsController(IService service, IWebHostEnvironment webHostEnvironment, StudentAppContext context)
    {
        _service = service;
        _webHostEnvironment = webHostEnvironment;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentResponse>>> GetStudent()
    {
        var students = new List<StudentResponse>();
        var result = await _service.Get();
        if (result is null)  return Ok(students);
        for (var i = 0; i < result.Count;i++)
        {
            students.Add(new StudentResponse(result[i]));
        }
        return Ok(students);
    }

    [HttpGet("GetAsId/")]
    public async Task<ActionResult<StudentResponse>> GetAsId(int id)
    {
        var result = await _service.GetAsId(id);
        if (result == null) return NotFound($"Wrong Id {id}");
        return Ok(new StudentResponse(result));
    }

    [HttpPost]
    public async Task<ActionResult<StudentResponse>> AddStudent(AddStudentRequest addStudentRequest)
    {
        //var images = await SaveImageAsync(uploadedFile);
        var request = new AddStudentRequest();
        var student = request.ToStudent(addStudentRequest);
        //if (images is not null) student.ImageStudent = images;
        var result = await _service.AddStudent(student);
        return Ok(new StudentResponse(result));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<StudentResponse>> UpdateStudent(int id, UpdateStudentRequest updateStudentRequest)
    {
        var request = new UpdateStudentRequest();
        var student = request.ToUpdateStudent(updateStudentRequest);
        var result = await _service.UpdateStudent(id, student);

        if (result == null)
            return NotFound($"Wrong Id {id}");
        return Ok(new StudentResponse(result));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool?>> DeleteStudent(int id)
    {
        var result = await _service.DeleteStudent(id);
        if (result == null)
            return NotFound($"False: Wrong Id {id}");
        if (result == false)
            return NotFound($"False: Deleting Id {id} is NOT successful");
        return Ok($"True: Deleting Id {id} is successful");
    }

    //UploadImage 
    [HttpPut("UploadImage")]
    //[Route("{id}")]
    public async Task<ActionResult> UploadImage(IFormFile uploadedFile, int studentId, int imageId)
    {
        var result = await _service.GetAsId(studentId);
        if (result == null) return NotFound($"Wrong Id {studentId}");
        try
        {
            //Save image to wwwroot/image
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string path = Path.Combine(wwwRootPath + "\\Image\\", uploadedFile.FileName);

            //Save image path to Db 
            result.ImageStudent.ToList().Find(i => i.ImageId == imageId).ImageName = uploadedFile.FileName;
            result.ImageStudent.ToList().Find(i => i.ImageId == imageId).Path = path;

            await _service.UpdateStudent(studentId, result);

            using var stream = new MemoryStream();
            await uploadedFile.CopyToAsync(stream);
            var byteArray = stream.ToArray();

            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(byteArray);
                };
            }
            return Ok(new StudentResponse(result));
        }
        catch (Exception ex)
        {
            return NotFound("false: uploading image is not successful");
        }
    }

    [HttpGet("ExportImage/")]
    public async Task<FileContentResult> ExportImage(int studentId, int imageId) 
    {

        //Ihtimalleri yap studentId ve imageId icin,  ExportImage resim acilmiyor.
        //133-134 kapat, 132'i ac, Dene....  RESIM ACMIYORRRRR!!!  SONRASINDA YENI BRANCH (AzureBlob)

        //if (!((studentId >= 0) || (imageId >= 0)))
        //{
        //    return File(Encoding.UTF8.GetBytes(""), "StudentId and ImageId can't be null or empty.", null);
        //}

        ////Account number can not be null or empty.
        //if (string.IsNullOrEmpty(Convert.ToString(Request.Query["accountNumber"])))
        //{
        //    return Content("Account number can not be null or empty.");
        //}

        string contentType = "image/jpg";

        var result = await _service.GetAsId(studentId);
        if (result == null) return File(Encoding.UTF8.GetBytes(""), contentType, null);
        var name = result.ImageStudent.ToList().Find(i => i.ImageId == imageId).ImageName;
        var path = result.ImageStudent.ToList().Find(i => i.ImageId == imageId).Path;
        //string wwwRootPath = _webHostEnvironment.WebRootPath;
        //string path = Path.Combine(wwwRootPath + "\\Image\\", name);

        //export image
        FileInfo fi = new FileInfo($"{path}");
        FileStream fs = fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
        StreamReader sr = new StreamReader(fs);
        string image = sr.ReadToEnd();
        sr.Close();
        fs.Close();
        //var content = Encoding.UTF8.GetBytes(image);
        byte[] content = Encoding.UTF8.GetBytes(image);
        return File(content, contentType, name);
    }

    private async Task<List<StudentImage>> SaveImageAsync(IFormFile uploadedFile)
    {
        //Save image to wwwroot/image
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        string path = Path.Combine(wwwRootPath + "/Image/", uploadedFile.FileName);

        using var stream = new MemoryStream();
        await uploadedFile.CopyToAsync(stream);
        var byteArray = stream.ToArray();

        using (var fs = new FileStream(path, FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs)) { bw.Write(byteArray); };
        }

        var images = new List<StudentImage>();
        var studentImage = new StudentImage { ImageName = uploadedFile.FileName, Path = path };
        images.Add(studentImage);
        return images;
    }
}