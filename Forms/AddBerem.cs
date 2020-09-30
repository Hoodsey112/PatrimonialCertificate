using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NLog;

namespace Родовые_сертификаты
{
    public partial class AddBerem : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        int client_id = 0;
        bool insert = false;
        DataTable forUpdate;
        DataTable woman;
        
        public AddBerem(int id, bool insUPD)
        {
            InitializeComponent();
            client_id = id;
            insert = insUPD;
        }

        string[] docName = { "", "Паспорт гражданина СССР", "Загранпаспорт гражданина СССР", "Свидетельство о рождении", "Удостоверение личности офицера", "Справка об освобождении из места лишения свободы", "Паспорт Минморфлота",
                             "Военный билет солдата (матроса, сержанта, старшины)", "Дипломатический паспорт гражданина РФ", "Иностранный паспорт", "Свидетельство о регистрации ходатайства о признании иммигранта беженцем", "Вид на жительство",
                             "Удостоверение беженца в РФ", "Временное удостоверение личности гражданина РФ", "Паспорт гражданина РФ", "Загранпаспорт гражданина РФ", "Паспорт моряка", "Военный билет офицера запаса", "Иные документы, выдаваемые органами МВД" };

        private void AddBerem_Load(object sender, EventArgs e)
        {
            try
            {
                cbTypeDoc.DataSource = docName;

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
                    forUpdate = InsertUpdateClass.LoadMotherUPDT_BTAL1(client_id);

                    tbLNAME.Text = forUpdate.Rows[0][1].ToString();
                    tbFNAME.Text = forUpdate.Rows[0][2].ToString();
                    tbMNAME.Text = forUpdate.Rows[0][3].ToString();
                    dateBDATE.Value = DateTime.Parse(forUpdate.Rows[0][4].ToString());
                    tbADDRESS.Text = forUpdate.Rows[0][5].ToString();
                    tbSNILS.Text = forUpdate.Rows[0][6].ToString();
                    tbSPOLICY.Text = forUpdate.Rows[0][7].ToString();
                    tbNPOLICY.Text = forUpdate.Rows[0][8].ToString();
                    cbTypeDoc.SelectedIndex = int.Parse(forUpdate.Rows[0][9].ToString());
                    tbSDOC.Text = forUpdate.Rows[0][10].ToString();
                    tbNDOC.Text = forUpdate.Rows[0][11].ToString();
                    dateDDOC.Value = DateTime.Parse(forUpdate.Rows[0][12].ToString());
                    tbODOC.Text = forUpdate.Rows[0][13].ToString();
                    tbSCERTIF.Text = forUpdate.Rows[0][14].ToString();
                    tbNCERTIF.Text = forUpdate.Rows[0][15].ToString();
                    dateDCERTIF.Value = DateTime.Parse(forUpdate.Rows[0][16].ToString());
                    tbSLEAF.Text = forUpdate.Rows[0][17].ToString();
                    tbNLEAF.Text = forUpdate.Rows[0][18].ToString();
                    dateDLEAF.Value = DateTime.Parse(forUpdate.Rows[0][19].ToString());
                    dateDREG.Value = DateTime.Parse(forUpdate.Rows[0][21].ToString());
                    tbQTW.Text = forUpdate.Rows[0][22].ToString();
                    cBMULP.Checked = (forUpdate.Rows[0][23].ToString() == "1" ? true : false);
                    cBPREM.Checked = (forUpdate.Rows[0][24].ToString() == "1" ? true : false);
                    PHELP_CB.Checked = (forUpdate.Rows[0][25].ToString() == "1" ? true : false);

                    if (forUpdate.Rows[0][20].ToString() == "1")
                    {
                        if (forUpdate.Rows[0][25].ToString() == "1")
                        {
                            lbCHK.ForeColor = Color.Black;
                            lbCHK.Text = "4000,00";
                        }
                        else
                        {
                            lbCHK.ForeColor = Color.Black;
                            lbCHK.Text = "3000,00";
                        }
                    }

                    this.Text = "Редактирование записи";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void FuncInsertMother(int chk, int phelp)
        {
            try
            {
                //tbSNILS.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string Mother_query = "insert into BTAL1 (DREG, QTW, MULP, PREM, CHK, PHELP, SCERTIF, NCERTIF, DCERTIF, SNILS, LNAME, FNAME, MNAME, BDATE, ADDRESS, TDOC, NDOC, SDOC, DDOC, ODOC, SPOLICY, NPOLICY, NCARD, DCARD, SLEAF, NLEAF, DLEAF, client_id) " +
                              $"VALUE ('{dateDREG.Value.ToString("yyyy-MM-dd")}', {tbQTW.Text}, {(cBMULP.Checked == true ? 1 : 0)}, {(cBPREM.Checked == true ? 1 : 0)}, {chk}, {phelp}, '{tbSCERTIF.Text}', '{tbNCERTIF.Text}', '{(tbSCERTIF.Text == "" && tbNCERTIF.Text == "" ? "" : dateDCERTIF.Value.ToString("yyyy-MM-dd"))}', '{tbSNILS.Text}', '{tbLNAME.Text}', '{tbFNAME.Text}'," +
                                    $" '{tbMNAME.Text}', '{dateBDATE.Value.ToString("yyyy-MM-dd")}', '{tbADDRESS.Text}', {cbTypeDoc.SelectedIndex}, '{tbNDOC.Text}', '{tbSDOC.Text}', '{(cbTypeDoc.SelectedIndex == 0 && tbNDOC.Text == "" && tbSDOC.Text == "" ? "" : dateDDOC.Value.ToString("yyyy-MM-dd"))}', '{tbODOC.Text}', '{tbSPOLICY.Text}', '{tbNPOLICY.Text}'," +
                                    $" '{tbNCARD.Text}', '{(tbNCARD.Text.Length > 0 ? dateDCARD.Value.ToString("yyyy-MM-dd") : "")}', '{tbSLEAF.Text}', '{tbNLEAF.Text}', '{(tbSLEAF.Text.Length > 0 || tbNLEAF.Text.Length > 0 ? dateDLEAF.Value.ToString("yyyy-MM-dd") : "")}', {client_id})";

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

        private void FuncUpdateMother(int chk, int phelp)
        {
            try
            {
                string Mother_query = $"UPDATE BTAL1 SET DREG = '{dateDREG.Value.ToString("yyyy-MM-dd")}', QTW = {tbQTW.Text}, MULP = {(cBMULP.Checked == true ? 1 : 0)}, PREM = {(cBPREM.Checked == true ? 1 : 0)}, CHK = {chk}, PHELP = {phelp}, SCERTIF = '{tbSCERTIF.Text}', NCERTIF = '{tbNCERTIF.Text}', DCERTIF = '{(tbSCERTIF.Text == "" && tbNCERTIF.Text == "" ? "" : dateDCERTIF.Value.ToString("yyyy-MM-dd"))}'," +
                                                      $" SNILS = '{tbSNILS.Text}', LNAME = '{tbLNAME.Text}', FNAME = '{tbFNAME.Text}', MNAME = '{tbMNAME.Text}', BDATE = '{dateBDATE.Value.ToString("yyyy-MM-dd")}', ADDRESS = '{tbADDRESS.Text}', TDOC = {cbTypeDoc.SelectedIndex}, NDOC = '{tbNDOC.Text}', " +
                                                      $" SDOC = '{tbSDOC.Text}', DDOC = '{(cbTypeDoc.SelectedIndex == 0 && tbNDOC.Text == "" && tbSDOC.Text == "" ? "" : dateDDOC.Value.ToString("yyyy-MM-dd"))}', ODOC = '{tbODOC.Text}', SPOLICY = '{tbSPOLICY.Text}', NPOLICY = '{tbNPOLICY.Text}', NCARD = '{tbNCARD.Text}', DCARD = '{(tbNCARD.Text.Length > 0 ? dateDCARD.Value.ToString("yyyy-MM-dd") : "")}', " +
                                                      $" SLEAF = '{tbSLEAF.Text}', NLEAF = '{tbNLEAF.Text}', DLEAF = '{(tbSLEAF.Text.Length > 0 || tbNLEAF.Text.Length > 0 ? dateDLEAF.Value.ToString("yyyy-MM-dd") : "")}'" +
                                     $" where client_id = {client_id}" +
                                       $" and id = {forUpdate.Rows[0][0].ToString()} and deleted = 0";

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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateDREG.Value.Date > dateDCERTIF.Value.Date) MessageBox.Show("Дата постановки на учёт больше даты выдачи сертификата", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if ((dateDCERTIF.Value.Date - dateDREG.Value.Date).Days / 7 < 12)
                    {
                        if (MessageBox.Show("Период наблюдения меньше 12 недель. Сохранить изменения?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (tbQTW.Text == "") MessageBox.Show("Поле \"Срок беременности на момент выдачи сертификата\" не может быть пустым, пожалуйста, укажите срок", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                            {
                                if (int.Parse(tbQTW.Text) < 30)
                                {
                                    if (cBPREM.Checked == true || cBMULP.Checked == true)
                                    {
                                        lbCHK.ForeColor = Color.Black;
                                        if (PHELP_CB.Checked == true) lbCHK.Text = "4000,00";
                                        else lbCHK.Text = "3000,00";
                                        if (insert == true) FuncInsertMother(1, PHELP_CB.Checked == true?1:0);
                                        else FuncUpdateMother(1, PHELP_CB.Checked == true ? 1 : 0);
                                        MessageBox.Show("Данные успешно сохранены", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();
                                    }
                                    else if (MessageBox.Show("Срок беременности на момент выдачи сертификата меньше 30 недель. Сохранить изменения?", "Талон не подлежит оплате", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                    {
                                        if (insert == true) FuncInsertMother(0, PHELP_CB.Checked == true ? 1 : 0);
                                        else FuncUpdateMother(0, PHELP_CB.Checked == true ? 1 : 0);
                                        MessageBox.Show("Данные успешно сохранены", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    if (insert == true) FuncInsertMother(0, PHELP_CB.Checked == true ? 1 : 0);
                                    else FuncUpdateMother(0, PHELP_CB.Checked == true ? 1 : 0);
                                    MessageBox.Show("Данные успешно сохранены", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (tbQTW.Text == "") MessageBox.Show("Поле \"Срок беременности на момент выдачи сертификата\" не может быть пустым, пожалуйста, укажите срок", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            if (int.Parse(tbQTW.Text) < 30)
                            {
                                if (cBPREM.Checked == true || cBMULP.Checked == true)
                                {
                                    lbCHK.ForeColor = Color.Black;
                                    if (PHELP_CB.Checked == true) lbCHK.Text = "4000,00";
                                    else lbCHK.Text = "3000,00";
                                    if (insert == true) FuncInsertMother(1, PHELP_CB.Checked == true ? 1 : 0);
                                    else FuncUpdateMother(1, PHELP_CB.Checked == true ? 1 : 0);
                                    MessageBox.Show("Данные успешно сохранены", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                                else if (MessageBox.Show("Срок беременности на момент выдачи сертификата меньше 30 недель. Сохранить изменения?", "Талон не подлежит оплате", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    if (insert == true) FuncInsertMother(0, PHELP_CB.Checked == true ? 1 : 0);
                                    else FuncUpdateMother(0, PHELP_CB.Checked == true ? 1 : 0);
                                    MessageBox.Show("Данные успешно сохранены", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                            }
                            else
                            {
                                lbCHK.ForeColor = Color.Black;
                                if (PHELP_CB.Checked == true) lbCHK.Text = "4000,00";
                                else lbCHK.Text = "3000,00";
                                if (insert == true) FuncInsertMother(1, PHELP_CB.Checked == true ? 1 : 0);
                                else FuncUpdateMother(1, PHELP_CB.Checked == true ? 1 : 0);
                                MessageBox.Show("Данные успешно сохранены", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void TbQTW_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbQTW.Text != "")
                {
                    if (((dateDREG.Value - dateDCERTIF.Value.AddDays(-int.Parse(tbQTW.Text) * 7)).Days / 7) > 0) tbBeremSrokUchet.Text = (((dateDREG.Value - dateDCERTIF.Value.AddDays(-int.Parse(tbQTW.Text) * 7)).Days / 7)).ToString();
                    else tbBeremSrokUchet.Text = "0";
                }
                else tbBeremSrokUchet.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
    }
}
