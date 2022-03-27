using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.Models;

public class Enrolment
{
    [Key]
    public int EnrolmentId { get; set; }

    [ForeignKey("CourseId")]
    public int CourseId { get; set; }
    [ForeignKey("StudentId")]
    public int StudentId { get; set; }

    public virtual Course Course { get; set; }
    public virtual Student Student { get; set; }
}

