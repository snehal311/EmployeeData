using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeData.Models
{
    public class DateOfBirthAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date <= DateTime.Today)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return new ValidationResult("Invalid date value.");
        }
    }
}