using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

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
    public IActionResult CourseList()
    {
        List<string> rowlist = new List<string>();
        foreach(var item in _context.Students.Include("Enrolments").Include("Enrolments.Course"))
        {
            string line = item.FirstName + " " + item.LastName;
            int i = 0;
            foreach(var en in item.Enrolments)
            {
                if (i == 0)
                {
                    line = line + "=>" + en.Course.CourseName;
                    i++;
                }
                else {
                    line = line + "," + en.Course.CourseName;
                }
            }
            rowlist.Add(line);
            
        }
        return View(rowlist);
    }
    public IActionResult Select()
    {
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
        var student = _context.Students.Include("Enrolments").Where(x => x.StudentId == ids.First()).First();
        for(var i = 1; i < ids.Count; i++)
        {
            if (_context.Enrolments.Any(x => x.StudentId == student.StudentId && x.CourseId == ids[i])) continue;
            student.Enrolments.Add(new Enrolment { CourseId = ids[i], StudentId = student.StudentId });
        }

        foreach(var item in student.Enrolments.Select(x => x.CourseId))
        {
            if (ids.Contains(item)) continue;
            else
            {
                var enrol = _context.Enrolments.FirstOrDefault(x => x.CourseId == item && x.StudentId == student.StudentId);
                _context.Enrolments.Remove(enrol);
            }
        }
        _context.SaveChanges();
        return RedirectToAction(nameof(Select));
    }
    public JsonResult GetStudentList(int id)
    {
        var courseIdList = _context.Students.Where(x => x.StudentId == id).Select(x => x.Enrolments.Select(x => x.CourseId));
        return Json(courseIdList);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

