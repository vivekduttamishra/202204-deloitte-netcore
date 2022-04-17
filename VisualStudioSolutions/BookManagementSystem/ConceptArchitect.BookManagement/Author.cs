using ConceptArchitect.BookManagement.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConceptArchitect.BookManagement
{
    [Age(MinAge =10, MaxAge =120)]
    [Serializable]
    public class Author
    {
        [Required]
        [UniqueAuthorId]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        
        [Required]
       // [PhotoUrl]
        public string Photo { get; set; }

        [Required]
        [StringLength(2000,MinimumLength=10,ErrorMessage ="Bigraphy must be 10-2000 chars")]
        [WordCount(MinWordCount =10)]
        public string Biography { get; set; }

        [DataType(DataType.Date)]
        [PastDate(Days = 3652)] //should be at least 10 years in past
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        [PastDate(Days =0)]
        public DateTime? DeathDate { get; set; }
        
        public IList<Book> Books { get; set; } = new List<Book>();
    }
}