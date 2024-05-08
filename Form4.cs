using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace deneme2
{
    public partial class Form4 : Form
    {
        private string connectionString = "Data Source = .; Initial Catalog = EminHocaninDeneme; Integrated Security = True;";
        private int remainingSeconds = 60;
        private int verificationCode; // Rastgele kodun saklandığı değişken
        private Timer countdownTimer;
        public Form4()
        {
            InitializeComponent();
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += new EventHandler(timer1_Tick);
        }
        private bool IsEmailExists(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM KullaniciGiris WHERE Email = @email";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@email", email);
                int count = (int)cmd.ExecuteScalar();
                connection.Close();
                return count > 0;
            }
        }
        private void SendEmail(string email, int verificationCode)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("sebahattintegi@outlook.com");
                    mail.To.Add(email);
                    mail.Subject = "Şifre Değiştirme Kodunuz Aşağıdadır";
                    mail.Body = $"Merhaba değerli Kullanıcımız,\n\nŞifre değiştirme kodunuz : {verificationCode}";

                    using (SmtpClient smtp = new SmtpClient("smtp.outlook.com"))
                    {
                        smtp.Port = 587;
                        smtp.Credentials = new NetworkCredential("sebahattintegi@outlook.com", "Sebahattin12!");
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
        private int GenerateRandomCode()
        {
            Random random = new Random();
            return random.Next(1000, 10000);
        }
        private void UpdatePasswordInDatabase(string newPassword)
        {
            string email = textBox1.Text;
            string query = "UPDATE KullaniciGiris SET Sifre = @Sifre WHERE Email = @Email";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Sifre", newPassword);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.ForeColor = Color.Black;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Lütfen E-mail Girin.");
                return;
            }
            verificationCode = GenerateRandomCode(); // Rastgele kodu oluştur ve değişkende sakla
            SendEmail(email, verificationCode); // E-postayı gönder
            if (IsEmailExists(email))
            {
                MessageBox.Show("E-mail gönderilmiştir");

                textBox1.Hide();
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;

                // Timer'ı başlat
                remainingSeconds = 60;
                countdownTimer.Start();
                button5.Visible = true;
                button3.Visible = false;
                
            }
            else
            {
                MessageBox.Show("Böyle bir kullanıcı bulunmamaktadır");
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            if (remainingSeconds <= 0)
            {
                countdownTimer.Stop();
                MessageBox.Show("Kodu girme süreniz doldu!");
            }
            label7.Text = TimeSpan.FromSeconds(remainingSeconds).ToString(@"mm\:ss");
            if (remainingSeconds == 0)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                button4.Visible = true;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            // E-posta gönderme işlemi
            SendEmail(email, verificationCode); // Rastgele kodu tekrar gönder

            if (IsEmailExists(email))
            {
                MessageBox.Show("Yeni E-mail gönderilmiştir");

                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            }
            // Timer'ı sıfırla ve tekrar başlat
            countdownTimer.Stop();
            remainingSeconds = 60; // Timer'ı istediğiniz başlangıç süresine ayarlayın
            label7.Text = TimeSpan.FromSeconds(remainingSeconds).ToString(@"mm\:ss");
            countdownTimer.Start();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string enteredCode = textBox2.Text.Trim();
            if (enteredCode == verificationCode.ToString())
            {
                if (textBox3.Text!=textBox4.Text)
                {
                    MessageBox.Show("Şifreler Uyuşmuyor Lütfen Kontrol edin");
                }
                else
                {
                    // Kod doğruysa şifre güncelleme işlemini gerçekleştir
                    string newPassword = GenerateNewPassword(); // Yeni bir şifre oluştur
                    UpdatePasswordInDatabase(newPassword); // Veritabanında şifreyi güncelle
                    MessageBox.Show("Şifreniz Başarıyla Değiştirilmiştir.");
                    countdownTimer.Stop();
                    Form1 asd = new Form1();
                    asd.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Kod yanlış. Lütfen doğru kodu girin.");
            }
        }
        private string GenerateNewPassword()
        {
            return textBox4.Text;
        }
    }
}
