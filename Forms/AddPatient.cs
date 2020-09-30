using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NLog;

namespace Родовые_сертификаты
{
    public partial class AddPatient : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        int client_id;
        public int BQNT = 0;
        bool insert = false;
        DataTable forTableUPD;
        DataTable woman;
        int AddCNTChild = 0;
        public DataTable Child = new DataTable();
        string oldCertif = null;
        
        public AddPatient(int id, bool insrt)
        {
            try
            {
                client_id = id;
                insert = insrt;

                InitializeComponent();

                Child.Columns.Add("id");
                Child.Columns.Add("SEX");
                Child.Columns.Add("WEIGHT");
                Child.Columns.Add("GROWTH");
                Child.Columns.Add("DIAG");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        string[] docName = { "", "Паспорт гражданина СССР", "Загранпаспорт гражданина СССР", "Свидетельство о рождении", "Удостоверение личности офицера", "Справка об освобождении из места лишения свободы", "Паспорт Минморфлота", 
                             "Военный билет солдата (матроса, сержанта, старшины)", "Дипломатический паспорт гражданина РФ", "Иностранный паспорт", "Свидетельство о регистрации ходатайства о признании иммигранта беженцем", "Вид на жительство",
                             "Удостоверение беженца в РФ", "Временное удостоверение личности гражданина РФ", "Паспорт гражданина РФ", "Загранпаспорт гражданина РФ", "Паспорт моряка", "Военный билет офицера запаса", "Иные документы, выдаваемые органами МВД" };

        private void FuncInsertMother()
        {
            try
            {
                string Mother_query = "insert into BTAL2 (DREG, DIAG, BQNT, CHK, SCERTIF, NCERTIF, DCERTIF, SNILS, LNAME, FNAME, MNAME, BDATE, ADDRESS, TDOC, SDOC, NDOC, DDOC, ODOC, SPOLICY, NPOLICY, SLEAF, NLEAF, DLEAF, NCARD, DCARD, client_id) " +
                              $"VALUE ('{dateDREG.Value.ToString("yyyy-MM-dd")}', '{tbDIAG.Text}', {tbBQNT.Text}, 1, '{tbSCERTIF.Text}', '{tbNCERTIF.Text}', '{(tbSCERTIF.Text == "" && tbNCERTIF.Text == "" ? "" : dateDCERTIF.Value.ToString("yyyy-MM-dd"))}', '{tbSNILS.Text}', '{tbLNAME.Text}', '{tbFNAME.Text}'," +
                                    $" '{tbMNAME.Text}', '{dateBDATE.Value.ToString("yyyy-MM-dd")}', '{tbADDRESS.Text}', {cbTypeDoc.SelectedIndex}, '{tbSDOC.Text}', '{tbNDOC.Text}', '{(cbTypeDoc.SelectedIndex == 0 && tbNDOC.Text == "" && tbSDOC.Text == "" ? "" : dateDDOC.Value.ToString("yyyy-MM-dd"))}', '{tbODOC.Text}', '{tbSPOLICY.Text}', '{tbNPOLICY.Text}'," +
                                    $" '{tbSLEAF.Text}', '{tbNLEAF.Text}', '{(tbSLEAF.Text.Length > 0 || tbNLEAF.Text.Length > 0 ? dateDLEAF.Value.ToString("yyyy-MM-dd") : "")}', '{tbNCARD.Text}', '{(tbNCARD.Text.Length > 0 ? dateDCARD.Value.ToString("yyyy-MM-dd") : "")}', {client_id})";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(Mother_query, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void FuncInsertChild()
        {        
            try
            {
                string Child_query = "";

                for (int i = 0; i < dgvCHILD.Rows.Count; i++)
                {
                    Child_query = $"insert into CHILD (SEX, WEIGHT, GROWTH, DIAG, mother_id, motherCertif) value ('{dgvCHILD[1,i].Value}', {dgvCHILD[2,i].Value}, {dgvCHILD[3,i].Value}, '{dgvCHILD[4,i].Value}', {client_id}, '{tbSCERTIF.Text + " " + tbNCERTIF.Text}')";

                    using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                    {
                        if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                        MySqlCommand sqlCommand = new MySqlCommand(Child_query, sqlConnection);
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

        private void FuncUpdateMother()
        {
            try
            {
                string Mother_query = $"UPDATE BTAL2 SET DREG = '{dateDREG.Value.ToString("yyyy-MM-dd")}', DIAG = '{tbDIAG.Text}', BQNT = {tbBQNT.Text}, SCERTIF = '{tbSCERTIF.Text}', NCERTIF = '{tbNCERTIF.Text}', DCERTIF = '{(tbSCERTIF.Text == "" && tbNCERTIF.Text == "" ? "" : dateDCERTIF.Value.ToString("yyyy-MM-dd"))}', " +
                                                      $" SNILS = '{tbSNILS.Text}', LNAME = '{tbLNAME.Text}', FNAME = '{tbFNAME.Text}', MNAME = '{tbMNAME.Text}', BDATE = '{dateBDATE.Value.ToString("yyyy-MM-dd")}', ADDRESS = '{tbADDRESS.Text}', TDOC = {cbTypeDoc.SelectedIndex}, SDOC = '{tbSDOC.Text}', " +
                                                      $" NDOC = '{tbNDOC.Text}', DDOC = '{(cbTypeDoc.SelectedIndex == 0 && tbNDOC.Text == "" && tbSDOC.Text == "" ? "" : dateDDOC.Value.ToString("yyyy-MM-dd"))}', ODOC = '{tbODOC.Text}', SPOLICY = '{tbSPOLICY.Text}', NPOLICY = '{tbNPOLICY.Text}', SLEAF = '{tbSLEAF.Text}', " +
                                                      $" NLEAF = '{tbNLEAF.Text}', DLEAF = '{(tbSLEAF.Text.Length > 0 || tbNLEAF.Text.Length > 0 ? dateDLEAF.Value.ToString("yyyy-MM-dd") : "")}', NCARD = '{tbNCARD.Text}', DCARD = '{(tbNCARD.Text.Length > 0 ? dateDCARD.Value.ToString("yyyy-MM-dd") : "")}'" +
                                     $" WHERE client_id = {client_id} and id = {forTableUPD.Rows[0][25].ToString()} and deleted = 0";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(Mother_query, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }

                if (oldCertif != $"{tbSCERTIF.Text + " " + tbNCERTIF.Text}")
                {
                    string Child_query = $"UPDATE CHILD SET motherCertif = '{tbSCERTIF.Text + " " + tbNCERTIF.Text}'" +
                                     $" WHERE mother_id = {client_id} and motherCertif = '{oldCertif}' and deleted = 0";

                    using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                    {
                        if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                        MySqlCommand sqlCommand = new MySqlCommand(Child_query, sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void FuncMINIUpdateMother()
        {
            try
            {
                string Mother_query = $"UPDATE BTAL2 SET BQNT = {tbBQNT.Text}" +
                                     $" WHERE client_id = {client_id} and id = {forTableUPD.Rows[0][25].ToString()} and deleted = 0";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(Mother_query, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void FuncMINIInsertChild(int i)
        {
            try
            {
                string Child_query = $"insert into CHILD (SEX, WEIGHT, GROWTH, DIAG, mother_id, motherCertif) value ('{dgvCHILD[1, i].Value}', '{dgvCHILD[2, i].Value}', {dgvCHILD[3, i].Value}, '{dgvCHILD[4, i].Value}', {client_id}, '{tbSCERTIF.Text + " " + tbNCERTIF.Text}')";

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

        private void FuncCheckChild()
        {
            try
            {
                for (int i = 0; i < dgvCHILD.Rows.Count; i++)
                {
                    string checkChild = $"select exists(select id from CHILD where id = {dgvCHILD[0, i].Value} and mother_id = {client_id} and deleted = 0)"; //SEX = '{dgvCHILD[1, i].Value}' and WEIGHT = '{dgvCHILD[2, i].Value}' and GROWTH = {dgvCHILD[3, i].Value} and DIAG = '{dgvCHILD[4, i].Value}'

                    using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                    {
                        if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                        using (MySqlCommand sqlCommand = new MySqlCommand(checkChild, sqlConnection))
                        {
                            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                            {
                                reader.Read();
                                if (reader.GetInt32(0) == 0) FuncMINIInsertChild(i);
                                else FuncMINIUpdateChild(i);
                            }
                        }
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

        private void FuncMINIUpdateChild(int i)
        {
            try
            {
                string Child_query = $"UPDATE CHILD SET SEX = '{dgvCHILD[1, i].Value}', WEIGHT = '{dgvCHILD[2, i].Value}', GROWTH = {dgvCHILD[3, i].Value}, DIAG = '{dgvCHILD[4, i].Value}'" +
                                               $" WHERE mother_id = {client_id} and id = {dgvCHILD[0, i].Value} and deleted = 0";

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
        
        private void AddPatient_Load(object sender, EventArgs e)
        {
            try
            {
                cbTypeDoc.DataSource = docName;

                dgvCHILD.AutoGenerateColumns = false;
                dgvCHILD.DataSource = Child;

                if (insert == true)
                {
                    woman = LoadClass.FuncINSRT(client_id);

                    tbLNAME.Text = woman.Rows[0][0].ToString();
                    tbFNAME.Text = woman.Rows[0][1].ToString();
                    tbMNAME.Text = woman.Rows[0][2].ToString();
                    dateBDATE.Value = DateTime.Parse(woman.Rows[0][3].ToString());
                    tbADDRESS.Text = woman.Rows[0][4].ToString();
                    tbSNILS.Text = woman.Rows[0][5].ToString();
                    tbSPOLICY.Text = woman.Rows[0][6].ToString();
                    tbNPOLICY.Text = woman.Rows[0][7].ToString();
                    cbTypeDoc.SelectedIndex = int.Parse(woman.Rows[0][8].ToString());
                    tbSDOC.Text = woman.Rows[0][9].ToString();
                    tbNDOC.Text = woman.Rows[0][10].ToString();
                    dateDDOC.Value = DateTime.Parse(woman.Rows[0][11].ToString());
                    tbODOC.Text = woman.Rows[0][12].ToString();
                    tbSCERTIF.Text = woman.Rows[0][13].ToString();
                    tbNCERTIF.Text = woman.Rows[0][14].ToString();
                    dateDCERTIF.Value = DateTime.Parse(woman.Rows[0][15].ToString());
                    tbSLEAF.Text = woman.Rows[0][16].ToString();
                    tbNLEAF.Text = woman.Rows[0][17].ToString();
                    dateDLEAF.Value = DateTime.Parse(woman.Rows[0][18].ToString());

                }
                else
                {
                    forTableUPD = InsertUpdateClass.FuncMotherUPD(client_id);

                    dateDREG.Value = DateTime.Parse(forTableUPD.Rows[0][0].ToString());
                    tbDIAG.Text = forTableUPD.Rows[0][1].ToString();
                    tbBQNT.Text = forTableUPD.Rows[0][2].ToString();
                    tbLNAME.Text = forTableUPD.Rows[0][3].ToString();
                    tbFNAME.Text = forTableUPD.Rows[0][4].ToString();
                    tbMNAME.Text = forTableUPD.Rows[0][5].ToString();
                    dateBDATE.Value = DateTime.Parse(forTableUPD.Rows[0][6].ToString());
                    tbADDRESS.Text = forTableUPD.Rows[0][7].ToString();
                    tbSNILS.Text = forTableUPD.Rows[0][8].ToString();
                    tbSPOLICY.Text = forTableUPD.Rows[0][9].ToString();
                    tbNPOLICY.Text = forTableUPD.Rows[0][10].ToString();
                    cbTypeDoc.SelectedIndex = int.Parse(forTableUPD.Rows[0][11].ToString());
                    tbSDOC.Text = forTableUPD.Rows[0][12].ToString();
                    tbNDOC.Text = forTableUPD.Rows[0][13].ToString();
                    dateDDOC.Value = DateTime.Parse(forTableUPD.Rows[0][14].ToString());
                    tbODOC.Text = forTableUPD.Rows[0][15].ToString();
                    tbSCERTIF.Text = forTableUPD.Rows[0][16].ToString();
                    tbNCERTIF.Text = forTableUPD.Rows[0][17].ToString();
                    dateDCERTIF.Value = DateTime.Parse(forTableUPD.Rows[0][18].ToString());
                    tbSLEAF.Text = forTableUPD.Rows[0][19].ToString();
                    tbNLEAF.Text = forTableUPD.Rows[0][20].ToString();
                    dateDLEAF.Value = DateTime.Parse(forTableUPD.Rows[0][21].ToString());
                    tbNCARD.Text = forTableUPD.Rows[0][22].ToString();
                    dateDCARD.Value = DateTime.Parse(forTableUPD.Rows[0][23].ToString());

                    BQNT = int.Parse(tbBQNT.Text);
                    Child = InsertUpdateClass.FuncChildUPD(client_id, tbSCERTIF.Text + " " + tbNCERTIF.Text);
                    dgvCHILD.DataSource = Child;

                    AddCNTChild = dgvCHILD.Rows.Count;
                    oldCertif = $"{tbSCERTIF.Text + " " + tbNCERTIF.Text}";
                    this.Text = "Редактирование записи";
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
                if (insert == true)
                {
                    if (dgvCHILD.Rows.Count > 0)
                    {
                        FuncInsertMother();
                        FuncInsertChild();

                        MessageBox.Show("Данные успешно сохранены", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else MessageBox.Show("Нет информации о новорожденных", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (dgvCHILD.Rows.Count > 0)
                    {
                        FuncUpdateMother();
                        FuncCheckChild();

                        MessageBox.Show("Данные успешно сохранены", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else MessageBox.Show("Нет информации о новорожденных", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
        
        private void TbBQNT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbBQNT.Text != "") BQNT = int.Parse(tbBQNT.Text);
                else BQNT = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void DgvCHILD_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete && insert == false)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить данную запись?", "Удаление выделенной записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        DeleteClass.deleteChild(client_id, int.Parse(dgvCHILD.CurrentRow.Cells[0].Value.ToString()), tbSCERTIF.Text + " " + tbNCERTIF.Text);

                        if (BQNT - 1 == 0) tbBQNT.Text = "0";
                        else tbBQNT.Text = string.Format("{0}", BQNT - 1);

                        FuncMINIUpdateMother();

                        dgvCHILD.AutoGenerateColumns = false;
                        dgvCHILD.DataSource = null;
                        dgvCHILD.DataSource = InsertUpdateClass.FuncChildUPD(client_id, tbSCERTIF.Text + " " + tbNCERTIF.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void AddChild_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                using (AddChildForm acForm = new AddChildForm(true, dgvCHILD.Rows.Count == 0 ? 0 : int.Parse(dgvCHILD[0, dgvCHILD.Rows.Count - 1].Value.ToString())))
                {
                    acForm.Owner = this;
                    acForm.ShowDialog();
                }

                tbBQNT.Text = Convert.ToString(BQNT);
                dgvCHILD.Refresh();
                AddCNTChild = dgvCHILD.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void dgvCHILD_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvCHILD.Rows.Count > 0)
            {
                using (AddChildForm acForm = new AddChildForm(false, dgvCHILD.CurrentRow.Index))
                {
                    acForm.Owner = this;
                    acForm.ShowDialog();
                }
            }
        }
    }
}
