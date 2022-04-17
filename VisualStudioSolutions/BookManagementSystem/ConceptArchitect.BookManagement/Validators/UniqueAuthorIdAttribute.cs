using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Validators
{
    public class UniqueAuthorIdAttribute:ValidationAttribute
    {
     
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id = value.ToString();

            var authorService = validationContext.GetService(typeof(IAuthorService)) as IAuthorService;
            if (authorService == null)
                throw new NotImplementedException("IAuthorService not configured");

            var author = authorService.GetAuthorById(id).Result; //let operation complete synchronously
            if (author != null)
                return new ValidationResult($"{id} already associated with {author.Name}");
            else
                return ValidationResult.Success;

        }
    }
}
