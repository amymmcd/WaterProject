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
    public IActionResult GetAllProjects(int pageSize = 5, int pageNum = 1, [FromQuery] List<string>? projectTypes = null) //because you are returning an object instead of a list of projects, use IActionResult instead of IEnumerable
    {
        var query = _context.Projects.AsQueryable();
        
        if (projectTypes != null && projectTypes.Any())
        {
            query=query.Where(p => projectTypes.Contains(p.ProjectType));
        }
        
        var projectCount = query.Count();
        
        var result = query
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        

        var someObject = new //or you could create a class and then create an instance of that class. or you could build the object directly in the return statement (not ideal). 
        {
            Projects = result,
            ProjectCount = projectCount
        };

        return Ok(someObject); //okay returns http 200 and converts it to json
    }

    [HttpGet("GetProjectTypes")]
    public IActionResult GetProjectTypes()
    {
        var projectTypes = _context.Projects
            .Select(p => p.ProjectType)
            .Distinct()
            .ToList();
        
        return Ok(projectTypes);
    }

}