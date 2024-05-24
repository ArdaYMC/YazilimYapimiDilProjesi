using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneme2
{
    public partial class Form3 : Form
    {
        public Form3(int kullaniciID)
        {
            InitializeComponent();
            label1.Text = DateTime.Now.ToString();
            this.kullaniciID = kullaniciID;
        }
        private  int kullaniciID;
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 anamenu = new Form1();
            anamenu.Show();
            this.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 quizeGiris = new Form5();
            quizeGiris.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 kelimeekleme = new Form6(kullaniciID);
            kelimeekleme.Show();
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            KullaniciBilgiler info = new KullaniciBilgiler(kullaniciID);
            info.Show();
            this.Close();
        }
    }
}
