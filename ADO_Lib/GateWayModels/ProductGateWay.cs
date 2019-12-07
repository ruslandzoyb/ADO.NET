using ADO_Lib.Interface;
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
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Product Get(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }
        public void R()
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
                foreach (var it in products)
                {
                    Console.WriteLine($"{it.Id}  {it.Name} {it.Price}");
                }
            }
        }
    }
}
