using System;
using KUSYS_Demo.Models;

namespace KUSYS_Demo
{
	public class StudentRepository
	{
		private StudentSystemContext context;

		public StudentRepository(StudentSystemContext context)
		{
			this.context = context;
		}
		public IEnumerable<Student> GetStudents()
		{
			return context.Students.ToList();
		}
		public void InsertStudent(Student student)
		{
			context.Students.Add(student);
		}
	}

}

