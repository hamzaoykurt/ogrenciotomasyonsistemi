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

namespace denemestaj
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox2.KeyDown += textBox2_KeyDown;
        }

        SqlConnection baglanti = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True");

        private void girisyap_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string sql = "Select * From parola WHERE OgrenciAdi=@ograd AND Sifre=@sifre";
                SqlParameter prm1 = new SqlParameter("@ograd", textBox1.Text);
                SqlParameter prm2 = new SqlParameter("@sifre", textBox2.Text);
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.Add(prm1);
                komut.Parameters.Add(prm2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string studentName = textBox1.Text;
                    bool isAdmin = (textBox1.Text.ToLower() == "admin");

                    Form1 fr = new Form1(studentName, isAdmin);
                    fr.Show();
                    Form form2 = Application.OpenForms["Form2"];
                    if (form2 != null)
                    {
                        form2.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Hatalı Giriş");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
            textBox1.Clear();
            textBox2.Clear();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                girisyap.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form Form = Application.OpenForms["Form3"];
            if (Form != null)
            {
                Form.Show();
            }
            else
            {
                Form3 frm3 = new Form3();
                frm3.Show();
            }
            this.Hide();

        }
    }
}
