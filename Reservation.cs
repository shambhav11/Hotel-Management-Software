using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;

namespace Hotel_Management_Software
{
    //Class to handle all reservation activities

    class Reservation
    {
        Connect conn = new Connect();
        Room room = new Room();

        public DataTable getReservations()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.GetConnection();
            command.CommandText = "Select * from `reservations`";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);


            return dataTable;
        }

        public bool addReservation(int rno, int clientId, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "INSERT INTO `reservations` (`roomNumber`, `clientId`, `DateIn`, `DateOut`) VALUES (@rmn, @cid,@din,@dout)";

            command.CommandText = insertQuery;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@rmn", MySqlDbType.Int32).Value = rno;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = clientId;
            command.Parameters.Add("@din", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@dout", MySqlDbType.Date).Value = dateOut;

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

        public bool editReservation(int reserveId,int rno, int clientId, DateTime dateIn, DateTime dateOut)
        {


            MySqlCommand command = new MySqlCommand();
            String editQuery = "UPDATE `reservations` SET `roomNumber` = @rmn, `clientId` = @cid, `DateIn` = @din, `DateOut` = @dout WHERE (`reservation_id` = @rid)";
            command.CommandText = editQuery;
            command.Connection = conn.GetConnection();
            command.Parameters.Add("@rid", MySqlDbType.Int32).Value = reserveId;
            command.Parameters.Add("@rmn", MySqlDbType.Int32).Value = rno;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = clientId;
            command.Parameters.Add("@din", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@dout", MySqlDbType.Date).Value = dateOut;

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

        public bool editReservationCheckout(int reserveId, DateTime dateOut)
        {


            MySqlCommand command = new MySqlCommand();
            String editQuery = "UPDATE `reservations` SET `DateOut` = @dout WHERE (`reservation_id` = @rid)";
            command.CommandText = editQuery;
            command.Connection = conn.GetConnection();
            command.Parameters.Add("@rid", MySqlDbType.Int32).Value = reserveId;
            command.Parameters.Add("@dout", MySqlDbType.Date).Value = dateOut;

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





        public bool deleteReservation(int reserveId)
        {


            MySqlCommand command = new MySqlCommand();
            String deleteQuery = "DELETE FROM `reservations` WHERE (`reservation_id` = @rid)";
            command.CommandText = deleteQuery;
            command.Connection = conn.GetConnection();
            command.Parameters.Add("@rid", MySqlDbType.Int32).Value = reserveId;

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

        public List<int> getreservationClient(int clientId)
        {
            List<int> reserveIds = new List<int>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.GetConnection();
            command.CommandText = "Select `reservation_id` from `reservations` WHERE(`clientId` = @cid)";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = clientId;
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            foreach(DataRow item in dataTable.Rows)
            {
                reserveIds.Add(Convert.ToInt32(item["reservation_id"]));
            }
            return reserveIds;
        }

        public bool setRoomFreeClientDeletion(int reservationId)
        {
            List<int> reserveIds = new List<int>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn.GetConnection();
            command.CommandText = "Select `roomNumber` ,`dateOut` from `reservations` WHERE(`reservation_id` = @rid)";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();
            command.Parameters.Add("@rid", MySqlDbType.Int32).Value = reservationId;
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            foreach (DataRow item in dataTable.Rows)
            {
                DateTime dateTime = Convert.ToDateTime(item["dateOut"]);
                if(DateTime.Compare(dateTime.Date,DateTime.Now.Date) >= 0)
                {
                    room.editRoomAvailability(Convert.ToInt32(item["roomNumber"]), "YES");
                    return true;
                }
            }
            return false;
        }





    }
}
