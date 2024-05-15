using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Security.Policy;

namespace deneme2
{
    public partial class Form2 : Form
    {
        private string connectionString = "Data Source = .; Initial Catalog = 6TekrardaDilOgrenme; Integrated Security = True;";
        public Form2()
        {
            InitializeComponent();

        }
        private void Form2_Load(object sender, EventArgs e)
        {
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string password = textBox2.Text;
            string email = textBox4.Text;

            //Boş alan kontrolü
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }
            if (password != textBox3.Text)
            {
                MessageBox.Show("Şifreler Uyuşmuyor.");
                return;
            }
            if (IsUserExists(name))
            {
                MessageBox.Show("Bu kullanıcı zaten mevcut. Başka bir kullanıcı adı seçiniz.");
                return;
            }  // Kullanıcı adının veritabanında olup olmadığı kontrolü

            SendMailToUser(email,name); // Kullanıcıya hesap oluşturulduğu hakkında bilgi maili

            AddUser(name, email, password); //Kullanıcının hesabını veritabanına ekleme
            
        }
        private bool IsUserExists(string username) // Aynı isimde kullanıcı adının veritabanında olup olmadığının kontrolü
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM KullaniciGiris WHERE kullaniciAdi = @Username";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Username", username);
                int count = (int)cmd.ExecuteScalar();
                connection.Close();
                return count > 0;
            }
        } 

        private void AddUser(string isim,string sifre,string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO KullaniciGiris (kullaniciAdi, sifre, email) VALUES (@isim, @sifre, @email)", connection);
                command.Parameters.AddWithValue("@isim", isim);
                command.Parameters.AddWithValue("@sifre", sifre);
                command.Parameters.AddWithValue("@email", email);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla eklendi.");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox4.Clear();
                        textBox3.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt eklenirken bir hata oluştu.");
                    }
                    connection.Close();
                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        } 
        private void SendMailToUser(string kullaniciEmail,string isim)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("Sebahattintegi@outlook.com"); // Gönderici mail adresi
                    mail.To.Add(kullaniciEmail); // Alıcı mail adresi
                    mail.Subject = "Kaydınız Başarıyla Oluşturuldu";
                    mail.Body = $"Merhaba {isim},\n\nKaydınız başarıyla oluşturulmuştur.";

                    using (SmtpClient smtp = new SmtpClient("smtp.outlook.com")) // SMTP sunucu adresi
                    {
                        smtp.Port = 587; // SMTP port numarası
                        smtp.Credentials = new NetworkCredential("Sebahattintegi@outlook.com", "Sebahattin12!"); // Gönderici mail adresi ve şifre
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}

    


