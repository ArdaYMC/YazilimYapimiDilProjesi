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

namespace deneme2
{
    public partial class Form2 : Form
    {
        private string connectionString = "Data Source = .; Initial Catalog = EminHocaninDeneme; Integrated Security = True;";
        public Form2()
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            string isim = textBox1.Text;
            string sifre = textBox2.Text;
            string email = textBox4.Text;
            string ingilizceSeviyesi = comboBox1.Text;
            string hedefSeviye = comboBox2.Text;

            if (string.IsNullOrEmpty(isim) || string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(ingilizceSeviyesi) || string.IsNullOrEmpty(hedefSeviye))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }
            if (sifre != textBox3.Text)
            {
                MessageBox.Show("Şifreler Uyuşmuyor Lütfen Tekrar Deneyin");
                textBox3.Clear();
                return;
            }
            // Kullanıcı adının veritabanında olup olmadığını kontrol et
            if (IsUserExists(isim))
            {
                MessageBox.Show("Bu kullanıcı zaten mevcut. Başka bir kullanıcı adı seçiniz.");
                return;
            }
            // Mail gönderme işlemi
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("projectmailforshaf@gmail.com"); // Gönderici mail adresi
                    mail.To.Add(email); // Alıcı mail adresi
                    mail.Subject = "Kaydınız Başarıyla Oluşturuldu";
                    mail.Body = $"Merhaba {isim},\n\nKaydınız başarıyla oluşturulmuştur.";

                    using (SmtpClient smtp = new SmtpClient("smtp.outlook.com")) // SMTP sunucu adresi
                    {
                        smtp.Port = 587; // SMTP port numarası
                        smtp.Credentials = new NetworkCredential("projectmailforshaf@gmail.com", "Shaf123321"); // Gönderici mail adresi ve şifre
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

              //  MessageBox.Show("Mail başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO KullaniciGiris (KullaniciAdi, Sifre, Email, Seviyesi, HedefledigiSeviyesi) VALUES (@Isim, @Sifre, @Email, @IngilizceSeviyesi, @HedefSeviye)", connection);
                command.Parameters.AddWithValue("@Isim", isim);
                command.Parameters.AddWithValue("@Sifre", sifre);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@IngilizceSeviyesi", ingilizceSeviyesi);
                command.Parameters.AddWithValue("@HedefSeviye", hedefSeviye);
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
                        comboBox1.Items.Clear();
                        comboBox2.Items.Clear();
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

        // Kullanıcı adının veritabanında olup olmadığını kontrol eden metod
        private bool IsUserExists(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM KullaniciGiris WHERE KullaniciAdi = @Username";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Username", username);
                int count = (int)cmd.ExecuteScalar();
                connection.Close();
                return count > 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}

    


