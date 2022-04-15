using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.BookStoreRepository
{
    public class FlatFileBookSerializer : IStoreSerializer
    {
        BookStore store;
        String path;

        public FlatFileBookSerializer(string path)
        {
            this.path = path;
            try
            {
                using(var stream=new StreamReader(path))
                {
                    var formatter = new BinaryFormatter();
                    store = (BookStore)formatter.Deserialize(stream.BaseStream);
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
            await Task.Yield();
            using(var stream = new StreamWriter(path))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream.BaseStream, store);
            }
        }
    }
}






