using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NLog;
using MySql.Data.MySqlClient;
using System.Data;

namespace Родовые_сертификаты
{
    public partial class ConnectionSettingsForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        List<string> dbName = new List<string>();
        bool flagNew = false;

        public ConnectionSettingsForm()
        {
            InitializeComponent();
        }

        private void ConnectionSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDBDefault();

                txtServer.Text = Properties.Settings.Default.server;
                cbDB.SelectedItem = Properties.Settings.Default.database;
                txtLogin.Text = Properties.Settings.Default.user;
                txtPass.Text = Properties.Settings.Default.password;
                eventTypeTB.Text = Properties.Settings.Default.eventType_id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void LoadDBDefault()
        {
            try
            {
                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand("SHOW DATABASES", sqlConnection);
                    MySqlDataReader reader = sqlCommand.ExecuteReader();
                    dbName.Clear();
                    while (reader.Read())
                    {
                        dbName.Add(reader.GetString(0));
                    }

                    cbDB.DataSource = null;
                    cbDB.DataSource = dbName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void LoadDB()
        {
            try
            {
                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionSettings(txtServer.Text, txtLogin.Text, txtPass.Text))
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand("SHOW DATABASES", sqlConnection);
                    MySqlDataReader reader = sqlCommand.ExecuteReader();
                    dbName.Clear();
                    while (reader.Read())
                    {
                        dbName.Add(reader.GetString(0));
                    }
                    cbDB.DataSource = null;
                    cbDB.DataSource = dbName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.server = txtServer.Text;
                Properties.Settings.Default.database = cbDB.SelectedItem.ToString();
                Properties.Settings.Default.user = txtLogin.Text;
                Properties.Settings.Default.password = txtPass.Text;
                Properties.Settings.Default.eventType_id = eventTypeTB.Text;
                Properties.Settings.Default.Save();

                MessageBox.Show("Настройки сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }

        }

        private void BtnCheckConnect_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionSettings(txtServer.Text, txtLogin.Text, txtPass.Text))
                {
                    sqlConnection.Open();
                    if (sqlConnection.State == ConnectionState.Open) MessageBox.Show("Соединение успешно установлено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else MessageBox.Show("Проверьте правильность введенных данных", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void TxtServer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtServer.Text != Properties.Settings.Default.server)
                {
                    flagNew = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void CbDB_DropDown(object sender, EventArgs e)
        {
            try
            {
                if (flagNew == true) LoadDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
    }
}
