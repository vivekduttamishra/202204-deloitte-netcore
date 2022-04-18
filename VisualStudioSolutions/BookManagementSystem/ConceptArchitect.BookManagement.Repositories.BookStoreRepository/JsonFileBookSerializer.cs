using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.BookStoreRepository
{
    public class JsonFileBookSerializer : IStoreSerializer
    {
        BookStore store;
        String path;

        public JsonFileBookSerializer(string path)
        {
            this.path = path;
            try
            {
                using(var stream=new StreamReader(path))
                {
                    store=JsonSerializer.DeserializeAsync(stream.BaseStream, typeof(BookStore)).Result as BookStore;
                   // store = (BookStore)formatter.Deserialize(stream.BaseStream);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error Loading store " + ex.Message);
                store = new BookStore();
            }
        }

        public async Task<BookStore> Get()
        {
            await Task.Yield();
            return store;
        }

        public async Task Save()
        {
           
            using(var stream = new StreamWriter(path))
            {
                await JsonSerializer.SerializeAsync(stream.BaseStream, store);
            }
        }
    }
}






