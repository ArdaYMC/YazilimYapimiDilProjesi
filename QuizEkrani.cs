using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace deneme2
{
    public partial class QuizEkrani : Form
    {
        SqlConnection connection = new SqlConnection("Data Source = .; Initial Catalog = 6TekrardaDilOgrenme; Integrated Security = True;");
        SqlCommand command;
        SqlDataReader reader;
        string correctAnswer;
        string[] wrongChoices = new string[4];
        string[] choices = new string[4];
        int questionIndex = 0;
        int totalQuestions;

        ProgressBar progressBar1 = new ProgressBar();
        int correctAnswers = 0;
        bool questionAnsweredCorrectly = false;
        DateTime startTime;
        Dictionary<string, int> questionCorrectCount = new Dictionary<string, int>();
        HashSet<string> learnedQuestions = new HashSet<string>();

        public QuizEkrani(int questionCount)
        {
            InitializeComponent();
            totalQuestions = questionCount;
            progressBar1.Location = new Point(111, 185);
            progressBar1.Width = 422;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = totalQuestions;        // Bu kısmı form5'deki textbox1.text kısmından alacak 
            progressBar1.Step = 1;
            this.Controls.Add(progressBar1);
            ShowQuestions();
            startTime = DateTime.Now;
        }
        int kullaniciID;
        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            string elapsedTimeString = string.Format("{0:D2}:{1:D2}:{2:D2}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
            SaveElapsedTime(elapsedTimeString);
            CheckAnswer();
            if ((reader != null) && (!reader.IsClosed) && (questionIndex < totalQuestions - 1))
            {
                reader.Read();
                questionIndex++;
                DisplayQuestion();
                startTime = DateTime.Now;
            }
            else
            {
                ShowQuestions();
            }
            UpdateProgressBar();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        private void ShowQuestions()
        {
            try
            {
                connection.Open();
                string query = "SELECT K.ingilizceKelime , K.turkceKelime , K.kelimeResimYolu FROM Kelimeler K INNER JOIN KelimeSeviye KS ON K.kelimeID = KS.kelimeID " +
                    " WHERE KS.KelimeDurum = 0 ORDER BY NEWID();";

                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    DisplayQuestion();
                }
                else
                {
                    MessageBox.Show("Öğrenilmemiş soru bulunamadı!");
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
                if (reader != null && !reader.IsClosed)
                {
                    label3.Text = reader["ingilizceKelime"].ToString();
                    correctAnswer = reader["turkceKelime"].ToString();
                    string imageUrl = reader["kelimeResimYolu"].ToString();

                    if (!string.IsNullOrEmpty(imageUrl) && System.IO.File.Exists(imageUrl))
                    {
                        pictureBox1.ImageLocation = imageUrl;
                    }
                    else
                    {
                        MessageBox.Show("Görsel yüklenemedi: Dosya yolu geçersiz veya dosya bulunamadı.");
                        pictureBox1.Image = null; // Resim bulunamazsa boş resim göster
                    }

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
                UpdateQuestionCorrectCount();
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

                string query = "SELECT TOP 3 turkceKelime FROM Kelimeler WHERE turkceKelime <> @CorrectAnswer ORDER BY NEWID()";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CorrectAnswer", correctAnswer);

                SqlDataReader wrongChoicesReader = command.ExecuteReader();

                if (wrongChoicesReader.HasRows)
                {
                    int choiceIndex = 0;
                    while (wrongChoicesReader.Read() && choiceIndex < 3)
                    {
                        wrongChoices[choiceIndex] = wrongChoicesReader["turkceKelime"].ToString();
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
                progressBar1.Value = Math.Min(progressBar1.Maximum, progressBar1.Value + progressBar1.Step);
            }

            if (progressBar1.Value == progressBar1.Maximum)
            {
                MessageBox.Show("Tebrikler! Quiz tamamlandı.");
                Form3 anamenu = new Form3(kullaniciID);
                anamenu.Show();
                this.Close();
            }
        }
        private void SaveElapsedTime(string elapsedTimeString)
        {
            try
            {
                connection.Open();
                string query = "UPDATE KelimeSeviye SET harcananSure = @ElapsedTime, kelimeTarih = GETDATE()" +
                    "FROM KelimeSeviye KS INNER JOIN Kelimeler K ON KS.kelimeID = K.kelimeID WHERE K.ingilizceKelime = @Question";
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
        private void UpdateQuestionCorrectCount()
        {
            if (questionCorrectCount.ContainsKey(label3.Text))
            {
                questionCorrectCount[label3.Text]++;
                if (questionCorrectCount[label3.Text] < 6)
                {
                    questionCorrectCount[label3.Text] = 0;
                }
            }
            else
            {
                questionCorrectCount.Add(label3.Text, 1);
            }

            if (questionCorrectCount[label3.Text] >= 6)
            {
                MoveQuestionToKnownList(label3.Text);
            }
        }
        private void MoveQuestionToKnownList(string question)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO kelimeDurum (ingilizceKelime) VALUES (@Question)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Question", question);
                command.ExecuteNonQuery();
                learnedQuestions.Add(question);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Soruyu bilinen sorular listesine taşırken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void QuizEkrani_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 donus = new Form3(kullaniciID);
            donus.Show();
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string query = "SELECT K.kelimeOrnek " +
                    "FROM KelimeOrnek K " +
                    "INNER JOIN Kelimeler L ON K.kelimeID = L.kelimeID " +
                    "WHERE L.ingilizceKelime = @Question";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Question", label3.Text);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string kelimeOrnek = reader["kelimeOrnek"].ToString();
                    MessageBox.Show("Örnek cümle: " + kelimeOrnek);
                }
                else
                {
                    MessageBox.Show("Örnek cümle bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Örnek cümle alınırken bir hata oluştu: " + ex.Message);
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
