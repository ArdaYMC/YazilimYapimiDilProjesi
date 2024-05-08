using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Drawing;

namespace deneme2
{
    public partial class QuizEkrani : Form
    {
        SqlConnection connection = new SqlConnection("Data Source = .; Initial Catalog = EminHocaninDeneme; Integrated Security = True;");
        SqlCommand command;
        SqlDataReader reader;
        string correctAnswer;
        string[] wrongChoices = new string[4];
        string[] choices = new string[4];
        int questionIndex = 0;
        int totalQuestions = 0;
        ProgressBar progressBar1 = new ProgressBar();
        int correctAnswers = 0;
        bool questionAnsweredCorrectly = false;
        DateTime startTime;

        public QuizEkrani()
        {
            InitializeComponent();
        }

        private void QuizEkrani_Load(object sender, EventArgs e)
        {
            progressBar1.Location = new Point(50, 250);
            progressBar1.Width = 300;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 10;
            this.Controls.Add(progressBar1);
            ShowQuestions();
            startTime = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            string elapsedTimeString = string.Format("{0:D2}:{1:D2}:{2:D2}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);

            SaveElapsedTime(elapsedTimeString);

            CheckAnswer();

            if (!reader.IsClosed && questionIndex < totalQuestions)
            {
                reader.Read();
                questionIndex++;
                DisplayQuestion();
                startTime = DateTime.Now; // Yeni soruya geçildiğinde başlangıç zamanını güncelle
            }
            ShowQuestions();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 geridon = new Form5();
            geridon.Show();
            this.Close();
        }

        private void ShowQuestions()
        {
            try
            {
                connection.Open();
                string query = "SELECT TOP 1 SorulanKelime, DogruCevap, Kelime_gorseli FROM Test_sorulari_tablosu ORDER BY NEWID()";
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    totalQuestions++;
                    DisplayQuestion();
                }
                else
                {
                    MessageBox.Show("Sorular bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorular yüklenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void DisplayQuestion()
        {
            try
            {
                if (!reader.IsClosed)
                {
                    label3.Text = reader["SorulanKelime"].ToString();
                    correctAnswer = reader["DogruCevap"].ToString();
                    string imageUrl = reader["Kelime_gorseli"].ToString();

                    // PictureBox'a resmi yükle
                    pictureBox1.ImageLocation = imageUrl;

                    GetWrongChoices();
                    ShuffleChoices();

                    radioButton1.Text = choices[0];
                    radioButton2.Text = choices[1];
                    radioButton3.Text = choices[2];
                    radioButton4.Text = choices[3];
                }
                else
                {
                    MessageBox.Show("Sorular gösterilirken bir hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Soruları görüntülerken bir hata oluştu: " + ex.Message);
            }
        }

        private void CheckAnswer()
        {
            string userAnswer = GetSelectedAnswer();
            if (userAnswer == correctAnswer)
            {
                MessageBox.Show("Doğru cevap!");
                correctAnswers++;
                questionAnsweredCorrectly = true;
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false && radioButton4.Checked == false)
            {
                MessageBox.Show("Lütfen Bir Şık seçiniz.");
            }
            else
            {
                MessageBox.Show("Yanlış cevap! Doğru cevap: " + correctAnswer);
                questionAnsweredCorrectly = false;
            }

            UpdateProgressBar();
        }

        private void GetWrongChoices()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                string query = "SELECT TOP 3 DogruCevap FROM Test_sorulari_tablosu WHERE DogruCevap <> @CorrectAnswer ORDER BY NEWID()";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CorrectAnswer", correctAnswer);

                SqlDataReader wrongChoicesReader = command.ExecuteReader();

                if (wrongChoicesReader.HasRows)
                {
                    int choiceIndex = 0;
                    while (wrongChoicesReader.Read() && choiceIndex < 3)
                    {
                        wrongChoices[choiceIndex] = wrongChoicesReader["DogruCevap"].ToString();
                        choiceIndex++;
                    }
                    wrongChoices[choiceIndex] = correctAnswer;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yanlış seçenekler yüklenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void ShuffleChoices()
        {
            choices = wrongChoices.OrderBy(x => Guid.NewGuid()).ToArray();
        }

        private string GetSelectedAnswer()
        {
            if (radioButton1.Checked)
            {
                return radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                return radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                return radioButton3.Text;
            }
            else if (radioButton4.Checked)
            {
                return radioButton4.Text;
            }
            else
            {
                return "";
            }
        }

        private void UpdateProgressBar()
        {
            if (questionAnsweredCorrectly)
            {
                progressBar1.PerformStep();
            }

            if (progressBar1.Value == progressBar1.Maximum)
            {
                MessageBox.Show("Tebrikler! Quiz tamamlandı.");
                // Quiz tamamlandığında yapılacak işlemler burada olabilir.
            }
        }

        private void SaveElapsedTime(string elapsedTimeString)
        {
            try
            {
                connection.Open();
                string query = "UPDATE Test_sorulari_tablosu SET HarcananSure = @ElapsedTime WHERE SorulanKelime = @Question";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ElapsedTime", elapsedTimeString);
                command.Parameters.AddWithValue("@Question", label3.Text);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Harcanan süre Veritabanına kaydedilirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
