namespace CP8507_v7
{
    partial class IEC101_SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IEC101_SettingsForm));
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.iec101Reason_comboBox = new System.Windows.Forms.ComboBox();
            this.label72 = new System.Windows.Forms.Label();
            this.iec101ASDUType_comboBox = new System.Windows.Forms.ComboBox();
            this.label73 = new System.Windows.Forms.Label();
            this.iec101InfoAddress_comboBox = new System.Windows.Forms.ComboBox();
            this.label71 = new System.Windows.Forms.Label();
            this.iec101ASDUAddress_comboBox = new System.Windows.Forms.ComboBox();
            this.label70 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.answerTime_textBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.read_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox23.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.iec101Reason_comboBox);
            this.groupBox23.Controls.Add(this.label72);
            this.groupBox23.Controls.Add(this.iec101ASDUType_comboBox);
            this.groupBox23.Controls.Add(this.label73);
            this.groupBox23.Controls.Add(this.iec101InfoAddress_comboBox);
            this.groupBox23.Controls.Add(this.label71);
            this.groupBox23.Controls.Add(this.iec101ASDUAddress_comboBox);
            this.groupBox23.Controls.Add(this.label70);
            this.groupBox23.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox23.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox23.Location = new System.Drawing.Point(0, 0);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(722, 97);
            this.groupBox23.TabIndex = 6;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Параметры протокола МЭК-101";
            // 
            // iec101Reason_comboBox
            // 
            this.iec101Reason_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iec101Reason_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.iec101Reason_comboBox.FormattingEnabled = true;
            this.iec101Reason_comboBox.Items.AddRange(new object[] {
            "1 байт",
            "2 байта"});
            this.iec101Reason_comboBox.Location = new System.Drawing.Point(583, 24);
            this.iec101Reason_comboBox.Name = "iec101Reason_comboBox";
            this.iec101Reason_comboBox.Size = new System.Drawing.Size(101, 24);
            this.iec101Reason_comboBox.TabIndex = 23;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label72.Location = new System.Drawing.Point(348, 25);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(238, 18);
            this.label72.TabIndex = 22;
            this.label72.Text = "Размер поля \'причина передачи\':";
            // 
            // iec101ASDUType_comboBox
            // 
            this.iec101ASDUType_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iec101ASDUType_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.iec101ASDUType_comboBox.FormattingEnabled = true;
            this.iec101ASDUType_comboBox.Items.AddRange(new object[] {
            "<9>   M_ME_NA",
            "<10> M_ME_TA",
            "<13> M_ME_NC",
            "<14> M_ME_TC",
            "<21> M_ME_ND",
            "<34> M_ME_TD",
            "<36> M_ME_TF"});
            this.iec101ASDUType_comboBox.Location = new System.Drawing.Point(175, 24);
            this.iec101ASDUType_comboBox.Name = "iec101ASDUType_comboBox";
            this.iec101ASDUType_comboBox.Size = new System.Drawing.Size(167, 24);
            this.iec101ASDUType_comboBox.TabIndex = 27;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label73.Location = new System.Drawing.Point(6, 25);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(82, 18);
            this.label73.TabIndex = 26;
            this.label73.Text = "Тип ASDU:";
            // 
            // iec101InfoAddress_comboBox
            // 
            this.iec101InfoAddress_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iec101InfoAddress_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.iec101InfoAddress_comboBox.FormattingEnabled = true;
            this.iec101InfoAddress_comboBox.Items.AddRange(new object[] {
            "2 байта",
            "3 байта"});
            this.iec101InfoAddress_comboBox.Location = new System.Drawing.Point(231, 59);
            this.iec101InfoAddress_comboBox.Name = "iec101InfoAddress_comboBox";
            this.iec101InfoAddress_comboBox.Size = new System.Drawing.Size(111, 24);
            this.iec101InfoAddress_comboBox.TabIndex = 21;
            // 
            // label71
            // 
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label71.Location = new System.Drawing.Point(6, 51);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(219, 43);
            this.label71.TabIndex = 20;
            this.label71.Text = "Размер адреса объекта информации:";
            // 
            // iec101ASDUAddress_comboBox
            // 
            this.iec101ASDUAddress_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iec101ASDUAddress_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.iec101ASDUAddress_comboBox.FormattingEnabled = true;
            this.iec101ASDUAddress_comboBox.Items.AddRange(new object[] {
            "1 байт",
            "2 байта"});
            this.iec101ASDUAddress_comboBox.Location = new System.Drawing.Point(583, 60);
            this.iec101ASDUAddress_comboBox.Name = "iec101ASDUAddress_comboBox";
            this.iec101ASDUAddress_comboBox.Size = new System.Drawing.Size(101, 24);
            this.iec101ASDUAddress_comboBox.TabIndex = 19;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label70.Location = new System.Drawing.Point(348, 60);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(219, 18);
            this.label70.TabIndex = 18;
            this.label70.Text = "Размер общего адреса ASDU:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.answerTime_textBox);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox5.Location = new System.Drawing.Point(0, 97);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(722, 36);
            this.groupBox5.TabIndex = 35;
            this.groupBox5.TabStop = false;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(351, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(345, 22);
            this.label6.TabIndex = 28;
            this.label6.Text = "Пример расчета нормализованного параметра";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(6, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(217, 18);
            this.label5.TabIndex = 27;
            this.label5.Text = "Коэффициент нормализации:";
            // 
            // answerTime_textBox
            // 
            this.answerTime_textBox.Enabled = false;
            this.answerTime_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.answerTime_textBox.Location = new System.Drawing.Point(231, 8);
            this.answerTime_textBox.Multiline = true;
            this.answerTime_textBox.Name = "answerTime_textBox";
            this.answerTime_textBox.ReadOnly = true;
            this.answerTime_textBox.Size = new System.Drawing.Size(111, 22);
            this.answerTime_textBox.TabIndex = 10;
            this.answerTime_textBox.Text = "15000";
            this.answerTime_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.read_button);
            this.groupBox1.Controls.Add(this.save_button);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 554);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(722, 36);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // read_button
            // 
            this.read_button.BackColor = System.Drawing.Color.Transparent;
            this.read_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.read_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.read_button.Image = ((System.Drawing.Image)(resources.GetObject("read_button.Image")));
            this.read_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.read_button.Location = new System.Drawing.Point(10, 2);
            this.read_button.Name = "read_button";
            this.read_button.Size = new System.Drawing.Size(120, 33);
            this.read_button.TabIndex = 36;
            this.read_button.Text = "Чтение";
            this.read_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.read_button.UseVisualStyleBackColor = false;
            this.read_button.Click += new System.EventHandler(this.read_button_Click);
            // 
            // save_button
            // 
            this.save_button.BackColor = System.Drawing.Color.Transparent;
            this.save_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.save_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.save_button.Image = ((System.Drawing.Image)(resources.GetObject("save_button.Image")));
            this.save_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.save_button.Location = new System.Drawing.Point(602, 2);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(120, 33);
            this.save_button.TabIndex = 35;
            this.save_button.Text = "Запись";
            this.save_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.save_button.UseVisualStyleBackColor = false;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 421);
            this.panel1.TabIndex = 38;
            // 
            // IEC101_SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 590);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox23);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IEC101_SettingsForm";
            this.ShowIcon = false;
            this.Text = "Параметры протокола МЭК";
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label73;
        public System.Windows.Forms.ComboBox iec101Reason_comboBox;
        public System.Windows.Forms.ComboBox iec101InfoAddress_comboBox;
        public System.Windows.Forms.ComboBox iec101ASDUAddress_comboBox;
        public System.Windows.Forms.ComboBox iec101ASDUType_comboBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox answerTime_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button read_button;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Panel panel1;
    }
}