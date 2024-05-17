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
        SqlCommand reader;
        public QuizEkrani(int questionCount)
        {
            InitializeComponent();
        }
        private void ShowQuestion()
        {
            try 
            { 
                connection.Open();
                string query = "SELECT ingilizceKelime,turkceKelime,kelimeResimYolu FROM Kelimeler K INNER JOIN KelimeSeviye KS ON K.kelimeID=KS.kelimeID";
            }
            catch 
            { 
            }
        }
       
    }
}
