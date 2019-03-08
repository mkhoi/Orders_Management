using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Orders_management.Model;

namespace Orders_management
{
    public partial class AddOrUpdateItem : Form
    {
        public int Id = 0;
        DatabaseContext db = new DatabaseContext();
        public AddOrUpdateOrder addOrUpdateOrder;

        public AddOrUpdateItem()
        {
            InitializeComponent();
        }

        private void AddOrUpdateItem_Load(object sender, EventArgs e)
        {

        }
    }
}
