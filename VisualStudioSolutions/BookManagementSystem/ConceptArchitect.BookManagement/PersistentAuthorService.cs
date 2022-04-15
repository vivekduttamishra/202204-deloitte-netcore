using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class PersistentAuthorService : IAuthorService
    {
        IAuthorRepository repository;
        public PersistentAuthorService(IAuthorRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddAuthor(Author author)
        {
            //step1: add the required validation
            if (author == null || string.IsNullOrEmpty(author.Id) || string.IsNullOrEmpty(author.Name))
                throw new ArgumentException("Invalid Author Object");
            var a = GetAuthorById(author.Id);
            if (a != null)
                throw new InvalidOperationException($"Duplicate Author Id: {author.Id}");

            //step2: add book to the repository
            await repository.Add(author);
            await repository.Save(); //perist
        }

        public async Task<IList<Author>> GetAllAuthors()
        {
            return await repository.GetAll();
        }

        public async Task<Author> GetAuthorById(string id)
        {
            return await repository.GetById(id);
        }

        public async Task<IList<Book>> GetBooksByAuthor(string id)
        {
            var author = await GetAuthorById(id);
            if (author != null)
                return author.Books;
            else
                return null;

        }

        public async Task RemoveAuthor(string id)
        {
            await repository.Remove(id);
            await repository.Save();
        }

        public async Task<IList<Author>> Search(string term)
        {
            term = term.ToLower();
            return await repository.Search((Author author) => author.Name.ToLower().Contains(term) ||
                                                              author.Biography.ToLower().Contains(term)
                                                              );
        }

        public async Task UpdateAuthor(Author author)
        {
            var existing =await GetAuthorById(author.Id);
            if(existing!=null)
            {
                //existing.Id = author.Id; //id is not changeable
                //comment out any property that are immutable.
                existing.Name = author.Name;
                existing.Biography = author.Biography;
                existing.Photo = author.Photo;
                existing.Email = author.Email;
                existing.BirthDate = author.BirthDate;
                existing.DeathDate = author.DeathDate;
                await repository.Save();
            }
        }
    }
}
