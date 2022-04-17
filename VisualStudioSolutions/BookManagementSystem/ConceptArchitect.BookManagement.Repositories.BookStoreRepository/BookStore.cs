using System;
using System.Collections.Generic;

namespace ConceptArchitect.BookManagement.Repositories.BookStoreRepository
{
    [Serializable]
    public class BookStore
    {
        public Dictionary<string, Book> Books { get; set; } = new Dictionary<string, Book>();
        public Dictionary<string, Author> Authors { get; set; } = new Dictionary<string, Author>();


    }
}
