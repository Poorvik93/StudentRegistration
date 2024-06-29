using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StudentRPDbContext _dbContext;

        public CourseRepository()
        {
            _dbContext = new StudentRPDbContext();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _dbContext.Courses.ToList();
        }

        public Course GetCourseById(int id)
        {
            return _dbContext.Courses.Find(id);
        }

        public void AddCourse(Course course)
        {
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            _dbContext.Entry(course).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            var course = _dbContext.Courses.Find(id);
            if (course != null)
            {
                _dbContext.Courses.Remove(course);
                _dbContext.SaveChanges();
            }
        }
    }
}
