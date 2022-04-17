using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.BookStoreRepository
{
    public class BookStoreBookRepository : IBookRepository
    {
        IStoreSerializer serializer;
        BookStore store;

        public BookStoreBookRepository(IStoreSerializer serializer)
        {
            store = serializer.Get().Result;
            this.serializer = serializer;
        }
        public async Task Add(Book book)
        {
            await Task.Yield();
            store.Books[book.Isbn.ToLower()] = book;
        }

        public async Task<IList<Book>> GetAll()
        {
            await Task.Yield();
            return store.Books.Values.ToList();
        }

        public async Task<Book> GetById(string id)
        {
            await Task.Yield();
            if (store.Books.ContainsKey(id))
                return store.Books[id];
            else
                return null;
        }

        public async Task Remove(string id)
        {
            await Task.Yield();
            if (store.Books.ContainsKey(id))
                store.Books.Remove(id);
        }

        public async Task Save()
        {
            await serializer.Save();
        }

        public async Task<IList<Book>> Search(Func<Book, bool> condition)
        {
            await Task.Yield();
            return store.Books.Values.Where(condition).ToList();
        }
    }
}
