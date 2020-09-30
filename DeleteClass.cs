using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using NLog;
using System;

namespace Родовые_сертификаты
{
    class DeleteClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void deleteRod(int client_id, int id, string motherCertif)
        {
            try
            {
                string del_query = "UPDATE BTAL2 SET deleted = 1" +
                                 $" where client_id = {client_id} and id = {id}";

                string del_child_query = $"UPDATE CHILD SET deleted = 1" +
                                        $" WHERE mother_id = {client_id}" +
                                          $" and motherCertif = '{motherCertif}'";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(del_child_query, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                    sqlConnection.Open();
                    sqlCommand = new MySqlCommand(del_query, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        public static void deleteBerem(int client_id, int id)
        {
            try
            {
                string delBerem = $"UPDATE BTAL1 SET deleted = 1" +
                                 $" WHERE client_id = {client_id} and id = {id}";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(delBerem, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        public static void deleteChild(int client_id, int id, string motherCertif)
        {
            try
            {
                string Child_query = $"UPDATE CHILD SET deleted = 1" +
                                    $" WHERE mother_id = {client_id} and id = {id} and motherCertif = '{motherCertif}'";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(Child_query, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
    }
}
