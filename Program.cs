using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_2._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=LAPTOP-2;Initial Catalog=stationery;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection successful!");
                SqlCommand command = new SqlCommand("SELECT * FROM Products", connection);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("All information about products:");
                while (reader.Read())
                {
                    Console.WriteLine("Product ID: {0}, Product Name: {1}, Product Type: {2}, Quantity: {3}, Cost: {4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                }
                reader.Close();
                command = new SqlCommand("SELECT DISTINCT ProductType FROM Products", connection);
                reader = command.ExecuteReader();
                Console.WriteLine("\nAll product types:");
                while (reader.Read())
                {
                    Console.WriteLine(reader[0]);
                }
                reader.Close();
                command = new SqlCommand("SELECT * FROM SalesManagers", connection);
                reader = command.ExecuteReader();
                Console.WriteLine("\nAll sales managers:");
                while (reader.Read())
                {
                    Console.WriteLine("Manager ID: {0}, Manager Name: {1}", reader[0], reader[1]);
                }
                reader.Close();
                command = new SqlCommand("SELECT * FROM Products WHERE Quantity = (SELECT MAX(Quantity) FROM Products)", connection);
                reader = command.ExecuteReader();
                Console.WriteLine("\nProducts with maximum quantity:");
                while (reader.Read())
                {
                    Console.WriteLine("Product ID: {0}, Product Name: {1}, Product Type: {2}, Quantity: {3}, Cost: {4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                }
                reader.Close();
                command = new SqlCommand("SELECT * FROM Products WHERE Quantity = (SELECT MIN(Quantity) FROM Products)", connection);
                reader = command.ExecuteReader();
                Console.WriteLine("\nProducts with minimum quantity:");
                while (reader.Read())
                {
                    Console.WriteLine("Product ID: {0}, Product Name: {1}, Product Type: {2}, Quantity: {3}, Cost: {4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                }
                reader.Close();
                command = new SqlCommand("SELECT * FROM Products WHERE Cost = (SELECT MIN(Cost) FROM Products)", connection);
                reader = command.ExecuteReader();
                Console.WriteLine("\nProducts with minimum cost:");
                while (reader.Read())
                {
                    Console.WriteLine("Product ID: {0}, Product Name: {1}, Product Type: {2}, Quantity: {3}, Cost: {4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                }
                reader.Close();
                command = new SqlCommand("SELECT * FROM Products WHERE Cost = (SELECT MAX(Cost) FROM Products)", connection);
                reader = command.ExecuteReader();
                Console.WriteLine("\nProducts with maximum cost:");
                while (reader.Read())
                {
                    Console.WriteLine("Product ID: {0}, Product Name: {1}, Product Type: {2}, Quantity: {3}, Cost: {4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.Read();
        }
    }
}
