using ConceptArchitect.BookManagement;
using ConceptArchitect.BookManagement.Repositories.BookStoreRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BooksWebUnitTests
{
    public class BookStoreAuthorRepositoryTests
    {
        FlatFileBookSerializer serializer;
        string id1 = "id1", id2 = "id2";
        string name1 = "Name1", name2 = "Name2";
        string path = @".\author-repository-test.db";
        int totalBooks = 2;
        BookStoreAuthorRepository repository;

        public BookStoreAuthorRepositoryTests()
        {
            if (File.Exists(path))
                File.Delete(path);

            serializer = new FlatFileBookSerializer(path);
            var store = serializer.Get().Result;
            store.Authors[id1] = new Author() { Id = id1, Name = name1 };
            store.Authors[id2] = new Author() { Id = id2, Name = name2 };

            repository = new BookStoreAuthorRepository(serializer);
        }

        [Fact]
        public async Task GetAll_ReturnsAllTheBooks()
        {
            var books = await repository.GetAll();

            Assert.Equal(2, books.Count);
        }

        [Fact]
        public async Task GetById_ReturnsRightBookWithValidId()
        {
            var author = await repository.GetById(id1);

            Assert.Equal(name1, author.Name);
        }

        [Fact]
        public async Task GetById_ReturnsNullForInvalidId()
        {
            var author = await repository.GetById("invalid-id");
            Assert.Null(author);
        }

        [Fact]
        public async Task Remove_RemovesTheAuthor()
        {
            await repository.Remove(id1);
            //if previous command is successful, a query with removed id should return null
            var author = await repository.GetById(id1);
            Assert.Null(author);
        }

        [Fact]
        public async Task Add_ShouldAddAnAuthor()
        {
            var id3 = "id3";
            var name3 = "Name3";

            await repository.Add(new Author() { Id = id3, Name = name3 });

            var author = await repository.GetById(id3);
            Assert.Equal(name3, author.Name);


        }


    }
}
