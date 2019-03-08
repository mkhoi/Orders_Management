using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders_management.Model
{
    public class Order
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
        public DateTime Date { get; set; }

        public virtual List<Item> Items { get; set; }

    }
}
