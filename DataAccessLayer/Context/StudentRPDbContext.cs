using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class StudentRPDbContext : DbContext
    {
        public StudentRPDbContext() : base("name=StudentRPDbConnectionString")
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> Roles { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<SelectedCourse> SelectedCourses { get; set; }


    }
}
