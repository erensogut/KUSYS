using System;
using KUSYS_Demo.Models;

namespace KUSYS_Demo.Interfaces
{
	public interface IStudentService
	{
		public List<Student> GetStudents();
		public void CreateStudent(Student student);
		public Student FindById(int? id);
		public void EditStudent(Student student);
		public void DeleteStudent(int id);

	}
}

