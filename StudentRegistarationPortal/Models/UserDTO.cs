using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using StudentRegistarationPortal.Services.Validations;

namespace StudentRegistarationPortal.Models
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Username field cannot be blank")]
        [DisplayName("Username")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password field cannnot be blank")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters are required for password")]
        public string Password { get; set; }

        //[Display(Name = "Confirm password")]
        //[Required(ErrorMessage = "Please enter confirm password")]
        //[Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        //[DataType(DataType.Password)]
        //public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email field cannot be blank")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [TermsAcceptanceValidation]
        [DisplayName("I accept the Terms of Use")]
        public bool IsAcceptedTerms { get; set; }
    }
}