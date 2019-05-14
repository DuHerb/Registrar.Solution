using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Registrar.Models;

namespace Registrar.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            List<Course> allCourses = Course.GetAll();
            return View(allCourses);
        }

        public IActionResult New()
        {
          return View();
        }

        [HttpPost("/course/create")]
        public IActionResult Create(string courseName, string courseNum)
        {
          Course newCourse = new Course(courseName, courseNum);
          newCourse.Save();
          return RedirectToAction("Index");
        }

    }
}
