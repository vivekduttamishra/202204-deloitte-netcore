using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConceptArchitect.BookManagement
{
    public class Review
    {
        //[Key] --->default
        public int Id { get; set; }

        public User Reviewer { get; set; }
        public Book Book { get; set; }

        public string ReviewText { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }
    }
}
