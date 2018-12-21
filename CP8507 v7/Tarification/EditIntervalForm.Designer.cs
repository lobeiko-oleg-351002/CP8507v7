namespace CP8507_v7
{
    partial class EditIntervalForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditIntervalForm));
            this.label2 = new System.Windows.Forms.Label();
            this.minute_endUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.hour_endUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.minute_startUpDown = new System.Windows.Forms.NumericUpDown();
            this.label130 = new System.Windows.Forms.Label();
            this.hour_startUpDown = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.addFixDate_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.minute_endUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour_endUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minute_startUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour_startUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(262, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 18);
            this.label2.TabIndex = 71;
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
            this.minute_endUpDown.Location = new System.Drawing.Point(213, 69);
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
            this.minute_endUpDown.TabIndex = 70;
            this.minute_endUpDown.ValueChanged += new System.EventHandler(this.minute_endUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(157, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 18);
            this.label3.TabIndex = 69;
            this.label3.Text = "часов";
            // 
            // hour_endUpDown
            // 
            this.hour_endUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hour_endUpDown.Location = new System.Drawing.Point(108, 68);
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
            this.hour_endUpDown.TabIndex = 68;
            this.hour_endUpDown.ValueChanged += new System.EventHandler(this.hour_endUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 37);
            this.label4.TabIndex = 67;
            this.label4.Text = "Конец интервала:";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label131.Location = new System.Drawing.Point(262, 26);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(49, 18);
            this.label131.TabIndex = 66;
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
            this.minute_startUpDown.Location = new System.Drawing.Point(213, 23);
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
            this.minute_startUpDown.TabIndex = 65;
            this.minute_startUpDown.ValueChanged += new System.EventHandler(this.minute_startUpDown_ValueChanged);
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label130.Location = new System.Drawing.Point(157, 26);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(49, 18);
            this.label130.TabIndex = 64;
            this.label130.Text = "часов";
            // 
            // hour_startUpDown
            // 
            this.hour_startUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hour_startUpDown.Location = new System.Drawing.Point(108, 22);
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
            this.hour_startUpDown.TabIndex = 63;
            this.hour_startUpDown.ValueChanged += new System.EventHandler(this.hour_startUpDown_ValueChanged);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(12, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(90, 37);
            this.label20.TabIndex = 62;
            this.label20.Text = "Начало интервала:";
            // 
            // addFixDate_button
            // 
            this.addFixDate_button.BackColor = System.Drawing.Color.Transparent;
            this.addFixDate_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addFixDate_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addFixDate_button.Image = ((System.Drawing.Image)(resources.GetObject("addFixDate_button.Image")));
            this.addFixDate_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addFixDate_button.Location = new System.Drawing.Point(191, 103);
            this.addFixDate_button.Name = "addFixDate_button";
            this.addFixDate_button.Size = new System.Drawing.Size(115, 27);
            this.addFixDate_button.TabIndex = 72;
            this.addFixDate_button.Text = "Изменить";
            this.addFixDate_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addFixDate_button.UseVisualStyleBackColor = false;
            this.addFixDate_button.Click += new System.EventHandler(this.addFixDate_button_Click);
            // 
            // EditIntervalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 138);
            this.Controls.Add(this.addFixDate_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.minute_endUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hour_endUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label131);
            this.Controls.Add(this.minute_startUpDown);
            this.Controls.Add(this.label130);
            this.Controls.Add(this.hour_startUpDown);
            this.Controls.Add(this.label20);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditIntervalForm";
            this.Text = "Изменить интервал";
            ((System.ComponentModel.ISupportInitialize)(this.minute_endUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour_endUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minute_startUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour_startUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown minute_endUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown hour_endUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.NumericUpDown minute_startUpDown;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.NumericUpDown hour_startUpDown;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button addFixDate_button;
    }
}