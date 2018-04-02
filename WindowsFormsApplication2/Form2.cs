using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKR_FlangeCoupling
{
    public partial class Form2 : Form
    {
        KompasManager Manager = new KompasManager();

        public Form2()
        {
            InitializeComponent();
            pictureBox1.Size = new Size((int)((double)pictureBox1.Image.Size.Width / 1.7), (int)((double)pictureBox1.Image.Size.Height / 1.7));
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            
        }

        private void button_RebuildModel_Click(object sender, EventArgs e)
        {
            Manager.LoadKompas();
            Manager.RebuildModel(dataGridView1.DataSource as DataTable);
        }
    }
}
