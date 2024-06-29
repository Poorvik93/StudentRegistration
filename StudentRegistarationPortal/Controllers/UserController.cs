using AutoMapper;
using BusinessLogicLayer.Repository;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using StudentRegistarationPortal.Constant;
using StudentRegistarationPortal.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly StudentRPDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, StudentRPDbContext dbContext, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public ActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public ActionResult SignUpUser(UserDTO user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool userAlreadyExists = _userRepository.GetUserByUsername(user.UserName) != null;

                if (userAlreadyExists)
                {
                    ModelState.AddModelError(string.Empty, "User Already Exists!");
                    return View("SignUp");
                }

                var newUser = new User();
                AutoMapper.Mapper.Map(user, newUser);
                _userRepository.AddUser(newUser);

                //Seeded admin user by using migration and any new signup from now on results in user role of student.
                var newUserRole = new UserRole()
                {
                    UserId = newUser.Id,
                    RoleName = UserRoles.Student,
                };

                _dbContext.Roles.Add(newUserRole);
                _dbContext.SaveChanges();

                return RedirectToAction("Login");
            }

            return View("SignUp");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("SignUp");
        }
    }

    public ActionResult LogIn()
    {
        return View();
    }   

    [HttpPost]
    public ActionResult LogInUser(UserLoginDetails loginDetails)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var validUser = _userRepository.GetUserByUsername(loginDetails.UserName);

                if (validUser != null && validUser.Password == loginDetails.Password)
                {
                    FormsAuthentication.SetAuthCookie(loginDetails.UserName, true);

                    var userRoles = _dbContext.Roles.Where(r => r.UserId == validUser.Id).Select(r => r.RoleName).ToList();

                    
                    if (userRoles.Contains(UserRoles.Admin))
                        return RedirectToAction("Courses", "Admin"); 
                    else if (userRoles.Contains(UserRoles.Student))
                        return RedirectToAction("AllCourses", "Student"); 

                }

                ModelState.AddModelError(string.Empty, "Invalid Username or Password");
            }

            return View("LogIn");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("LogIn");
        }
    }


    public ActionResult LogOut()
    {
        FormsAuthentication.SignOut();
        return RedirectToAction("LogIn");
    }
}
