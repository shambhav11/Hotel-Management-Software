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
    public partial class ManageReservationsForm : Form
    {
        Room room = new Room();
        Reservation reservation = new Reservation();
        int formerrno;
        public ManageReservationsForm()
        {
            InitializeComponent();
        }

        private void buttonClearFields_Click(object sender, EventArgs e)
        {
            textBoxClientId.Text = "";
            textBoxReserveId.Text = "";
            comboBoxRoomType.SelectedIndex = 0;
            comboBoxRoomNumber.DataSource = room.getRoomByType(Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString()));
            dateTimePickerIn.Value = DateTime.Now;
            dateTimePickerOut.Value = DateTime.Now;
        }

        private void ManageReservationsForm_Load(object sender, EventArgs e)
        {
            comboBoxRoomType.DataSource = room.roomTypeList();
            comboBoxRoomType.DisplayMember = "label";
            comboBoxRoomType.ValueMember = "category_id";
            int type;
            try
            {
                if (comboBoxRoomType != null && comboBoxRoomType.SelectedValue != null)
                {
                    type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
                    comboBoxRoomNumber.DataSource = room.getRoomByType(type);
                }


            }
            catch (Exception E)
            {
                type = 0;
                comboBoxRoomNumber.DataSource = room.getRoomByType(type);

            }

            comboBoxRoomNumber.DisplayMember = "roomNumber";
            comboBoxRoomNumber.ValueMember = "roomNumber";

            dataGridView1.DataSource = reservation.getReservations();

        }

        private void comboBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { 
            int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            comboBoxRoomNumber.DataSource = room.getRoomByType(type);
            comboBoxRoomNumber.DisplayMember = "roomNumber";
            comboBoxRoomNumber.ValueMember = "roomNumber";
            }
            catch(Exception)
            {

            }
        }

        private void buttonAddReservation_Click(object sender, EventArgs e)
        {
            int cid;

            try
            {
                cid = Convert.ToInt32(textBoxClientId.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show("Incorrect Client ID (only numbers allowed)", "Incorrect Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int rtype = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            int roomNo = Convert.ToInt32(comboBoxRoomNumber.SelectedValue.ToString());
            DateTime dateIn = dateTimePickerIn.Value;
            DateTime dateOut = dateTimePickerOut.Value;
            Console.WriteLine(dateIn + " : " + DateTime.Now);
            if(DateTime.Compare(dateIn.Date,DateTime.Now.Date)<0)
            {
                MessageBox.Show("Checkin Date should be today or after today", "Incorrect Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(DateTime.Compare(dateOut.Date,dateIn.Date)<0)
            {
                MessageBox.Show("Checkout Date should be after checkin", "Incorrect Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (reservation.addReservation(roomNo, cid, dateIn, dateOut))
                {
                    room.editRoomAvailability(roomNo, "NO");
                    MessageBox.Show("Reservation added Successfully", "Addition of Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = reservation.getReservations();
                    buttonClearFields.PerformClick();
                    
                }
                else
                {
                    MessageBox.Show("Reservation Couldn't be added", "Addition of Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch(Exception E)
            {
                    MessageBox.Show(E.Message, "Can't Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonEditReservation_Click(object sender, EventArgs e)
        {
            int cid;

            try
            {
                cid = Convert.ToInt32(textBoxClientId.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show("Incorrect Client ID (only numbers allowed)", "Incorrect Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int reserveId;
            try
            {
               reserveId = Convert.ToInt32(textBoxReserveId.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect Reservation ID", "Deleting Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int rtype = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            int roomNo = Convert.ToInt32(comboBoxRoomNumber.SelectedValue.ToString());
            DateTime dateIn = dateTimePickerIn.Value;
            DateTime dateOut = dateTimePickerOut.Value;
            
            if (dateOut < dateIn)
            {
                MessageBox.Show("Checkout Date should be after checkin", "Incorrect Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if(formerrno != roomNo)
                {
                    var selectedOption = MessageBox.Show("Confirm change in room number", "Editing Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(selectedOption != DialogResult.Yes)
                    {
                        roomNo = formerrno;
                    }
                    
                }
                if (reservation.editReservation(reserveId,roomNo, cid, dateIn, dateOut))
                {
                    if (DateTime.Compare(dateOut.Date, DateTime.Now.Date) > 0)
                    {
                        room.editRoomAvailability(formerrno, "YES");
                        room.editRoomAvailability(roomNo, "NO");
                    }
                    MessageBox.Show("Reservation edited Successfully", "Editing Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = reservation.getReservations();
                    buttonClearFields.PerformClick();

                }
                else
                {
                    MessageBox.Show("Reservation Couldn't be edited", "Editing Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Can't Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            }
            catch(Exception)
            {

            }
            textBoxReserveId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxClientId.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            String roomNumber = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            int rno = Convert.ToInt32(roomNumber);
            formerrno = rno;
            comboBoxRoomType.SelectedValue = room.getRoomTypeforEdit(rno);
            comboBoxRoomNumber.DataSource = room.getRoomByTypeforEdit(Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString()),rno);
            comboBoxRoomNumber.SelectedValue = roomNumber;
            dateTimePickerIn.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value);
            dateTimePickerOut.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value);


        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {

            }
            textBoxReserveId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxClientId.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            String roomNumber = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            int rno = Convert.ToInt32(roomNumber);
            formerrno = rno;
            comboBoxRoomType.SelectedValue = room.getRoomTypeforEdit(rno);
            comboBoxRoomNumber.DataSource = room.getRoomByTypeforEdit(Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString()),rno);
            comboBoxRoomNumber.SelectedValue = roomNumber;
            dateTimePickerIn.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value);
            dateTimePickerOut.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value);

        }

        private void buttonRemoveReservation_Click(object sender, EventArgs e)
        {
            int reserveId;
            try
            {
                reserveId = Convert.ToInt32(textBoxReserveId.Text.ToString());
            }
            catch(Exception)
            {
                MessageBox.Show("Incorrect Reservation ID", "Deleting Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (reservation.deleteReservation(reserveId))
                {
                    room.editRoomAvailability(formerrno, "YES");
                    MessageBox.Show("Reservation deleted Successfully", "Deleting Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = reservation.getReservations();
                    buttonClearFields.PerformClick();

                }
                else
                {
                    MessageBox.Show("Reservation Couldn't be deleted", "Deleting Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Can't Delete Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonCheckout_Click(object sender, EventArgs e)
        {
            int reserveId;
            try
            {
                reserveId = Convert.ToInt32(textBoxReserveId.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect Reservation ID", "Deleting Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if(DateTime.Compare(dateTimePickerIn.Value.Date,DateTime.Now.Date)>0)
                {
                    MessageBox.Show("Can't checkout since the person hasn't checked in yet", "Checking out Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (reservation.editReservationCheckout(reserveId,DateTime.Now))
                {
                    room.editRoomAvailability(formerrno, "YES");
                    MessageBox.Show("Reservation Cheeckedout Successfully", "Checking out Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = reservation.getReservations();
                    buttonClearFields.PerformClick();

                }
                else
                {
                    MessageBox.Show("Reservation Couldn't be checked Out", "Checking Out Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Can't Checkout Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonCheckPrice_Click(object sender, EventArgs e)
        {
            try
            {
                int rType = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
                int numDays = (dateTimePickerOut.Value.Date - dateTimePickerIn.Value.Date).Days;
                Console.WriteLine(rType.ToString()+ ": "+ numDays.ToString());
                double price = room.calculatePrice(rType, numDays);

                MessageBox.Show("Price for selected room and dates : Rs " + price, "Price Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(Exception)
            {
                MessageBox.Show("Error encountered","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
        }
    }
}

