using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace ConceptArchitect.BookManagement.Repositories.EFRepository
{
    public class AdoNetBookRepository : IBookRepository
    {
        string connectionString;
        public AdoNetBookRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Task Add(Book author)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Book>> GetAll()
        {
            SqlConnection client = null;

            await Task.Yield();   

            try
            {
                client = new SqlConnection() { ConnectionString = connectionString };
                var command = new SqlCommand()
                {
                    Connection = client,
                    CommandText = "select * from books"
                };

                client.Open();
                var reader = command.ExecuteReader();
                var result = new List<Book>();
                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var title = (string)reader["title"];
                    var price = (int)reader["price"];
                    var rating = (double)reader["rating"];
                    var author = (string)reader["author"];

                    var book = new Book()
                    {
                        Isbn = id.ToString(),
                        Title = title,
                        Author = new Author() { Name = author },
                        Price = price,
                        Rating = rating
                    };
                    result.Add(book);
                }
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
            finally
            {
                client.Close();
            }
            

        }

        public Task<Book> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Book>> Search(Func<Book, bool> condition)
        {
            throw new NotImplementedException();
        }
    }
}
