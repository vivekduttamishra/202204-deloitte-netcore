using System;
using System.Collections.Generic;
using System.Text;

namespace ConceptSathish.BookManagement
{
    public class Author
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Biography { get; set; }
        public IList<Book> Books { get; set; } = new List<Book>();
    }
}
