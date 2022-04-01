using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models;

public class Student
{
    [Key]
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    public virtual ICollection<Enrolment> Enrolments { get; set; }

}

