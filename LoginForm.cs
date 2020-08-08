using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel_Management_Software
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();
            String query = "SELECT * FROM users WHERE username=@usn And password=@pass";

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = textBoxUsername.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBoxPassword.Text;
            try
            {
                command.CommandText = query;
                command.Connection = conn.GetConnection();

                adapter.SelectCommand = command;

                adapter.Fill(dataTable);
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "Can't Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(dataTable.Rows.Count>0)
            {
               // MessageBox.Show("Congratulations");
                this.Hide();
                Main_Form main_Form = new Main_Form();
                main_Form.Show();
            }
            else if (textBoxUsername.Text.Trim().Equals(""))
            {
                MessageBox.Show("Enter your Username to login","Empty Username",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else if (textBoxPassword.Text.Trim().Equals(""))
            {
                MessageBox.Show("Enter your Password to login", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Enter correct username and password","Incorrect Credentials",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }


        }
    }
}
