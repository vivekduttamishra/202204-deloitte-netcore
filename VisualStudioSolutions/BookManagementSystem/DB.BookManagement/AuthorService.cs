using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.BookManagement
{
    public class AuthorService : IAuthorService
    {
        Dictionary<string, Authors> authors;
        public AuthorService()
        {
            Authors[] _authors =
            {
                new Authors()
                {
                    Name="Vivek Dutta Mishra",
                    AuthorUrl="https://avatars.githubusercontent.com/u/9464908?v=4",
                    Bibliography="Author of Amazon #1 Best seller The Accursed God"
                },

                new Authors()
                {
                    Name="Alexandre Dumas",
                    AuthorUrl="https://m.media-amazon.com/images/I/51Fncy7geTL._SX450_.jpg",
                    Bibliography="One of the altime greatest author of English and French"
                },
                new Authors()
                {
                    Name="Conan Doyle",
                    AuthorUrl="https://m.media-amazon.com/images/S/amzn-author-media-prod/s0gktk6c6oen9bivv1vlnd9bq9._SX450_.jpg",
                    Bibliography="The creator of famous Sherlock Holmes"
                },
                new Authors()
                {
                    Name="Jeffrey Archer",
                    AuthorUrl="https://m.media-amazon.com/images/S/amzn-author-media-prod/8tf5spo4ri40bkl7hrvrd7t4q9._SX450_.jpg",
                    Bibliography ="One of the contemporary best seller author in English Fiction"
                },

                new Authors()
                {
                    Name="John Grisham",
                    AuthorUrl="https://m.media-amazon.com/images/S/amzn-author-media-prod/d661gj9v9di7dgupcm7st49gpt._SX450_.jpg",
                    Bibliography="Best seller author Legal Fiction"
                }

            };

            authors = new Dictionary<string, Authors>();
            var task = AddAll(_authors);

        }

        private async Task AddAll(Authors[] _authors)
        {
            foreach (var author in _authors)
                await AddAuthor(author);
        }

        public async Task AddAuthor(Authors author)
        {
            await Task.Yield();
            if (string.IsNullOrEmpty(author.Id))
            {
                author.Id = author.Name.ToLower().Replace(" ", "-");
            }

            authors[author.Id] = author;
        }

        public async Task<IList<Authors>> GetAllAuthors()
        {
            await Task.Yield();
            return authors.Values.ToList();
        }

        public async Task<Authors> GetAuthorById(string id)
        {
            await Task.Yield();
            if (authors.ContainsKey(id))
                return authors[id];
            else
                return null;
        }
    }
}
