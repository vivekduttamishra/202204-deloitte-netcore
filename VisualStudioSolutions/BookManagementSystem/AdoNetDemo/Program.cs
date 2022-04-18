using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdoNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SqlConnection() { ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\MyWorks\Corporate\202204-deloitte-dotnetcore\VisualStudioSolutions\BookManagementSystem\BooksWeb\Data\books.mdf;Integrated Security=True;Connect Timeout=30" };


            var command = new SqlCommand() { Connection = client };
            client.Open();

            command.CommandText = "select * from books";

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var title = (string)reader["title"];
                var price = (int)reader["price"];
                var rating = (double)reader["rating"];
                var author = (string)reader["author"];

                Console.WriteLine($"{id}\t{title}\t{author}\t{price}\t{rating}/5");
            }


        }
    }
}
