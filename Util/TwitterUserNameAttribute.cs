using BencomWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BencomWebApp.Util
{
    public class TwitterUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            string userName = (string)value;
            Regex r = new Regex("^[a-zA-Z0-9_]+$"); // Checks for an alphanumeric expression with underscores and a length of at least 1.
            if (r.IsMatch(userName) && userName.Length <= Constants.MaxLengthTwitterUserName)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"{userName} is not a valid Twitter username");
            }
        }
    }
}
