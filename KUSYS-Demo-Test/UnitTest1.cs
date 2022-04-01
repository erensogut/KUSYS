using System.Collections.Generic;
using System.Linq;
using KUSYS_Demo.Controllers;
using KUSYS_Demo.Interfaces;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;


namespace KUSYS_Demo_Test;

public class UnitTest1
{
    [Fact]
    public void TestGetAllStudentSuccess()
    {
        var logger = Mock.Of<ILogger<StudentController>>();
        var studentService = new Mock<IStudentService>();
        Student s = new Student { StudentId=1,FirstName= "First",LastName="Last",BirthDate=System.DateTime.Today };
        List<Student> sList = new List<Student>();
        sList.Add(s);
        studentService.Setup(x=>x.GetStudents()).Returns(sList);
        StudentController sc = new StudentController(logger,studentService.Object);


        var result = sc.Get() as ViewResult;

        Assert.Equal(result.Model, sList);

    }
}
