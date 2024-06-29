using BusinessLogicLayer.Repository;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using StudentRegistarationPortal.Constant;
using StudentRegistarationPortal.Models;
using System;
using System.Linq;
using System.Web.Mvc;

[Authorize(Roles = UserRoles.Student)]
public class StudentController : Controller
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;
    private readonly StudentRPDbContext _dbContext;

    public StudentController(ICourseRepository courseRepository, IUserRepository userRepository, StudentRPDbContext dbContext)
    {
        _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ActionResult AllCourses()
    {
        try
        {
            ViewBag.CourseList = _courseRepository.GetAllCourses().Where(course => course.Date >= DateTime.Now).ToList();
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    public ActionResult SelectedCourses()
    {
        try
        {
            var validUser = _userRepository.GetUserByUsername(User.Identity.Name);

            if (validUser != null)
            {
                var validUserId = validUser.Id;
                ViewBag.SelectedCourses = _dbContext.SelectedCourses.Where(course => course.UserId == validUserId).ToList();
            }
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddCourse(int courseId, SelectedCourseDTO selectedCourseDetails)
    {
        try
        {
            var validCourse = _courseRepository.GetCourseById(courseId);
            var validUser = _userRepository.GetUserByUsername(User.Identity.Name);

            if (validCourse != null && validUser != null)
            {
                var validUserId = validUser.Id;

                var userHasCourse = _dbContext.SelectedCourses.Any(course => course.CourseId == courseId && course.UserId == validUserId);

                if (!userHasCourse)
                {
                    var minusOneHour = selectedCourseDetails.Time - selectedCourseDetails.Time.AddHours(1);
                    var plusOneHour = selectedCourseDetails.Time.AddHours(1) - selectedCourseDetails.Time;

                    var userSelectedCourseTimeClash = _dbContext.SelectedCourses.Any(course =>
                        course.UserId == validUserId &&
                        selectedCourseDetails.FromDate == course.FromDate);

                    if (!userSelectedCourseTimeClash)
                    {
                        if (selectedCourseDetails.FromDate < validCourse.Date.AddDays(validCourse.NoOfDays))
                        {
                            var newSelectedCourse = new SelectedCourse
                            {
                                UserId = validUserId,
                                CourseId = courseId,
                                ToDate = selectedCourseDetails.FromDate.AddDays(validCourse.NoOfDays)
                            };
                            AutoMapper.Mapper.Map(selectedCourseDetails, newSelectedCourse);

                            _dbContext.SelectedCourses.Add(newSelectedCourse);
                            _dbContext.SaveChanges();

                            return RedirectToAction("SelectedCourses");
                        }
                        ModelState.AddModelError(string.Empty, "Selected date is not qualified to complete the course.");
                        ViewBag.CourseList = _courseRepository.GetAllCourses().Where(course => course.Date >= DateTime.Now).ToList();
                        return View("AllCourses");
                    }
                    ModelState.AddModelError(string.Empty, "Selected date clashes with existing courses");
                    ViewBag.CourseList = _courseRepository.GetAllCourses().Where(course => course.Date >= DateTime.Now).ToList();
                    return View("AllCourses");

                }
                ModelState.AddModelError(string.Empty, "Course already exists");

            }
            ViewBag.CourseList = _courseRepository.GetAllCourses().Where(course => course.Date >= DateTime.Now).ToList();
            return View("AllCourses");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            ViewBag.CourseList = _courseRepository.GetAllCourses().ToList();
            return View("Courses");
        }
    }

    [HttpPost]
    //public ActionResult AddCourse(int courseId, SelectedCourseDTO selectedCourseDetails)
    //{
    //    try
    //    {
    //        var validCourse = _courseRepository.GetCourseById(courseId);
    //        var validUser = _userRepository.GetUserByUsername(User.Identity.Name);

    //        if (validCourse != null && validUser != null)
    //        {
    //            var validUserId = validUser.Id;

    //            // Check if the user has already selected the same course on any date
    //            var userSelectedCourse = _dbContext.SelectedCourses
    //                .FirstOrDefault(course => course.CourseId == courseId && course.UserId == validUserId);

    //            if (userSelectedCourse != null)
    //            {
    //                ModelState.AddModelError(string.Empty, "Course already selected");
    //                ViewBag.CourseList = _courseRepository.GetAllCourses().Where(course => course.Date >= DateTime.Now).ToList();
    //                return View("AllCourses");
    //            }

    //            // Check if the user has already selected any course on the same FromDate
    //            var userSelectedCoursesOnSameDate = _dbContext.SelectedCourses
    //                .Where(course => course.UserId == validUserId && course.FromDate == selectedCourseDetails.FromDate)
    //                .ToList();

    //            // Allow multiple course selections on the same date
    //            var courseAlreadySelected = userSelectedCoursesOnSameDate.Any(course =>
    //                course.CourseId == courseId);

    //            if (courseAlreadySelected)
    //            {
    //                ModelState.AddModelError(string.Empty, "Course already selected on the same date");
    //                ViewBag.CourseList = _courseRepository.GetAllCourses().Where(course => course.Date >= DateTime.Now).ToList();
    //                return View("AllCourses");
    //            }

    //            var courseStartDate = validCourse.Date;
    //            var courseEndDate = courseStartDate.AddDays(validCourse.NoOfDays);

    //            // Check if selected date and time is valid for the course
    //            if (selectedCourseDetails.FromDate < courseStartDate || selectedCourseDetails.FromDate > courseEndDate)
    //            {
    //                ModelState.AddModelError(string.Empty, "Selected date is not within the course duration");
    //                ViewBag.CourseList = _courseRepository.GetAllCourses().Where(course => course.Date >= DateTime.Now).ToList();
    //                return View("AllCourses");
    //            }

    //            var newSelectedCourse = new SelectedCourse
    //            {
    //                UserId = validUserId,
    //                CourseId = courseId,
    //                FromDate = selectedCourseDetails.FromDate,
    //                Time = selectedCourseDetails.Time,
    //                ToDate = selectedCourseDetails.FromDate.AddDays(validCourse.NoOfDays)
    //            };
    //            AutoMapper.Mapper.Map(selectedCourseDetails, newSelectedCourse);

    //            _dbContext.SelectedCourses.Add(newSelectedCourse);
    //            _dbContext.SaveChanges();

    //            return RedirectToAction("SelectedCourses");
    //        }

    //        ModelState.AddModelError(string.Empty, "Invalid Course");
    //        ViewBag.CourseList = _courseRepository.GetAllCourses().Where(course => course.Date >= DateTime.Now).ToList();
    //        return View("AllCourses");
    //    }
    //    catch (Exception ex)
    //    {
    //        ModelState.AddModelError(string.Empty, ex.Message);
    //        ViewBag.CourseList = _courseRepository.GetAllCourses().ToList();
    //        return View("Courses");
    //    }
    //}

    public ActionResult UnregisterCourse(int courseId)
    {
        try
        {
            var validUser = _userRepository.GetUserByUsername(User.Identity.Name);

            if (validUser != null)
            {
                var validUserId = validUser.Id;

                var validSelectedCourse = _dbContext.SelectedCourses.Single(selectedCourse => selectedCourse.CourseId == courseId && selectedCourse.UserId == validUserId);

                if (validSelectedCourse != null)
                {
                    _dbContext.SelectedCourses.Remove(validSelectedCourse);
                    _dbContext.SaveChanges();
                }
                ModelState.AddModelError(string.Empty, "Not a valid course.");
            }
            ModelState.AddModelError(string.Empty, "Not a valid User.");

            return RedirectToAction("SelectedCourses");

        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("SelectedCourses");
        }
    }
}
