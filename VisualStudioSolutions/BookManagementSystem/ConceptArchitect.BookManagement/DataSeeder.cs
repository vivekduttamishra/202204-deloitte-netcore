using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class DataSeeder
    {
        IAuthorService authorService;
        IUserManager userManager;
        public DataSeeder(IAuthorService service,IUserManager userManager)
        {
            this.authorService = service;
            this.userManager = userManager;
        }
        public async Task Seed()
        {
            var authors = await authorService.GetAllAuthors();
            if (authors.Count > 0)
                return ;

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

            foreach (var author in _authors)
            {
                author.Id = author.Name.ToLower().Replace(" ", "-");
                await authorService.AddAuthor(author);
            }

            //await userManager.Register(new User() { Name = "Vivek Dutta Mishra", Email = "vivek@conceptarchitect.in", Password = "p@ss#1" });
            await userManager.Register(new User() { Name = "Aman Singh", Email = "aman@conceptarchitect.in", Password = "p@ss#1" });





        }
    }
}
