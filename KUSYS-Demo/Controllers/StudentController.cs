using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KUSYS_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Controllers;

public class StudentController : Controller
{
    private readonly ILogger<StudentController> _logger;
    private readonly StudentSystemContext _context;



    public StudentController(ILogger<StudentController> logger, StudentSystemContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Get()
    {
        var studentList = _context.Students.ToList();
        return View(studentList);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Student model)
    {
        _context.Students.Add(model);
        _context.SaveChanges();
        return RedirectToAction(nameof(Get));
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Students.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,BirthDate")] Student student)
    {
        if (id != student.StudentId)
        {
            return NotFound();
        }

        
        try
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Students.Any(x=>x.StudentId == student.StudentId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Get));
        
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(m => m.StudentId == id);
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
        var student = await _context.Students.FindAsync(id);
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Get));
    }
    public IActionResult Privacy()
    {
        return View();
    }
    public ActionResult DetailPartial(int? id)
    {
        var model = _context.Students.FirstOrDefault(x=>x.StudentId==id);
       
        return PartialView("_DetailPartial",model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

