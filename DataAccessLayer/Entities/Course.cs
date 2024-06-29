using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public int NoOfDays { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

    }
}
