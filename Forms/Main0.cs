using System;
using System.Windows.Forms;
using NLog;

namespace Родовые_сертификаты
{
    public partial class Main0 : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Main0()
        {
            InitializeComponent();
        }

        private void BtnMonitoringForm_Click(object sender, EventArgs e)
        {
            try
            {
                using (MainForm mForm = new MainForm())
                {
                    mForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void BtnParturientWomen_Click(object sender, EventArgs e)
        {
            try
            {
                using (ParturientForm partForm = new ParturientForm())
                {
                    partForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void BtnWC_Click(object sender, EventArgs e)
        {
            try
            {
                using (BeremForm bForm = new BeremForm())
                {
                    bForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void ConnectionSettingsStrip_Click(object sender, EventArgs e)
        {
            try
            {
                using (ConnectionSettingsForm csForm = new ConnectionSettingsForm())
                {
                    csForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        private void ExitStrip_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LPUSettingsStrip_Click(object sender, EventArgs e)
        {
            try
            {
                using (LPUForm lForm = new LPUForm())
                {
                    lForm.ShowDialog();
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
