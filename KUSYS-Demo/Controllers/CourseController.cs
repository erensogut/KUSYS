using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KUSYS_Demo.Controllers;

public class CourseController : Controller
{
    private readonly ILogger<CourseController> _logger;
    private readonly StudentSystemContext _context;

    public CourseController(ILogger<CourseController> logger, StudentSystemContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Select()
    {
        //Öğrenci isimleri ve ders listesi olan bir selection grubu olmalı
        //Aşağısında selected courses isimli bir alan olmalı
        CourseSelectionModel courseSelectionModel = new CourseSelectionModel();
        courseSelectionModel.StudentList = _context.Students.Select(x=>new SelectListItem {Value=x.StudentId.ToString(),Text=x.FirstName+" "+x.LastName }).ToList();
        courseSelectionModel.CourseList = _context.Courses.ToList();
        return View(courseSelectionModel);
    }
    [HttpPost]
    public IActionResult Select(CourseSelectionModel courseSelectionModel)
    {
        
        return View();
    }
    [HttpPost]
    public IActionResult SaveSelection([FromBody]List<int> ids)
    {
        var student = _context.Students.Where(x => x.StudentId == ids.First()).First();
        List<Enrolment> enrolments = new List<Enrolment>();
        for(var i = 1; i < ids.Count; i++)
        {
            enrolments.Add(new Enrolment { CourseId = ids[i], StudentId = student.StudentId });
        }
        student.Enrolments = enrolments;
        _context.SaveChanges();
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

