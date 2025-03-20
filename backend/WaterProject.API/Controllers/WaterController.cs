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
    public IActionResult GetAllProjects(int pageSize = 5, int pageNum = 1) //because you are returning an object instead of a list of projects, use IActionResult instead of IEnumerable
    {
        string? favProjType = Request.Cookies["FavoriteProjectType"];
        Console.WriteLine("~~~~COOKIE~~~~\n" + favProjType);
        
        HttpContext.Response.Cookies.Append("FavoriteProjectType", "Borehole Well and Hand Pump", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddMinutes(1)
        });
        
        var result = _context.Projects
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        var projectCount = _context.Projects.Count();

        var someObject = new //or you could create a class and then create an instance of that class. or you could build the object directly in the return statement (not ideal). 
        {
            Projects = result,
            ProjectCount = projectCount
        };

        return Ok(someObject); //okay returns http 200 and converts it to json
    }

    [HttpGet("FunctionalProjects")]
    public IEnumerable<Project> GetFunctionalProjects()
    {
        var result = _context.Projects.Where(p => p.ProjectFunctionalityStatus == "Functional").ToList();
        return result;
    }
}