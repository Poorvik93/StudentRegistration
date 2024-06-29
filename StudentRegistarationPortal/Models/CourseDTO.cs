using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentRegistarationPortal.Models
{
    public class CourseDTO
    {
        [Required(ErrorMessage = "Course name cannot be blank")]
        [DisplayName("COURSE NAME")]
        public string Name { get; set; }

        [MaxLength(2000, ErrorMessage = "Description cannot be more than 2000 letters")]
        [DisplayName("COURSE DESCRIPTION")]
        public string Description { get; set; }

        [Required(ErrorMessage = "No. of days for the given course is missing")]
        [Range(1, 10, ErrorMessage = "No. of days should be greater than 0 and less than 11")]
        [DisplayName("NO OF DAYS")]
        public int NoOfDays { get; set; }

        [Required(ErrorMessage = "Start date of the course is missing")]
        public List<DateTime> Date { get; set; }

        [Required(ErrorMessage = "Start time of the course is missing")]
        public List<DateTime> Time { get; set; }
    }
}