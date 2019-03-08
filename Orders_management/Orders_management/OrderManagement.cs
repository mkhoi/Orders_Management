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
    public partial class OrderManagement : Form
    {
        public OrderManagement()
        {
            InitializeComponent();
        }

        private void OrderManagement_Load(object sender, EventArgs e)
        {
            DatabaseContext db = new DatabaseContext();
            dataGridView1.DataSource = db.Orders.ToString();
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Height = this.Height - (dataGridView1.Top + 50);
            dataGridView1.Left = 18;
            dataGridView1.Width = this.Width - 50;
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {

        }
    }
}
