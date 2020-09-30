using System;
using System.Windows.Forms;
using NLog;

namespace Родовые_сертификаты
{
    public partial class ParturientForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public ParturientForm()
        {
            InitializeComponent();
            dataRodOtdel.AutoGenerateColumns = false;
            dataCertifRod.AutoGenerateColumns = false;
        }

        private void AddRodBTN()
        {
            try
            {
                using (AddPatient apForm = new AddPatient(int.Parse(dataRodOtdel.CurrentRow.Cells[0].Value.ToString()), true))
                {
                    apForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void EditRodBTN()
        {
            try
            {
                using (AddPatient apForm = new AddPatient(int.Parse(dataCertifRod.CurrentRow.Cells[13].Value.ToString()), false))
                {
                    apForm.ShowDialog();
                }

                dataCertifRod.DataSource = LoadClass.FuncLoadFromTable(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                dataRodOtdel.DataSource = LoadClass.FuncLoad(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));

                dataCertifRod.DataSource = LoadClass.FuncLoadFromTable(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            { 
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void DataRodOtdel_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AddRodBTN();
        }
        
        private void DataCertifRod_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditRodBTN();
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
                                                
                        dataCertifRod.DataSource = LoadClass.FuncLoadFromTable(dateFrom.Value.ToString("yyyy-MM-dd"), dateTo.Value.ToString("yyyy-MM-dd"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void AddEditBTN_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (dataRodOtdel.Rows.Count > 0) AddRodBTN();
                else MessageBox.Show("Нет данных для добавления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dataCertifRod.Rows.Count > 0) EditRodBTN();
                else MessageBox.Show("Нет данных для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) AddEditBTN.Text = "Добавить";
            else AddEditBTN.Text = "Редактировать";
        }

        private void ParturientForm_Load(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) AddEditBTN.Text = "Добавить";
            else AddEditBTN.Text = "Редактировать";
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0) (dataRodOtdel.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format($"FIO like '{tbSearch.Text}%'");
                else (dataCertifRod.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format($"FIO_rod like '{tbSearch.Text}%'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
    }
}
