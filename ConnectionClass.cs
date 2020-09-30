using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NLog;

namespace Родовые_сертификаты
{
    class ConnectionClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static MySqlConnection GetStringConnection()
        {
            try
            {
                MySqlConnection sqlConnection = new MySqlConnection($"server={Properties.Settings.Default.server};user id={Properties.Settings.Default.user}; password = {Properties.Settings.Default.password}; database={Properties.Settings.Default.database}");
                return sqlConnection;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
                return null;
            }
        }

        public static MySqlConnection GetStringConnectionTable()
        {
            try
            {
                MySqlConnection sqlConnection = new MySqlConnection($"server=172.16.16.172;user id=root; password = fg67klbn0; database=RodCertificate");
                return sqlConnection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
                return null;
            }
        }

        public static MySqlConnection GetStringConnectionSettings(string server, string user, string password)
        {
            try
            {
                MySqlConnection sqlConnection = new MySqlConnection($"server={server};user id={user}; password = {password}");
                return sqlConnection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
                return null;
            }
        }
    }
}
