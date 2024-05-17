
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace deneme2
{
    public partial class KullaniciGirisEkrani : Form
    {
        private string connectionString = "Data Source = .; Initial Catalog = 6TekrardaDilOgrenme; Integrated Security = True;"; // Veritabanı bağlantı dizesi
        public KullaniciGirisEkrani()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            KullaniciKayitEkrani form2 = new KullaniciKayitEkrani();
            form2.Show();  
            this.Hide();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            

            bool isAuthenticated = AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Giriş Yapılmıştır.");
                SecimEkrani form3 = new SecimEkrani(username);
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Böyle Bir Kullanıcı Yoktur!");
            }
        }
        private bool AuthenticateUser(string username, string password)
        {
            bool isAuthenticated = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT kullaniciAdi, sifre FROM KullaniciGiris WHERE kullaniciAdi = @Username AND sifre = @Password", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                
                
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        isAuthenticated = true;
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
            return isAuthenticated;
        }
        private void label5_Click(object sender, EventArgs e)
        {
            SifreSifirla form4 = new SifreSifirla();
            form4.Show();
            this.Hide();
        }

    
    }
}
