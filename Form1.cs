﻿
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


namespace deneme2
{


    public partial class Form1 : Form
    {
        private string connectionString = "Data Source = .; Initial Catalog = 6TekrardaDilOgrenme; Integrated Security = True;"; // Veritabanı bağlantı dizesi
        private int kullaniciID;

        public Form1()
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
            Form2 form2 = new Form2();
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
                Form3 form3 = new Form3(kullaniciID);
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
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT kullaniciAdi, sifre FROM KullaniciGiris WHERE kullaniciAdi = @Username AND sifre = @Password", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                string query = "SELECT kullaniciID FROM KullaniciGiris WHERE kullaniciAdi=@kullaniciAdi";
                SqlCommand comm=new SqlCommand(query, connection);
                comm.Parameters.AddWithValue("@kullaniciAdi", username);

                kullaniciID= Convert.ToInt32(comm.ExecuteScalar());
                try
                {
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
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

    }
}
