using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KUSYS_Demo.Models;
using Microsoft.EntityFrameworkCore;
using KUSYS_Demo.Interfaces;

namespace KUSYS_Demo.Controllers;

public class StudentController : Controller
{
    private readonly ILogger<StudentController> _logger;
    private readonly IStudentService _service;


    public StudentController(ILogger<StudentController> logger, IStudentService service)
    {
        _logger = logger;
        _service = service;
    }

    public IActionResult Get()
    {
        var studentList = _service.GetStudents();
        return View(studentList);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Student model)
    {
        _service.CreateStudent(model);
        return RedirectToAction(nameof(Get));
    }
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = _service.FindById(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("StudentId,FirstName,LastName,BirthDate")] Student student)
    {
        if (id != student.StudentId)
        {
            return NotFound();
        }

        _service.EditStudent(student);
        
       
        return RedirectToAction(nameof(Get));
        
    }
    public  IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = _service.FindById(id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _service.DeleteStudent(id);
        return RedirectToAction(nameof(Get));
    }
    public IActionResult Privacy()
    {
        return View();
    }
    public ActionResult DetailPartial(int? id)
    {
        var model = _service.FindById(id);
       
        return PartialView("_DetailPartial",model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

