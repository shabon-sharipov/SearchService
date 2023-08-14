using Microsoft.AspNetCore.Mvc;
using SearchService.Models;
using SearchService.Services.Interfacec;

namespace SearchService.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private IStudentService _studentService;
    private ILogger<StudentController> _logger;

    public StudentController(IStudentService studentService, ILogger<StudentController> logger)
    {
        _studentService = studentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentResponsModel>>> GetAll(int pageNumber, int pageSize)
    {
        try
        {
            var respons = await _studentService.GetAll(pageSize, pageNumber);

            return Ok(respons);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    [HttpGet("Search")]
    public async Task<ActionResult<List<StudentResponsModel>>> Searsh(string searchSymbol,
        CancellationToken cancellationToken)
    {
        var respons = await _studentService.Search(searchSymbol, cancellationToken);

        return Ok(respons);
    }
}