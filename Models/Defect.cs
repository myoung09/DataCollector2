using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Models
{
    internal class Defect
    {
        string name;
        int quantity;

        public Defect(string name, int quantity)
        {
            this.name = name;
            this.quantity = quantity;
        }

        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}
