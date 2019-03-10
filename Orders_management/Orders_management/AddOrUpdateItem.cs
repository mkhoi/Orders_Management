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
        //List<Item> items = new List<Item>();

        public delegate void _Created(Item item);
        public event _Created onCreated = null;

        public delegate void _Updated(AddOrUpdateOrder addOrUpdate);
        public event _Updated onUpdated = null;

        public AddOrUpdateItem()
        {
            InitializeComponent();
            btnUpdate.Hide();
        }

        private void AddOrUpdateItem_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (sai())
            {
                MessageBox.Show("Xin vui long kiem tra lai", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (onCreated != null)
                {
                    Item item = GetItemInfo();
                    
                   // items.Add(item);
                    /*BindingList<Item> bindingList = new BindingList<Item>(items);
                    BindingSource source = new BindingSource(bindingList, null);
                    datagri.DataSource = source;
                    dataGridView2.Refresh();*/
                    //db.Items.Add(item);
                    //db.SaveChanges();
                    onCreated(item);
                    
                    this.Close();
                }
            }
        }

        private Item GetItemInfo()
        {
            Item item = new Item();

            //item.ItemID = Id;
            item.ItemName = txtItemName.Text;
            item.Price = Convert.ToInt32(txtPrice.Text);
            item.Qty = Convert.ToInt32(txtQuantity.Text);

            return item;
        }

        private bool sai()
        {
            int number;
            if (txtItemName.Text == "") { return true; }
            if (txtPrice.Text == "") { return true; }
            if (txtQuantity.Text == "") { return true; }
            if (!int.TryParse(txtPrice.Text, out number)) { return true; }
            if (!int.TryParse(txtQuantity.Text, out number)) { return true; }
            return false;
        }
    }
}
