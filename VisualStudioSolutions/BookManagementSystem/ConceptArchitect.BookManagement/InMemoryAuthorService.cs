using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class InMemoryAuthorService : IAuthorService
    {
        Dictionary<string, Author> authors;
        public InMemoryAuthorService()
        {
            Author[] _authors =
            {
                new Author()
                {
                    Name="Vivek Dutta Mishra",
                    Photo="https://avatars.githubusercontent.com/u/9464908?v=4",
                    Biography="Author of Amazon #1 Best seller The Accursed God"
                },

                new Author()
                {
                    Name="Alexandre Dumas",
                    Photo="https://biographygist.com/wp-content/uploads/2021/05/Alexandre-Dumas1.jpg",
                    Biography="One of the altime greatest author of English and French"
                },
                new Author()
                {
                    Name="Conan Doyle",
                    Photo="https://cdn.vocab.com/units/fsyoq26b/author.jpg?width=400&v=16d64ff4cf4",
                    Biography="The creator of famous Sherlock Holmes"
                },
                new Author()
                {
                    Name="Jeffrey Archer",
                    Photo="https://pbs.twimg.com/media/FIgReE4WUAEw9cL.jpg",
                    Biography ="One of the contemporary best seller author in English Fiction"
                },

                new Author()
                {
                    Name="John Grisham",
                    Photo="https://res.cloudinary.com/bookbub/image/upload/w_400,h_400,c_fill,g_face/v1580317832/john-grisham.jpg",
                    Biography="Best seller author Legal Fiction"
                }

            };

            authors = new Dictionary<string, Author>();
            var task = AddAll(_authors);

        }

        private async Task AddAll(Author[] _authors)
        {
            foreach (var author in _authors)
                await AddAuthor(author);
        }

        public async Task AddAuthor(Author author)
        {
            await Task.Yield();
            if (string.IsNullOrEmpty(author.Id))
            {
                author.Id = author.Name.ToLower().Replace(" ", "-");
            }

            authors[author.Id] = author;
        }

        public async Task<IList<Author>> GetAllAuthors()
        {
            await Task.Yield();
            return authors.Values.ToList();
        }

        public async Task<Author> GetAuthorById(string id)
        {
            await Task.Yield();
            if (authors.ContainsKey(id))
                return authors[id];
            else
                return null;
        }

        public Task<IList<Author>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Book>> GetBooksByAuthor(string id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAuthor(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
