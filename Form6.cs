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
            AddWordToDB(txtBoxEngWordName.Text, txtBoxTrWordName.Text,txtBoxWordExample.Text);


        }
        private string connectionString = "Data Source= .;Initial Catalog = 6TekrardaDilOgrenme; Integrated Security = True";
        private void AddWordToDB(string engWordName, string trWordName,string wordExample)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand insterWordCommand = new SqlCommand("INSERT INTO Kelimeler(ingilizceKelime, turkceKelime,kelimeResimYolu) values(@ingilizceKelime,@turkceKelime,@kelimeResimYolu); SELECT SCOPE_IDENTITY();", connection);
                insterWordCommand.Parameters.AddWithValue("@ingilizceKelime", engWordName);
                insterWordCommand.Parameters.AddWithValue("@turkceKelime", trWordName);
                insterWordCommand.Parameters.AddWithValue("@kelimeResimYolu", resimSec.FileName);

                int kelimeID=Convert.ToInt32(insterWordCommand.ExecuteScalar());

                SqlCommand insertExampleCommand = new SqlCommand("INSERT INTO KelimeOrnek (kelimeID, kelimeOrnek) VALUES (@KelimeID, @KelimeOrnek);", connection);
                insertExampleCommand.Parameters.AddWithValue("@KelimeID", kelimeID);
                insertExampleCommand.Parameters.AddWithValue("@KelimeOrnek", wordExample);

                try
                {
                    insertExampleCommand.ExecuteNonQuery();
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

 
             
                


            
        


