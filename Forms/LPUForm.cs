using System;
using System.Windows.Forms;
using NLog;

namespace Родовые_сертификаты
{
    public partial class LPUForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public LPUForm()
        {
            InitializeComponent();
        }

        private void LPUForm_Load(object sender, EventArgs e)
        {
            try
            {
                var defSettings = Properties.Settings.Default;

                //Реквизиты ЛПУ

                INNTB.Text = defSettings.INN_LPU;
                RegNumbTB.Text = defSettings.RegNumb_LPU;
                KPPTB.Text = defSettings.KPP_LPU;
                OGRNTB.Text = defSettings.OGRN_LPU;
                NameTB.Text = defSettings.Name_LPU;
                AddressTB.Text = defSettings.Address_LPU;
                TypeCB.SelectedItem = defSettings.Type_LPU;
                OwnDocTB.Text = defSettings.OwnDoc_LPU;
                OwnBookKeepTB.Text = defSettings.OwnBookKeep;
                RSTB.Text = defSettings.RS_LPU;
                BankNameTB.Text = defSettings.BankName_LPU;
                BIKTB.Text = defSettings.BIK_LPU;
                KSTB.Text = defSettings.KS_LPU;

                //Реквизиты ФСС
                INNTB_FSS.Text = defSettings.INN_FSS;
                KPPTB_FSS.Text = defSettings.KPP_FSS;
                NameTB_FSS.Text = defSettings.Name_FSS;
                AddressTB_FSS.Text = defSettings.Address_FSS;
                RSTB_FSS.Text = defSettings.RS_FSS;
                BankNameTB_FSS.Text = defSettings.BankName_FSS;
                BIKTB_FSS.Text = defSettings.BIK_FSS;
                KSTB_FSS.Text = defSettings.KS_FSS;

                DocNumberTB.Text = defSettings.DocNumb_FSS;
                DocDate_FSS.Value = DateTime.Parse(defSettings.DocDate_FSS);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void SaveBTN_Click(object sender, EventArgs e)
        {
            try
            {
                var defSettings = Properties.Settings.Default;

                //Реквизиты ЛПУ

                defSettings.INN_LPU = INNTB.Text;
                defSettings.RegNumb_LPU = RegNumbTB.Text;
                defSettings.KPP_LPU = KPPTB.Text;
                defSettings.OGRN_LPU = OGRNTB.Text;
                defSettings.Name_LPU = NameTB.Text;
                defSettings.Address_LPU = AddressTB.Text;
                defSettings.Type_LPU = TypeCB.SelectedItem.ToString();
                defSettings.OwnDoc_LPU = OwnDocTB.Text;
                defSettings.OwnBookKeep = OwnBookKeepTB.Text;
                defSettings.RS_LPU = RSTB.Text;
                defSettings.BankName_LPU = BankNameTB.Text;
                defSettings.BIK_LPU = BIKTB.Text;
                defSettings.KS_LPU = KSTB.Text;

                //Реквизиты ФСС
                defSettings.INN_FSS = INNTB_FSS.Text;
                defSettings.KPP_FSS = KPPTB_FSS.Text;
                defSettings.Name_FSS = NameTB_FSS.Text;
                defSettings.Address_FSS = AddressTB_FSS.Text;
                defSettings.RS_FSS = RSTB_FSS.Text;
                defSettings.BankName_FSS = BankNameTB_FSS.Text;
                defSettings.BIK_FSS = BIKTB_FSS.Text;
                defSettings.KS_FSS = KSTB_FSS.Text;

                defSettings.DocNumb_FSS = DocNumberTB.Text;
                defSettings.DocDate_FSS = DocDate_FSS.Value.ToString("dd.MM.yyyy");
                defSettings.Save();
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
    }
}
