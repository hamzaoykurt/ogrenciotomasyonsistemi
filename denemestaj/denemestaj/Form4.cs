using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using denemestaj.Models;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace denemestaj
{
    public partial class Form4 : Form
    {
        public string currentStudentName;
        public bool isadmin;
        private bool isLoading = false;
        private Dictionary<int, NotModel> notlarDizisi = new Dictionary<int, NotModel>();

        public Form4(string studentName, bool isAdmin)
        {
            InitializeComponent();
            currentStudentName = studentName;

            if (isAdmin)
            {
                LoadStudentNames();
                LoadLessonNames();
                LoadRecords(currentStudentName, isAdmin);
                ogrencisecim.SelectedItem = "Tümünü Seç";
                derslercomboBox.SelectedItem = "Yeni Ders Ekle";

            }
            else
            {
                ogrencisecim.Items.Add(studentName);
                LoadRecords(studentName, isAdmin);
                ogrencisecim.SelectedItem = studentName;
                yeniderseklebaslik.Visible = false;
                derslercomboBox.Visible = false;
                zorunluders.Visible = false;
                yenidersadi.Visible = false;
                yenidersekle.Visible = false;
                derslerlbl.Visible = false;
                
                seciliderssil.Visible = false;
            }

            isadmin = isAdmin;


            AyriDersYukle(currentStudentName);
        }

        public void LoadRecords(string ogrenciAdi, bool isAdmin)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                string query = "";

                if (isAdmin)
                {
                    if (string.IsNullOrWhiteSpace(ogrenciAdi))
                    {
                        query = "SELECT * FROM [dbo].[table]";
                    }
                    else
                    {
                        query = "SELECT * FROM [dbo].[table] WHERE OgrenciAdi = @OgrenciAdi";
                    }
                }
                else
                {
                    query = "SELECT * FROM [dbo].[table] WHERE OgrenciAdi = @OgrenciAdi";
                }

                SqlCommand command = new SqlCommand(query, connection);

                if (!string.IsNullOrWhiteSpace(ogrenciAdi))
                {
                    command.Parameters.AddWithValue("@OgrenciAdi", ogrenciAdi);
                }

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                notlarDizisi.Clear();

                while (reader.Read())
                {
                    NotModel model = new NotModel
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        OgrID = Convert.ToInt32(reader["OgrID"]),
                        DersID = Convert.ToInt32(reader["DersID"]),
                        OgrenciAdi = reader["OgrenciAdi"].ToString(),
                        Sinav1 = Convert.ToDouble(reader["Sinav1"]),
                        Sinav2 = Convert.ToDouble(reader["Sinav2"]),
                        Sinav3 = Convert.ToDouble(reader["Sinav3"]),
                        Sozlu1 = Convert.ToDouble(reader["Sozlu1"]),
                        Sozlu2 = Convert.ToDouble(reader["Sozlu2"]),
                        NotOrtalamasi = Convert.ToDouble(reader["NotOrtalamasi"]),
                        SonucDegerlendirme = reader["SonucDegerlendirme"].ToString()
                    };

                    notlarDizisi.Add(model.ID, model);
                }

                reader.Close();
                connection.Close();
            }
        }

        private void LoadStudentNames()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                string query = "SELECT DISTINCT OgrenciAdi FROM ogrencidb WHERE OgrenciAdi <> 'admin'";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                ogrencisecim.Items.Add("Tümünü Seç");
                while (reader.Read())
                {
                    ogrencisecim.Items.Add(reader["OgrenciAdi"].ToString());
                }
                connection.Close();
            }
        }

        private void AyriDersYukle(string ogrenciAdi)
        {
            secilenders.Items.Clear();
            isLoading = true;
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                string dersQuery = "SELECT DersAdi, ZorunluDers FROM dersdb";
                string query = $@"SELECT DersAdi FROM [dbo].[table] WHERE 1=1 {(ogrenciAdi == "Tümünü Seç" ? string.Empty : $@" AND OgrenciAdi = @OgrenciAdi ")} ";

                SqlCommand dersCommand = new SqlCommand(dersQuery, connection);
                connection.Open();
              
                
                List<string> ogrencisahipdersler = new List<string>();
                if (ogrenciAdi != "Tümünü Seç")
                {
                   SqlCommand ogrenciCommand = new SqlCommand(query, connection);
                    ogrenciCommand.Parameters.AddWithValue("@OgrenciAdi", ogrenciAdi);
                    SqlDataReader ogrencilist = ogrenciCommand.ExecuteReader();

                    while (ogrencilist.Read())
                    {
                        ogrencisahipdersler.Add(ogrencilist["DersAdi"].ToString());

                    }
                    ogrencilist.Close();
                }

                SqlDataReader dersReader = dersCommand.ExecuteReader();
                Dictionary<string, bool> geneldersler = new Dictionary<string, bool>();
                while (dersReader.Read())
                {
                    geneldersler.Add(dersReader["DersAdi"].ToString(), dersReader["ZorunluDers"].ToString() == "Zorunlu");
                }
                dersReader.Close();
             
                foreach (var item in geneldersler)
                {
                 
                    int index = secilenders.Items.Add(item.Key);

                    if (ogrencisahipdersler.Contains(item.Key))
                    {
                        secilenders.SetItemChecked(index, true);
                    }
                    if (item.Value)
                    {
                        secilenders.SetItemChecked(index, true);
                    }
                }

                connection.Close();
            }

            isLoading = false;
            SeciliDersleriZorunluIsaretle();
        }

        private void derslerikaydet_Click(object sender, EventArgs e)
        {
            string ogrenciAdi = ogrencisecim.SelectedItem.ToString();
            List<string> seciliDersler = new List<string>();

            foreach (object item in secilenders.CheckedItems)
            {
                seciliDersler.Add(item.ToString());
            }

            Dictionary<string, bool> dersler = new Dictionary<string, bool>();
            foreach (var item in secilenders.Items)
            {
                dersler.Add(item.ToString(), secilenders.CheckedItems.Contains(item.ToString()));
            }

            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                connection.Open();

                if (ogrenciAdi == "Tümünü Seç")
                {
                    DialogResult uyarizorunlu = MessageBox.Show("Zorunlu dersler TÜM ÖĞRENCİLERE kaydedilsin mi?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (uyarizorunlu == DialogResult.Yes)
                    {
                        List<string> tumOgrenciler = new List<string>();
                        string tumOgrencilerQuery = "SELECT OgrenciAdi FROM ogrencidb WHERE OgrenciAdi <> 'admin'";
                        SqlCommand tumOgrencilerCommand = new SqlCommand(tumOgrencilerQuery, connection);
                        SqlDataReader tumOgrencilerReader = tumOgrencilerCommand.ExecuteReader();
                        while (tumOgrencilerReader.Read())
                        {
                            tumOgrenciler.Add(tumOgrencilerReader["OgrenciAdi"].ToString());
                        }
                        tumOgrencilerReader.Close();

                        foreach (string currentStudent in tumOgrenciler)
                        {
                            List<string> mevcutDersler = new List<string>();
                            string mevcutDerslerQuery = "SELECT DersAdi FROM [dbo].[table] WHERE OgrenciAdi = @OgrenciAdi";
                            SqlCommand mevcutDerslerCommand = new SqlCommand(mevcutDerslerQuery, connection);
                            mevcutDerslerCommand.Parameters.AddWithValue("@OgrenciAdi", currentStudent);
                            SqlDataReader mevcutDerslerReader = mevcutDerslerCommand.ExecuteReader();
                            while (mevcutDerslerReader.Read())
                            {
                                mevcutDersler.Add(mevcutDerslerReader["DersAdi"].ToString());
                            }
                            mevcutDerslerReader.Close();

                            foreach (string ders in seciliDersler)
                            {
                                if (!mevcutDersler.Contains(ders))
                                {
                                    string ogrenciIdQuery = "SELECT OgrID FROM ogrencidb WHERE OgrenciAdi = @OgrenciAdi";
                                    SqlCommand ogrenciIdCommand = new SqlCommand(ogrenciIdQuery, connection);
                                    ogrenciIdCommand.Parameters.AddWithValue("@OgrenciAdi", currentStudent);
                                    int ogrenciId = Convert.ToInt32(ogrenciIdCommand.ExecuteScalar());

                                    string dersIdQuery = "SELECT DersID FROM dersdb WHERE DersAdi = @DersAdi";
                                    SqlCommand dersIdCommand = new SqlCommand(dersIdQuery, connection);
                                    dersIdCommand.Parameters.AddWithValue("@DersAdi", ders);
                                    int dersId = Convert.ToInt32(dersIdCommand.ExecuteScalar());

                                    string insertQuery = "INSERT INTO [dbo].[table] (OgrenciAdi, DersAdi, OgrID, DersID) VALUES (@OgrenciAdi, @DersAdi, @OgrID, @DersID)";
                                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                                    insertCommand.Parameters.AddWithValue("@OgrenciAdi", currentStudent);
                                    insertCommand.Parameters.AddWithValue("@DersAdi", ders);
                                    insertCommand.Parameters.AddWithValue("@OgrID", ogrenciId);
                                    insertCommand.Parameters.AddWithValue("@DersID", dersId);
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        MessageBox.Show("Zorunlu dersler tüm öğrencilere kaydedildi.");
                    }
                }
                else
                {
                    foreach (var item in dersler)
                    {
                        if (item.Value)
                        {
                            List<string> ogrenciveritabanıdersler = new List<string>();
                            string aldigiDerslerQuery = "SELECT DersAdi FROM [dbo].[table] WHERE OgrenciAdi = @OgrenciAdi";
                            SqlCommand aldigiDerslerCommand = new SqlCommand(aldigiDerslerQuery, connection);
                            aldigiDerslerCommand.Parameters.AddWithValue("@OgrenciAdi", ogrenciAdi);
                            SqlDataReader aldigiDerslerReader = aldigiDerslerCommand.ExecuteReader();
                            while (aldigiDerslerReader.Read())
                            {
                                ogrenciveritabanıdersler.Add(aldigiDerslerReader["DersAdi"].ToString());
                            }
                            aldigiDerslerReader.Close();

                            if (!ogrenciveritabanıdersler.Contains(item.Key))
                            {
                                string aldigidersekleQuery = "INSERT INTO [dbo].[table] (DersAdi, OgrenciAdi, OgrID, DersID) VALUES (@DersAdi, @OgrenciAdi, @OgrID, @DersID)";
                                SqlCommand aldigidersekleCommand = new SqlCommand(aldigidersekleQuery, connection);

                                string ogrenciIdQuery = "SELECT OgrID FROM ogrencidb WHERE OgrenciAdi = @OgrenciAdi";
                                SqlCommand ogrenciIdCommand = new SqlCommand(ogrenciIdQuery, connection);
                                ogrenciIdCommand.Parameters.AddWithValue("@OgrenciAdi", ogrenciAdi);
                                int ogrenciId = Convert.ToInt32(ogrenciIdCommand.ExecuteScalar());

                                string dersIdQuery = "SELECT DersID FROM dersdb WHERE DersAdi = @DersAdi";
                                SqlCommand dersIdCommand = new SqlCommand(dersIdQuery, connection);
                                dersIdCommand.Parameters.AddWithValue("@DersAdi", item.Key);
                                int dersId = Convert.ToInt32(dersIdCommand.ExecuteScalar());

                                aldigidersekleCommand.Parameters.AddWithValue("@DersAdi", item.Key);
                                aldigidersekleCommand.Parameters.AddWithValue("@OgrenciAdi", ogrenciAdi);
                                aldigidersekleCommand.Parameters.AddWithValue("@OgrID", ogrenciId);
                                aldigidersekleCommand.Parameters.AddWithValue("@DersID", dersId);

                                aldigidersekleCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            List<string> ogrenciveritabanıdersler = new List<string>();
                            string aldigiDerslerQuery = "SELECT DersAdi FROM [dbo].[table] WHERE OgrenciAdi = @OgrenciAdi";
                            SqlCommand aldigiDerslerCommand = new SqlCommand(aldigiDerslerQuery, connection);
                            aldigiDerslerCommand.Parameters.AddWithValue("@OgrenciAdi", ogrenciAdi);
                            SqlDataReader aldigiDerslerReader = aldigiDerslerCommand.ExecuteReader();
                            while (aldigiDerslerReader.Read())
                            {
                                ogrenciveritabanıdersler.Add(aldigiDerslerReader["DersAdi"].ToString());
                            }
                            aldigiDerslerReader.Close();

                            if (ogrenciveritabanıdersler.Contains(item.Key))
                            {
                                string derssil = "DELETE FROM [dbo].[table] WHERE DersAdi = @DersAdi AND OgrenciAdi = @OgrenciAdi";
                                SqlCommand derssilCommand = new SqlCommand(derssil, connection);
                                derssilCommand.Parameters.AddWithValue("@DersAdi", item.Key);
                                derssilCommand.Parameters.AddWithValue("@OgrenciAdi", ogrenciAdi);
                                derssilCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Dersler kaydedildi.");
                }

                connection.Close();
            }
            return;
        }


        private void yenidersadi_TextChanged(object sender, EventArgs e)
        {
            string dersAdi = yenidersadi.Text.Trim();

            if (string.IsNullOrWhiteSpace(dersAdi))
                return;

            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                string query = "SELECT ZorunluDers FROM dersdb WHERE DersAdi = @DersAdi";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DersAdi", dersAdi);


                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        if (result.ToString() == "Zorunlu")
                            zorunluders.Checked = true;
                        else
                            zorunluders.Checked = false;
                    }
                    else
                    {
                        zorunluders.Checked = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void secilenders_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            SeciliDersleriZorunluIsaretle();
        }

        private void ogrencisecim_SelectedIndexChanged(object sender, EventArgs e)
        {
            secilenders.Enabled = true;
            AyriDersYukle(ogrencisecim.Text);
        }

        private void secilenders_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!isLoading)
            {
                var dersAdi = secilenders.Items[e.Index].ToString();

                if (ZorunluDersMi(dersAdi) && e.NewValue == CheckState.Unchecked)
                {
                    e.NewValue = CheckState.Checked;
                }
            }
            if (ogrencisecim.SelectedItem.ToString() == "Tümünü Seç")
            {
                secilenders.Enabled = false;
            }
        }

        private bool ZorunluDersMi(string dersAdi)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                string query = "SELECT ZorunluDers FROM dersdb WHERE DersAdi = @DersAdi";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DersAdi", dersAdi);

                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();

                return result != null && result.ToString() == "Zorunlu";
            }
        }

        private void SeciliDersleriZorunluIsaretle()
        {
            for (int i = 0; i < secilenders.Items.Count; i++)
            {
                var dersAdi = secilenders.Items[i].ToString();

                if (ZorunluDersMi(dersAdi))
                {
                    secilenders.SetItemChecked(i, true);
                }
            }
        }

        private void LoadLessonNames()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                string query = "SELECT DersAdi FROM dersdb";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                derslercomboBox.Items.Add("Yeni Ders Ekle");
                while (reader.Read())
                {
                    derslercomboBox.Items.Add(reader["DersAdi"].ToString());
                }

                connection.Close();
            }
        }

        private void derslercomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLesson = derslercomboBox.SelectedItem.ToString();

            if (selectedLesson == "Yeni Ders Ekle")
            {
                yenidersadi.Enabled = true;
                yenidersadi.Clear();
            }

            else
            {
                yenidersadi.Enabled = false;
                yenidersadi.Text = selectedLesson;
            }
        }

        private void yenidersadi_MouseDown(object sender, MouseEventArgs e)
        {
            if (derslercomboBox.SelectedItem.ToString() == "Yeni Ders Ekle")
            {
                yenidersadi.Clear();
                yenidersadi.Focus();

            }
        }

        private void yenidersekle_Click(object sender, EventArgs e)
        {
            string yeniDersAdi = yenidersadi.Text.Trim();

            if (string.IsNullOrEmpty(yeniDersAdi))
            {
                MessageBox.Show("Lütfen bir ders adı girin.");
                return;
            }

            bool zorunlu = zorunluders.Checked;

            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                connection.Open();

                if (derslercomboBox.SelectedItem.ToString() == "Yeni Ders Ekle")
                {
                    string controlQuery = "SELECT COUNT(*) FROM dersdb WHERE DersAdi = @DersAdi";
                    SqlCommand controlCommand = new SqlCommand(controlQuery, connection);
                    controlCommand.Parameters.AddWithValue("@DersAdi", yeniDersAdi);

                    int dersSayisi = Convert.ToInt32(controlCommand.ExecuteScalar());

                    if (dersSayisi > 0)
                    {
                        MessageBox.Show("Bu ders zaten mevcut.");
                        return;
                    }

                    string ekleQuery = "INSERT INTO dersdb (DersAdi, ZorunluDers) VALUES (@DersAdi, @ZorunluDers)";
                    SqlCommand ekleCommand = new SqlCommand(ekleQuery, connection);
                    ekleCommand.Parameters.AddWithValue("@DersAdi", yeniDersAdi);
                    ekleCommand.Parameters.AddWithValue("@ZorunluDers", zorunlu ? "Zorunlu" : "Zorunlu Değil");

                    ekleCommand.ExecuteNonQuery();

                    MessageBox.Show("Yeni ders başarıyla eklendi.");
                    yenidersadi.Clear();
                }
                else
                {
                    string updateDurumQuery = "UPDATE dersdb SET ZorunluDers = @ZorunluDers WHERE DersAdi = @DersAdi";
                    SqlCommand updateDurumCommand = new SqlCommand(updateDurumQuery, connection);
                    updateDurumCommand.Parameters.AddWithValue("@ZorunluDers", zorunlu ? "Zorunlu Değil" : "Zorunlu");
                    updateDurumCommand.Parameters.AddWithValue("@DersAdi", yeniDersAdi);

                    updateDurumCommand.ExecuteNonQuery();

                    string durumMesaji = zorunlu ? "Zorunlu Değil" : "Zorunlu";

                    MessageBox.Show($"'{yeniDersAdi}' {durumMesaji} olarak güncellendi.");

                }

                connection.Close();
            }

        }
        
        private void yenidersadi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                yenidersekle.PerformClick();
            }

        }

        private void seciliderssil_Click(object sender, EventArgs e)
        {
            string silinecekDersAdi = yenidersadi.Text.Trim();

            if (string.IsNullOrEmpty(silinecekDersAdi))
            {
                MessageBox.Show("Silinecek ders bulunamadı.");
                return;
            }

            if (ZorunluDersMi(silinecekDersAdi))
            {
                MessageBox.Show("Zorunlu dersi silemezsiniz!");
                return;
            }

            bool dersKullanimda = DersKullanimdaMi(silinecekDersAdi);

            if (dersKullanimda)
            {
                DialogResult result = MessageBox.Show("Seçili dersi bir veya daha fazla öğrenci almıştır. Bu dersi silmek istediğinizden emin misiniz?", "Ders Kullanımda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }

                SilDersVerileri(silinecekDersAdi);
            }

            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                connection.Open();

                string silQuery = "DELETE FROM dersdb WHERE DersAdi = @DersAdi";
                SqlCommand silCommand = new SqlCommand(silQuery, connection);
                silCommand.Parameters.AddWithValue("@DersAdi", silinecekDersAdi);

                int affectedRows = silCommand.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Ders başarıyla silindi.");
                }
                else
                {
                    MessageBox.Show("Silinecek ders bulunamadı.");
                }

                connection.Close();
            }
        }

        private bool DersKullanimdaMi(string dersAdi)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                string query = "SELECT COUNT(*) FROM [dbo].[table] WHERE DersAdi = @DersAdi";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DersAdi", dersAdi);

                connection.Open();
                int rowCount = (int)command.ExecuteScalar();
                connection.Close();

                return rowCount > 0;
            }
        }

        private void SilDersVerileri(string dersAdi)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                connection.Open();

                string silQuery = "DELETE FROM [dbo].[table] WHERE DersAdi = @DersAdi";
                SqlCommand silCommand = new SqlCommand(silQuery, connection);
                silCommand.Parameters.AddWithValue("@DersAdi", dersAdi);

                silCommand.ExecuteNonQuery();
                connection.Close();

            }
        }
        
        private void yenidersadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void derslercomboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void zorunluluklbl_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
