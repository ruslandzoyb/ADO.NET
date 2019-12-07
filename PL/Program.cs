using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADO_Lib.GateWayModels;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Product_ADO;"
            + "Integrated Security=true";
            //var str = "MSSQLLocalDB; Initial Catalog = Product_ADO; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            //SqlConnection connection = new SqlConnection(str);

            
           ProductGateWay gateWay = new ProductGateWay(connectionString);
            /* gateWay.Create(new ADO_Lib.Models.Product()
                 { Name = "Jacobs",
                 Price = 10 }
             );*/
            //  var pr = gateWay.Get(5);
            //Console.WriteLine($"{pr.Id}  {pr.Name}  {pr.Price}  ");

            var cl = gateWay.GetProviderByCat(new ADO_Lib.Models.Category()
            {
                Name = "Laptop"
            });
            foreach (var item in cl)
            {
                Console.WriteLine($"{item.Id} {item.Name} {item.City} ");
            }
            Console.ReadLine();
        }
    }
}
