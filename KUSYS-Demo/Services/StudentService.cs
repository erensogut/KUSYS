using System;
using KUSYS_Demo.Interfaces;
using KUSYS_Demo.Models;

namespace KUSYS_Demo.Services
{
	public class StudentService: IStudentService
	{
		private readonly StudentSystemContext _context;

		public StudentService(StudentSystemContext context)
		{
			_context = context;

		}

		public List<Student> GetStudents()
        {
			return _context.Students.ToList();
		}
		public void CreateStudent(Student student)
		{
			_context.Students.Add(student);
			_context.SaveChanges();
		}
		public Student FindById(int? id)
        {
            return _context.Students.FirstOrDefault(x=>x.StudentId == id);
		}
		public void EditStudent(Student student)
        {
			_context.Update(student);
			_context.SaveChangesAsync();
		}
		public void DeleteStudent(int id)
        {
			var student =  _context.Students.Find(id);
			_context.Students.Remove(student);
			_context.SaveChangesAsync();
		}
	}
}

