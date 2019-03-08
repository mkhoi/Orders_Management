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
    public partial class AddOrUpdateOrder : Form
    {
        public int Id = 0;
        DatabaseContext db = new DatabaseContext();
        public OrderManagement orderManagement;

        public AddOrUpdateOrder()
        {
            InitializeComponent();
            btnUpdate.Hide();
        }

        public AddOrUpdateOrder(int id)
        {
            InitializeComponent();
            Order order = db.Orders.Where(x => x.ID == id).Select(x => x).FirstOrDefault();
            Id = id;
            txtName.Text = order.Name;
            txtTotal.Text = order.Total.ToString();
            lblCreateOrderDate.Text = DateTime.Today.ToString();
            btnSave.Hide();
        }

        private void AddOrUpdateOrder_Load(object sender, EventArgs e)
        {
            lblCreateOrderDate.Text = DateTime.Today.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (sai())
            {
                MessageBox.Show("Xin vui long kiem tra lai", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Order order = GetOrderMasterInfo();
                db.Orders.Add(order);
                db.SaveChanges();
                orderManagement.ReloadData();
                this.Close();
            }
        }

        private Order GetOrderMasterInfo()
        {
            Order order = new Order();

            order.ID = Id;
            order.Name = txtName.Text;
            order.Total = Convert.ToInt32(txtTotal.Text);
            order.Date = Convert.ToDateTime(lblCreateOrderDate.Text);

            return order;
        }

        private bool sai()
        {
            int number;
            if (txtName.Text == "") { return true; }
            if (txtTotal.Text == "") { return true; }
            if (!int.TryParse(txtTotal.Text, out number)) { return true; }
            return false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (sai())
            {
                MessageBox.Show("Xin vui long kiem tra lai", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {        
                Order _order = db.Orders.Where(x => x.ID == Id).Select(x => x).FirstOrDefault();
                _order.Name = txtName.Text;
                _order.Total = Convert.ToInt32(txtTotal.Text);
                _order.Date = Convert.ToDateTime(lblCreateOrderDate.Text);
                db.SaveChanges();
                orderManagement.ReloadData();
                this.Close();
            }
        }
    }
}
