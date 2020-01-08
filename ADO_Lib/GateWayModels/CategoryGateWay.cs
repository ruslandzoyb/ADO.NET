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
    public class CategoryGateWay : IGateWay<Category>
    {
        private string connectionString;
        public CategoryGateWay(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(Category item)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"Insert Into Categories(Name) Values('{item.Name}')";
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

            }
          
        }

        public void Delete(int? id)
        {
            string query = $"DELETE FROM CATEGORIES WHERE Id={id} ;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
            }
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Category Get(int? id)
        {
            string query = $" SELECT * FROM CATEGORIES WHERE Id={id} ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Category category = new Category();
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    category.Id = reader.GetInt32(0);
                    
                    category.Name = reader.GetString(1);
                

                }
                return category;
            }

        }

        public IEnumerable<Category> GetAll()
        {
            string query = "SELECT * FROM Categories;";
            //string query = "INSERT INTO PRODUCTS (Name, Price, CategoryID, ProviderID ) VALUES('@item.Name', '@item.Price' ,'@item.Category.Id' ,'')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Category> categories = new List<Category>();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    
                   
                    categories.Add(new Category()
                    {
                        Id = id,
                        Name = name,
                        


                    });
                }
                return categories;
            }
        }

        public void Update(Category item)
        {
            string query = $"UPDATE Categories Set Name='{item.Name}'  WHERE Id='{item.Id}' ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

            }
        }
        public void GetAmountUnCateg()
        {
            string query = "select  pr.Name  ,Count(DISTINCT  ctg.Name) as Uniq from Products   as ps join Categories as ctg on ps.CategoryID = ctg.Id join  Providers as pr on ps.ProviderID = pr.Id  group by pr.Name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($" Provider {reader.GetString(0)} Amount of catagories {reader.GetInt32(1)} ");
                }
            }
        }
    }
}
