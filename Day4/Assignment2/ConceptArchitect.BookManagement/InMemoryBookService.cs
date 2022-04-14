using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class InMemoryBookService : IBookService
    {
        Dictionary<string, Book> books;
        public InMemoryBookService()
        {
            Book[] _books =
            {
                new Book()
                {
                    Title="The Accursed God: The Lost Epic : Book 1",
                    AuthorId="Vivek-Dutta-Mishra",
                    Cover="https://m.media-amazon.com/images/I/41-KqB1-cqL.jpg",
                    Description ="THE LOST EPICThe story of the epic battle of Kurukshetra has been told and retold for ages. Millennia of dust, fables, imaginations — and the epic itself is lost. What remained is the story of a family feud and ambition of Kauravas and Pandavas. But why, then, was this an epic war?",
                    Price=399,
                    Rating=5.0,
                    Tags=new List<string>(){ "Kurukshetra ", "Kauravas", "Pandavas" }
                },
                new Book()
                {
                    Title="The Accursed God: The Lost Epic: Book 2",
                    AuthorId="Vivek-Dutta-Mishra",
                    Cover="https://m.media-amazon.com/images/I/41-KqB1-cqL.jpg",
                    Description ="THE LOST EPICThe story of the epic battle of Kurukshetra has been told and retold for ages. Millennia of dust, fables, imaginations — and the epic itself is lost. What remained is the story of a family feud and ambition of Kauravas and Pandavas. But why, then, was this an epic war?",
                    Price=399,
                    Rating=5.0,
                    Tags=new List<string>(){ "Kurukshetra ", "Kauravas", "Pandavas" }
                },
                new Book()
                {
                    Title="The Adventures of Sherlock Holmes",
                    AuthorId="Conan Doyle",
                    Cover="https://m.media-amazon.com/images/I/41-KqB1-cqL.jpg",
                    Description ="THE LOST EPICThe story of the epic battle of Kurukshetra has been told and retold for ages. Millennia of dust, fables, imaginations — and the epic itself is lost. What remained is the story of a family feud and ambition of Kauravas and Pandavas. But why, then, was this an epic war?",
                    Price=399,
                    Rating=5.0,
                    Tags=new List<string>(){ "Kurukshetra ", "Kauravas", "Pandavas" }
                },
                new Book()
                {
                    Title="The Hound of the Baskervilles",
                    AuthorId="Conan Doyle",
                    Cover="https://m.media-amazon.com/images/I/41-KqB1-cqL.jpg",
                    Description ="THE LOST EPICThe story of the epic battle of Kurukshetra has been told and retold for ages. Millennia of dust, fables, imaginations — and the epic itself is lost. What remained is the story of a family feud and ambition of Kauravas and Pandavas. But why, then, was this an epic war?",
                    Price=399,
                    Rating=5.0,
                    Tags=new List<string>(){ "Kurukshetra ", "Kauravas", "Pandavas" }
                },
                new Book()
                {
                    Title="A Study in Scarlet",
                    AuthorId="Conan Doyle",
                    Cover="https://m.media-amazon.com/images/I/41-KqB1-cqL.jpg",
                    Description ="THE LOST EPICThe story of the epic battle of Kurukshetra has been told and retold for ages. Millennia of dust, fables, imaginations — and the epic itself is lost. What remained is the story of a family feud and ambition of Kauravas and Pandavas. But why, then, was this an epic war?",
                    Price=399,
                    Rating=5.0,
                    Tags=new List<string>(){ "Kurukshetra ", "Kauravas", "Pandavas" }
                },
                new Book()
                {
                    Title="Le Comte de Monte-Cristo",
                    AuthorId="Alexandre Dumas",
                    Cover="https://m.media-amazon.com/images/I/41-KqB1-cqL.jpg",
                    Description ="THE LOST EPICThe story of the epic battle of Kurukshetra has been told and retold for ages. Millennia of dust, fables, imaginations — and the epic itself is lost. What remained is the story of a family feud and ambition of Kauravas and Pandavas. But why, then, was this an epic war?",
                    Price=399,
                    Rating=5.0,
                    Tags=new List<string>(){ "Kurukshetra ", "Kauravas", "Pandavas" }
                },
                new Book()
                {
                    Title="The Three Musketeers",
                    AuthorId="Alexandre Dumas",
                    Cover="https://m.media-amazon.com/images/I/41-KqB1-cqL.jpg",
                    Description ="THE LOST EPICThe story of the epic battle of Kurukshetra has been told and retold for ages. Millennia of dust, fables, imaginations — and the epic itself is lost. What remained is the story of a family feud and ambition of Kauravas and Pandavas. But why, then, was this an epic war?",
                    Price=399,
                    Rating=5.0,
                    Tags=new List<string>(){ "Kurukshetra ", "Kauravas", "Pandavas" }
                },
                new Book()
                {
                    Title="Twenty Years After",
                    AuthorId="Alexandre Dumas",
                    Cover="https://m.media-amazon.com/images/I/41-KqB1-cqL.jpg",
                    Description ="THE LOST EPICThe story of the epic battle of Kurukshetra has been told and retold for ages. Millennia of dust, fables, imaginations — and the epic itself is lost. What remained is the story of a family feud and ambition of Kauravas and Pandavas. But why, then, was this an epic war?",
                    Price=399,
                    Rating=5.0,
                    Tags=new List<string>(){ "Kurukshetra ", "Kauravas", "Pandavas" }
                }

            };

            books = new Dictionary<string, Book>();
            var task = AddAll(_books);

        }

        private async Task AddAll(Book[] _books)
        {
            foreach (var book in _books)
                await AddBook(book);
        }

        public async Task AddBook(Book book)
        {
            await Task.Yield();
            if (string.IsNullOrEmpty(book.Isbn))
            {
                book.Isbn = book.Title.ToLower().Replace(" ", "-");
            }

            books[book.Isbn] = book;
        }

        public async Task<IList<Book>> GetAllBooks()
        {
            await Task.Yield();
            return books.Values.ToList();
        }

        public async Task<Book> GetBookById(string id)
        {
            await Task.Yield();
            if (books.ContainsKey(id))
                return books[id];
            else
                return null;
        }

        public async Task<IList<Book>> GetBookByAuthor(string id)
        {
            await Task.Yield();
            return books.Values.Where(book => book.AuthorId.ToLower() == id.ToLower()).ToList();
        }
    }
}
