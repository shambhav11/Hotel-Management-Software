using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Hotel_Management_Software
{
    // Class to add edit remove or retrieve details of clients
    class Client
    {
        Connect conn = new Connect();
        Reservation reservation = new Reservation();
        Room room = new Room();

        public bool insertClient(String fname, String lname, String phoneno, String country)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "INSERT INTO `clients` (`first_name`, `last_name`, `phone`, `country`) VALUES(@fnm, @lnm, @phn, @cnr)";
            command.CommandText = insertQuery;
            command.Connection = conn.GetConnection();
            
            command.Parameters.Add("@fnm", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lnm", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phoneno;
            command.Parameters.Add("@cnr", MySqlDbType.VarChar).Value = country;

            conn.OpenConnection();
            if(command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
            
        }

        public DataTable getClients()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.GetConnection();
            command.CommandText = "Select * from `clients`";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);


            return dataTable;
        }


        public bool editClient(int id,String fname, String lname, String phoneno, String country)
        {


            MySqlCommand command = new MySqlCommand();
            String editQuery = "UPDATE `clients` SET `first_name` = @fnm, `last_name` = @lnm, `phone` = @phn, `country` = @cnr WHERE (`id` = @cid)";
            command.CommandText = editQuery;
            command.Connection = conn.GetConnection();
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fnm", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lnm", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phoneno;
            command.Parameters.Add("@cnr", MySqlDbType.VarChar).Value = country;

            conn.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
            
        }

        public bool deleteClient(int id)
        {
            MySqlCommand command = new MySqlCommand();
            String deleteQuery = "DELETE FROM `clients` WHERE (`id` = @cid)";
            command.CommandText = deleteQuery;
            command.Connection = conn.GetConnection();
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = id;
            conn.OpenConnection();
            List<int> reserveIds = reservation.getreservationClient(id);
            foreach(int reserveId in reserveIds)
            {
                reservation.setRoomFreeClientDeletion(reserveId);
            }
            if (command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }

        }

    }
}
