namespace CP8507_v7
{
    partial class TarificationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TarificationForm));
            this.seasons_tabControl = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.чтениеДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изФалаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изПрибораToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранениеДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вПриборToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сезонToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьИнтервалToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расписаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.тестНаОшибкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TarifOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.TarifSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // seasons_tabControl
            // 
            this.seasons_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seasons_tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.seasons_tabControl.Location = new System.Drawing.Point(0, 25);
            this.seasons_tabControl.Name = "seasons_tabControl";
            this.seasons_tabControl.SelectedIndex = 0;
            this.seasons_tabControl.Size = new System.Drawing.Size(964, 501);
            this.seasons_tabControl.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.правкаToolStripMenuItem,
            this.тестНаОшибкиToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(964, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.чтениеДанныхToolStripMenuItem,
            this.сохранениеДанныхToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // чтениеДанныхToolStripMenuItem
            // 
            this.чтениеДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.изФалаToolStripMenuItem,
            this.изПрибораToolStripMenuItem});
            this.чтениеДанныхToolStripMenuItem.Name = "чтениеДанныхToolStripMenuItem";
            this.чтениеДанныхToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.чтениеДанныхToolStripMenuItem.Text = "Чтение";
            // 
            // изФалаToolStripMenuItem
            // 
            this.изФалаToolStripMenuItem.Name = "изФалаToolStripMenuItem";
            this.изФалаToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.изФалаToolStripMenuItem.Text = "Из файла";
            this.изФалаToolStripMenuItem.Click += new System.EventHandler(this.изФалаToolStripMenuItem_Click);
            // 
            // изПрибораToolStripMenuItem
            // 
            this.изПрибораToolStripMenuItem.Name = "изПрибораToolStripMenuItem";
            this.изПрибораToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.изПрибораToolStripMenuItem.Text = "Из прибора";
            this.изПрибораToolStripMenuItem.Click += new System.EventHandler(this.изПрибораToolStripMenuItem_Click);
            // 
            // сохранениеДанныхToolStripMenuItem
            // 
            this.сохранениеДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вФайлToolStripMenuItem,
            this.вПриборToolStripMenuItem});
            this.сохранениеДанныхToolStripMenuItem.Name = "сохранениеДанныхToolStripMenuItem";
            this.сохранениеДанныхToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.сохранениеДанныхToolStripMenuItem.Text = "Сохранение";
            // 
            // вФайлToolStripMenuItem
            // 
            this.вФайлToolStripMenuItem.Name = "вФайлToolStripMenuItem";
            this.вФайлToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.вФайлToolStripMenuItem.Text = "В файл";
            this.вФайлToolStripMenuItem.Click += new System.EventHandler(this.вФайлToolStripMenuItem_Click);
            // 
            // вПриборToolStripMenuItem
            // 
            this.вПриборToolStripMenuItem.Name = "вПриборToolStripMenuItem";
            this.вПриборToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.вПриборToolStripMenuItem.Text = "В прибор";
            this.вПриборToolStripMenuItem.Click += new System.EventHandler(this.вПриборToolStripMenuItem_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сезонToolStripMenuItem,
            this.расписаниеToolStripMenuItem,
            this.фToolStripMenuItem});
            this.правкаToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // сезонToolStripMenuItem
            // 
            this.сезонToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.изменитьИнтервалToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.сезонToolStripMenuItem.Name = "сезонToolStripMenuItem";
            this.сезонToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.сезонToolStripMenuItem.Text = "Сезон";
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
            // 
            // изменитьИнтервалToolStripMenuItem
            // 
            this.изменитьИнтервалToolStripMenuItem.Name = "изменитьИнтервалToolStripMenuItem";
            this.изменитьИнтервалToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.изменитьИнтервалToolStripMenuItem.Text = "Изменить интервал";
            this.изменитьИнтервалToolStripMenuItem.Click += new System.EventHandler(this.изменитьИнтервалToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // расписаниеToolStripMenuItem
            // 
            this.расписаниеToolStripMenuItem.Name = "расписаниеToolStripMenuItem";
            this.расписаниеToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.расписаниеToolStripMenuItem.Text = "Добавить запись в расписание";
            this.расписаниеToolStripMenuItem.Click += new System.EventHandler(this.расписаниеToolStripMenuItem_Click);
            // 
            // фToolStripMenuItem
            // 
            this.фToolStripMenuItem.Name = "фToolStripMenuItem";
            this.фToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.фToolStripMenuItem.Text = "Добавить фиксированную дата";
            this.фToolStripMenuItem.Click += new System.EventHandler(this.фToolStripMenuItem_Click);
            // 
            // тестНаОшибкиToolStripMenuItem
            // 
            this.тестНаОшибкиToolStripMenuItem.Name = "тестНаОшибкиToolStripMenuItem";
            this.тестНаОшибкиToolStripMenuItem.Size = new System.Drawing.Size(137, 21);
            this.тестНаОшибкиToolStripMenuItem.Text = "Проверка на ошибки";
            this.тестНаОшибкиToolStripMenuItem.Click += new System.EventHandler(this.тестНаОшибкиToolStripMenuItem_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(65, 21);
            this.помощьToolStripMenuItem.Text = "Справка";
            // 
            // TarificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(964, 526);
            this.Controls.Add(this.seasons_tabControl);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TarificationForm";
            this.Text = "Редактор тарифов";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl seasons_tabControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem чтениеДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранениеДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сезонToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem расписаниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьИнтервалToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem тестНаОшибкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изФалаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изПрибораToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вПриборToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog TarifOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog TarifSaveFileDialog;




    }
}