using Microsoft.AspNetCore.Mvc;
using SearchService.Models;
using SearchService.Services.Interfacec;

namespace SearchService.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentResponsModel>>> GetAll( int pageNumber,int pageSize )
    {
        var respons = await _studentService.GetAll(pageSize, pageNumber);
     
        return Ok(respons);
    }
}