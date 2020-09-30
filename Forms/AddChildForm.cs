using System;
using System.Data;
using System.Windows.Forms;
using NLog;

namespace Родовые_сертификаты
{
    public partial class AddChildForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        int id = 0;
        bool insert = false;
        
        public AddChildForm(bool insertX, int idx)
        {
            insert = insertX;

            InitializeComponent();
            
            if (insert == true)
            {
                id = idx;
                AddBTN.Text = "Добавить";
            }
            else
            {
                id = idx;
                AddBTN.Text = "Сохранить";
            }
        }

        private void FuncLoadChild()
        {

        }
        private void AddBTN_Click(object sender, EventArgs e)
        {
            try
            {
                AddPatient main = this.Owner as AddPatient;

                if (insert == true)
                {
                    id++;

                    if (main != null)
                    {
                        main.Child.Rows.Add(id, sexTB.SelectedItem, weightTB.Text, growthTB.Text, diagTB.Text == null || diagTB.Text == "" ? null : diagTB.Text);
                        main.BQNT = ++main.BQNT;
                        MessageBox.Show("Данные добавлены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    sexTB.SelectedIndex = -1;
                    weightTB.Clear();
                    growthTB.Clear();
                    diagTB.Clear();
                }
                else
                {
                    if (main != null)
                    {
                        DataRow dataRow = main.Child.Rows[id];

                        dataRow[1] = sexTB.SelectedItem.ToString();
                        dataRow[2] = weightTB.Text;
                        dataRow[3] = growthTB.Text;
                        dataRow[4] = diagTB.Text;
                    }
                    MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                    Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void CloseBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddChildForm_Load(object sender, EventArgs e)
        { 
            AddPatient main = this.Owner as AddPatient;

            if (main != null)
            {
                sexTB.SelectedItem = main.Child.Rows[id].ItemArray[1].ToString();
                weightTB.Text = main.Child.Rows[id].ItemArray[2].ToString();
                growthTB.Text = main.Child.Rows[id].ItemArray[3].ToString();
                diagTB.Text = main.Child.Rows[id].ItemArray[4].ToString();
            }
        }
    }
}
