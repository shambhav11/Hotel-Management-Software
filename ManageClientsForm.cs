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
    public partial class ManageClientsForm : Form
    {
        Client client = new Client();
        public ManageClientsForm()
        {
            InitializeComponent();
        }

        private void buttonClearFields_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhone.Text = "";
            textBoxCountry.Text = "";
        }

        private void buttonAddClient_Click(object sender, EventArgs e)
        {
            String fname = textBoxFirstName.Text;
            String lname = textBoxLastName.Text;
            String phone = textBoxPhone.Text;
            String country = textBoxCountry.Text;
            if ((fname.Trim().Equals("") || lname.Trim().Equals("") || phone.Trim().Equals("") || country.Trim().Equals("")))
            {
                MessageBox.Show("All Fields except ID are compulsory for adding", "Addition of Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                Boolean insertclient = client.insertClient(fname, lname, phone, country);


                if (insertclient)
                {
                    dataGridView1.DataSource = client.getClients();

                    MessageBox.Show("New Client Inserted Successful", "Addition of Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonClearFields.PerformClick();

                }
                else
                {
                    MessageBox.Show("Error in adding Client", "Addition of Client", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void ManageClientsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = client.getClients();
        }

        private void buttonEditClient_Click(object sender, EventArgs e)
        {
            int id;
            try
            {
                id = Convert.ToInt32(textBoxID.Text);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch(Exception E)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                MessageBox.Show("Select a user Entry to edit", "Editing Client Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String fname = textBoxFirstName.Text;
            String lname = textBoxLastName.Text;
            String phone = textBoxPhone.Text;
            String country = textBoxCountry.Text;
            if ((fname.Trim().Equals("") || lname.Trim().Equals("") || phone.Trim().Equals("") || country.Trim().Equals("")))
            {
                MessageBox.Show("All Fields except ID are compulsory for editing", "Editing Client Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                Boolean editclient = client.editClient(id,fname, lname, phone, country);


                if (editclient)
                {
                    dataGridView1.DataSource = client.getClients();

                    MessageBox.Show("Client Data Edited Successfully", "Editing Client Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonClearFields.PerformClick();

                }
                else
                {
                    MessageBox.Show("Error in editing data", "Editing Client Data", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxCountry.Text = dataGridView1.CurrentRow.Cells[4  ].Value.ToString();
        }

        private void buttonRemoveClient_Click(object sender, EventArgs e)
        {
            int id;
            try { 
            id = Convert.ToInt32(textBoxID.Text);
            }
            catch(Exception E)
            {
                MessageBox.Show("Select a user Entry to delete", "Removal of Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Boolean deleteclient = client.deleteClient(id);


            if (deleteclient)
            {
                dataGridView1.DataSource = client.getClients();

                MessageBox.Show("Client Data Deleted", "Removal of Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonClearFields.PerformClick();
            }
            else
            {
                MessageBox.Show("Error in deleting Client", "Removal of Client", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


            
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxCountry.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxCountry_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
