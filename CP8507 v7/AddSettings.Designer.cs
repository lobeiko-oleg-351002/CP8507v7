namespace CP8507_v7
{
    partial class AddSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSettings));
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.saveFactoryNumber_button = new System.Windows.Forms.Button();
            this.factoryNumber_textBox = new System.Windows.Forms.TextBox();
            this.numberOfParams_textBox = new System.Windows.Forms.TextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.modification_comboBox = new System.Windows.Forms.ComboBox();
            this.saveModification_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.relays_comboBox = new System.Windows.Forms.ComboBox();
            this.saveRelaysNumber_button = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.interfaces_comboBox = new System.Windows.Forms.ComboBox();
            this.saveInterfacesNumber_button = new System.Windows.Forms.Button();
            this.readCoefs_button = new System.Windows.Forms.Button();
            this.groupBox20.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.saveFactoryNumber_button);
            this.groupBox20.Controls.Add(this.factoryNumber_textBox);
            this.groupBox20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox20.Location = new System.Drawing.Point(15, 77);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(256, 55);
            this.groupBox20.TabIndex = 4;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Заводской номер";
            // 
            // saveFactoryNumber_button
            // 
            this.saveFactoryNumber_button.BackColor = System.Drawing.Color.Transparent;
            this.saveFactoryNumber_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveFactoryNumber_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveFactoryNumber_button.Image = ((System.Drawing.Image)(resources.GetObject("saveFactoryNumber_button.Image")));
            this.saveFactoryNumber_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveFactoryNumber_button.Location = new System.Drawing.Point(148, 21);
            this.saveFactoryNumber_button.Name = "saveFactoryNumber_button";
            this.saveFactoryNumber_button.Size = new System.Drawing.Size(98, 27);
            this.saveFactoryNumber_button.TabIndex = 14;
            this.saveFactoryNumber_button.Text = "Запись";
            this.saveFactoryNumber_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveFactoryNumber_button.UseVisualStyleBackColor = false;
            this.saveFactoryNumber_button.Click += new System.EventHandler(this.saveFactoryNumber_button_Click);
            // 
            // factoryNumber_textBox
            // 
            this.factoryNumber_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.factoryNumber_textBox.Location = new System.Drawing.Point(6, 23);
            this.factoryNumber_textBox.Multiline = true;
            this.factoryNumber_textBox.Name = "factoryNumber_textBox";
            this.factoryNumber_textBox.Size = new System.Drawing.Size(130, 23);
            this.factoryNumber_textBox.TabIndex = 13;
            // 
            // numberOfParams_textBox
            // 
            this.numberOfParams_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numberOfParams_textBox.Location = new System.Drawing.Point(163, 30);
            this.numberOfParams_textBox.Multiline = true;
            this.numberOfParams_textBox.Name = "numberOfParams_textBox";
            this.numberOfParams_textBox.ReadOnly = true;
            this.numberOfParams_textBox.Size = new System.Drawing.Size(108, 23);
            this.numberOfParams_textBox.TabIndex = 18;
            // 
            // label65
            // 
            this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label65.Location = new System.Drawing.Point(12, 20);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(160, 44);
            this.label65.TabIndex = 17;
            this.label65.Text = "Число измеряемых параметров:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.modification_comboBox);
            this.groupBox1.Controls.Add(this.saveModification_button);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(15, 138);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 55);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Модификация прибора";
            // 
            // modification_comboBox
            // 
            this.modification_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.modification_comboBox.FormattingEnabled = true;
            this.modification_comboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14"});
            this.modification_comboBox.Location = new System.Drawing.Point(6, 22);
            this.modification_comboBox.Name = "modification_comboBox";
            this.modification_comboBox.Size = new System.Drawing.Size(130, 24);
            this.modification_comboBox.TabIndex = 17;
            // 
            // saveModification_button
            // 
            this.saveModification_button.BackColor = System.Drawing.Color.Transparent;
            this.saveModification_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveModification_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveModification_button.Image = ((System.Drawing.Image)(resources.GetObject("saveModification_button.Image")));
            this.saveModification_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveModification_button.Location = new System.Drawing.Point(148, 21);
            this.saveModification_button.Name = "saveModification_button";
            this.saveModification_button.Size = new System.Drawing.Size(98, 26);
            this.saveModification_button.TabIndex = 14;
            this.saveModification_button.Text = "Запись";
            this.saveModification_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveModification_button.UseVisualStyleBackColor = false;
            this.saveModification_button.Click += new System.EventHandler(this.saveModification_button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.relays_comboBox);
            this.groupBox2.Controls.Add(this.saveRelaysNumber_button);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(277, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 55);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Количество реле в приборе";
            // 
            // relays_comboBox
            // 
            this.relays_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.relays_comboBox.FormattingEnabled = true;
            this.relays_comboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.relays_comboBox.Location = new System.Drawing.Point(6, 22);
            this.relays_comboBox.Name = "relays_comboBox";
            this.relays_comboBox.Size = new System.Drawing.Size(130, 24);
            this.relays_comboBox.TabIndex = 17;
            // 
            // saveRelaysNumber_button
            // 
            this.saveRelaysNumber_button.BackColor = System.Drawing.Color.Transparent;
            this.saveRelaysNumber_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveRelaysNumber_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveRelaysNumber_button.Image = ((System.Drawing.Image)(resources.GetObject("saveRelaysNumber_button.Image")));
            this.saveRelaysNumber_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveRelaysNumber_button.Location = new System.Drawing.Point(148, 21);
            this.saveRelaysNumber_button.Name = "saveRelaysNumber_button";
            this.saveRelaysNumber_button.Size = new System.Drawing.Size(98, 26);
            this.saveRelaysNumber_button.TabIndex = 14;
            this.saveRelaysNumber_button.Text = "Запись";
            this.saveRelaysNumber_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveRelaysNumber_button.UseVisualStyleBackColor = false;
            this.saveRelaysNumber_button.Click += new System.EventHandler(this.saveRelaysNumber_button_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.interfaces_comboBox);
            this.groupBox3.Controls.Add(this.saveInterfacesNumber_button);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(277, 138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 55);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Количество интерфейсов";
            // 
            // interfaces_comboBox
            // 
            this.interfaces_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.interfaces_comboBox.FormattingEnabled = true;
            this.interfaces_comboBox.Items.AddRange(new object[] {
            "1",
            "2"});
            this.interfaces_comboBox.Location = new System.Drawing.Point(6, 22);
            this.interfaces_comboBox.Name = "interfaces_comboBox";
            this.interfaces_comboBox.Size = new System.Drawing.Size(130, 24);
            this.interfaces_comboBox.TabIndex = 17;
            // 
            // saveInterfacesNumber_button
            // 
            this.saveInterfacesNumber_button.BackColor = System.Drawing.Color.Transparent;
            this.saveInterfacesNumber_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveInterfacesNumber_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveInterfacesNumber_button.Image = ((System.Drawing.Image)(resources.GetObject("saveInterfacesNumber_button.Image")));
            this.saveInterfacesNumber_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveInterfacesNumber_button.Location = new System.Drawing.Point(148, 21);
            this.saveInterfacesNumber_button.Name = "saveInterfacesNumber_button";
            this.saveInterfacesNumber_button.Size = new System.Drawing.Size(98, 26);
            this.saveInterfacesNumber_button.TabIndex = 14;
            this.saveInterfacesNumber_button.Text = "Запись";
            this.saveInterfacesNumber_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveInterfacesNumber_button.UseVisualStyleBackColor = false;
            this.saveInterfacesNumber_button.Click += new System.EventHandler(this.saveInterfacesNumber_button_Click);
            // 
            // readCoefs_button
            // 
            this.readCoefs_button.BackColor = System.Drawing.Color.Transparent;
            this.readCoefs_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.readCoefs_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.readCoefs_button.Image = ((System.Drawing.Image)(resources.GetObject("readCoefs_button.Image")));
            this.readCoefs_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.readCoefs_button.Location = new System.Drawing.Point(349, 20);
            this.readCoefs_button.Name = "readCoefs_button";
            this.readCoefs_button.Size = new System.Drawing.Size(184, 44);
            this.readCoefs_button.TabIndex = 22;
            this.readCoefs_button.Text = "Чтение параметров";
            this.readCoefs_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.readCoefs_button.UseVisualStyleBackColor = false;
            this.readCoefs_button.Click += new System.EventHandler(this.readCoefs_button_Click);
            // 
            // AddSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 205);
            this.Controls.Add(this.readCoefs_button);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numberOfParams_textBox);
            this.Controls.Add(this.label65);
            this.Controls.Add(this.groupBox20);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddSettings";
            this.Text = "Дополнительные настройки";
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Button saveFactoryNumber_button;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button saveModification_button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button saveRelaysNumber_button;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button saveInterfacesNumber_button;
        private System.Windows.Forms.Button readCoefs_button;
        public System.Windows.Forms.TextBox factoryNumber_textBox;
        public System.Windows.Forms.TextBox numberOfParams_textBox;
        public System.Windows.Forms.ComboBox modification_comboBox;
        public System.Windows.Forms.ComboBox relays_comboBox;
        public System.Windows.Forms.ComboBox interfaces_comboBox;
    }
}