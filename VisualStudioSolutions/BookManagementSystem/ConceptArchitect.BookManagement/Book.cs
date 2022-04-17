using System;
using System.Collections.Generic;

namespace ConceptArchitect.BookManagement
{
    [Serializable]
    public class Book
    {
        public string Isbn { get; set; }


        public string Title { get; set; }

        public Author Author { get; set; }
        public string Cover { get; set; }

        public string Description { get; set; }

        public int  Price { get; set; }

        public double Rating { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

    }
}
