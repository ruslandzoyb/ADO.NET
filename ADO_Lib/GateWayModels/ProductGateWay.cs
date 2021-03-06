﻿using ADO_Lib.Interface;
using ADO_Lib.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Lib.GateWayModels
{
    public class ProductGateWay : IGateWay<Product>
    {
        private string connectionString;
        public ProductGateWay(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Create(Product item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("AddProduct", connection);
                command.Connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Price", item.Price);
                command.Parameters.AddWithValue("@CategoryID", 1);
                command.Parameters.AddWithValue("@ProviderID", 1);

                command.ExecuteNonQuery();

                    }
        }

        public void Delete(int? id)
        {
            string query = $"DELETE FROM PRODUCTS WHERE Id={id} ;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
            }
            }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Product> GetByCategory(Category category)
        {
            string query = $"SELECT * FROM PRODUCTS WHERE CategoryId=(SELECT DISTINCT Id FROM CATEGORIES WHERE Name='{category.Name}')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Product> products = new List<Product>();
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int price = reader.GetInt32(2);
                    int catid = reader.GetInt32(3);
                    products.Add(new Product()
                    {
                        Id = id,
                        Name = name,
                        Price = price


                    });

                }
                return products;

            }
            }
      
       
        public Product Get(int? id)
        {
            string query = $" SELECT * FROM PRODUCTS WHERE Id={id} ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Product product = new Product();
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    product.Id = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Price = reader.GetInt32(2);
                    
                }
                return product;
            }
            
            }

        public IEnumerable<Product> GetByProvider(Provider provider)
        {
            string query = $"SELECT * FROM PRODUCTS WHERE ProviderId=(SELECT DISTINCT Id FROM Providers WHERE Name='{provider.Name}')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Product> products = new List<Product>();
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int price = reader.GetInt32(2);
                    Category category = new Category()
                    {
                        Id = reader.GetInt32(3)
                    };


                    products.Add(new Product()
                    {
                        Id = id,
                        Name = name,
                        Price = price,
                        Category = new Category()
                        {
                            Id = id
                        }

                    });


                }
                return products;
            }
        }
        public IEnumerable<Product> GetAll()
        {
            string query = "SELECT * FROM PRODUCTS;";
            //string query = "INSERT INTO PRODUCTS (Name, Price, CategoryID, ProviderID ) VALUES('@item.Name', '@item.Price' ,'@item.Category.Id' ,'')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Product> products = new List<Product>();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int price = reader.GetInt32(2);
                    int catid = reader.GetInt32(3);
                    products.Add(new Product()
                    {
                        Id = id,
                        Name = name,
                        Price = price


                    });
                }
                return products;
            }
        }

        public void Update(Product item)
        {
            string query = $"UPDATE PRODUCTS SET Name='{item.Name}' ,Price='{item.Price}' ,CategoryID='{item.Category.Id}' WHERE Id='{item.Id}' ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                
            }

            }
        
    }
}
