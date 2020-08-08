using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management_Software
{
    public partial class ManageCategories : Form
    {
        Room room = new Room();
        public ManageCategories()
        {
            InitializeComponent();
        }

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            String label = textBoxlabel.Text;
            String price = textBoxPrice.Text;

            double priced;
            try
            {
                priced = Convert.ToDouble(price);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid price Entered", "Adding room category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (label.Trim().Equals(""))
            {
                MessageBox.Show("Invalid label Entered", "Adding room category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Boolean insertcategory = room.addRoomCategory(label, priced);


            if (insertcategory)
            {
                dataGridView1.DataSource = room.roomTypeList();

                MessageBox.Show("New Category Inserted Successful", "Addition of Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonClearFields.PerformClick();

            }
            else
            {
                MessageBox.Show("Error in adding category", "Addition of category", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void buttonEditCategory_Click(object sender, EventArgs e)
        {
            int id;
            try
            {
                id = Convert.ToInt32(textBoxID.Text);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception E)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                MessageBox.Show("Select a category Entry to edit", "Editing category Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String label = textBoxlabel.Text;
            String price = textBoxPrice.Text;
            double priced;
            try
            {
                priced = Convert.ToDouble(price);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid price Entered", "Adding room category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (label.Trim().Equals(""))
            {
                MessageBox.Show("Invalid label Entered", "Adding room category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Boolean editcategory = room.editRoomCategory(id, label, priced);


            if (editcategory)
            {
                dataGridView1.DataSource = room.roomTypeList();

                MessageBox.Show("Category Data Edited Successfully", "Editing Category Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonClearFields.PerformClick();

            }
            else
            {
                MessageBox.Show("Error in editing data", "Editing Category Data", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void buttonRemoveCategory_Click(object sender, EventArgs e)
        {
            int id;
            try
            {
                id = Convert.ToInt32(textBoxID.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show("Select a category Entry to delete", "Removal of Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Boolean deleteclient = room.deleteRoomCategory(id);


            if (deleteclient)
            {
                dataGridView1.DataSource = room.roomTypeList();

                MessageBox.Show("Category Data Deleted", "Removal of Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonClearFields.PerformClick();
            }
            else
            {
                MessageBox.Show("Error in deleting Category", "Removal of Category", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonClearFields_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxlabel.Text = "";
            textBoxPrice.Text = "";
        }

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = room.roomTypeList();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxlabel.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxPrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxlabel.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxPrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        
    }
}
