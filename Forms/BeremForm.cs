using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using MySql.Data.MySqlClient;

namespace Родовые_сертификаты
{
    public partial class BeremForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        DataTable tableBerem;
        DataTable CertifBerem;

        string query_str = "";
        static string snils = "";
        string strDBorn = "";
        string firstName = "";
        string lastName = "";
        string patrName = "";

        private void FuncLoad()
        {
            try
            {
                tableBerem = new DataTable();
                query_str = "";

                if (lastName != "") query_str += lastName;

                if (firstName != "") query_str += firstName;

                if (patrName != "") query_str += patrName;

                if (strDBorn != "") query_str += strDBorn;

                if (snils != "")
                {
                    if (maskSNILS.Text.Length < 11)
                    {
                        MessageBox.Show("Проверьте корректность номера СНИЛС", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else query_str += snils;
                }

                string query = "SELECT DISTINCT c.id," +
                                     " c.lastName LName," +
                                     " c.firstName FName," +
                                     " c.patrName MName," +
                                     " DATE_FORMAT(c.birthDate, '%d.%m.%Y') dateBorn," +
                                     " IF(c.SNILS = '', '', CONCAT(SUBSTRING(c.SNILS, 1, 3), '-', SUBSTRING(c.SNILS, 4, 3), '-', SUBSTRING(c.SNILS, 7, 3), ' ', SUBSTRING(c.SNILS, 10, 2))) SNILS," +
                                     " z_getClientRodDocument(c.id) rodCertif," +
                                     " z_getClientRodDocumentDate(c.id) dateCertif," +
                                     " z_getClientRodDocumentWhoGive(c.id) whoGiveCertif" +
                              " FROM Client c" +
                              " JOIN Event e ON c.id = e.client_id" +
                              " JOIN ClientIdentification ci ON c.id = ci.client_id" +
                              " WHERE c.sex = 2" +
                                " AND e.deleted = 0" +
                               $" AND e.eventType_id in ({Properties.Settings.Default.eventType_id})" +
                                " AND ci.accountingSystem_id = 11" +
                               $" {query_str}";

                /*string query = "SELECT c.id," +
                                     " c.lastName LName," +
                                     " c.firstName FName," +
                                     " c.patrName MName," +
                                     " DATE_FORMAT(c.birthDate, '%d.%m.%Y') dateBorn," +
                                     " IF(c.SNILS = '', '', CONCAT(SUBSTRING(c.SNILS, 1, 3), '-', SUBSTRING(c.SNILS, 4, 3), '-', SUBSTRING(c.SNILS, 7, 3), ' ', SUBSTRING(c.SNILS, 10, 2))) SNILS," +
                                     " CONCAT(cd.serial, ' ', cd.number) rodCertif," +
                                     " date_FORMAT(cd.date, '%d.%m.%Y') dateCertif," +
                                     " cd.origin whoGiveCertif," +
                                     " age(c.birthDate, CURRENT_DATE()) age_" +
                              " FROM Client c" +
                              " LEFT JOIN ClientDocument cd ON cd.client_id = c.id AND cd.documentType_id = 22 AND cd.deleted = 0" +
                             $" WHERE c.sex = {sex}" +
                               $" {query_str}" +
                              " HAVING age_ BETWEEN 10 AND 60";*/

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                    tableBerem.Load(sqlCommand.ExecuteReader());
                }

                dataBerem.AutoGenerateColumns = false;
                dataBerem.DataSource = tableBerem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void LoadBeremCertif()
        {
            try
            {
                string query = "SELECT CONCAT(SCERTIF, ' ', NCERTIF) CERTIF," +
                                     " IF(DCERTIF = '00.00.0000', '', DATE_FORMAT(DCERTIF, '%d.%m.%Y')) DCERTIF," +
                                     " SNILS," +
                                     " CONCAT(LNAME, ' ', FNAME, ' ', MNAME) FIO_berem," +
                                     " DATE_FORMAT(BDATE, '%d.%m.%Y') BDATE," +
                                     " ADDRESS," +
                                     " CONCAT('Серия ', SDOC, ', номер ', NDOC, ', дата выдачи ', DATE_FORMAT(DDOC, '%d.%m.%Y'), ', выдан ', ODOC) DocType," + //" IF(SDOC <> '' and NDOC <> '' and DDOC <> '00.00.0000' and ODOC <> '', CONCAT('Серия ', SDOC, ', номер ', NDOC, ', дата выдачи ', DATE_FORMAT(DDOC, '%d.%m.%Y'), ', выдан ', ODOC), '') DocType," +
                                     " IF(SPOLICY = '', NPOLICY, CONCAT(SPOLICY, ' ', NPOLICY)) POLICY," +
                                     " IF(SLEAF = '' AND NLEAF = '', '', CONCAT(SLEAF, ' ', NLEAF, ', выдан ', DLEAF)) SICKLIST," +
                                     " DATE_FORMAT(DREG, '%d.%m.%Y') DREG," +
                                     " IF(QTW < 20, 0, QTW - 19) fromQTW," +
                                     " QTW," +
                                     " client_id," +
                                     " id" +
                              " FROM BTAL1 b1" +
                              " where deleted = 0";

                CertifBerem = new DataTable();
                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                    CertifBerem.Load(sqlCommand.ExecuteReader());
                }

                dataCertifRod.AutoGenerateColumns = false;
                dataCertifRod.DataSource = CertifBerem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void AddBeremBTN()
        {
            try
            {
                using (AddBerem abForm = new AddBerem(int.Parse(dataBerem.CurrentRow.Cells[0].Value.ToString()), true))
                {
                    abForm.ShowDialog();
                }
                LoadBeremCertif();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void EditBeremBTN()
        {
            try
            {
                using (AddBerem abForm = new AddBerem(int.Parse(dataCertifRod.CurrentRow.Cells[12].Value.ToString()), false))
                {
                    abForm.ShowDialog();
                }
                LoadBeremCertif();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        public BeremForm()
        {
            InitializeComponent();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                int check = 0;
                foreach (Control control in this.groupBox2.Controls)//this.Controls) //Перебираем все контролы на форме
                    if (control is CheckBox) //Если контрол - чекбокс, 
                    {
                        CheckBox checkBox = (CheckBox)control;

                        if (checkBox.Checked) check = 1;
                    }
                    else continue;

                if (check == 1)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    FuncLoad();
                    Cursor.Current = Cursors.Default;
                    check = 0;
                }
                else MessageBox.Show("Вы не выбрали ни одного фильтра для поиска, данный запрос не будет обработан. Пожалуйста, установите фильтр для поиска данных.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
        private void BeremForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0) AddEditBTN.Text = "Добавить";
                else AddEditBTN.Text = "Редактировать";

                tbLName.Enabled = false;
                tbFName.Enabled = false;
                tbMName.Enabled = false;
                dBorn.Enabled = false;
                maskSNILS.Enabled = false;
                LoadBeremCertif();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void CBLName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cBLName.Checked == true) tbLName.Enabled = true;
                else
                {
                    tbLName.Enabled = false;
                    tbLName.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
        
        private void CBFName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cBFName.Checked == true) tbFName.Enabled = true;
                else
                {
                    tbFName.Enabled = false;
                    tbFName.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void CBMName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cBMName.Checked == true) tbMName.Enabled = true;
                else
                {
                    tbMName.Enabled = false;
                    tbMName.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void CBBDate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cBBDate.Checked == true) dBorn.Enabled = true;
                else
                {
                    dBorn.Enabled = false;
                    strDBorn = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void CBSNILS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cBSNILS.Checked == true) maskSNILS.Enabled = true;
                else
                {
                    maskSNILS.Enabled = false;
                    maskSNILS.Clear();
                    snils = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }      

        private void MaskSNILS_TextChanged(object sender, EventArgs e)
        {
            try
            {
                maskSNILS.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                snils = $" and c.SNILS like '{maskSNILS.Text}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void TbLName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lastName = $" and lastName like '{tbLName.Text}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void DBorn_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                strDBorn = $" and birthDate = '{dBorn.Value.ToString("yyyy-MM-dd")}'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void DBorn_CloseUp(object sender, EventArgs e)
        {
            try
            {
                strDBorn = $" and birthDate = '{dBorn.Value.ToString("yyyy-MM-dd")}'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void TbFName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                firstName = $" and firstName like '{tbFName.Text}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void TbMName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                patrName = $" and patrName like '{tbMName.Text}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void DataBerem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AddBeremBTN();
        }

        private void DataCertifRod_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditBeremBTN();
        }

        private void deleteBerem()
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить данную запись?", "Удаление выделенной записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string delBerem = $"UPDATE BTAL1 SET deleted = 1" +
                                          $" WHERE client_id = {dataCertifRod.CurrentRow.Cells[11].Value.ToString()} and id = {dataCertifRod.CurrentRow.Cells[12].Value.ToString()}";

                    using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                    {
                        if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                        MySqlCommand sqlCommand = new MySqlCommand(delBerem, sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
        private void DataCertifRod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить данную запись?", "Удаление выделенной записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        deleteBerem();
                        LoadBeremCertif();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) AddEditBTN.Text = "Добавить";
            else AddEditBTN.Text = "Редактировать";
        }

        private void AddEditBTN_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (dataBerem.Rows.Count > 0) AddBeremBTN();
                else MessageBox.Show("Нет данных для добавления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dataCertifRod.Rows.Count > 0) EditBeremBTN();
                else MessageBox.Show("Нет данных для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
