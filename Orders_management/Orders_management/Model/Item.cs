using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Orders_management.Model
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }
        public int? Qty { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
