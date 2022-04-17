using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConceptArchitect.BookManagement.Validators
{
    public class WordCountAttribute: ValidationAttribute
    {
        public int MinWordCount { get; set; } = -1; //no limit
        public int MaxWordCount { get; set; } = -1; //no limit

       

        public override bool IsValid(object value)
        {
            if(string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = $"Invalid Word Count. Should be: ";
                if (MinWordCount > 0)
                    ErrorMessage += $"Min: {MinWordCount}";
                if (MaxWordCount > 0)
                    ErrorMessage += $"\tMax: {MaxWordCount}";
            }
            
            var str = value as string;
            if (string.IsNullOrEmpty(str))
                return true;

            var wordCount = str.Split().Length;

            if (MinWordCount > 0 && wordCount < MinWordCount)
                return false;

            if (MaxWordCount > 0 && wordCount > MaxWordCount)
                return false;

            return true;


        }
    }
}
