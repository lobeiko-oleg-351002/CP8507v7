namespace CP8507_v7
{
    partial class AddFixDateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFixDateForm));
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.minute_startUpDown = new System.Windows.Forms.NumericUpDown();
            this.label130 = new System.Windows.Forms.Label();
            this.hour_startUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.minute_endUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.hour_endUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.addFixDate_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.monthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.dayUpDown = new System.Windows.Forms.NumericUpDown();
            this.tarif_comboBox = new System.Windows.Forms.ComboBox();
            this.seasons_comboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.minute_startUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour_startUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minute_endUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour_endUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dayUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(12, 42);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(90, 37);
            this.label20.TabIndex = 30;
            this.label20.Text = "Начало интервала:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 19);
            this.label1.TabIndex = 33;
            this.label1.Text = "Дата:";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label131.Location = new System.Drawing.Point(262, 59);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(49, 18);
            this.label131.TabIndex = 37;
            this.label131.Text = "минут";
            // 
            // minute_startUpDown
            // 
            this.minute_startUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minute_startUpDown.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.minute_startUpDown.Location = new System.Drawing.Point(213, 56);
            this.minute_startUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.minute_startUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.minute_startUpDown.Name = "minute_startUpDown";
            this.minute_startUpDown.Size = new System.Drawing.Size(43, 24);
            this.minute_startUpDown.TabIndex = 36;
            this.minute_startUpDown.ValueChanged += new System.EventHandler(this.minute_numericUpDown_ValueChanged);
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label130.Location = new System.Drawing.Point(157, 59);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(49, 18);
            this.label130.TabIndex = 35;
            this.label130.Text = "часов";
            // 
            // hour_startUpDown
            // 
            this.hour_startUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hour_startUpDown.Location = new System.Drawing.Point(108, 55);
            this.hour_startUpDown.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.hour_startUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.hour_startUpDown.Name = "hour_startUpDown";
            this.hour_startUpDown.Size = new System.Drawing.Size(43, 24);
            this.hour_startUpDown.TabIndex = 34;
            this.hour_startUpDown.ValueChanged += new System.EventHandler(this.hour_numericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(262, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 18);
            this.label2.TabIndex = 42;
            this.label2.Text = "минут";
            // 
            // minute_endUpDown
            // 
            this.minute_endUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minute_endUpDown.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.minute_endUpDown.Location = new System.Drawing.Point(213, 102);
            this.minute_endUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.minute_endUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.minute_endUpDown.Name = "minute_endUpDown";
            this.minute_endUpDown.Size = new System.Drawing.Size(43, 24);
            this.minute_endUpDown.TabIndex = 41;
            this.minute_endUpDown.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(157, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 18);
            this.label3.TabIndex = 40;
            this.label3.Text = "часов";
            // 
            // hour_endUpDown
            // 
            this.hour_endUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hour_endUpDown.Location = new System.Drawing.Point(108, 101);
            this.hour_endUpDown.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.hour_endUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.hour_endUpDown.Name = "hour_endUpDown";
            this.hour_endUpDown.Size = new System.Drawing.Size(43, 24);
            this.hour_endUpDown.TabIndex = 39;
            this.hour_endUpDown.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 37);
            this.label4.TabIndex = 38;
            this.label4.Text = "Конец интервала:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 19);
            this.label5.TabIndex = 43;
            this.label5.Text = "Тариф:";
            // 
            // addFixDate_button
            // 
            this.addFixDate_button.BackColor = System.Drawing.Color.Transparent;
            this.addFixDate_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addFixDate_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addFixDate_button.Image = ((System.Drawing.Image)(resources.GetObject("addFixDate_button.Image")));
            this.addFixDate_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addFixDate_button.Location = new System.Drawing.Point(196, 205);
            this.addFixDate_button.Name = "addFixDate_button";
            this.addFixDate_button.Size = new System.Drawing.Size(115, 27);
            this.addFixDate_button.TabIndex = 46;
            this.addFixDate_button.Text = "Добавить";
            this.addFixDate_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addFixDate_button.UseVisualStyleBackColor = false;
            this.addFixDate_button.Click += new System.EventHandler(this.addFixDate_button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(262, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 18);
            this.label6.TabIndex = 50;
            this.label6.Text = "месяц";
            // 
            // monthUpDown
            // 
            this.monthUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthUpDown.Location = new System.Drawing.Point(213, 11);
            this.monthUpDown.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.monthUpDown.Name = "monthUpDown";
            this.monthUpDown.Size = new System.Drawing.Size(43, 24);
            this.monthUpDown.TabIndex = 49;
            this.monthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.monthUpDown.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(157, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 18);
            this.label7.TabIndex = 48;
            this.label7.Text = "день";
            // 
            // dayUpDown
            // 
            this.dayUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dayUpDown.Location = new System.Drawing.Point(108, 10);
            this.dayUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.dayUpDown.Name = "dayUpDown";
            this.dayUpDown.Size = new System.Drawing.Size(43, 24);
            this.dayUpDown.TabIndex = 47;
            this.dayUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dayUpDown.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // tarif_comboBox
            // 
            this.tarif_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tarif_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tarif_comboBox.FormattingEnabled = true;
            this.tarif_comboBox.Items.AddRange(new object[] {
            "Тариф 1",
            "Тариф 2",
            "Тариф 3",
            "Тариф 4",
            "Тариф 5",
            "Тариф 6",
            "Тариф 7",
            "Тариф 8"});
            this.tarif_comboBox.Location = new System.Drawing.Point(108, 138);
            this.tarif_comboBox.Name = "tarif_comboBox";
            this.tarif_comboBox.Size = new System.Drawing.Size(109, 24);
            this.tarif_comboBox.TabIndex = 51;
            // 
            // seasons_comboBox
            // 
            this.seasons_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seasons_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.seasons_comboBox.FormattingEnabled = true;
            this.seasons_comboBox.Location = new System.Drawing.Point(108, 171);
            this.seasons_comboBox.Name = "seasons_comboBox";
            this.seasons_comboBox.Size = new System.Drawing.Size(109, 24);
            this.seasons_comboBox.TabIndex = 53;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(12, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 19);
            this.label8.TabIndex = 52;
            this.label8.Text = "Сезон:";
            // 
            // AddFixDateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 244);
            this.Controls.Add(this.seasons_comboBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tarif_comboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.monthUpDown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dayUpDown);
            this.Controls.Add(this.addFixDate_button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.minute_endUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hour_endUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label131);
            this.Controls.Add(this.minute_startUpDown);
            this.Controls.Add(this.label130);
            this.Controls.Add(this.hour_startUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label20);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddFixDateForm";
            this.Text = "Фиксированная дата";
            ((System.ComponentModel.ISupportInitialize)(this.minute_startUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour_startUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minute_endUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour_endUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dayUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.NumericUpDown minute_startUpDown;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.NumericUpDown hour_startUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown minute_endUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown hour_endUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button addFixDate_button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown monthUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown dayUpDown;
        private System.Windows.Forms.ComboBox tarif_comboBox;
        private System.Windows.Forms.ComboBox seasons_comboBox;
        private System.Windows.Forms.Label label8;
    }
}