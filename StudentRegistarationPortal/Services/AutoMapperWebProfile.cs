using AutoMapper;
using DataAccessLayer.Entities;
using StudentRegistarationPortal.Models;

namespace StudentRegistarationPortal.Services
{
    public class AutoMapperWebProfile : Profile
    {
        public AutoMapperWebProfile()
        {
            CreateMap<CourseDTO, Course>();
            CreateMap<UserDTO, User>();
            CreateMap<SelectedCourseDTO, SelectedCourse>();

        }
    }
}
