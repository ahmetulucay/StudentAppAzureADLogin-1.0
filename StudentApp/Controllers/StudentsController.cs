
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using StudentApp.AzureStorage;
using StudentApp.Configurations;
using StudentApp.Models;
using StudentApp.Services;

namespace StudentApp.Controllers;

//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
//[Authorize]
[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IService _service;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _configuration;
    private readonly string _wwwRootPath;
    private readonly IStorageService _storageService;
    public StudentsController(IStorageService storageService, IConfiguration configuration, IService service, IWebHostEnvironment webHostEnvironment)
    {
        _service = service;
        _webHostEnvironment = webHostEnvironment;
        _configuration = configuration;
        _wwwRootPath = _webHostEnvironment.WebRootPath;
        _storageService = storageService;
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
        var request = new AddStudentRequest();
        var student = request.ToStudent(addStudentRequest);
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
    public async Task<ActionResult> UploadImage(IFormFile uploadedFile, int studentId, int imageId)
    {
        //student id check
        var result = await _service.GetAsId(studentId);
        if (result == null) return NotFound($"Wrong studentId: {studentId}");
        try
        {
            _storageService.Upload(uploadedFile);

            //Save image to wwwroot/image
            string path = Path.Combine(_wwwRootPath + "\\Image\\", uploadedFile.FileName);

            //image id check
            var imageIdContext = result.ImageStudent.ToList().Find(i => i.StudentsId == studentId).ImageId;
            if (imageIdContext != imageId) return NotFound($"Wrong imageId: {imageId}");
            result.ImageStudent.ToList().Find(i => i.ImageId == imageId).ImageName = uploadedFile.FileName;
            result.ImageStudent.ToList().Find(i => i.ImageId == imageId).Path = path;

            //Save FileName and path to Db
            await _service.UpdateStudent(studentId, result);

            /*
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
            */
            return Ok(new StudentResponse(result));
        }
        catch (Exception ex)
        {
            return NotFound("false: uploading image is not successful");
        }
    }

    [HttpGet("ExportImage/")]
    public async Task<ActionResult> ExportImage(int studentId, int imageId) 
    {
        string contentType = "image/jpg";

        //student id check
        var result = await _service.GetAsId(studentId);
        if(result == null) return NotFound($"studentId: {studentId}, content-type: {contentType}");
        
        try
        {
            //image id check
            var imageIdContext = result.ImageStudent.ToList().Find(i => i.StudentsId == studentId).ImageId;
            if (imageIdContext != imageId)
            {          
                if (_configuration.Get<AppConfig>().AppSettings.IsFakeValue)
                {
                    return File(System.IO.File.OpenRead(Path.Combine(_wwwRootPath + "\\Image\\", "1n.jpg")), "image/jpeg");
                }
                return BadRequest($"Wrong imageId: {imageId}");
            }
            var name = result.ImageStudent.ToList().Find(i => i.ImageId == imageId).ImageName;
            var path = result.ImageStudent.ToList().Find(i => i.ImageId == imageId).Path;

            //display image in swagger screen
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, "image/jpeg");
        }
        catch (Exception ex)
        {
            return NotFound("false: exporting image is not successful");
        }
    }
}