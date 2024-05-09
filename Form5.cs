using System;
using System.Windows.Forms;

namespace deneme2
{
    public partial class Form5 : Form
    {
        
        public Form5()
        {
            InitializeComponent();
        }
        int questionCount;
        private void button1_Click(object sender, EventArgs e)
        { 
            // Kullanıcı girdisi alma
            if (!int.TryParse(textBox1.Text, out int questionCount) || questionCount <= 0)
            {
                MessageBox.Show("Geçerli bir sayı giriniz.");
                return ;
            }

            // Quiz ekranını aç
            QuizEkrani quizEkrani = new QuizEkrani(questionCount);
            quizEkrani.Show();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Uygulamayı kapat
            Application.Exit();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
        }
    }
}
