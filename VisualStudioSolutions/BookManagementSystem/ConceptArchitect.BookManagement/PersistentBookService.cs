using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class PersistentBookService : IBookService
    {
        IBookRepository repository;
        public PersistentBookService(IBookRepository repository)
        {
            this.repository = repository;
        }
        public async Task AddBook(Book book)
        {
            if (book == null || string.IsNullOrEmpty(book.Isbn) || string.IsNullOrEmpty(book.Title))
                throw new ArgumentException("Invalid Book Object");
            var a = await GetBookById(book.Isbn);
            if (a != null)
                throw new InvalidOperationException($"Duplicate Book Id: {book.Isbn}");

            //step2: add book to the repository
            await repository.Add(book);
            await repository.Save(); //perist
        }

        public async Task<IList<Book>> GetAllBooks()
        {
            return await repository.GetAll();
        }

        public async Task<Book> GetBookById(string id)
        {
            return await repository.GetById(id);
        }

        public async Task RemoveBook(string id)
        {
            await repository.Remove(id);
            await repository.Save();
         }

        public async Task<IList<Book>> Search(string term)
        {
            term = term.ToLower();
            return await repository.Search((Book book) => book.Title.ToLower().Contains(term) ||
                                                              book.Description.ToLower().Contains(term)
                                                              );
        }

        public async Task UpdateBook(Book book)
        {
            var existing = await GetBookById(book.Isbn);
            if (existing != null)
            {
                
                existing.Title = book.Title;
                existing.Cover = book.Cover;
                existing.Description = book.Description;
                existing.Price = book.Price;
                existing.Tags = book.Tags;
                existing.Rating = book.Rating;
                await repository.Save();
            }
        }
    }
}
