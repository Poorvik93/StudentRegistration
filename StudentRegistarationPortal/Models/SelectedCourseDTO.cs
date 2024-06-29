using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentRegistarationPortal.Models
{
    public class SelectedCourseDTO
    {
        [Required(ErrorMessage = "Start date of the course is missing")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Start time of the course is missing")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
    }
}