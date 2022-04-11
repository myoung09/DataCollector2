using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Models
{
    internal class Packaging
    {
        bool value;
        string name;

        public Packaging(string name, bool value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
