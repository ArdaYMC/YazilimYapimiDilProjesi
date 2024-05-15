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
    public partial class Form6 : Form
    {
        public Form6()
        {
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
            AddWordToDB(txtBoxEngWordName.Text, txtBoxTrWordName.Text);
 
        }
        private string connectionString = "Data Source= .;Initial Catalog = 6TekrardaDilOgrenme; Integrated Security = True";
        private void AddWordToDB(string engWordName, string trWordName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("Insert into Kelimeler(ingilizceKelime, turkceKelime,kelimeResimYolu) values(@ingilizceKelime,@turkceKelime,@kelimeResimYolu)", connection);
                command.Parameters.AddWithValue("@ingilizceKelime", engWordName);
                command.Parameters.AddWithValue("@turkceKelime", trWordName);
                command.Parameters.AddWithValue("@kelimeResimYolu", resimSec.FileName);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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
 
             
                


            
        


