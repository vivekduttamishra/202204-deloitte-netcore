using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EFRepository
{
    public class EFAuthorRepository : IAuthorRepository
    {
        BMSContext context;
        public EFAuthorRepository(BMSContext context)
        {
            this.context = context;
        }
        public async Task Add(Author author)
        {
            await context.Authors.AddAsync(author);
        }

        public async  Task<IList<Author>> GetAll()
        {
            await Task.Yield();
            return context.Authors.ToList();
        }

        public async Task<Author> GetById(string id)
        {
            await Task.Yield();
            return  (     from author in context.Authors
                          where author.Id == id
                          select author).FirstOrDefault();
        }

        public async Task Remove(string id)
        {
            var author = await GetById(id);
            if (author != null)
                context.Authors.Remove(author);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();//all insert/update/delete happens here
        }

        public async Task<IList<Author>> Search(Func<Author, bool> condition)
        {
            await Task.Yield();
            return (from author in context.Authors
                    where condition(author)
                    select author).ToList();
        }
    }
}
