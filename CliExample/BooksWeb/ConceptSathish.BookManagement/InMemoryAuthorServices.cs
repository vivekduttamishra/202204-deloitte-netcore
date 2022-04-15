using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptSathish.BookManagement
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
                    Photo="https://m.media-amazon.com/images/I/51Fncy7geTL._SX450_.jpg",
                    Biography="One of the altime greatest author of English and French"
                },
                new Author()
                {
                    Name="Conan Doyle",
                    Photo="https://m.media-amazon.com/images/S/amzn-author-media-prod/s0gktk6c6oen9bivv1vlnd9bq9._SX450_.jpg",
                    Biography="The creator of famous Sherlock Holmes"
                },
                new Author()
                {
                    Name="Jeffrey Archer",
                    Photo="https://m.media-amazon.com/images/S/amzn-author-media-prod/8tf5spo4ri40bkl7hrvrd7t4q9._SX450_.jpg",
                    Biography ="One of the contemporary best seller author in English Fiction"
                },

                new Author()
                {
                    Name="John Grisham",
                    Photo="https://m.media-amazon.com/images/S/amzn-author-media-prod/d661gj9v9di7dgupcm7st49gpt._SX450_.jpg",
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
    }
}
