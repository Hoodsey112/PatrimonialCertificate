namespace Родовые_сертификаты
{
    partial class AddChildForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddChildForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CloseBTN = new System.Windows.Forms.Button();
            this.AddBTN = new System.Windows.Forms.Button();
            this.diagTB = new System.Windows.Forms.TextBox();
            this.weightTB = new System.Windows.Forms.MaskedTextBox();
            this.growthTB = new System.Windows.Forms.TextBox();
            this.sexTB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CloseBTN);
            this.groupBox1.Controls.Add(this.AddBTN);
            this.groupBox1.Controls.Add(this.diagTB);
            this.groupBox1.Controls.Add(this.weightTB);
            this.groupBox1.Controls.Add(this.growthTB);
            this.groupBox1.Controls.Add(this.sexTB);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация о ребенке";
            // 
            // CloseBTN
            // 
            this.CloseBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBTN.Location = new System.Drawing.Point(104, 122);
            this.CloseBTN.Name = "CloseBTN";
            this.CloseBTN.Size = new System.Drawing.Size(75, 23);
            this.CloseBTN.TabIndex = 6;
            this.CloseBTN.Text = "Закрыть";
            this.CloseBTN.UseVisualStyleBackColor = true;
            this.CloseBTN.Click += new System.EventHandler(this.CloseBTN_Click);
            // 
            // AddBTN
            // 
            this.AddBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.AddBTN.Location = new System.Drawing.Point(15, 122);
            this.AddBTN.Name = "AddBTN";
            this.AddBTN.Size = new System.Drawing.Size(75, 23);
            this.AddBTN.TabIndex = 5;
            this.AddBTN.Text = "Доб\\Сохр";
            this.AddBTN.UseVisualStyleBackColor = true;
            this.AddBTN.Click += new System.EventHandler(this.AddBTN_Click);
            // 
            // diagTB
            // 
            this.diagTB.Location = new System.Drawing.Point(104, 90);
            this.diagTB.Name = "diagTB";
            this.diagTB.Size = new System.Drawing.Size(48, 20);
            this.diagTB.TabIndex = 4;
            // 
            // weightTB
            // 
            this.weightTB.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            this.weightTB.Location = new System.Drawing.Point(105, 39);
            this.weightTB.Mask = "0\\.000";
            this.weightTB.Name = "weightTB";
            this.weightTB.Size = new System.Drawing.Size(48, 20);
            this.weightTB.TabIndex = 2;
            // 
            // growthTB
            // 
            this.growthTB.Location = new System.Drawing.Point(105, 61);
            this.growthTB.Name = "growthTB";
            this.growthTB.Size = new System.Drawing.Size(48, 20);
            this.growthTB.TabIndex = 3;
            // 
            // sexTB
            // 
            this.sexTB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sexTB.FormattingEnabled = true;
            this.sexTB.Items.AddRange(new object[] {
            "М",
            "Ж"});
            this.sexTB.Location = new System.Drawing.Point(105, 16);
            this.sexTB.MaxLength = 1;
            this.sexTB.Name = "sexTB";
            this.sexTB.Size = new System.Drawing.Size(48, 21);
            this.sexTB.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 28);
            this.label4.TabIndex = 3;
            this.label4.Text = "МКБ-10 (в случае смерти)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Рост, см";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Вес, кг";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Пол";
            // 
            // AddChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 152);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddChildForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить ребенка";
            this.Load += new System.EventHandler(this.AddChildForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button AddBTN;
        private System.Windows.Forms.TextBox diagTB;
        private System.Windows.Forms.MaskedTextBox weightTB;
        private System.Windows.Forms.TextBox growthTB;
        private System.Windows.Forms.ComboBox sexTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseBTN;
    }
}