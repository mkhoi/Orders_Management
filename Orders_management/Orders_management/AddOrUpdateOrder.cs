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
        public int itemId = 1;
        DatabaseContext db = new DatabaseContext();

        public delegate void _Created(OrderManagement management);
        public event _Created onCreated = null;

        public delegate void _Updated(OrderManagement management);
        public event _Updated onUpdated = null;

        List<Item> items = new List<Item>();
        public OrderManagement orderManagement;

        public AddOrUpdateOrder()
        {
            InitializeComponent();
            lblCreateOrderDate.Text = DateTime.Today.ToString();
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
                if(onCreated != null)
                { 
                    Order order = GetOrderInfo();
                    DatabaseContext db = new DatabaseContext();
                    db.Items.AddRange(items);
                    db.Orders.Add(order);
                    db.SaveChanges();
                    
                    onCreated(orderManagement);
                   
                    this.Close();
                }
            }
        }

        private Order GetOrderInfo()
        {
            Order order = new Order();

            order.ID = Id;
            order.Name = txtName.Text;
            order.Total = Convert.ToInt32(txtTotal.Text);
            order.Date = Convert.ToDateTime(lblCreateOrderDate.Text);
            foreach (Item item in items)
            {
                item.OrderId = order.ID;
            }
            
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
                if (onUpdated != null)
                {
                    Order _order = db.Orders.Where(x => x.ID == Id).Select(x => x).FirstOrDefault();
                    _order.Name = txtName.Text;
                    _order.Total = Convert.ToInt32(txtTotal.Text);
                    _order.Date = Convert.ToDateTime(lblCreateOrderDate.Text);
                    db.SaveChanges();
                    onUpdated(orderManagement);
                    
                    this.Close();
                }      
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddOrUpdateItem AddItem = new AddOrUpdateItem();
            //AddItem.addOrUpdateOrder = this;
            AddItem.onCreated += AddItem_onCreated;
            AddItem.Show();
        }

        private void AddItem_onCreated(Item item)
        {
            item.ItemID = itemId;
            items.Add(item);
            BindingList<Item> bindingList = new BindingList<Item>(items);
            BindingSource source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            dataGridView1.Refresh();
            itemId++;
        }

        private void ReloadData()
        {
            DatabaseContext db = new DatabaseContext();
            dataGridView1.DataSource = db.Items.ToList();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Item item = items.Where(x => x.ItemID == id).Select(x => x).FirstOrDefault();

            AddOrUpdateItem UpdateItem = new AddOrUpdateItem(item);
            //AddItem.addOrUpdateOrder = this;
           
            UpdateItem.Show();
            UpdateItem.onUpdated += UpdateItem_onUpdated;
        }

        private void UpdateItem_onUpdated(Item item)
        {
            Item _item = items.Where(x => x.ItemID == item.ItemID).Select(x => x).FirstOrDefault();
            _item.ItemName = item.ItemName;
            _item.Price = item.Price;
            _item.Qty = item.Qty;

            dataGridView1.Refresh();
        }
    }
}
