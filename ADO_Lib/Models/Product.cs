using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Lib.Models
{
  public  class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public Category Category { get; set; }
        public virtual ICollection<Provider> Providers { get; set; }
        public Product()
        {
            Providers = new List<Provider>();
        }
    }
}
