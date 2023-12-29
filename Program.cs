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
                UpdateProduct(connection, 1, "New Namfdfdse", "New Tybfdsbfdpe", 50, 15.99m);
                DeleteProduct(connection, 1);
                UpdateProductType(connection, 1, "New Tybfdsbfdspe");
                UpdateSalesManager(connection, 1, "New Manafbdsbfdger Name");
                UpdateCustomerCompany(connection, 1, "New Compbfdbdsany Name");
                InsertNewProduct(connection, "Product Name", "Product Type", 100, 10.5m);
                InsertNewProductType(connection, "New Type");
                InsertNewSalesManager(connection, "Manager Name");
                InsertNewCustomerCompany(connection, "Company Name");
                Console.Write("\nEnter a product type: ");
                string productType = Console.ReadLine();
                SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE ProductType = @ProductType", connection);
                command.Parameters.AddWithValue("@ProductType", productType);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("\nProducts of type {0}:", productType);
                while (reader.Read())
                {
                    Console.WriteLine("Product ID: {0}, Product Name: {1}, Quantity: {2}, Cost: {3}", reader[0], reader[1], reader[3], reader[4]);
                }
                reader.Close();
                Console.Write("\nEnter a sales manager name: ");
                string managerName = Console.ReadLine();
                command = new SqlCommand("SELECT Products.ProductName, Sales.Quantity, Sales.Price, Sales.SaleDate FROM Sales INNER JOIN Products ON Sales.ProductID = Products.ProductID INNER JOIN SalesManagers ON Sales.ManagerID = SalesManagers.ManagerID WHERE SalesManagers.ManagerName = @ManagerName", connection);
                command.Parameters.AddWithValue("@ManagerName", managerName);
                reader = command.ExecuteReader();
                Console.WriteLine("\nProducts sold by {0}:", managerName);
                while (reader.Read())
                {
                    Console.WriteLine("Product Name: {0}, Quantity: {1}, Price: {2}, Sale Date: {3}", reader[0], reader[1], reader[2], reader[3]);
                }
                reader.Close();
                Console.Write("\nEnter a customer name: ");
                string customerName = Console.ReadLine();
                command = new SqlCommand("SELECT Products.ProductName, Sales.Quantity, Sales.Price, Sales.SaleDate FROM Sales INNER JOIN Products ON Sales.ProductID = Products.ProductID INNER JOIN Customers ON Sales.CustomerID = Customers.CustomerID WHERE Customers.CustomerName = @CustomerName", connection);
                command.Parameters.AddWithValue("@CustomerName", customerName);
                reader = command.ExecuteReader();
                Console.WriteLine("\nProducts purchased by {0}:", customerName);
                while (reader.Read())
                {
                    Console.WriteLine("Product Name: {0}, Quantity: {1}, Price: {2}, Sale Date: {3}", reader[0], reader[1], reader[2], reader[3]);
                }
                reader.Close();
                command = new SqlCommand("SELECT Products.ProductName, Sales.Quantity, Sales.Price, Sales.SaleDate FROM Sales INNER JOIN Products ON Sales.ProductID = Products.ProductID WHERE Sales.SaleDate >= DATEADD(day, -7, GETDATE())", connection);
                reader = command.ExecuteReader();
                Console.WriteLine("\nInformation about recent sales:");
                while (reader.Read())
                {
                    Console.WriteLine("Product Name: {0}, Quantity: {1}, Price: {2}, Sale Date: {3}", reader[0], reader[1], reader[2], reader[3]);
                }
                reader.Close();
                command = new SqlCommand("SELECT ProductType, AVG(Quantity) FROM Products GROUP BY ProductType", connection);
                reader = command.ExecuteReader();
                Console.WriteLine("\nAverage quantity of products by product type:");
                while (reader.Read())
                {
                    Console.WriteLine("Product Type: {0}, Average Quantity: {1}", reader[0], reader[1]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.Read();
        }
        static void InsertNewProduct(SqlConnection connection, string productName, string productType, int quantity, decimal cost)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Products (ProductName, ProductType, Quantity, Cost) VALUES (@ProductName, @ProductType, @Quantity, @Cost)", connection);
            insertCommand.Parameters.AddWithValue("@ProductName", productName);
            insertCommand.Parameters.AddWithValue("@ProductType", productType);
            insertCommand.Parameters.AddWithValue("@Quantity", quantity);
            insertCommand.Parameters.AddWithValue("@Cost", cost);
            insertCommand.ExecuteNonQuery();
        }
        static void InsertNewProductType(SqlConnection connection, string productType)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO ProductTypes (ProductType) VALUES (@ProductType)", connection);
            insertCommand.Parameters.AddWithValue("@ProductType", productType);
            insertCommand.ExecuteNonQuery();
        }
        static void InsertNewSalesManager(SqlConnection connection, string managerName)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO SalesManagers (ManagerName) VALUES (@ManagerName)", connection);
            insertCommand.Parameters.AddWithValue("@ManagerName", managerName);
            insertCommand.ExecuteNonQuery();
        }
        static void InsertNewCustomerCompany(SqlConnection connection, string companyName)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Customers (CustomerName) VALUES (@CustomerName)", connection);
            insertCommand.Parameters.AddWithValue("@CustomerName", companyName);
            insertCommand.ExecuteNonQuery();
        }
        static void UpdateProduct(SqlConnection connection, int productId, string productName, string productType, int quantity, decimal cost)
        {
            SqlCommand updateCommand = new SqlCommand("UPDATE Products SET ProductName = @ProductName, ProductType = @ProductType, Quantity = @Quantity, Cost = @Cost WHERE ProductID = @ProductID", connection);
            updateCommand.Parameters.AddWithValue("@ProductName", productName);
            updateCommand.Parameters.AddWithValue("@ProductType", productType);
            updateCommand.Parameters.AddWithValue("@Quantity", quantity);
            updateCommand.Parameters.AddWithValue("@Cost", cost);
            updateCommand.Parameters.AddWithValue("@ProductID", productId);
            updateCommand.ExecuteNonQuery();
        }
        static void UpdateProductType(SqlConnection connection, int productTypeId, string productType)
        {
            SqlCommand updateCommand = new SqlCommand("UPDATE ProductTypes SET ProductType = @ProductType WHERE ProductTypeID = @ProductTypeID", connection);
            updateCommand.Parameters.AddWithValue("@ProductType", productType);
            updateCommand.Parameters.AddWithValue("@ProductTypeID", productTypeId);
            updateCommand.ExecuteNonQuery();
        }
        static void UpdateSalesManager(SqlConnection connection, int managerId, string managerName)
        {
            SqlCommand updateCommand = new SqlCommand("UPDATE SalesManagers SET ManagerName = @ManagerName WHERE ManagerID = @ManagerID", connection);
            updateCommand.Parameters.AddWithValue("@ManagerName", managerName);
            updateCommand.Parameters.AddWithValue("@ManagerID", managerId);
            updateCommand.ExecuteNonQuery();
        }
        static void UpdateCustomerCompany(SqlConnection connection, int customerId, string companyName)
        {
            SqlCommand updateCommand = new SqlCommand("UPDATE Customers SET CustomerName = @CustomerName WHERE CustomerID = @CustomerID", connection);
            updateCommand.Parameters.AddWithValue("@CustomerName", companyName);
            updateCommand.Parameters.AddWithValue("@CustomerID", customerId);
            updateCommand.ExecuteNonQuery();
        }
        static void DeleteProduct(SqlConnection connection, int productId)
        {
            SqlCommand archiveCommand = new SqlCommand("INSERT INTO ArchivedProducts SELECT * FROM Products WHERE ProductID = @ProductID", connection);
            archiveCommand.Parameters.AddWithValue("@ProductID", productId);
            archiveCommand.ExecuteNonQuery();
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM Products WHERE ProductID = @ProductID", connection);
            deleteCommand.Parameters.AddWithValue("@ProductID", productId);
            deleteCommand.ExecuteNonQuery();
        }
        static void DeleteProductType(SqlConnection connection, int productTypeId)
        {
            SqlCommand archiveCommand = new SqlCommand("INSERT INTO ArchivedProductTypes SELECT * FROM ProductTypes WHERE ProductTypeID = @ProductTypeID", connection);
            archiveCommand.Parameters.AddWithValue("@ProductTypeID", productTypeId);
            archiveCommand.ExecuteNonQuery();
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM ProductTypes WHERE ProductTypeID = @ProductTypeID", connection);
            deleteCommand.Parameters.AddWithValue("@ProductTypeID", productTypeId);
            deleteCommand.ExecuteNonQuery();
        }
        static void DeleteSalesManager(SqlConnection connection, int managerId)
        {
            SqlCommand archiveCommand = new SqlCommand("INSERT INTO ArchivedSalesManagers SELECT * FROM SalesManagers WHERE ManagerID = @ManagerID", connection);
            archiveCommand.Parameters.AddWithValue("@ManagerID", managerId);
            archiveCommand.ExecuteNonQuery();
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM SalesManagers WHERE ManagerID = @ManagerID", connection);
            deleteCommand.Parameters.AddWithValue("@ManagerID", managerId);
            deleteCommand.ExecuteNonQuery();
        }
        static void DeleteCustomerCompany(SqlConnection connection, int customerId)
        {
            SqlCommand archiveCommand = new SqlCommand("INSERT INTO ArchivedCustomers SELECT * FROM Customers WHERE CustomerID = @CustomerID", connection);
            archiveCommand.Parameters.AddWithValue("@CustomerID", customerId);
            archiveCommand.ExecuteNonQuery();
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM Customers WHERE CustomerID = @CustomerID", connection);
            deleteCommand.Parameters.AddWithValue("@CustomerID", customerId);
            deleteCommand.ExecuteNonQuery();
        }
    }
}