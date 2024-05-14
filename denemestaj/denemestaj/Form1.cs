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
    public partial class Form1 : Form
    {
        public string currentStudentName;
        public bool isadmin;
        private Dictionary<int, NotModel> notlarDizisi = new Dictionary<int, NotModel>();
        public Form1(string studentName, bool isAdmin)
        {
            InitializeComponent();
            currentStudentName = studentName;
            resimbox.ReadOnly = true;

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            if (isAdmin)
            {
                LoadStudentNames();
                LoadRecords(currentStudentName, isAdmin);
                comboBox1.SelectedItem = "Tümünü Gör";
                textBox9.Visible = false;
                resimbox.Visible = false;
                derssecimibtn.Visible = false;
            }
            else
            {
                comboBox1.Items.Add(studentName);
                LoadRecords(studentName, isAdmin);
                comboBox1.SelectedItem = studentName;
                yeniderseklebtn.Visible = false;
                textBox9.Visible = false;
                resimbox.Visible = false;
                yeniderseklebtn.Visible = false;
                
            }

            isadmin = isAdmin;
            LoadPhotoUpload();
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
            LoadPhotoUpload();
        }

        private void LoadPhotoUpload()
        {
            string ogrenciAdi = currentStudentName;

            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                string query = "SELECT FotoURL FROM ogrencidb WHERE OgrenciAdi = @OgrenciAdi";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@OgrenciAdi", SqlDbType.NVarChar).Value = ogrenciAdi;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        string photoURL = (string)result;
                        pictureBox1.ImageLocation = photoURL;
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fotoğraf yüklenirken bir hata oluştu: " + ex.Message);
                }
            }
        }

        public void LoadRecords(string ogrenciAdi)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                string query = "";

                if (string.IsNullOrWhiteSpace(ogrenciAdi) || ogrenciAdi == "Tümünü Gör")
                {
                    query = "SELECT * FROM [dbo].[table]";
                }
                else
                {
                    if (isadmin)
                    {
                        query = "SELECT * FROM [dbo].[table] WHERE OgrenciAdi = @OgrenciAdi ";
                    }
                    else
                    {
                        query = "SELECT * FROM [dbo].[table] WHERE OgrenciAdi = @OgrenciAdi AND (Sinav1 <> 0 OR Sinav2 <> 0 OR Sinav3 <> 0 OR Sozlu1 <> 0 OR Sozlu2 <> 0)";
                    }
                }

                SqlCommand command = new SqlCommand(query, connection);

                if (!string.IsNullOrWhiteSpace(ogrenciAdi))
                {
                    command.Parameters.AddWithValue("@OgrenciAdi", ogrenciAdi);
                }

                connection.Open();
                Refresh();
                SqlDataReader reader = command.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        public void LoadStudentNames()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                string query = "SELECT DISTINCT OgrenciAdi FROM [dbo].[table]";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                comboBox1.Items.Add("Tümünü Gör");
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["OgrenciAdi"].ToString());
                }
                connection.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentStudentName = comboBox1.SelectedItem.ToString();
            LoadRecords(currentStudentName);
            LoadPhotoUpload();
        }

        private void resimekle_Click(object sender, EventArgs e)
        {
            if (isadmin && comboBox1.SelectedItem.ToString() == "Tümünü Gör")
            {
                MessageBox.Show("Lütfen bir öğrenci seçin.");
                return;
            }

            openFileDialog1.Filter = "Resim Dosyaları (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            openFileDialog1.Title = "Resim Seç";
            openFileDialog1.FileName = "";

            openFileDialog1.ShowDialog();

            if (string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                            
            }
            else
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                resimbox.Text = openFileDialog1.FileName;

                DialogResult mesaj = MessageBox.Show("Resim güncellensin mi?", "Bildiri", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (mesaj == DialogResult.Yes)
                {
                    string fotoURL = resimbox.Text;

                    if (isadmin && comboBox1.SelectedItem.ToString() == "Tümünü Gör")
                    {
                        MessageBox.Show("Lütfen bir öğrenci seçin.");
                        return;
                    }

                    string ogrenciAdi = comboBox1.SelectedItem.ToString();

                    using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
                    {
                        connection.Open();
                        string updateQuery = "UPDATE ogrencidb SET FotoURL=@fotoURL WHERE OgrenciAdi=@ogrenciAdi";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);

                        updateCommand.Parameters.AddWithValue("@fotoURL", fotoURL);
                        updateCommand.Parameters.AddWithValue("@ogrenciAdi", ogrenciAdi);

                        updateCommand.ExecuteNonQuery();

                        connection.Close();
                        LoadRecords(ogrenciAdi);

                        MessageBox.Show("Profil resmi başarıyla güncellendi!");
                    }
                }
                else
                {
                    string ogrenciAdi = comboBox1.SelectedItem.ToString();

                    using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
                    {
                        connection.Open();
                        string selectQuery = "SELECT FotoURL FROM ogrencidb WHERE OgrenciAdi=@ogrenciAdi";
                        SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                        selectCommand.Parameters.AddWithValue("@ogrenciAdi", ogrenciAdi);

                        SqlDataReader reader = selectCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                pictureBox1.ImageLocation = reader.GetString(0);
                            }
                            else
                            {
                                pictureBox1.Image = null;
                            }
                        }
                        connection.Close();
                    }
                }
            
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isadmin) 
                return;

            if (e.KeyCode == Keys.Back && dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int selectedOgrID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["OgrID"].Value);

                if (dataGridView1.SelectedCells.Count == dataGridView1.Rows[selectedRowIndex].Cells.Count)
                {
                    if (MessageBox.Show("Seçilen satırdaki öğrencinin TÜM KAYITLARINI silmek istediğinize emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
                        {
                            connection.Open();
                            string deleteQuery = "DELETE FROM [dbo].[table] WHERE OgrID = @SelectedOgrID";

                            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                            {
                                command.Parameters.AddWithValue("@SelectedOgrID", selectedOgrID);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Öğrencinin tüm kayıtları başarıyla silindi.");
                                    LoadRecords(currentStudentName);
                                }
                            }
                        }
                    }
                }

                else
                {
                    if (MessageBox.Show("Seçilen satırdaki öğrencinin tüm notları silinecek! Silmek istediğinize emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
                        {
                            connection.Open();
                            string deleteQuery = "UPDATE [dbo].[table] SET Sinav1 = 0, Sinav2 = 0, Sinav3 = 0, Sozlu1 = 0, Sozlu2 = 0, NotOrtalamasi = 0, SonucDegerlendirme = '' WHERE OgrID = @SelectedOgrID";

                            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                            {
                                command.Parameters.AddWithValue("@SelectedOgrID", selectedOgrID);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Öğrencinin sınav ve sözlü notları başarıyla silindi.");
                                    LoadRecords(currentStudentName);
                                }
                            }
                        }
                    }
                }
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                KopyalananNotlariAl();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                YapıştırılanNotlariYapıştır();
            }
        }//Kayıt Sil

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string selectedID = selectedRow.Cells["ID"].Value.ToString();

                textBox9.Text = selectedID;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int selectedRowID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["ID"].Value);

                using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "SELECT Sinav1, Sinav2, Sinav3, Sozlu1, Sozlu2, NotOrtalamasi, SonucDegerlendirme FROM [dbo].[table] WHERE ID=@id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", selectedRowID);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string kopyalananBilgiler = $"{reader["Sinav1"]}, {reader["Sinav2"]}, {reader["Sinav3"]}, {reader["Sozlu1"]}, {reader["Sozlu2"]}, {reader["NotOrtalamasi"]}, {reader["SonucDegerlendirme"]}";
                            Clipboard.SetText(kopyalananBilgiler);
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen ID'ye sahip kayıt bulunamadı.");
                        }

                        reader.Close();
                    }
                }
            }

            if (e.Control && e.KeyCode == Keys.V)
            {
                string kopyalananBilgiler = Clipboard.GetText();
                string[] bilgiler = kopyalananBilgiler.Split(',');

                string selectedID = textBox9.Text;

                using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "UPDATE [dbo].[table] SET Sinav1=@sinav1, Sinav2=@sinav2, Sinav3=@sinav3, Sozlu1=@sozlu1, Sozlu2=@sozlu2, NotOrtalamasi=@notortalamasi, SonucDegerlendirme=@sonucdegerlendirme WHERE ID=@pid";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@sinav1", Convert.ToDouble(bilgiler[0]));
                        command.Parameters.AddWithValue("@sinav2", Convert.ToDouble(bilgiler[1]));
                        command.Parameters.AddWithValue("@sinav3", Convert.ToDouble(bilgiler[2]));
                        command.Parameters.AddWithValue("@sozlu1", Convert.ToDouble(bilgiler[3]));
                        command.Parameters.AddWithValue("@sozlu2", Convert.ToDouble(bilgiler[4]));
                        command.Parameters.AddWithValue("@notortalamasi", Convert.ToDouble(bilgiler[5]));
                        command.Parameters.AddWithValue("@sonucdegerlendirme", bilgiler[6]);
                        command.Parameters.AddWithValue("@pid", selectedID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Notlar başarıyla yapıştırıldı.");
                            LoadRecords(currentStudentName);
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen ID'ye sahip kayıt bulunamadı.");
                        }
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (isadmin)
            {
                DataGridViewTextBoxEditingControl textBoxControl = (DataGridViewTextBoxEditingControl)e.Control;
                string columnName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;

                if (columnName == "Sinav1" || columnName == "Sinav2" || columnName == "Sinav3" ||
                    columnName == "Sozlu1" || columnName == "Sozlu2")
                {
                    textBoxControl.ReadOnly = false;

                    textBoxControl.KeyPress += new KeyPressEventHandler(textBoxControl_KeyPress);
                }
                else
                {
                    textBoxControl.ReadOnly = true;
                }
            }
            else
            {
                DataGridViewTextBoxEditingControl textBoxControl = (DataGridViewTextBoxEditingControl)e.Control;
                textBoxControl.ReadOnly = true;
            }
        }

        private void textBoxControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool updatingCell;

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isadmin)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && !updatingCell)
                {
                    int studentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
                    string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
                    double newValue;

                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                    {
                        updatingCell = true;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                        UpdateGradeInDatabase(studentID, columnName, 0);
                        updatingCell = false;
                    }
                    else if (double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out newValue))
                    {
                        if (newValue > 100)
                        {
                            updatingCell = true;
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                            UpdateGradeInDatabase(studentID, columnName, 0);
                            updatingCell = false;
                        }
                        else
                        {
                            UpdateGradeInDatabase(studentID, columnName, newValue);
                        }
                    }
                    else
                    {
                        updatingCell = true;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                        UpdateGradeInDatabase(studentID, columnName, 0);
                        updatingCell = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Notları güncellemek için yetkiniz bulunmamaktadır.");
            }
        }

        private void UpdateGradeInDatabase(int studentID, string columnName, double newValue)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                connection.Open();

                string query = "SELECT Sinav1, Sinav2, Sinav3, Sozlu1, Sozlu2, NotOrtalamasi, SonucDegerlendirme FROM [dbo].[table] WHERE ID = @studentID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentID", studentID);
                SqlDataReader reader = command.ExecuteReader();

                double sinav1 = 0, sinav2 = 0, sinav3 = 0, sozlu1 = 0, sozlu2 = 0;
                double notOrtalamasi = 0;
                string sonucDegerlendirme = "";

                if (reader.Read())
                {
                    sinav1 = reader.IsDBNull(reader.GetOrdinal("Sinav1")) ? 0 : Convert.ToDouble(reader["Sinav1"]);
                    sinav2 = reader.IsDBNull(reader.GetOrdinal("Sinav2")) ? 0 : Convert.ToDouble(reader["Sinav2"]);
                    sinav3 = reader.IsDBNull(reader.GetOrdinal("Sinav3")) ? 0 : Convert.ToDouble(reader["Sinav3"]);
                    sozlu1 = reader.IsDBNull(reader.GetOrdinal("Sozlu1")) ? 0 : Convert.ToDouble(reader["Sozlu1"]);
                    sozlu2 = reader.IsDBNull(reader.GetOrdinal("Sozlu2")) ? 0 : Convert.ToDouble(reader["Sozlu2"]);
                    notOrtalamasi = reader.IsDBNull(reader.GetOrdinal("NotOrtalamasi")) ? 0 : Convert.ToDouble(reader["NotOrtalamasi"]);
                    sonucDegerlendirme = reader["SonucDegerlendirme"].ToString();
                }
                reader.Close();

                switch (columnName)
                {
                    case "Sinav1":
                        sinav1 = newValue;
                        break;
                    case "Sinav2":
                        sinav2 = newValue;
                        break;
                    case "Sinav3":
                        sinav3 = newValue;
                        break;
                    case "Sozlu1":
                        sozlu1 = newValue;
                        break;
                    case "Sozlu2":
                        sozlu2 = newValue;
                        break;
                }

                notOrtalamasi = (sinav1 * 0.1) + (sinav2 * 0.1) + (sinav3 * 0.4) + (sozlu1 * 0.2) + (sozlu2 * 0.2);

                if (notOrtalamasi < 40)
                {
                    sonucDegerlendirme = "FF";
                }
                else if (notOrtalamasi < 50)
                {
                    sonucDegerlendirme = "DC";
                }
                else if (notOrtalamasi < 60)
                {
                    sonucDegerlendirme = "CC";
                }
                else if (notOrtalamasi < 70)
                {
                    sonucDegerlendirme = "CB";
                }
                else if (notOrtalamasi < 80)
                {
                    sonucDegerlendirme = "BB";
                }
                else if (notOrtalamasi < 90)
                {
                    sonucDegerlendirme = "BA";
                }
                else if (notOrtalamasi <= 100)
                {
                    sonucDegerlendirme = "AA";
                }

                query = "UPDATE [dbo].[table] SET Sinav1 = @sinav1, Sinav2 = @sinav2, Sinav3 = @sinav3, Sozlu1 = @sozlu1, Sozlu2 = @sozlu2, NotOrtalamasi = @notOrtalamasi, SonucDegerlendirme = @sonucDegerlendirme WHERE ID = @studentID";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@sinav1", sinav1);
                command.Parameters.AddWithValue("@sinav2", sinav2);
                command.Parameters.AddWithValue("@sinav3", sinav3);
                command.Parameters.AddWithValue("@sozlu1", sozlu1);
                command.Parameters.AddWithValue("@sozlu2", sozlu2);
                command.Parameters.AddWithValue("@notOrtalamasi", notOrtalamasi);
                command.Parameters.AddWithValue("@sonucDegerlendirme", sonucDegerlendirme);
                command.Parameters.AddWithValue("@studentID", studentID);
                command.ExecuteNonQuery();
            }
            LoadRecords(currentStudentName);
        }

        private NotModel kopyalananNotModel = new NotModel();

        private void KopyalananNotlariAl()
        {
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            int selectedRowID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["ID"].Value);

            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                connection.Open();
                string query = "SELECT Sinav1, Sinav2, Sinav3, Sozlu1, Sozlu2, NotOrtalamasi, SonucDegerlendirme FROM [dbo].[table] WHERE ID=@id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", selectedRowID);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        kopyalananNotModel.ID = selectedRowID;
                        kopyalananNotModel.Sinav1 = Convert.ToDouble(reader["Sinav1"]);
                        kopyalananNotModel.Sinav2 = Convert.ToDouble(reader["Sinav2"]);
                        kopyalananNotModel.Sinav3 = Convert.ToDouble(reader["Sinav3"]);
                        kopyalananNotModel.Sozlu1 = Convert.ToDouble(reader["Sozlu1"]);
                        kopyalananNotModel.Sozlu2 = Convert.ToDouble(reader["Sozlu2"]);
                        kopyalananNotModel.NotOrtalamasi = Convert.ToDouble(reader["NotOrtalamasi"]);
                        kopyalananNotModel.SonucDegerlendirme = reader["SonucDegerlendirme"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Belirtilen ID'ye sahip kayıt bulunamadı.");
                    }

                    reader.Close();
                }
            }
        }

        private void YapıştırılanNotlariYapıştır()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True"))
            {
                connection.Open();

                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int selectedRowID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["ID"].Value);

                string query = "UPDATE [dbo].[table] SET Sinav1=@sinav1, Sinav2=@sinav2, Sinav3=@sinav3, Sozlu1=@sozlu1, Sozlu2=@sozlu2, NotOrtalamasi=@notortalama, SonucDegerlendirme=@sonucdgr WHERE ID=@pid";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sinav1", kopyalananNotModel.Sinav1);
                    command.Parameters.AddWithValue("@sinav2", kopyalananNotModel.Sinav2);
                    command.Parameters.AddWithValue("@sinav3", kopyalananNotModel.Sinav3);
                    command.Parameters.AddWithValue("@sozlu1", kopyalananNotModel.Sozlu1);
                    command.Parameters.AddWithValue("@sozlu2", kopyalananNotModel.Sozlu2);
                    command.Parameters.AddWithValue("@notortalama", kopyalananNotModel.NotOrtalamasi);
                    command.Parameters.AddWithValue("@sonucdgr", kopyalananNotModel.SonucDegerlendirme);
                    command.Parameters.AddWithValue("@pid", selectedRowID);
                    
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Notlar başarıyla yapıştırıldı.");
                        LoadRecords(currentStudentName);
                    }
                    else
                    {
                        MessageBox.Show("Belirtilen ID'ye sahip kayıt bulunamadı.");
                    }
                }
            }
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            textBox9.Clear();
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cikis_Click(object sender, EventArgs e)
        {
            Form form1 = Application.OpenForms["Form1"];
            if (form1 != null)
            {
                form1.Show();
            }
            Form form2 = Application.OpenForms["Form2"];
            form2.Show();
            this.Close();
        }
      
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form form2 = Application.OpenForms["Form2"];
            form2.Show();
        }
        
        private void yeniderseklebtn_Click(object sender, EventArgs e)
        {
            Form form4 = Application.OpenForms["Form4"];
            if (form4 != null && !form4.IsDisposed)
            {
                form4.Show();
            }
            else
            {
                form4 = new Form4(currentStudentName,isadmin);
                form4.Show();
            }
        }

        private void derssecimibtn_Click(object sender, EventArgs e)
        {
            Form form4 = Application.OpenForms["Form4"];
            if (form4 != null && !form4.IsDisposed)
            {
                form4.Show();
            }
            else
            {
                form4 = new Form4(currentStudentName, isadmin);
                form4.Show();
            }
        }

    }
}



