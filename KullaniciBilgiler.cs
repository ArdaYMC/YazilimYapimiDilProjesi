using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace deneme2
{
    public partial class KullaniciBilgiler : Form
    {
        private int kullaniciID;
        private string _connectionString = "Data Source=.;Initial Catalog=6TekrardaDilOgrenme;Integrated Security=True;";
        private DataGridView dataGridView;

        public KullaniciBilgiler(int kullaniciID)
        {
            InitializeComponent();
            this.kullaniciID = kullaniciID;

            // DataGridView bileşenini oluştur ve form'a ekle
            dataGridView = new DataGridView
            {
                Dock = DockStyle.Bottom,
                Height = 200,
                Visible = false // Başlangıçta gizli
            };
            Controls.Add(dataGridView);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 donus = new Form3(kullaniciID);
            donus.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // DataGridView'i görünür yap
            dataGridView.Visible = true;

            // Veri çekme işlemleri sınıfını oluştur
            VeriCekmeIslemleri veriCekmeIslemleri = new VeriCekmeIslemleri(_connectionString, kullaniciID);
            // DataGridView için veri çek ve doldur
            DataTable dataTable = veriCekmeIslemleri.VeriCekViewAnaliz();
            dataGridView.DataSource = dataTable;
        }

        public class VeriCekmeIslemleri
        {
            private string _connectionString;
            private int _kullaniciID;

            public VeriCekmeIslemleri(string connectionString, int kullaniciID)
            {
                _connectionString = connectionString;
                _kullaniciID = kullaniciID;
            }

            public DataTable VeriCekDataGridView()
            {
                DataTable dataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT K.ingilizceKelime, KS.kelimeSeviye, DATEDIFF(SECOND, '00:00:00', KS.harcananSure) AS HarcananSure, KO.kelimeOrnek FROM Kelimeler K INNER JOIN KelimeSeviye KS ON K.kelimeID = KS.kelimeID INNER JOIN KelimeOrnek KO ON K.kelimeID = KO.kelimeID WHERE KS.KullaniciID = @KullaniciID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KullaniciID", _kullaniciID);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader); // Verileri DataTable'a yükle
                        }
                    }
                }

                return dataTable;
            }

            public DataTable VeriCekViewAnaliz()
            {
                DataTable dataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM view_analiz";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader); // Verileri DataTable'a yükle
                        }
                    }
                }

                return dataTable;
            }
        }
    }
}
