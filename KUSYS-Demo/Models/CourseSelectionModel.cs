using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KUSYS_Demo.Models;

public class CourseSelectionModel
{
    public IEnumerable<SelectListItem> StudentList { get; set; }
    public IEnumerable<Course> CourseList{ get; set; }
    public List<int> SelectedCourseList { get; set; }

}

