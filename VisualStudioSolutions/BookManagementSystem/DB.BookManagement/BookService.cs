using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.BookManagement
{
    public class BookService : IBookService
    {
        Dictionary<string, Books> books;
        public BookService()
        {
            Books[] _books =
            {
                new Books()
                {
                    Name="To Kill a Mockingbird",
                    BookUrl="https://mir-s3-cdn-cf.behance.net/project_modules/disp/15a6f610463583.560e56a82c21c.jpg",
                    Summary="The novel examines racism in the American South through the innocent wide eyes of a clever young girl named Jean Louise (“Scout”) Finch. "
                },

                new Books()
                {
                    Name="The Great Gatsby",
                    BookUrl="https://upload.wikimedia.org/wikipedia/commons/7/7a/The_Great_Gatsby_Cover_1925_Retouched.jpg",
                    Summary="The novel chronicles an era that Fitzgerald himself dubbed the Jazz Age. Following the shock and chaos of World War I, American society enjoyed unprecedented levels of prosperity during the roar"
                },
                new Books()
                {
                    Name="Pride and Prejudice",
                    BookUrl="http://prodimage.images-bn.com/pimages/9781435159631_p0_v1_s1200x630.jpg",
                    Summary="The book is narrated in free indirect speech following the main character Elizabeth Bennet as she deals with matters of upbringing, marriage, moral rightness and education in her aristocratic society"
                },
            };

            books = new Dictionary<string, Books>();
            var task = AddAll(_books);

        }

        private async Task AddAll(Books[] _books)
        {
            foreach (var book in _books)
                await AddBooks(book);
        }

        public async Task AddBooks(Books book)
        {
            await Task.Yield();
            if (string.IsNullOrEmpty(book.Id))
            {
                book.Id = book.Name.ToLower().Replace(" ", "-");
            }

            books[book.Id] = book;
        }

        public async Task<IList<Books>> GetAllBooks()
        {
            await Task.Yield();
            return books.Values.ToList();
        }

        public async Task<Books> GetBookById(string id)
        {
            await Task.Yield();
            if (books.ContainsKey(id))
                return books[id];
            else
                return null;
        }
    }
}
