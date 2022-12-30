
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using StudentApp.Models;
using StudentApp.Services;

namespace StudentApp.Controllers;

[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[Authorize]
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
    public async Task<ActionResult<Students>> GetStudent()
    {
        var result = await _service.Get();
        return Ok(result);
    }

    [HttpGet("GetAsId/")]
    public async Task<ActionResult<Students>> GetAsId(int id)
    {
        var result = await _service.GetAsId(id);
        if (result == null)
            return NotFound($"Wrong Id {id}");
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AddStudentRequest>> AddStudent(AddStudentRequest addStudentRequest)
    {
        var request = new AddStudentRequest();
        var student = request.ToStudent(addStudentRequest);
        var result = await _service.AddStudent(student);
        return Ok(new AddStudentRequest(result));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<UpdateStudentRequest>> UpdateStudent(int id, UpdateStudentRequest updateStudentRequest)
    {
        var request = new UpdateStudentRequest();
        var student = request.UpdateStudent(updateStudentRequest);
        var result = await _service.UpdateStudent(id, student);

        if (result == null)
            return NotFound($"Wrong Id {id}");
        return Ok(new UpdateStudentRequest(result));
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