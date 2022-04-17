using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConceptArchitect.BookManagement.Validators
{
    class AgeAttribute:ValidationAttribute
    {
        public int MinAge { get; set; } = 1;
        public int MaxAge { get; set; } = -1; //No upper limit

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var author = validationContext.ObjectInstance as Author;
            if (author == null)
                return ValidationResult.Success; //He isn't an author


            var ageCalculationLastDate = author.DeathDate.HasValue ? author.DeathDate.Value : DateTime.Now;

            var age = ageCalculationLastDate - author.BirthDate;

            var approxYears =(int)(age.TotalDays / 365);

            if (MinAge > 0 &&  approxYears<MinAge)
                return new ValidationResult($"Min Age should be {MinAge} years. Found {(int)approxYears} years");

            if (MaxAge > 0 && approxYears>MaxAge)
                return new ValidationResult($"Max Age should be {MaxAge} years. Found {(int)approxYears} years");

            return ValidationResult.Success;
        }
    }
}
