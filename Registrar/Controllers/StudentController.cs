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

    }
}
