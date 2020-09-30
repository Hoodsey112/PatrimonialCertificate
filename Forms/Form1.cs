using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using NLog;

namespace Родовые_сертификаты
{
    public partial class MainForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static MainForm mForm;
        private Application application;
        private Workbook workBook;
        private Worksheet worksheet;
        System.Data.DataTable dataReportTable = new System.Data.DataTable();
        System.Windows.Forms.CheckBox checkBox;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ExportDataBEREM()
        {
            try
            {
                string queryB = "select IF(DREG = '00.00.0000', '', DATE_FORMAT(DREG, '%Y.%m.%d')) DREG," +
                                      " QTW," +
                                      " MULP," +
                                      " PREM," +
                                      " CHK," +
                                      " SCERTIF," +
                                      " NCERTIF," +
                                      " IF(DCERTIF = '00.00.0000', '', DATE_FORMAT(DCERTIF, '%Y.%m.%d')) DCERTIF," +
                                      " SNILS," +
                                      " LNAME," +
                                      " FNAME," +
                                      " MNAME," +
                                      " IF(BDATE = '00.00.0000', '', DATE_FORMAT(BDATE, '%Y.%m.%d')) BDATE," +
                                      " ADDRESS," +
                                      " TDOC," +
                                      " NDOC," +
                                      " SDOC," +
                                      " IF(DDOC = '00.00.0000', '', DATE_FORMAT(DDOC, '%Y.%m.%d')) DDOC," +
                                      " ODOC," +
                                      " SPOLICY," +
                                      " NPOLICY," +
                                      " NCARD," +
                                      " IF(DCARD = '00.00.0000', '', DATE_FORMAT(DCARD, '%Y.%m.%d')) DCARD," +
                                      " SLEAF," +
                                      " NLEAF," +
                                      " IF(DLEAF = '00.00.0000', '', DATE_FORMAT(DLEAF, '%Y.%m.%d')) DLEAF," +
                                      " PHELP" +
                               " from BTAL1" +
                              $" where DCERTIF between '{dateFrom.Value.ToString("yyyy-MM-dd")}' and '{dateTo.Value.ToString("yyyy-MM-dd")}'" +
                                 " and deleted = 0";

                CreateXML.LoadTableBTAL1(queryB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void ExportDataROD()
        {
            try
            {
                string queryM = "select IF(DREG = '00.00.0000', '', DATE_FORMAT(DREG, '%Y.%m.%d')) DREG," +
                                      " DIAG," +
                                      " BQNT," +
                                      " CHK," +
                                      " SCERTIF," +
                                      " NCERTIF," +
                                      " IF(DCERTIF = '00.00.0000', '', DATE_FORMAT(DCERTIF, '%Y.%m.%d')) DCERTIF," +
                                      " SNILS," +
                                      " LNAME," +
                                      " FNAME," +
                                      " MNAME," +
                                      " IF(BDATE = '00.00.0000', '', DATE_FORMAT(BDATE, '%Y.%m.%d')) BDATE," +
                                      " ADDRESS," +
                                      " TDOC," +
                                      " SDOC," +
                                      " NDOC," +
                                      " IF(DDOC = '00.00.0000', '', DATE_FORMAT(DDOC, '%Y.%m.%d')) DDOC," +
                                      " ODOC," +
                                      " SPOLICY," +
                                      " NPOLICY," +
                                      " SLEAF," +
                                      " NLEAF," +
                                      " IF(DLEAF = '00.00.0000', '', DATE_FORMAT(DLEAF, '%Y.%m.%d')) DLEAF," +
                                      " NCARD," +
                                      " IF(DCARD = '00.00.0000', '', DATE_FORMAT(DCARD, '%Y.%m.%d')) DCARD," +
                                      " client_id," +
                                      " id" +
                               " from BTAL2" +
                              $" where DREG between '{dateFrom.Value.ToString("yyyy-MM-dd")}' and '{dateTo.Value.ToString("yyyy-MM-dd")}'" +
                                 " and deleted = 0";

                string queryChild = "select mother_id," +
                                          " SEX," +
                                          " WEIGHT," +
                                          " GROWTH," +
                                          " DIAG" +
                                   " from CHILD" +
                                   " where deleted = 0";

                CreateXML.LoadTableBTAL2(queryM, queryChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                txtNameOrg.Text = Properties.Settings.Default.Name_LPU;

                DateTime checkDate = DateTime.Today.AddMonths(-1);

                dateFrom.Value = new DateTime(checkDate.Year, checkDate.Month, 25);
                dateTo.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 25);

                dataCertifRod.AutoGenerateColumns = false;
                dataCertifRod.DataSource = LoadClass.FuncLoadFromTable(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));

                dataBerem.AutoGenerateColumns = false;
                dataBerem.DataSource = LoadClass.LoadBeremCertif(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));

                FuncInsertDataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void FuncInsertDataTable()
        {
            try
            {
                dataReportTable = LoadClass.FuncTable(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));

                for (int i = 0; i < dataReportTable.Rows.Count; i++)
                {
                    if (dataReportTable.Rows[i][0].ToString() == "В период беременности")
                    {
                        lblPeriodBerem.Text = dataReportTable.Rows[i][2].ToString();
                        lblPeriodBeremCountryWomen.Text = dataReportTable.Rows[i][1].ToString();
                    }
                    else
                    {
                        lblPeriodRodov.Text = dataReportTable.Rows[i][2].ToString();
                        lblPeriodRodovCountryWomen.Text = dataReportTable.Rows[i][1].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void DateFrom_CloseUp(object sender, EventArgs e)
        {
            try
            {
                dataCertifRod.AutoGenerateColumns = false;
                dataCertifRod.DataSource = LoadClass.FuncLoadFromTable(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));

                dataBerem.AutoGenerateColumns = false;
                dataBerem.DataSource = LoadClass.LoadBeremCertif(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));

                FuncInsertDataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void DateTo_CloseUp(object sender, EventArgs e)
        {
            try
            {
                dataCertifRod.AutoGenerateColumns = false;
                dataCertifRod.DataSource = LoadClass.FuncLoadFromTable(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));

                dataBerem.AutoGenerateColumns = false;
                dataBerem.DataSource = LoadClass.LoadBeremCertif(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));

                FuncInsertDataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
        
        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (cBBEREM.Checked == false && cBROD.Checked == false) MessageBox.Show("Вы не выбрали данные для экспорта", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    foreach (System.Windows.Forms.CheckBox checkBox in groupBox2.Controls.OfType<System.Windows.Forms.CheckBox>())
                    {
                        switch (checkBox.Checked)
                        {
                            case true:
                                switch (checkBox.Name)
                                {
                                    case "cBBEREM":
                                        if (dataBerem.Rows.Count > 0) ExportDataBEREM();
                                        else MessageBox.Show("За отчетный период нет данных для экспорта", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        break;
                                    case "cBROD":
                                        if (dataCertifRod.Rows.Count > 0) ExportDataROD();
                                        else MessageBox.Show("За отчетный период нет данных для экспорта", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        break;
                                }
                                MessageBox.Show("Данные успешно выгружены", "Выгрузка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;  
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
        
        private void DataBerem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                using (AddBerem abForm = new AddBerem(int.Parse(dataBerem.CurrentRow.Cells[12].Value.ToString()), false))
                {
                    abForm.ShowDialog();
                }

                dataBerem.AutoGenerateColumns = false;
                dataBerem.DataSource = LoadClass.LoadBeremCertif(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void DataCertifRod_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                using (AddPatient apForm = new AddPatient(int.Parse(dataCertifRod.CurrentRow.Cells[13].Value.ToString()), false))
                {
                    apForm.ShowDialog();
                }

                dataCertifRod.AutoGenerateColumns = false;
                dataCertifRod.DataSource = LoadClass.FuncLoadFromTable(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
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
                        DeleteClass.deleteRod(int.Parse(dataCertifRod.CurrentRow.Cells[13].Value.ToString()), int.Parse(dataCertifRod.CurrentRow.Cells[14].Value.ToString()), dataCertifRod.CurrentRow.Cells[0].Value.ToString());

                        dataCertifRod.AutoGenerateColumns = false;
                        dataCertifRod.DataSource = LoadClass.FuncLoadFromTable(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
                    }
                }
                else return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void DataBerem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить данную запись?", "Удаление выделенной записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteClass.deleteBerem(int.Parse(dataBerem.CurrentRow.Cells[11].Value.ToString()), int.Parse(dataBerem.CurrentRow.Cells[12].Value.ToString()));

                        dataBerem.AutoGenerateColumns = false;
                        dataBerem.DataSource = LoadClass.LoadBeremCertif(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
                    }
                }
                else return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void ExportDataBTN_Click(object sender, EventArgs e)
        {
            try
            {
                application = new Application
                {
                    DisplayAlerts = false
                };

                // Файл шаблона
                const string template = "Мониторинг Проектного комитета (Родовые сертификаты).xlsx";

                // Открываем книгу
                workBook = application.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, template));

                // Получаем активную таблицу
                worksheet = workBook.ActiveSheet as Worksheet;

                // Записываем данные
                worksheet.Range["H4"].Value = txtNameOrg.Text;
                worksheet.Range["P9"].Value = lblPeriodBerem.Text;
                worksheet.Range["P13"].Value = lblPeriodRodov.Text;
                worksheet.Range["X9"].Value = lblPeriodBeremCountryWomen.Text;
                worksheet.Range["X13"].Value = lblPeriodRodovCountryWomen.Text;

                // Показываем приложение
                application.Visible = true;
                TopMost = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
    }
}
