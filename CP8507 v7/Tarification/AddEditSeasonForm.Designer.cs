namespace CP8507_v7
{
    partial class AddEditSeasonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditSeasonForm));
            this.addSeasonDate_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.month_startUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.day_startUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.month_endUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.day_endUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.month_startUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.day_startUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.month_endUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.day_endUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // addSeasonDate_button
            // 
            this.addSeasonDate_button.BackColor = System.Drawing.Color.Transparent;
            this.addSeasonDate_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addSeasonDate_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addSeasonDate_button.Image = ((System.Drawing.Image)(resources.GetObject("addSeasonDate_button.Image")));
            this.addSeasonDate_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addSeasonDate_button.Location = new System.Drawing.Point(194, 108);
            this.addSeasonDate_button.Name = "addSeasonDate_button";
            this.addSeasonDate_button.Size = new System.Drawing.Size(115, 27);
            this.addSeasonDate_button.TabIndex = 57;
            this.addSeasonDate_button.Text = "Добавить";
            this.addSeasonDate_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addSeasonDate_button.UseVisualStyleBackColor = false;
            this.addSeasonDate_button.Click += new System.EventHandler(this.addSeasonDate_button_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 37);
            this.label4.TabIndex = 52;
            this.label4.Text = "Конец интервала:";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(12, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(90, 37);
            this.label20.TabIndex = 47;
            this.label20.Text = "Начало интервала:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(262, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 18);
            this.label6.TabIndex = 61;
            this.label6.Text = "месяц";
            // 
            // month_startUpDown
            // 
            this.month_startUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.month_startUpDown.Location = new System.Drawing.Point(213, 23);
            this.month_startUpDown.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.month_startUpDown.Name = "month_startUpDown";
            this.month_startUpDown.Size = new System.Drawing.Size(43, 24);
            this.month_startUpDown.TabIndex = 60;
            this.month_startUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.month_startUpDown.ValueChanged += new System.EventHandler(this.monthUpDown_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(157, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 18);
            this.label7.TabIndex = 59;
            this.label7.Text = "день";
            // 
            // day_startUpDown
            // 
            this.day_startUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.day_startUpDown.Location = new System.Drawing.Point(108, 22);
            this.day_startUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.day_startUpDown.Name = "day_startUpDown";
            this.day_startUpDown.Size = new System.Drawing.Size(43, 24);
            this.day_startUpDown.TabIndex = 58;
            this.day_startUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.day_startUpDown.ValueChanged += new System.EventHandler(this.dayUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(262, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 18);
            this.label1.TabIndex = 65;
            this.label1.Text = "месяц";
            // 
            // month_endUpDown
            // 
            this.month_endUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.month_endUpDown.Location = new System.Drawing.Point(213, 69);
            this.month_endUpDown.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.month_endUpDown.Name = "month_endUpDown";
            this.month_endUpDown.Size = new System.Drawing.Size(43, 24);
            this.month_endUpDown.TabIndex = 64;
            this.month_endUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.month_endUpDown.ValueChanged += new System.EventHandler(this.month_endUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(157, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 18);
            this.label2.TabIndex = 63;
            this.label2.Text = "день";
            // 
            // day_endUpDown
            // 
            this.day_endUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.day_endUpDown.Location = new System.Drawing.Point(108, 68);
            this.day_endUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.day_endUpDown.Name = "day_endUpDown";
            this.day_endUpDown.Size = new System.Drawing.Size(43, 24);
            this.day_endUpDown.TabIndex = 62;
            this.day_endUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.day_endUpDown.ValueChanged += new System.EventHandler(this.day_endUpDown_ValueChanged);
            // 
            // AddEditSeasonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 148);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.month_endUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.day_endUpDown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.month_startUpDown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.day_startUpDown);
            this.Controls.Add(this.addSeasonDate_button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label20);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddEditSeasonForm";
            this.Text = "qwe";
            ((System.ComponentModel.ISupportInitialize)(this.month_startUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.day_startUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.month_endUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.day_endUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addSeasonDate_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown month_startUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown day_startUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown month_endUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown day_endUpDown;

    }
}