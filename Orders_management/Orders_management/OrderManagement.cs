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
        DatabaseContext db = new DatabaseContext();

        public OrderManagement()
        {
            InitializeComponent();
        }

        private void OrderManagement_Load(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            DatabaseContext db = new DatabaseContext();
            dataGridView1.DataSource = db.Orders.ToList();
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Height = this.Height - (dataGridView1.Top + 50);
            dataGridView1.Left = 18;
            dataGridView1.Width = this.Width - 50;
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            AddOrUpdateOrder addOrder = new AddOrUpdateOrder();
            addOrder.orderManagement = this;
            addOrder.onCreated += AddOrder_onCreated;
            addOrder.Show();
        }

        private void AddOrder_onCreated(OrderManagement management)
        {
            management.ReloadData();
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            AddOrUpdateOrder updateOrder = new AddOrUpdateOrder(id);
            updateOrder.orderManagement = this;
            updateOrder.onUpdated += UpdateOrder_onUpdated;
            updateOrder.Show();
        }

        private void UpdateOrder_onUpdated(OrderManagement management)
        {
            management.ReloadData();
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure delete this Order?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            using (DatabaseContext db = new DatabaseContext())
            {
                Order order = db.Orders.Where(x => x.ID == id).Select(x => x).FirstOrDefault();
                db.Orders.Remove(order);
                db.SaveChanges();
                ReloadData();
            }
        }
    }
}
