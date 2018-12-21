namespace CP8507_v7
{
    partial class EditFixDateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFixDateForm));
            this.label6 = new System.Windows.Forms.Label();
            this.monthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.dayUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.editFixDate_button = new System.Windows.Forms.Button();
            this.deleteFixDay_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.monthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dayUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(262, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 18);
            this.label6.TabIndex = 55;
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
            this.monthUpDown.TabIndex = 54;
            this.monthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.monthUpDown.ValueChanged += new System.EventHandler(this.monthUpDown_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(157, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 18);
            this.label7.TabIndex = 53;
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
            this.dayUpDown.TabIndex = 52;
            this.dayUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dayUpDown.ValueChanged += new System.EventHandler(this.dayUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 19);
            this.label1.TabIndex = 51;
            this.label1.Text = "Дата:";
            // 
            // editFixDate_button
            // 
            this.editFixDate_button.BackColor = System.Drawing.Color.Transparent;
            this.editFixDate_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.editFixDate_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editFixDate_button.Image = ((System.Drawing.Image)(resources.GetObject("editFixDate_button.Image")));
            this.editFixDate_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editFixDate_button.Location = new System.Drawing.Point(194, 45);
            this.editFixDate_button.Name = "editFixDate_button";
            this.editFixDate_button.Size = new System.Drawing.Size(115, 27);
            this.editFixDate_button.TabIndex = 73;
            this.editFixDate_button.Text = "Изменить";
            this.editFixDate_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.editFixDate_button.UseVisualStyleBackColor = false;
            this.editFixDate_button.Click += new System.EventHandler(this.addFixDate_button_Click);
            // 
            // deleteFixDay_button
            // 
            this.deleteFixDay_button.BackColor = System.Drawing.Color.Transparent;
            this.deleteFixDay_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.deleteFixDay_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteFixDay_button.Image = ((System.Drawing.Image)(resources.GetObject("deleteFixDay_button.Image")));
            this.deleteFixDay_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.deleteFixDay_button.Location = new System.Drawing.Point(12, 45);
            this.deleteFixDay_button.Name = "deleteFixDay_button";
            this.deleteFixDay_button.Size = new System.Drawing.Size(115, 27);
            this.deleteFixDay_button.TabIndex = 74;
            this.deleteFixDay_button.Text = "Удалить";
            this.deleteFixDay_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteFixDay_button.UseVisualStyleBackColor = false;
            this.deleteFixDay_button.Click += new System.EventHandler(this.deleteFixDay_button_Click);
            // 
            // EditFixDateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 80);
            this.Controls.Add(this.deleteFixDay_button);
            this.Controls.Add(this.editFixDate_button);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.monthUpDown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dayUpDown);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditFixDateForm";
            this.Text = "Редактирование даты";
            ((System.ComponentModel.ISupportInitialize)(this.monthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dayUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown monthUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown dayUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button editFixDate_button;
        private System.Windows.Forms.Button deleteFixDay_button;
    }
}