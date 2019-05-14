using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Registrar.Models;

namespace Registrar.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            List<Student> allStudents = Student.GetAll();
            // List<Student> allStudents = new List<Student> {};
            // Student newStudent = new Student("studentName", "enrollmentDate", 23);
            // allStudents.Add(newStudent);
            return View(allStudents);
        }

        public IActionResult New()
        {
          return View();
        }

        [HttpPost("/student/create")]
        public IActionResult Create(string studentName, string enrollmentDate)
        {
          Student newStudent = new Student(studentName, enrollmentDate);
          newStudent.Save();
          return RedirectToAction("Index");
        }

        [HttpGet("/student/{studentId}")]
        public IActionResult Show(int studentId)
        {
          Student student = Student.Find(studentId);
          List<Course> allCourses = Course.GetAll();
          List<Course> studentCourses = Student.GetCourses(studentId);
          Dictionary<string, object> dictionary = new Dictionary<string, object>{};
          dictionary.Add("student", student);
          dictionary.Add("courses", allCourses);
          dictionary.Add("studentCourses", studentCourses);
          return View(dictionary);
        }

        [HttpPost("/student/{studentId}/addCourse")]
        public IActionResult AssignCourse(int studentId, string courseId)
        {
          Student.AssignCourse(studentId, int.Parse(courseId));
          return RedirectToAction("Show", new{studentId = studentId});
        }

    }
}
