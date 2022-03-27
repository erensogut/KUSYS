using System;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Models
{
	public static class SeedData
	{
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var context = new StudentSystemContext(
                    serviceProvider.GetRequiredService<
                        DbContextOptions<StudentSystemContext>>()))
                {
                //Student seeding
                    if (context == null || context.Students == null)
                    {
                        throw new ArgumentNullException("Null RazorPagesMovieContext");
                    }

                    // Look for any movies.
                    if (!context.Students.Any())
                    {
                        context.Students.AddRange(
                            new Student
                            {
                                StudentId = 12,
                                FirstName = "Eren",
                                LastName = "Sögüt",
                                BirthDate = DateTime.Now
                            }
                        );
                    }
                if (context == null || context.Courses == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.
                if (!context.Courses.Any())
                {
                    context.Courses.AddRange(
                        new Course
                        {
                            CourseName = "CS101"
                        },
                        new Course
                        {
                            CourseName = "CS150"
                        }, new Course
                        {
                            CourseName = "CS201"
                        }, new Course
                        {
                            CourseName = "CS202"
                        }
                    );
                }          
                context.SaveChanges();             
            }
            }
	}
}

