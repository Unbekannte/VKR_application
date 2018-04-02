namespace VKR_FlangeCoupling
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_RebuildModel = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button_getVars = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.данныеДляПодключенияКБДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.логинToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox_login = new System.Windows.Forms.ToolStripTextBox();
            this.парольToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox_password = new System.Windows.Forms.ToolStripTextBox();
            this.проверитьПодключениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_P = new System.Windows.Forms.TextBox();
            this.textBox_RPM = new System.Windows.Forms.TextBox();
            this.textBox_T = new System.Windows.Forms.TextBox();
            this.textBox_Diam1 = new System.Windows.Forms.TextBox();
            this.textBox_Diam2 = new System.Windows.Forms.TextBox();
            this.statusBox = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_type_load = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_OpenParamForEditing = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_v2_ispolnenie_custom = new System.Windows.Forms.TextBox();
            this.radioButton_v2_ispolnenie_custom = new System.Windows.Forms.RadioButton();
            this.radioButton_v2_ispolnenie2 = new System.Windows.Forms.RadioButton();
            this.comboBox_Diam2_GOST = new System.Windows.Forms.ComboBox();
            this.radioButton_v2_ispolnenie1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_v1_ispolnenie_custom = new System.Windows.Forms.TextBox();
            this.radioButton_v1_ispolnenie_custom = new System.Windows.Forms.RadioButton();
            this.radioButton_v1_ispolnenie2 = new System.Windows.Forms.RadioButton();
            this.radioButton_v1_ispolnenie1 = new System.Windows.Forms.RadioButton();
            this.comboBox_Diam1_GOST = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton_T = new System.Windows.Forms.RadioButton();
            this.radioButton_P = new System.Windows.Forms.RadioButton();
            this.button_MakeTechnicalDrawings = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_RebuildModel
            // 
            this.button_RebuildModel.Location = new System.Drawing.Point(22, 393);
            this.button_RebuildModel.Margin = new System.Windows.Forms.Padding(4);
            this.button_RebuildModel.Name = "button_RebuildModel";
            this.button_RebuildModel.Size = new System.Drawing.Size(190, 60);
            this.button_RebuildModel.TabIndex = 6;
            this.button_RebuildModel.Text = "Перестроить сборку";
            this.button_RebuildModel.UseVisualStyleBackColor = true;
            this.button_RebuildModel.Click += new System.EventHandler(this.button_RebuildModel_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button_getVars
            // 
            this.button_getVars.Location = new System.Drawing.Point(11, 35);
            this.button_getVars.Margin = new System.Windows.Forms.Padding(4);
            this.button_getVars.Name = "button_getVars";
            this.button_getVars.Size = new System.Drawing.Size(177, 26);
            this.button_getVars.TabIndex = 9;
            this.button_getVars.Text = "Создать сборку";
            this.button_getVars.UseVisualStyleBackColor = true;
            this.button_getVars.Click += new System.EventHandler(this.button_getVars_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(678, 31);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьФайлToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(62, 27);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьФайлToolStripMenuItem
            // 
            this.открытьФайлToolStripMenuItem.Name = "открытьФайлToolStripMenuItem";
            this.открытьФайлToolStripMenuItem.Size = new System.Drawing.Size(197, 28);
            this.открытьФайлToolStripMenuItem.Text = "Открыть файл";
            this.открытьФайлToolStripMenuItem.Click += new System.EventHandler(this.открытьФайлToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(197, 28);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.данныеДляПодключенияКБДToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(106, 27);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(333, 28);
            this.toolStripMenuItem1.Text = "Путь к референсной модели";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(300, 27);
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(366, 28);
            this.toolStripMenuItem.Text = "Указать путь...";
            this.toolStripMenuItem.Click += new System.EventHandler(this.указатьПутьToolStripMenuItem_Click);
            // 
            // данныеДляПодключенияКБДToolStripMenuItem
            // 
            this.данныеДляПодключенияКБДToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.логинToolStripMenuItem,
            this.toolStripTextBox_login,
            this.парольToolStripMenuItem,
            this.toolStripTextBox_password,
            this.проверитьПодключениеToolStripMenuItem});
            this.данныеДляПодключенияКБДToolStripMenuItem.Name = "данныеДляПодключенияКБДToolStripMenuItem";
            this.данныеДляПодключенияКБДToolStripMenuItem.Size = new System.Drawing.Size(333, 28);
            this.данныеДляПодключенияКБДToolStripMenuItem.Text = "Данные для подключения к БД";
            // 
            // логинToolStripMenuItem
            // 
            this.логинToolStripMenuItem.Enabled = false;
            this.логинToolStripMenuItem.Name = "логинToolStripMenuItem";
            this.логинToolStripMenuItem.Size = new System.Drawing.Size(201, 28);
            this.логинToolStripMenuItem.Text = "Логин";
            // 
            // toolStripTextBox_login
            // 
            this.toolStripTextBox_login.Name = "toolStripTextBox_login";
            this.toolStripTextBox_login.Size = new System.Drawing.Size(100, 27);
            this.toolStripTextBox_login.TextChanged += new System.EventHandler(this.toolStripTextBox_login_TextChanged);
            // 
            // парольToolStripMenuItem
            // 
            this.парольToolStripMenuItem.Enabled = false;
            this.парольToolStripMenuItem.Name = "парольToolStripMenuItem";
            this.парольToolStripMenuItem.Size = new System.Drawing.Size(201, 28);
            this.парольToolStripMenuItem.Text = "Пароль";
            // 
            // toolStripTextBox_password
            // 
            this.toolStripTextBox_password.Name = "toolStripTextBox_password";
            this.toolStripTextBox_password.Size = new System.Drawing.Size(100, 27);
            this.toolStripTextBox_password.TextChanged += new System.EventHandler(this.toolStripTextBox_password_TextChanged);
            // 
            // проверитьПодключениеToolStripMenuItem
            // 
            this.проверитьПодключениеToolStripMenuItem.Name = "проверитьПодключениеToolStripMenuItem";
            this.проверитьПодключениеToolStripMenuItem.Size = new System.Drawing.Size(201, 28);
            this.проверитьПодключениеToolStripMenuItem.Text = "Подключиться";
            this.проверитьПодключениеToolStripMenuItem.Click += new System.EventHandler(this.проверитьПодключениеToolStripMenuItem_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 117);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(298, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "Максимальная угловая скорость, об/мин";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 29);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(182, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "Диаметр конца вала, мм";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 29);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(182, 18);
            this.label10.TabIndex = 16;
            this.label10.Text = "Диаметр конца вала, мм";
            // 
            // textBox_P
            // 
            this.textBox_P.Location = new System.Drawing.Point(344, 48);
            this.textBox_P.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_P.Name = "textBox_P";
            this.textBox_P.Size = new System.Drawing.Size(89, 24);
            this.textBox_P.TabIndex = 17;
            this.textBox_P.Text = "26.18";
            // 
            // textBox_RPM
            // 
            this.textBox_RPM.Location = new System.Drawing.Point(344, 111);
            this.textBox_RPM.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_RPM.Name = "textBox_RPM";
            this.textBox_RPM.Size = new System.Drawing.Size(89, 24);
            this.textBox_RPM.TabIndex = 18;
            this.textBox_RPM.Text = "200";
            // 
            // textBox_T
            // 
            this.textBox_T.Location = new System.Drawing.Point(344, 79);
            this.textBox_T.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_T.Name = "textBox_T";
            this.textBox_T.Size = new System.Drawing.Size(89, 24);
            this.textBox_T.TabIndex = 19;
            this.textBox_T.Text = "9000";
            // 
            // textBox_Diam1
            // 
            this.textBox_Diam1.Location = new System.Drawing.Point(194, 26);
            this.textBox_Diam1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Diam1.Name = "textBox_Diam1";
            this.textBox_Diam1.Size = new System.Drawing.Size(89, 24);
            this.textBox_Diam1.TabIndex = 20;
            this.textBox_Diam1.Text = "160";
            this.textBox_Diam1.TextChanged += new System.EventHandler(this.textBox_Diam1_TextChanged);
            // 
            // textBox_Diam2
            // 
            this.textBox_Diam2.Location = new System.Drawing.Point(194, 26);
            this.textBox_Diam2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Diam2.Name = "textBox_Diam2";
            this.textBox_Diam2.Size = new System.Drawing.Size(89, 24);
            this.textBox_Diam2.TabIndex = 21;
            this.textBox_Diam2.Text = "160";
            this.textBox_Diam2.TextChanged += new System.EventHandler(this.textBox_Diam2_TextChanged);
            // 
            // statusBox
            // 
            this.statusBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusBox.Location = new System.Drawing.Point(115, 554);
            this.statusBox.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(562, 28);
            this.statusBox.TabIndex = 23;
            this.statusBox.Text = "label1";
            this.statusBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 18);
            this.label1.TabIndex = 24;
            this.label1.Text = "Тип нагрузки:";
            // 
            // comboBox_type_load
            // 
            this.comboBox_type_load.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_type_load.FormattingEnabled = true;
            this.comboBox_type_load.Items.AddRange(new object[] {
            "Постоянная по значению и направлению",
            "Переменная, переодически может достигать вукратного увеличения",
            "Переменная, возможно реверсивное вращение"});
            this.comboBox_type_load.Location = new System.Drawing.Point(124, 15);
            this.comboBox_type_load.Name = "comboBox_type_load";
            this.comboBox_type_load.Size = new System.Drawing.Size(497, 26);
            this.comboBox_type_load.TabIndex = 25;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_MakeTechnicalDrawings);
            this.panel2.Controls.Add(this.button_RebuildModel);
            this.panel2.Controls.Add(this.button_OpenParamForEditing);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.radioButton_T);
            this.panel2.Controls.Add(this.radioButton_P);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.comboBox_type_load);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textBox_P);
            this.panel2.Controls.Add(this.textBox_T);
            this.panel2.Controls.Add(this.textBox_RPM);
            this.panel2.Location = new System.Drawing.Point(12, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(655, 467);
            this.panel2.TabIndex = 26;
            // 
            // button_OpenParamForEditing
            // 
            this.button_OpenParamForEditing.Location = new System.Drawing.Point(238, 393);
            this.button_OpenParamForEditing.Margin = new System.Windows.Forms.Padding(4);
            this.button_OpenParamForEditing.Name = "button_OpenParamForEditing";
            this.button_OpenParamForEditing.Size = new System.Drawing.Size(190, 60);
            this.button_OpenParamForEditing.TabIndex = 40;
            this.button_OpenParamForEditing.Text = "Открыть параметры для редактирования";
            this.button_OpenParamForEditing.UseVisualStyleBackColor = true;
            this.button_OpenParamForEditing.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.textBox_v2_ispolnenie_custom);
            this.panel4.Controls.Add(this.radioButton_v2_ispolnenie_custom);
            this.panel4.Controls.Add(this.radioButton_v2_ispolnenie2);
            this.panel4.Controls.Add(this.comboBox_Diam2_GOST);
            this.panel4.Controls.Add(this.radioButton_v2_ispolnenie1);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.textBox_Diam2);
            this.panel4.Location = new System.Drawing.Point(344, 158);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(300, 205);
            this.panel4.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(82, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 20);
            this.label5.TabIndex = 38;
            this.label5.Text = "Ведомый вал";
            // 
            // textBox_v2_ispolnenie_custom
            // 
            this.textBox_v2_ispolnenie_custom.Location = new System.Drawing.Point(98, 164);
            this.textBox_v2_ispolnenie_custom.Name = "textBox_v2_ispolnenie_custom";
            this.textBox_v2_ispolnenie_custom.Size = new System.Drawing.Size(89, 24);
            this.textBox_v2_ispolnenie_custom.TabIndex = 37;
            // 
            // radioButton_v2_ispolnenie_custom
            // 
            this.radioButton_v2_ispolnenie_custom.AutoSize = true;
            this.radioButton_v2_ispolnenie_custom.Location = new System.Drawing.Point(7, 143);
            this.radioButton_v2_ispolnenie_custom.Name = "radioButton_v2_ispolnenie_custom";
            this.radioButton_v2_ispolnenie_custom.Size = new System.Drawing.Size(173, 40);
            this.radioButton_v2_ispolnenie_custom.TabIndex = 36;
            this.radioButton_v2_ispolnenie_custom.TabStop = true;
            this.radioButton_v2_ispolnenie_custom.Text = "Другая длина конца \r\nвала, мм";
            this.radioButton_v2_ispolnenie_custom.UseVisualStyleBackColor = true;
            this.radioButton_v2_ispolnenie_custom.CheckedChanged += new System.EventHandler(this.radioButton_ispolnenie_custom_CheckedChanged);
            // 
            // radioButton_v2_ispolnenie2
            // 
            this.radioButton_v2_ispolnenie2.AutoSize = true;
            this.radioButton_v2_ispolnenie2.Location = new System.Drawing.Point(7, 116);
            this.radioButton_v2_ispolnenie2.Name = "radioButton_v2_ispolnenie2";
            this.radioButton_v2_ispolnenie2.Size = new System.Drawing.Size(126, 22);
            this.radioButton_v2_ispolnenie2.TabIndex = 35;
            this.radioButton_v2_ispolnenie2.TabStop = true;
            this.radioButton_v2_ispolnenie2.Text = "Исполнение 2";
            this.radioButton_v2_ispolnenie2.UseVisualStyleBackColor = true;
            // 
            // comboBox_Diam2_GOST
            // 
            this.comboBox_Diam2_GOST.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_Diam2_GOST.FormattingEnabled = true;
            this.comboBox_Diam2_GOST.Location = new System.Drawing.Point(223, 57);
            this.comboBox_Diam2_GOST.Name = "comboBox_Diam2_GOST";
            this.comboBox_Diam2_GOST.Size = new System.Drawing.Size(60, 28);
            this.comboBox_Diam2_GOST.TabIndex = 31;
            this.comboBox_Diam2_GOST.SelectedIndexChanged += new System.EventHandler(this.comboBox_Diam2_GOST_SelectedIndexChanged);
            // 
            // radioButton_v2_ispolnenie1
            // 
            this.radioButton_v2_ispolnenie1.AutoSize = true;
            this.radioButton_v2_ispolnenie1.Location = new System.Drawing.Point(7, 89);
            this.radioButton_v2_ispolnenie1.Name = "radioButton_v2_ispolnenie1";
            this.radioButton_v2_ispolnenie1.Size = new System.Drawing.Size(126, 22);
            this.radioButton_v2_ispolnenie1.TabIndex = 32;
            this.radioButton_v2_ispolnenie1.TabStop = true;
            this.radioButton_v2_ispolnenie1.Text = "Исполнение 1";
            this.radioButton_v2_ispolnenie1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 18);
            this.label3.TabIndex = 29;
            this.label3.Text = "(по ГОСТ 12080)";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.textBox_v1_ispolnenie_custom);
            this.panel3.Controls.Add(this.radioButton_v1_ispolnenie_custom);
            this.panel3.Controls.Add(this.radioButton_v1_ispolnenie2);
            this.panel3.Controls.Add(this.radioButton_v1_ispolnenie1);
            this.panel3.Controls.Add(this.comboBox_Diam1_GOST);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.textBox_Diam1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(23, 158);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(295, 205);
            this.panel3.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(80, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "Ведущий вал";
            // 
            // textBox_v1_ispolnenie_custom
            // 
            this.textBox_v1_ispolnenie_custom.Location = new System.Drawing.Point(98, 164);
            this.textBox_v1_ispolnenie_custom.Name = "textBox_v1_ispolnenie_custom";
            this.textBox_v1_ispolnenie_custom.Size = new System.Drawing.Size(89, 24);
            this.textBox_v1_ispolnenie_custom.TabIndex = 37;
            // 
            // radioButton_v1_ispolnenie_custom
            // 
            this.radioButton_v1_ispolnenie_custom.AutoSize = true;
            this.radioButton_v1_ispolnenie_custom.Location = new System.Drawing.Point(7, 143);
            this.radioButton_v1_ispolnenie_custom.Name = "radioButton_v1_ispolnenie_custom";
            this.radioButton_v1_ispolnenie_custom.Size = new System.Drawing.Size(173, 40);
            this.radioButton_v1_ispolnenie_custom.TabIndex = 36;
            this.radioButton_v1_ispolnenie_custom.TabStop = true;
            this.radioButton_v1_ispolnenie_custom.Text = "Другая длина конца \r\nвала, мм";
            this.radioButton_v1_ispolnenie_custom.UseVisualStyleBackColor = true;
            this.radioButton_v1_ispolnenie_custom.CheckedChanged += new System.EventHandler(this.radioButton_ispolnenie_custom_CheckedChanged);
            // 
            // radioButton_v1_ispolnenie2
            // 
            this.radioButton_v1_ispolnenie2.AutoSize = true;
            this.radioButton_v1_ispolnenie2.Location = new System.Drawing.Point(7, 116);
            this.radioButton_v1_ispolnenie2.Name = "radioButton_v1_ispolnenie2";
            this.radioButton_v1_ispolnenie2.Size = new System.Drawing.Size(126, 22);
            this.radioButton_v1_ispolnenie2.TabIndex = 35;
            this.radioButton_v1_ispolnenie2.TabStop = true;
            this.radioButton_v1_ispolnenie2.Text = "Исполнение 2";
            this.radioButton_v1_ispolnenie2.UseVisualStyleBackColor = true;
            // 
            // radioButton_v1_ispolnenie1
            // 
            this.radioButton_v1_ispolnenie1.AutoSize = true;
            this.radioButton_v1_ispolnenie1.Location = new System.Drawing.Point(7, 89);
            this.radioButton_v1_ispolnenie1.Name = "radioButton_v1_ispolnenie1";
            this.radioButton_v1_ispolnenie1.Size = new System.Drawing.Size(126, 22);
            this.radioButton_v1_ispolnenie1.TabIndex = 32;
            this.radioButton_v1_ispolnenie1.TabStop = true;
            this.radioButton_v1_ispolnenie1.Text = "Исполнение 1";
            this.radioButton_v1_ispolnenie1.UseVisualStyleBackColor = true;
            this.radioButton_v1_ispolnenie1.CheckedChanged += new System.EventHandler(this.radioButton_ispolnenie_custom_CheckedChanged);
            // 
            // comboBox_Diam1_GOST
            // 
            this.comboBox_Diam1_GOST.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_Diam1_GOST.FormattingEnabled = true;
            this.comboBox_Diam1_GOST.Location = new System.Drawing.Point(223, 58);
            this.comboBox_Diam1_GOST.Name = "comboBox_Diam1_GOST";
            this.comboBox_Diam1_GOST.Size = new System.Drawing.Size(60, 28);
            this.comboBox_Diam1_GOST.TabIndex = 30;
            this.comboBox_Diam1_GOST.SelectedIndexChanged += new System.EventHandler(this.comboBox_Diam1_GOST_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 18);
            this.label2.TabIndex = 28;
            this.label2.Text = "(по ГОСТ 12080)";
            // 
            // radioButton_T
            // 
            this.radioButton_T.AutoSize = true;
            this.radioButton_T.Location = new System.Drawing.Point(22, 82);
            this.radioButton_T.Name = "radioButton_T";
            this.radioButton_T.Size = new System.Drawing.Size(190, 22);
            this.radioButton_T.TabIndex = 27;
            this.radioButton_T.TabStop = true;
            this.radioButton_T.Text = "Крутящий момент, H*м";
            this.radioButton_T.UseVisualStyleBackColor = true;
            this.radioButton_T.CheckedChanged += new System.EventHandler(this.radioButton_T_CheckedChanged);
            // 
            // radioButton_P
            // 
            this.radioButton_P.AutoSize = true;
            this.radioButton_P.Location = new System.Drawing.Point(22, 50);
            this.radioButton_P.Name = "radioButton_P";
            this.radioButton_P.Size = new System.Drawing.Size(240, 22);
            this.radioButton_P.TabIndex = 26;
            this.radioButton_P.TabStop = true;
            this.radioButton_P.Text = "Передаваемая мощность, кВт";
            this.radioButton_P.UseVisualStyleBackColor = true;
            this.radioButton_P.CheckedChanged += new System.EventHandler(this.radioButton_P_CheckedChanged);
            // 
            // button_MakeTechnicalDrawings
            // 
            this.button_MakeTechnicalDrawings.Location = new System.Drawing.Point(454, 393);
            this.button_MakeTechnicalDrawings.Margin = new System.Windows.Forms.Padding(4);
            this.button_MakeTechnicalDrawings.Name = "button_MakeTechnicalDrawings";
            this.button_MakeTechnicalDrawings.Size = new System.Drawing.Size(190, 60);
            this.button_MakeTechnicalDrawings.TabIndex = 41;
            this.button_MakeTechnicalDrawings.Text = "Построить чертежи";
            this.button_MakeTechnicalDrawings.UseVisualStyleBackColor = true;
            this.button_MakeTechnicalDrawings.Click += new System.EventHandler(this.button_MakeTechnicalDrawings_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 581);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.button_getVars);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Построение фланцевых муфт";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button_getVars;
        private System.Windows.Forms.Button button_RebuildModel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_P;
        private System.Windows.Forms.TextBox textBox_RPM;
        private System.Windows.Forms.TextBox textBox_T;
        private System.Windows.Forms.TextBox textBox_Diam1;
        private System.Windows.Forms.TextBox textBox_Diam2;
        private System.Windows.Forms.Label statusBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_type_load;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton_T;
        private System.Windows.Forms.RadioButton radioButton_P;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Diam2_GOST;
        private System.Windows.Forms.ComboBox comboBox_Diam1_GOST;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButton_v1_ispolnenie_custom;
        private System.Windows.Forms.RadioButton radioButton_v1_ispolnenie2;
        private System.Windows.Forms.RadioButton radioButton_v1_ispolnenie1;
        private System.Windows.Forms.TextBox textBox_v1_ispolnenie_custom;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_v2_ispolnenie_custom;
        private System.Windows.Forms.RadioButton radioButton_v2_ispolnenie_custom;
        private System.Windows.Forms.RadioButton radioButton_v2_ispolnenie2;
        private System.Windows.Forms.RadioButton radioButton_v2_ispolnenie1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_OpenParamForEditing;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem данныеДляПодключенияКБДToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem логинToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_login;
        private System.Windows.Forms.ToolStripMenuItem парольToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_password;
        private System.Windows.Forms.ToolStripMenuItem проверитьПодключениеToolStripMenuItem;
        private System.Windows.Forms.Button button_MakeTechnicalDrawings;
    }
}

