
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
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

    public StudentsController(IService service)
    {
        _service = service;
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
        return Ok($"True: Deleting Id {id} is successful");
    }
}