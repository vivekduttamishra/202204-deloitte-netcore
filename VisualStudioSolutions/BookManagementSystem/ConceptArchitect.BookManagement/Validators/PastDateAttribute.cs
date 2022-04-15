using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConceptArchitect.BookManagement.Validators
{
    class PastDateAttribute: ValidationAttribute
    {
        public int Days { get; set; } = 1;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var date = (DateTime)value;
                if (date == null)
                    return ValidationResult.Success;

                var diff = DateTime.Now - date;

                if (diff.Days >= Days)
                    return ValidationResult.Success;
                else
                    return new ValidationResult($"Date must be at least {Days} in Past"); //this is the error message
            }
            catch
            {
                return ValidationResult.Success;
            }
        }
    }
}
