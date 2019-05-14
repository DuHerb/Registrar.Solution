using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Registrar.Models;

namespace Registrar.Controllers
{
    public class MajorController : Controller
    {
        public IActionResult Index()
        {
            List<Major> allMajors = Major.GetAll();
            return View(allMajors);
        }

        public IActionResult New()
        {
          return View();
        }

        [HttpPost("/major/create")]
        public IActionResult Create(string majorName)
        {
          Major newMajor = new Major(majorName);
          newMajor.Save();
          return RedirectToAction("Index");
        }

        [HttpGet("/major/{majorId}")]
        public IActionResult Show(int majorId)
        {
          Major major = Major.Find(majorId);
          List<Course> allCourses = Course.GetAll();
          List<Course> majorCourses = Major.GetCourses(majorId);
          Dictionary<string, object> dictionary = new Dictionary<string, object>{};
          dictionary.Add("major", major);
          dictionary.Add("courses", allCourses);
          dictionary.Add("majorCourses", majorCourses);
          return View(dictionary);
        }

        [HttpPost("/major/{majorId}/addCourse")]
        public IActionResult AssignCourse(int majorId, string courseId)
        {
          Major.AssignCourse(majorId, int.Parse(courseId));
          return RedirectToAction("Show", new{majorId = majorId});
        }
    }
}
