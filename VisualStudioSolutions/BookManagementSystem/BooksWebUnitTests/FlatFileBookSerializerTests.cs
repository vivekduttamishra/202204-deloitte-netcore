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
    public class FlatFileBookSerializerTests
    {
        string path = @"d:\temp\test-store.db";
        FlatFileBookSerializer serializer;
        String id = "vivek-dutta-mishra";
        string name = "Vivek Dutta Mishra";


        public FlatFileBookSerializerTests()
        {
            //delete the test file before every test
            //this ensure each test works on a isolated enviornment
            if (File.Exists(path))
                File.Delete(path);

            serializer= new FlatFileBookSerializer(path);
        }

        [Fact]
        public void CanCreateEmptyRepositoryWithNonExistingPath()
        {
            
            Assert.NotNull(serializer);
        }

        [Fact]
        public async Task CanSaveEmptyRepositoryInTheDisk()
        {
           
            await serializer.Save();
            Assert.True(File.Exists(path));
        }


        [Fact]
        public async Task CanSaveNonEmptyBookStore()
        {
            await SaveOneAuthor();
            Assert.True(File.Exists(path));
        }

        private async Task SaveOneAuthor()
        {
            var store = await serializer.Get();
            store.Authors[id] = new Author() { Id = id, Name = name };

            await serializer.Save();
        }

        [Fact]
        public async Task CanReloadDataFromDisk()
        {
            await SaveOneAuthor();

            var serializer = new FlatFileBookSerializer(path); //load serializer from the path

            var store = await serializer.Get();

            var author = store.Authors[id];

            Assert.Equal(name, author.Name);

        }
    }
}
