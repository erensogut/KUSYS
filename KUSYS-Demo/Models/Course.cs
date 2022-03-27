using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models;

public class Course
{
    [Key]
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public virtual ICollection<Enrolment> Enrolments { get; set; }

}

