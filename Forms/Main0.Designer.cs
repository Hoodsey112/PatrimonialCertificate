namespace Родовые_сертификаты
{
    partial class Main0
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main0));
            this.btnWC = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnParturientWomen = new System.Windows.Forms.Button();
            this.btnMonitoringForm = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectionSettingsStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.LPUSettingsStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnWC
            // 
            this.btnWC.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnWC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnWC.Location = new System.Drawing.Point(35, 68);
            this.btnWC.Name = "btnWC";
            this.btnWC.Size = new System.Drawing.Size(51, 39);
            this.btnWC.TabIndex = 0;
            this.btnWC.Text = "ЖК";
            this.btnWC.UseVisualStyleBackColor = true;
            this.btnWC.Click += new System.EventHandler(this.BtnWC_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(32, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "Родовые сертификаты";
            // 
            // btnParturientWomen
            // 
            this.btnParturientWomen.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnParturientWomen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnParturientWomen.Location = new System.Drawing.Point(92, 68);
            this.btnParturientWomen.Name = "btnParturientWomen";
            this.btnParturientWomen.Size = new System.Drawing.Size(120, 39);
            this.btnParturientWomen.TabIndex = 2;
            this.btnParturientWomen.Text = "Роженицы";
            this.btnParturientWomen.UseVisualStyleBackColor = true;
            this.btnParturientWomen.Click += new System.EventHandler(this.BtnParturientWomen_Click);
            // 
            // btnMonitoringForm
            // 
            this.btnMonitoringForm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMonitoringForm.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMonitoringForm.Location = new System.Drawing.Point(218, 68);
            this.btnMonitoringForm.Name = "btnMonitoringForm";
            this.btnMonitoringForm.Size = new System.Drawing.Size(124, 39);
            this.btnMonitoringForm.TabIndex = 3;
            this.btnMonitoringForm.Text = "Форма мониторинга";
            this.btnMonitoringForm.UseVisualStyleBackColor = true;
            this.btnMonitoringForm.Click += new System.EventHandler(this.BtnMonitoringForm_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileStrip,
            this.SettingsStrip});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(377, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileStrip
            // 
            this.FileStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitStrip});
            this.FileStrip.Name = "FileStrip";
            this.FileStrip.Size = new System.Drawing.Size(45, 20);
            this.FileStrip.Text = "Файл";
            // 
            // ExitStrip
            // 
            this.ExitStrip.Name = "ExitStrip";
            this.ExitStrip.Size = new System.Drawing.Size(107, 22);
            this.ExitStrip.Text = "Выход";
            this.ExitStrip.Click += new System.EventHandler(this.ExitStrip_Click);
            // 
            // SettingsStrip
            // 
            this.SettingsStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectionSettingsStrip,
            this.LPUSettingsStrip});
            this.SettingsStrip.Name = "SettingsStrip";
            this.SettingsStrip.Size = new System.Drawing.Size(73, 20);
            this.SettingsStrip.Text = "Настройки";
            // 
            // ConnectionSettingsStrip
            // 
            this.ConnectionSettingsStrip.Name = "ConnectionSettingsStrip";
            this.ConnectionSettingsStrip.Size = new System.Drawing.Size(201, 22);
            this.ConnectionSettingsStrip.Text = "Настройки подключения";
            this.ConnectionSettingsStrip.Click += new System.EventHandler(this.ConnectionSettingsStrip_Click);
            // 
            // LPUSettingsStrip
            // 
            this.LPUSettingsStrip.Name = "LPUSettingsStrip";
            this.LPUSettingsStrip.Size = new System.Drawing.Size(201, 22);
            this.LPUSettingsStrip.Text = "Реквизиты ЛПУ";
            this.LPUSettingsStrip.Click += new System.EventHandler(this.LPUSettingsStrip_Click);
            // 
            // Main0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 119);
            this.Controls.Add(this.btnMonitoringForm);
            this.Controls.Add(this.btnParturientWomen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnWC);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(383, 127);
            this.Name = "Main0";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Родовые сертификаты";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnParturientWomen;
        private System.Windows.Forms.Button btnMonitoringForm;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileStrip;
        private System.Windows.Forms.ToolStripMenuItem ExitStrip;
        private System.Windows.Forms.ToolStripMenuItem SettingsStrip;
        private System.Windows.Forms.ToolStripMenuItem ConnectionSettingsStrip;
        private System.Windows.Forms.ToolStripMenuItem LPUSettingsStrip;
    }
}