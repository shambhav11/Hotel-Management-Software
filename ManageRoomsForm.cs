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
    public partial class ManageRoomsForm : Form
    {
        Room room = new Room();
        public ManageRoomsForm()
        {
            InitializeComponent();
        }


        private void ManageRoomsForm_Load(object sender, EventArgs e)
        {
            comboBoxRoomType.DataSource = room.roomTypeList();
            comboBoxRoomType.DisplayMember = "label";
            comboBoxRoomType.ValueMember = "category_id";

            radioButtonYES.Checked = true;
            dataGridView1.DataSource = room.getRooms();

        }

        private void buttonAddRoom_Click(object sender, EventArgs e)
        {
            int rno;
            
            try
            {
                rno = Convert.ToInt32(textBoxRno.Text);
            }
            catch(Exception E)
            {
                MessageBox.Show("Incorrect Room Number (only numbers allowed)", "Incorrect Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rtype = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            string phone = textBoxPhone.Text;
            string free;
            if(radioButtonNO.Checked)
            {
                free = "NO";
            }
            else
            {
                free = "YES";
            }
            try
            {
                if (room.addRoom(rno, rtype, phone, free))
                {
                    MessageBox.Show("Room added Successfully", "Addition of Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = room.getRooms();
                    buttonClearFields.PerformClick();
                }
                else
                {
                    MessageBox.Show("Room Couldn't be added", "Addition of Room", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "Can't Add Room", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonClearFields_Click(object sender, EventArgs e)
        {
            textBoxRno.Text = "";
            comboBoxRoomType.SelectedIndex = 0;
            textBoxPhone.Text = "";
            radioButtonYES.Checked = true;
            radioButtonNO.Checked = false;


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxRno.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBoxRoomType.SelectedValue = dataGridView1.CurrentRow.Cells[1].Value;
            if (dataGridView1.CurrentRow.Cells[3].Value.ToString().ToLower().Equals("yes"))
            {
                radioButtonYES.Checked = true;
            }
            else
            {
                radioButtonNO.Checked = true;
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBoxRno.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBoxRoomType.SelectedValue = dataGridView1.CurrentRow.Cells[1].Value;
            if(dataGridView1.CurrentRow.Cells[3].Value.ToString().ToLower().Equals("yes"))
            {
                radioButtonYES.Checked = true;
            }
            else
            {
                radioButtonNO.Checked = true;
            }
        }

        private void buttonEditRoom_Click(object sender, EventArgs e)
        {
            int rno;
            try
            {
                rno = Convert.ToInt32(textBoxRno.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show("Incorrect Room Number (only numbers allowed)", "Incorrect Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            String phone = textBoxPhone.Text;
            String free;

            if(radioButtonYES.Checked)
            {
                free = "YES";
            }
            else if (radioButtonNO.Checked)
            {
                free = "NO";
            }
            else
            {
                MessageBox.Show("Please input Availability of room", " Data missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(room.editRoom(rno,type,phone,free))
            {
                MessageBox.Show("Room Details Edited Successfully", "Edit Room Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = room.getRooms();
                buttonClearFields.PerformClick();
            }
            else
            {
                MessageBox.Show("Room Details Can't be edited", "Edit Room Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRemoveRoom_Click(object sender, EventArgs e)
        {
            int rno;
            try
            {
                rno = Convert.ToInt32(textBoxRno.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show("Incorrect Room Number (only numbers allowed)", "Incorrect Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(room.deleteRoom(rno))
            {
                MessageBox.Show("Room Deleted Successfully", "Room Deletion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = room.getRooms();
                buttonClearFields.PerformClick();
            }
            else
            {
                MessageBox.Show("Can't delete room", "Room Deletion", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
