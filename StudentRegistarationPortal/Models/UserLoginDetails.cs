using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentRegistarationPortal.Models
{
    public class UserLoginDetails
    {
        [Required(ErrorMessage = "Username field cannot be blank")]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password field cannnot be blank")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters are required for password")]
        public string Password { get; set; }

        [DisplayName("Keep me logged in")]
        public bool RememberMe { get; set; }
    }
}