using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;

namespace Hotel_Management_Software
{
    class Room
    {

        //Class to handle all room operations
        // like adding rooms, editing room details etc
        Connect conn = new Connect();

        public DataTable roomTypeList()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.GetConnection();
            command.CommandText = "Select * from `rooms_category`";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);


            return dataTable;
        }

        public DataTable getRoomByType(int type)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.GetConnection();
            command.CommandText = "Select * from `rooms` WHERE(`roomType` = @rtp and `free` = 'YES' )";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();
            command.Parameters.Add("@rtp", MySqlDbType.Int32).Value = type; 
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            


            return dataTable;
        }

        public DataTable getRoomByTypeforEdit(int type,int rno)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.GetConnection();
            command.CommandText = "Select * from `rooms` WHERE(`roomType` = @rtp and `free` = 'YES' )";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();
            command.Parameters.Add("@rtp", MySqlDbType.Int32).Value = type;
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            DataRow dr=dataTable.NewRow();
            dr["roomNumber"] = rno;
            dr["roomType"] = type;
            dr["free"] = "NO";
            dataTable.Rows.Add(dr);
            return dataTable;
        }

        public double calculatePrice(int rType, int numberDays)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.GetConnection();
            command.CommandText = "Select `price` from `rooms_category` WHERE (`category_id` = @rtp)";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();
            command.Parameters.Add("@rtp", MySqlDbType.Int32).Value = rType;
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            double price = Convert.ToDouble(dataTable.Rows[0][0].ToString());
            
            if(numberDays>0)
            {
                double cost = price * numberDays;
                return (Math.Floor(cost) == cost )? cost: Math.Floor(cost+1);
            }
            else if(numberDays == 0)
            {
                double cost = price * 0.75;
                return (Math.Floor(cost) == cost) ? cost : Math.Floor(cost+1);
            }
            else
            {
                throw new InvalidDataException();
            }
        }


        public int getRoomTypeforEdit(int rno)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.GetConnection();
            command.CommandText = "Select `roomType` from `rooms` WHERE(`roomNumber` = @rno)";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();
            command.Parameters.Add("@rno", MySqlDbType.Int32).Value = rno;
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            return Convert.ToInt32(dataTable.Rows[0][0].ToString());
        }

        public DataTable getRooms()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.GetConnection();
            command.CommandText = "Select * from `rooms`";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);


            return dataTable;
        }

        public bool addRoom(int rno, int type, String phoneno, String free)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "INSERT INTO `rooms` (`roomNumber`, `roomType`, `phone`, `free`) VALUES(@rmn, @rtp, @phn, @fre)";
            command.CommandText = insertQuery;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@rmn", MySqlDbType.Int32).Value = rno;
            command.Parameters.Add("@rtp", MySqlDbType.Int32).Value = type;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phoneno;
            command.Parameters.Add("@fre", MySqlDbType.VarChar).Value = free;

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

        public bool addRoomCategory(String label, double price)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "INSERT INTO `rooms_category` (`label`, `price`) VALUES(@lbl, @pri)";
            command.CommandText = insertQuery;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@lbl", MySqlDbType.VarChar).Value = label;
            command.Parameters.Add("@pri", MySqlDbType.VarChar).Value = price.ToString();
            

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



        public bool deleteRoom(int rno)
        {


            MySqlCommand command = new MySqlCommand();
            String deleteQuery = "DELETE FROM `rooms` WHERE (`roomNumber` = @rno)";
            command.CommandText = deleteQuery;
            command.Connection = conn.GetConnection();
            command.Parameters.Add("@rno", MySqlDbType.Int32).Value = rno;

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

        public bool deleteRoomCategory(int rType)
        {


            MySqlCommand command = new MySqlCommand();
            String deleteQuery = "DELETE FROM `rooms_category` WHERE (`category_id` = @rtp)";
            command.CommandText = deleteQuery;
            command.Connection = conn.GetConnection();
            command.Parameters.Add("@rtp", MySqlDbType.Int32).Value = rType;

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



        public bool editRoom(int rno, int rtp, String phoneno, String free)
        {


            MySqlCommand command = new MySqlCommand();
            String editQuery = "UPDATE `rooms` SET `roomType` = @rtp, `phone` = @phn, `free` = @fre WHERE (`roomNumber` = @rno)";
            command.CommandText = editQuery;
            command.Connection = conn.GetConnection();
            command.Parameters.Add("@rno", MySqlDbType.Int32).Value = rno;
            command.Parameters.Add("@rtp", MySqlDbType.Int32).Value = rtp;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phoneno;
            command.Parameters.Add("@fre", MySqlDbType.VarChar).Value = free;

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

        public bool editRoomCategory(int rtp, String label, double price)
        {


            MySqlCommand command = new MySqlCommand();
            String editQuery = "UPDATE `rooms_category` SET `label` = @lbl, `price` = @pri WHERE (`category_id` = @rtp)";
            command.CommandText = editQuery;
            command.Connection = conn.GetConnection();
            command.Parameters.Add("@rtp", MySqlDbType.Int32).Value = rtp;
            command.Parameters.Add("@lbl", MySqlDbType.VarChar).Value = label;
            command.Parameters.Add("@pri", MySqlDbType.VarChar).Value = price.ToString();

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
        public bool editRoomAvailability(int roomNo, String free)
        {
            MySqlCommand command = new MySqlCommand();
            String editAvailabilityQuery = "UPDATE `rooms` SET `free` = @fre WHERE (`roomNumber` = @rno)";
            command.CommandText = editAvailabilityQuery;
            command.Connection = conn.GetConnection();
            command.Parameters.Add("@rno", MySqlDbType.Int32).Value = roomNo;
            command.Parameters.Add("@fre", MySqlDbType.VarChar).Value = free;

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
    }
}
