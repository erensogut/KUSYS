using System;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Models
{
	

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext(DbContextOptions<StudentSystemContext> options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrolment> Enrolments { get; set; }

    }

}

