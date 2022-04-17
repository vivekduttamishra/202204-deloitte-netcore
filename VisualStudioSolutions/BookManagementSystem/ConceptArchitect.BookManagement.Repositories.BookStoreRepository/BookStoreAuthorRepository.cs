using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.BookStoreRepository
{
    public class BookStoreAuthorRepository : IAuthorRepository
    {
        IStoreSerializer serializer;
        BookStore store;

        public BookStoreAuthorRepository(IStoreSerializer serializer)
        {
            store = serializer.Get().Result;
            this.serializer = serializer;
        }

        public async Task Add(Author author)
        {
            await Task.Yield();
            store.Authors[author.Id.ToLower()] = author;
        }

        public async Task<IList<Author>> GetAll()
        {
            await Task.Yield();
            return store.Authors.Values.ToList();
        }

        public async Task<Author> GetById(string id)
        {
            await Task.Yield();
            if (store.Authors.ContainsKey(id))
                return store.Authors[id];
            else
                return null;
        }

        public async Task Remove(string id)
        {
            await Task.Yield();
            if (store.Authors.ContainsKey(id))
                store.Authors.Remove(id);
        }

        public async Task Save()
        {
            await serializer.Save();
        }

        public async Task<IList<Author>> Search(Func<Author, bool> condition)
        {
            await Task.Yield();
            return store.Authors.Values.Where(condition).ToList();
        }
    }
}
