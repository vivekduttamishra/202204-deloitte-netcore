using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConceptArchitect.BookManagement
{
    [Serializable]
    public class Book
    {
        [Key]   //specifiy primary key        
        public string Isbn { get; set; }


        public string Title { get; set; }

        public Author Author { get; set; }
        public string Cover { get; set; }

        public string Description { get; set; }

        public int  Price { get; set; }

        public double Rating { get; set; }

        public string Tags { get; set; }


        public IList<Review> Reviews { get; set; }
    }
}
