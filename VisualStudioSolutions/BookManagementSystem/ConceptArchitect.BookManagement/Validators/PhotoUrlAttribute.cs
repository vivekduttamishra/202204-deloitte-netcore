using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConceptArchitect.BookManagement.Validators
{
    public class PhotoUrlAttribute : ValidationAttribute
    {
        public string Extensions { get; set; } = ".jpg,.png";

        public override bool IsValid(object value)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = "Invalid Photo Url"; //will be displayed only if I return false;

            var url = value as string;

            if (string.IsNullOrEmpty(url))
                return true;  //If it is empty it's not invalid and it's not my job to check for required

            var i = url.LastIndexOf(".");
            if (i < 1)
                return false;
            var extension = url.Substring(i).ToLower();


            return Extensions.Contains(extension);

        }
    }
}
