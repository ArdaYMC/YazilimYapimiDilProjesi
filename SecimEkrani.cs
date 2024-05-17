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
    public partial class SecimEkrani : Form
    {
        string loggedusername;
        public SecimEkrani(string loggedusername)
        {
            this.loggedusername = loggedusername;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KullaniciGirisEkrani anamenu = new KullaniciGirisEkrani();
            anamenu.Show();
            this.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            GunlukKelimeSormaEkrani quizeGiris = new GunlukKelimeSormaEkrani();
            quizeGiris.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KelimeEkleme kelimeekleme = new KelimeEkleme(loggedusername);
            kelimeekleme.Show();
            this.Close();
        }
    }
}
