using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace deneme2
{
    public partial class KelimeEkleme : Form
    {
        string loggedusername;
        public KelimeEkleme(string loggedusername)
        {
            this.loggedusername = loggedusername;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resimal();
        }
        private void resimal()
        {

            resimSec.Filter = "Resim Dosyası | *.jpg;*.jpeg;*.png;";
            resimSec.ShowDialog();
            pictureBox1.Image = new Bitmap(resimSec.FileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddWordToDB(txtBoxEngWordName.Text, txtBoxTrWordName.Text,txtBoxWordExample.Text);


        }
        private string connectionString = "Data Source= .;Initial Catalog = 6TekrardaDilOgrenme; Integrated Security = True";
        private void AddWordToDB(string engWordName, string trWordName,string wordExample)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT kullaniciID FROM KullaniciGiris WHERE KullaniciAdi = @KullaniciAdi", connection);
                command.Parameters.AddWithValue("@KullaniciAdi", loggedusername);
                int kullaniciID = (int)command.ExecuteScalar();
                SqlCommand insterWordCommand = new SqlCommand("sp_EkleKelime", connection);
                insterWordCommand.CommandType = CommandType.StoredProcedure;
                insterWordCommand.Parameters.AddWithValue("@IngilizceKelime", engWordName);
                insterWordCommand.Parameters.AddWithValue("@TurkceKelime", trWordName);
                insterWordCommand.Parameters.AddWithValue("@kelimeResimYolu", resimSec.FileName);
                insterWordCommand.Parameters.AddWithValue("@Ornek", wordExample);
                insterWordCommand.Parameters.AddWithValue("KullaniciID", kullaniciID);
                try
                {
                    insterWordCommand.ExecuteNonQuery();
                    MessageBox.Show("Kelime eklenmiştir.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }

      
    }
}

 
             
                


            
        


