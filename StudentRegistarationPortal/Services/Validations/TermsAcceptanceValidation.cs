using StudentRegistarationPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentRegistarationPortal.Services.Validations
{
    public class TermsAcceptanceValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (UserDTO)validationContext.ObjectInstance;
            if (user.IsAcceptedTerms == true)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("You should accept our Terms and Conditions to continue");
        }
    }
}