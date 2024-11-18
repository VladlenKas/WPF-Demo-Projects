using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteTextBoxDemo.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
