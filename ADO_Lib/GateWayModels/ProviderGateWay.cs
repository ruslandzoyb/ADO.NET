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
  public  class ProviderGateWay:IGateWay<Provider>
    {
        private string connectionString;
        public ProviderGateWay(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(Provider item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"Insert Into Providers(Name,City) Values('{item.Name}','{item.City}') ";
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

            }
        }

        public void Delete(int? id)
        {
            string query = $"DELETE FROM Providers WHERE Id={id} ;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
            }
        }

        public IEnumerable<Provider> Find(Func<Provider, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Provider Get(int? id)
        {
            string query = $" SELECT * FROM Providers WHERE Id={id} ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Provider provider = new Provider();
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                   
                    provider.Id = reader.GetInt32(0);

                    provider.Name = reader.GetString(1);
                    provider.City = reader.GetString(2);


                }
                return provider;
            }
        }

        public IEnumerable<Provider> GetAll()
        {
            string query = "SELECT * FROM Providers;";
            //string query = "INSERT INTO PRODUCTS (Name, Price, CategoryID, ProviderID ) VALUES('@item.Name', '@item.Price' ,'@item.Category.Id' ,'')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Provider> providers = new List<Provider>();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string city = reader.GetString(2);


                    providers.Add(new Provider()
                    {
                        Id = id,
                        Name = name,
                        City=city



                    });
                }
                return providers;
            }
        }
        public IEnumerable<Provider> GetProviderByCat(Category category)
        {
            string query = $"SELECT PT.ProviderID, PR.Name, PR.City  from Products as PT  join Providers as PR  on PT.ProviderID = PR.Id   where PT.CategoryID = (SELECT DISTINCT Id FROM CATEGORIES WHERE Categories.Name = '{category.Name}' ) ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Provider> providers = new List<Provider>();
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string city = reader.GetString(2);
                    providers.Add(new Provider()
                    {
                        Id = id,
                        Name = name,
                        City = city
                    });

                }
                return providers;
            }
        }

        public void Update(Provider item)
        {
            string query = $"UPDATE Providers Set Name='{item.Name}',City='{item.City}'  WHERE Id='{item.Id}' ";
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
