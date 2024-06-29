using AutoMapper;
using BusinessLogicLayer.Repository;
using DataAccessLayer.Entities;
using StudentRegistarationPortal.Constant;
using StudentRegistarationPortal.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace StudentRegistarationPortal.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public AdminController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public ActionResult Courses()
        {
            var courses = _courseRepository.GetAllCourses();

            ViewBag.CourseList = courses;

            return View();
        }

        public JsonResult CoursesList()
        {
            try
            {
                var courses = _courseRepository.GetAllCourses();
                return Json(courses, JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { ErrorMessage = "Failed to fetch courses." });
            }
        }


        //Ajax call
//        public ActionResult CoursesList()
//{
//    // Fetch data from your repository or database
//    var courses = _courseRepository.GetAllCourses();

//    // Format data as JSON expected by DataTables
//    var jsonData = courses.Select(c => new
//    {
//        Name = c.Name,
//        Description = c.Description,
//        NoOfDays = c.NoOfDays,
//        Time = c.Time
//    });

//    return Json(new { data = jsonData });
//}


        public ActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCourse(CourseDTO courseDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool courseDateClash = false;

                    if (courseDetails.Date.Count >= 0)
                    {
                        if (courseDetails.Date.Count != courseDetails.Date.Distinct().Count())
                        {
                            courseDateClash = true;
                        }
                    }

                    if (!courseDateClash)
                    {
                        var courseDateAndTime = courseDetails.Date.Zip(courseDetails.Time, (date, time) => new { Date = date, Time = time });

                        foreach (var courseDateTime in courseDateAndTime)
                        {
                            var course = new Course();
                            Mapper.Map(courseDetails, course);
                            course.Date = courseDateTime.Date;
                            course.Time = courseDateTime.Time;

                            _courseRepository.AddCourse(course);
                        }

                        return RedirectToAction("Courses");
                    }

                    ModelState.AddModelError(string.Empty, "Course dates cannot be the same.");
                }

                return View("CreateCourse");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("CreateCourse");
            }
        }

        public ActionResult EditCourse(int id)
        {
            try
            {
                var courseDetails = _courseRepository.GetCourseById(id);

                if (courseDetails != null)
                {
                    ViewBag.CourseDetails = courseDetails; 
                    return View(courseDetails);
                }

                ModelState.AddModelError(string.Empty, "Course Not Found!");
                return RedirectToAction("Courses");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Courses");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCourse(int id, CourseDTO courseDetails)
        {
            var validCourse = _courseRepository.GetCourseById(id);

            try
            {
                if (validCourse != null && ModelState.IsValid)
                {
                    Mapper.Map(courseDetails, validCourse);
                    validCourse.Date = courseDetails.Date.First();
                    validCourse.Time = courseDetails.Time.First();

                    _courseRepository.UpdateCourse(validCourse);
                    return RedirectToAction("Courses");
                }

                return View("EditCourse");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("EditCourse");
            }
        }

        public ActionResult DeleteCourse(int id)
        {
            try
            {
                _courseRepository.DeleteCourse(id);
                return RedirectToAction("Courses");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Courses");
            }
        }
    }
}
