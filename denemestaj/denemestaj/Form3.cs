using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using denemestaj.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Reflection.Emit;


namespace denemestaj
{
    public partial class Form3 : Form
    {
        public Form3()
        {

            InitializeComponent();
        }

        private void kayitol_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ogrenciadikayit.Text))
            {
                MessageBox.Show("Lütfen bir kullanıcı adı oluşturunuz.");
                return;
            }
            bool ogrenciAdiVar = false; 
            
            List<string> mevcutOgrenciAdlari = new List<string>();
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                connection.Open();
                string queryMevcutOgrenciAdlari = "SELECT OgrenciAdi FROM ogrencidb";
                SqlCommand commandMevcutOgrenciAdlari = new SqlCommand(queryMevcutOgrenciAdlari, connection);
                SqlDataReader readerMevcutOgrenciAdlari = commandMevcutOgrenciAdlari.ExecuteReader();
                while (readerMevcutOgrenciAdlari.Read())
                {
                    string ogrenciAdi = readerMevcutOgrenciAdlari["OgrenciAdi"].ToString().ToLower();
                    mevcutOgrenciAdlari.Add(ogrenciAdi);
                }
                readerMevcutOgrenciAdlari.Close();
            }
            if (!ogrenciadikayit.Text.All(c => char.IsLetterOrDigit(c) || c == '_' || c == '.'))
            {
                MessageBox.Show("Kullanıcı adı sadece harf, rakam, alt çizgi ( _ ) ve nokta ( . ) karakterlerini içerebilir.");
                return;
            }
            if (ogrenciadikayit.Text.Count(char.IsLetter) < 2)
            {
                MessageBox.Show("Kullanıcı adı en az iki harf içermelidir.");
                return;
            }
            if (ogrenciadikayit.Text.All(char.IsDigit))
            {
                MessageBox.Show("Kullanıcı adı sadece rakamlardan oluşamaz.");
                return;
            }

            if (mevcutOgrenciAdlari.Contains(ogrenciadikayit.Text.ToLower())) 
            {
                ogrenciAdiVar = true;
            }

            if (ogrenciAdiVar)
            {
                MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor.");
                return;
            }
          
            if (string.IsNullOrWhiteSpace(sifrekayit.Text))
            {
                MessageBox.Show("Lütfen bir şifre oluşturunuz.");
                return;
            }
            
           

            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                connection.Open();

                string ogrenciAdi = ogrenciadikayit.Text;
                string sifre = sifrekayit.Text;
                string queryOgrenci = "INSERT INTO ogrencidb (OgrenciAdi) VALUES (@OgrenciAdi); SELECT SCOPE_IDENTITY();";
                SqlCommand commandOgrenci = new SqlCommand(queryOgrenci, connection);
                commandOgrenci.Parameters.AddWithValue("@OgrenciAdi", ogrenciAdi);
                int ogrIDEklenen = Convert.ToInt32(commandOgrenci.ExecuteScalar());

                string queryParola = "INSERT INTO parola (Sifre, OgrID, OgrenciAdi) VALUES (@Sifre, @OgrID, @OgrenciAdi)";
                SqlCommand commandParola = new SqlCommand(queryParola, connection);
                commandParola.Parameters.AddWithValue("@Sifre", sifre);
                commandParola.Parameters.AddWithValue("@OgrID", ogrIDEklenen);
                commandParola.Parameters.AddWithValue("@OgrenciAdi", ogrenciAdi);
                int rowsAffected = commandParola.ExecuteNonQuery();

                Form2 frm2 = new Form2();
                frm2.Show();
                this.Hide();
            }
        }

        private void girisyapyon_Click(object sender, EventArgs e)
        {
            Form Form = Application.OpenForms["Form2"];
            if (Form != null)
            {
                Form.Show();
            }
            else
            {
                Form2 frm3 = new Form2();
                frm3.Show();
            }
            this.Hide();
        }

        private void ogrenciadikayit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                sifrekayit.Focus();
            }
        }

        private void sifrekayit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                kayitol.PerformClick();
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
