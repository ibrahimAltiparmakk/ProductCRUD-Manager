using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazgem
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Price2 { get; set; }

        public Product()
        {
            Id = "";
            Name = "";
            Description = "";
            Price = "";
            Price2 = "";
        }


        public Product(string id, string name, string description, string price, string price2)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Price2 = price2;
        }
    }
}
