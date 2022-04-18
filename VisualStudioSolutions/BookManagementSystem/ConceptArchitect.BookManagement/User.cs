using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConceptArchitect.BookManagement
{
    public class User
    {
        [Key]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)] //helps is generating ui
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<Review> Reviews { get; set; }
    }
}
