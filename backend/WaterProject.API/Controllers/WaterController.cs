using System.Net;
using Microsoft.AspNetCore.Mvc;
using WaterProject.API.Data;

namespace WaterProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WaterController : ControllerBase
{
    private WaterDbContext _context;

    public WaterController(WaterDbContext temp) => _context = temp; //using lambda function instead of normal way

    [HttpGet("AllProjects")]
    public IEnumerable<Project> GetAllProjects()
    {
        var result = _context.Projects.ToList();
        return result;
    }

    [HttpGet("FunctionalProjects")]
    public IEnumerable<Project> GetFunctionalProjects()
    {
        var result = _context.Projects.Where(p => p.ProjectFunctionalityStatus == "Functional").ToList();
        return result;
    }
}