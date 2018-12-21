namespace CP8507_v7
{
    partial class ReadPKEForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadPKEForm));
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox57 = new System.Windows.Forms.GroupBox();
            this.label131 = new System.Windows.Forms.Label();
            this.minute_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label130 = new System.Windows.Forms.Label();
            this.hour_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.readEnergy_button = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.second_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox57.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minute_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.second_numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthCalendar1.Location = new System.Drawing.Point(7, 87);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadEnergyForm_KeyDown);
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(432, 69);
            this.label1.TabIndex = 1;
            this.label1.Text = "Внимание! \r\nВремя компьютера должно быть синхронизировано с прибором.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton1.Location = new System.Drawing.Point(183, 142);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(151, 22);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Считать интервал";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadEnergyForm_KeyDown);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton2.Location = new System.Drawing.Point(183, 170);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(262, 22);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "Считать архив за последний день";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadEnergyForm_KeyDown);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton3.Location = new System.Drawing.Point(183, 198);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(207, 22);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.Text = "Считать недельный архив";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadEnergyForm_KeyDown);
            // 
            // groupBox57
            // 
            this.groupBox57.Controls.Add(this.label3);
            this.groupBox57.Controls.Add(this.second_numericUpDown);
            this.groupBox57.Controls.Add(this.label131);
            this.groupBox57.Controls.Add(this.minute_numericUpDown);
            this.groupBox57.Controls.Add(this.label130);
            this.groupBox57.Controls.Add(this.hour_numericUpDown);
            this.groupBox57.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox57.Location = new System.Drawing.Point(179, 87);
            this.groupBox57.Name = "groupBox57";
            this.groupBox57.Size = new System.Drawing.Size(266, 47);
            this.groupBox57.TabIndex = 5;
            this.groupBox57.TabStop = false;
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label131.Location = new System.Drawing.Point(140, 15);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(39, 18);
            this.label131.TabIndex = 3;
            this.label131.Text = "мин.";
            // 
            // minute_numericUpDown
            // 
            this.minute_numericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minute_numericUpDown.Location = new System.Drawing.Point(97, 11);
            this.minute_numericUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.minute_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.minute_numericUpDown.Name = "minute_numericUpDown";
            this.minute_numericUpDown.Size = new System.Drawing.Size(43, 24);
            this.minute_numericUpDown.TabIndex = 2;
            this.minute_numericUpDown.ValueChanged += new System.EventHandler(this.minute_numericUpDown_ValueChanged);
            this.minute_numericUpDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadEnergyForm_KeyDown);
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label130.Location = new System.Drawing.Point(57, 15);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(36, 18);
            this.label130.TabIndex = 1;
            this.label130.Text = "час.";
            // 
            // hour_numericUpDown
            // 
            this.hour_numericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hour_numericUpDown.Location = new System.Drawing.Point(11, 11);
            this.hour_numericUpDown.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.hour_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.hour_numericUpDown.Name = "hour_numericUpDown";
            this.hour_numericUpDown.Size = new System.Drawing.Size(43, 24);
            this.hour_numericUpDown.TabIndex = 0;
            this.hour_numericUpDown.ValueChanged += new System.EventHandler(this.hour_numericUpDown_ValueChanged);
            this.hour_numericUpDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadEnergyForm_KeyDown);
            // 
            // readEnergy_button
            // 
            this.readEnergy_button.BackColor = System.Drawing.Color.Transparent;
            this.readEnergy_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.readEnergy_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.readEnergy_button.Image = ((System.Drawing.Image)(resources.GetObject("readEnergy_button.Image")));
            this.readEnergy_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.readEnergy_button.Location = new System.Drawing.Point(315, 232);
            this.readEnergy_button.Name = "readEnergy_button";
            this.readEnergy_button.Size = new System.Drawing.Size(125, 37);
            this.readEnergy_button.TabIndex = 21;
            this.readEnergy_button.Text = "Чтение";
            this.readEnergy_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.readEnergy_button.UseVisualStyleBackColor = false;
            this.readEnergy_button.Click += new System.EventHandler(this.readEnergy_button_Click);
            this.readEnergy_button.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadEnergyForm_KeyDown);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown1.Location = new System.Drawing.Point(340, 142);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(43, 24);
            this.numericUpDown1.TabIndex = 22;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadEnergyForm_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(385, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "(записей)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(227, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "сек.";
            // 
            // second_numericUpDown
            // 
            this.second_numericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.second_numericUpDown.Location = new System.Drawing.Point(180, 11);
            this.second_numericUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.second_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.second_numericUpDown.Name = "second_numericUpDown";
            this.second_numericUpDown.Size = new System.Drawing.Size(43, 24);
            this.second_numericUpDown.TabIndex = 4;
            this.second_numericUpDown.ValueChanged += new System.EventHandler(this.second_numericUpDown_ValueChanged);
            // 
            // ReadPKEForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 282);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.readEnergy_button);
            this.Controls.Add(this.groupBox57);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.monthCalendar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReadPKEForm";
            this.Text = "Чтение показателей качества энергии";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadEnergyForm_KeyDown);
            this.groupBox57.ResumeLayout(false);
            this.groupBox57.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minute_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.second_numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox57;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.NumericUpDown minute_numericUpDown;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.NumericUpDown hour_numericUpDown;
        private System.Windows.Forms.Button readEnergy_button;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown second_numericUpDown;
    }
}