using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using VKR_FlangeCoupling.Details;

namespace VKR_FlangeCoupling
{
    public partial class Form1 : Form
    {

        KompasManager Manager = new KompasManager();
        DataTable params_Table;
        GOST_Provider provider;
        Mufta mufta = new Mufta();
        Form2 form2 = new Form2();

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            comboBox_type_load.SelectedIndex = 0;
            comboBox_type_load.DropDownStyle = ComboBoxStyle.DropDownList;
            radioButton_P.Checked = true;
            radioButton_v1_ispolnenie1.Checked = true;
            radioButton_v2_ispolnenie1.Checked = true;
            timer1.Start();
            toolStripTextBox1.TextBox.Text = Properties.Settings.Default.PathToReferenceModel;
            toolStripTextBox_login.TextBox.Text = Properties.Settings.Default.DB_login;
            toolStripTextBox_password.TextBox.Text = Properties.Settings.Default.DB_password;

            try
            {
                provider = new GOST_Provider();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось подключиться к СУБД.\nВведите данные для подключения в настройках приложения");
                return;
            }
            fill_cb_from_gost();
        }



        private void textBox_Diam2_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Diam2.Text != comboBox_Diam2_GOST.Text)
            {
                comboBox_Diam2_GOST.SelectedIndex = -1;
            }
        }

        private void textBox_Diam1_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Diam1.Text != comboBox_Diam1_GOST.Text)
            {
                comboBox_Diam1_GOST.SelectedIndex = -1;
            }
        }

        private void проверитьПодключениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                provider = new GOST_Provider();
                OnLoad(e);
                MessageBox.Show("Авторизация выполнена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        private void toolStripTextBox_login_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DB_login = toolStripTextBox_login.TextBox.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void toolStripTextBox_password_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DB_password = toolStripTextBox_password.TextBox.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

        }

        private void button_RebuildModel_Click(object sender, EventArgs e)
        {
            try
            {
                Get_or_Calc_all_param_of_Mufta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            Manager.RebuildModel(params_Table);

        }
        
        private void radioButton_P_CheckedChanged(object sender, EventArgs e)
        {
            textBox_P.ReadOnly = false;
            textBox_P.Visible = true;
            textBox_T.ReadOnly = true;
            textBox_T.Visible = false;
        }

        private void radioButton_T_CheckedChanged(object sender, EventArgs e)
        {
            textBox_P.ReadOnly = true;
            textBox_P.Visible = false;
            textBox_T.ReadOnly = false;
            textBox_T.Visible = true;
        }

        void fill_cb_from_gost()
        {

            var list1 = provider.get_one_column_as_List("12080-66", "1", "d", "");
            var list2 = provider.get_one_column_as_List("12080-66", "1", "d", "");
            comboBox_Diam1_GOST.DataSource = list1;
            comboBox_Diam1_GOST.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Diam1_GOST.SelectedIndex = -1;

            comboBox_Diam2_GOST.DataSource = list2;
            comboBox_Diam2_GOST.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Diam2_GOST.SelectedIndex = -1;


        }

        private void comboBox_Diam1_GOST_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_Diam1.Text = comboBox_Diam1_GOST.Text;
        }

        private void comboBox_Diam2_GOST_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_Diam2.Text = comboBox_Diam2_GOST.Text;
        }

        private void radioButton_ispolnenie_custom_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_v1_ispolnenie_custom.Checked)
            {
                textBox_v1_ispolnenie_custom.Visible = true;
            }
            else
            {
                textBox_v1_ispolnenie_custom.Visible = false;
            }
            if (radioButton_v2_ispolnenie_custom.Checked)
            {
                textBox_v2_ispolnenie_custom.Visible = true;
            }
            else
            {
                textBox_v2_ispolnenie_custom.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form2.Show();
        }


        private void указатьПутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Файл сборки (.a3d)|*.a3d|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            try
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.PathToReferenceModel);
            }
            catch (Exception)
            {
                //throw;
            }

            openFileDialog1.Multiselect = false;

            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.PathToReferenceModel = openFileDialog1.FileName;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }
            toolStripTextBox1.TextBox.Text = Properties.Settings.Default.PathToReferenceModel;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            statusBox.Text = Manager.GetInfo();
            if (params_Table == null || Manager.get_current_doc() == null)
            {
                button_RebuildModel.Enabled = false;
                button_OpenParamForEditing.Enabled = false;
                button_MakeTechnicalDrawings.Enabled = false;

            }
            else
            {
                button_RebuildModel.Enabled = true;
                button_OpenParamForEditing.Enabled = true;
                button_MakeTechnicalDrawings.Enabled = true;
            }
        }
        
        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //    string appDir = System.Windows.Forms.Application.StartupPath;

            //   string parent_dir = @"..\..\..\";

            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Файл сборки (.a3d)|*.a3d|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.InitialDirectory = Directory.GetParent(Properties.Settings.Default.PathToReferenceModel).Parent.FullName;

            openFileDialog1.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            DialogResult result = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            string fileName = string.Empty;
            if (result == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                //   textBox1.Text = fileName;
                open_a3d_file(fileName);

            }

        }

        void open_a3d_file(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("Выберите файл!");
                return;
            }
            if (!System.IO.File.Exists(path))
            {
                MessageBox.Show("Файл не существует или указан неверный путь.");
                return;
            }
            try
            {
                Manager.openFile(path);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть файл.");
            }
        }
        
        private void button_getVars_Click(object sender, EventArgs e)
        {
            string path_to_basic_file = Properties.Settings.Default.PathToReferenceModel;
            string path_to_new_file = "";
            if (!File.Exists(path_to_basic_file))
            {
                MessageBox.Show("Файл сборки не найден. Укажите корректный путь в настройках.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directory.GetParent(path_to_basic_file).Parent.FullName;
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Фланцевая муфта [Mкр=0, Diam1=0, Diam2=0]";
            saveFileDialog.AddExtension = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                var ss = saveFileDialog.FileName;
                FileInfo fileInfo = new FileInfo(ss);

                var path_to_new_dir = fileInfo.DirectoryName + "\\" + Path.GetFileNameWithoutExtension(fileInfo.Name);

                DirectoryCopy(Directory.GetParent(path_to_basic_file).FullName, path_to_new_dir, true);

                path_to_new_file = path_to_new_dir + "\\" + new FileInfo(path_to_basic_file).Name;

            }
            
            open_a3d_file(path_to_new_file);

            getVariables();

        }

        //взял с StackOverflow
        private static void DirectoryCopy(
        string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                try
                {
                    // Create the path to the new copy of the file.
                    string temppath = Path.Combine(destDirName, file.Name);

                    // Copy the file.
                    file.CopyTo(temppath, true);
                }
                catch (Exception)
                {//throw;
                }
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void getVariables()
        {
            form2.dataGridView1.AutoGenerateColumns = true;


            List<kompasVariable> Variables = Manager.GetVarsOfAssembly();

            if (Variables == null)
            {
                return;
            }

            params_Table = provider.ToDataTable(Variables);

            form2.dataGridView1.DataSource = params_Table;

            form2.dataGridView1.Columns["displayName"].ReadOnly = true;
            form2.dataGridView1.Columns["name"].ReadOnly = true;
            form2.dataGridView1.Columns["name"].Visible = false;
            form2.dataGridView1.Columns["note"].ReadOnly = true;
            form2.dataGridView1.Columns["value"].ReadOnly = false;
            form2.dataGridView1.Columns["displayName"].HeaderText = "Имя";
            form2.dataGridView1.Columns["note"].HeaderText = "Комментарий";
            form2.dataGridView1.Columns["note"].Width *= 2;

            form2.dataGridView1.Columns["value"].HeaderText = "Значение";

        }

        void read_params_from_Form(ref double P, ref double T, ref double K_T, ref double RPM, ref double w, ref double Diam1,
            ref double Diam2, ref int ispolnenie_v1, ref int ispolnenie_v2, ref double l_v1, ref double l_v2)
        {
            try
            {
                RPM = double.Parse(textBox_RPM.Text);
            }
            catch (Exception)
            {
                throw new Exception("При вводе скорости вращения допущена ошибка");
            }

            try
            {
                Diam1 = double.Parse(textBox_Diam1.Text);
                Diam2 = double.Parse(textBox_Diam2.Text);
            }
            catch (Exception)
            {
                throw new Exception("При вводе диаметров допущена ошибка");
            }

            w = 2 * Math.PI * RPM / 60;

            if (radioButton_P.Checked)
            {
                try
                {
                    P = double.Parse(textBox_P.Text.Replace(".", ",")) * 1000;
                }
                catch (Exception)
                {
                    throw new Exception("При вводе мощности допущена ошибка");
                }
                T = P / w;
            }
            else
            {
                try
                {
                    T = double.Parse(textBox_T.Text);

                }
                catch (Exception)
                {
                    throw new Exception("При вводе крутящего момента допущена ошибка");
                }
            }

            switch (comboBox_type_load.SelectedIndex)
            {
                case 0:
                    K_T = 1;
                    break;
                case 1:
                    K_T = 1.4;
                    break;
                case 2:
                    K_T = 1.96;
                    break;
            }
            if (K_T == 0)
            {
                throw new Exception("Не выбран тип нагрузки");
            }
            T = T * K_T;



            if (radioButton_v1_ispolnenie1.Checked)
            {
                ispolnenie_v1 = 1;
            }
            else if (radioButton_v1_ispolnenie2.Checked)
            {
                ispolnenie_v1 = 2;
            }
            else if (radioButton_v1_ispolnenie_custom.Checked)
            {
                ispolnenie_v1 = 3;
                l_v1 = double.Parse(textBox_v1_ispolnenie_custom.Text.Replace(".", ","));
            }
            else
            {
                throw new Exception("Ошибка в выборе исполнения");
            }


            if (radioButton_v2_ispolnenie1.Checked)
            {
                ispolnenie_v2 = 1;
            }
            else if (radioButton_v2_ispolnenie2.Checked)
            {
                ispolnenie_v2 = 2;
            }
            else if (radioButton_v2_ispolnenie_custom.Checked)
            {
                ispolnenie_v2 = 3;
                l_v2 = double.Parse(textBox_v2_ispolnenie_custom.Text.Replace(".", ","));
            }
            else
            {
                throw new Exception("Ошибка в выборе исполнения");
            }

            if (ispolnenie_v1 != 3)
            {
                l_v1 = get_l_by_d(Diam1, ispolnenie_v1);
            }
            if (ispolnenie_v2 != 3)
            {
                l_v2 = get_l_by_d(Diam2, ispolnenie_v2);
            }

        }


        private void Get_or_Calc_all_param_of_Mufta()
        {
            if (params_Table == null)
            {
                throw new Exception("Таблица параметров еще не заполенена. Откройте КОМПАС-3D и загрузите модель");
            }

            double T = 0;
            double P = 0;
            double K_T = 0; //коэффицент нагрузки
            double w = 0;
            double RPM = 0;
            double Diam1 = 0;
            double Diam2 = 0;
            double V = 0;
            double l_v1 = 0;
            double l_v2 = 0;
            int ispolnenie_v1 = 0;
            int ispolnenie_v2 = 0;

            mufta = new Mufta();

            ///// чтение данных с формы и расчет недостающих парметров
            try
            {
                read_params_from_Form(ref P, ref T, ref K_T, ref RPM, ref w, ref Diam1, ref Diam2, ref ispolnenie_v1,
                    ref ispolnenie_v2, ref l_v1, ref l_v2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //throw;
            }


            ////// поиск подходящей муфты в ГОСТе

            bool mufta_is_selected = false;

            if (Diam1 == Diam2)
            {
                try
                {
                    DataTable polumufta1 = provider.get_rows("20761-96", "a1",
                        parameters: $"m_kr >= {T.ToString("#.##")}, d_inner = {Diam1.ToString("#.##")}, isp = {ispolnenie_v1}");
                    DataTable polumufta2 = provider.get_rows("20761-96", "a1",
                        parameters: $"m_kr >= {T.ToString("#.##")}, d_inner = {Diam2.ToString("#.##")}, isp = {ispolnenie_v2}");
                    V = double.Parse(polumufta1.Rows[0]["d_external"].ToString()) / 2 / 1000 * w; //R в метрах
                    if (V > 70)
                    {
                        throw new Exception(
                            "Окружная скорость на наружном диаметре муфты более 70 м/с. Использование фланцевой муфты в данном соединении невозможно.");
                    }


                    DialogResult result = MessageBox.Show(
                        $"В ГОСТ 20761 найдена муфта до {polumufta1.Rows[0]["m_kr"]} H*м.\nВыбрать эту муфту или рассчитать новую?",
                        "Уведомление", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        mufta.Create_by_GOST(T, Diam1, Diam2, ispolnenie_v1, ispolnenie_v2);

                        mufta_is_selected = true;
                    }
                    else if (result == DialogResult.No)
                    {
                        mufta_is_selected = false;
                    }

                }
                catch (Exception)
                {
                    //throw new Exception("В ГОСТ 20761 нет муфт, подходящих по данным параметрам.");
                }
            }

            ///////если не нашли или не удовлетворяет муфта из ГОСТ, выполняем расчет собственной
            if (mufta_is_selected == false)
            {
                mufta.Calculate_Param_of_Coupling(T, w, Diam1, Diam2, l_v1, l_v2, ispolnenie_v1, ispolnenie_v2);

            }

            //в конце меняем переменные в сборке.
            set_newValues_all(mufta);

        }


        void set_newValues_all(Mufta mufta)
        {
            set_new_Values("ST1", mufta.master);
            set_new_Values("ST2", mufta.slave);
            set_new_Values("B7796", mufta.bolt7796);
            set_new_Values("B7817", mufta.bolt7817);
            set_new_Values("G15521", mufta.gayka);
            set_new_Values("SH6402", mufta.shayba);
        }

        public void set_new_Values(string name_of_Element, object instance_of_class)
        {

            var fields = instance_of_class.GetType().GetFields();

            var count = fields.Length;
            string[] arrNames = new string[count];
            string[] arrValues = new string[count];
            for (int i = 0; i < count; i++)
            {
                var ss = fields[i].Name;
                var val = fields[i].GetValue(instance_of_class).ToString();
                arrNames[i] = ss;
                arrValues[i] = val;
            }

            for (int i = 0; i < count; i++)
            {
                set_newValue($"{name_of_Element}_{arrNames[i]}", arrValues[i]);
            }
        }

        void set_newValue(string colName, string val)
        {
            if (params_Table == null)
            {
                getVariables();
            }
            if (params_Table != null)
            {
                foreach (DataRow row in params_Table.Rows)
                {
                    try
                    {
                        if (row["name"].ToString() == colName)
                        {
                            row["value"] = val;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        double get_l_by_d(double d, int ispolnenie)
        {

            string column = "l" + ispolnenie.ToString();
            DataTable dliny_koncov_valov = provider.get_rows("12080-66", "1", column, $"d=" + d);

            double l = double.Parse(dliny_koncov_valov.Rows[0][column].ToString());

            return l;
        }

        private void button_MakeTechnicalDrawings_Click(object sender, EventArgs e)
        {
            getVariables();
            Manager.MakeTechnicalDrawings_STUPICA(mufta.master);
            Manager.MakeTechnicalDrawings_STUPICA(mufta.slave);
        }
    }
}
