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

namespace hastane_otomasyon
{
    public partial class uyeol : Form
    {
        public uyeol()
        {
            InitializeComponent();
        }

        private void uyeol_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            formlar.formAnaEkran.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formlar.formGiris.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {// üye ol butonuna tıklandıysa
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" || radioButton1.Checked == true || radioButton2.Checked == true)
            {// gerekli yerler boş değilse insert into into ile girilen bilgileri veritabanına ekleyip üye yapıyruz.
                SqlCommand u = new SqlCommand("insert into hastalar(TC,adi,soyadi,Cinsiyeti,DogumYeri,DogumTarihi,babaAdi,anneAdi,CepTel,Eposta,Parola)values(@tc,@adi,@soyadi,@cinsiyeti,@dogumYeri,@dogumTarihi,@babaAdi,@anneAdi,@cepTel,@eposta,@parola)", formlar.baglanti);
                u.Parameters.AddWithValue("@tc", textBox1.Text);
                u.Parameters.AddWithValue("@adi", textBox2.Text);
                u.Parameters.AddWithValue("@soyadi", textBox3.Text);
                u.Parameters.AddWithValue("@cinsiyeti", (radioButton1.Checked == true) ? "Bay" : "Bayan");
                u.Parameters.AddWithValue("@dogumYeri", textBox4.Text);
                u.Parameters.AddWithValue("@dogumTarihi", dateTimePicker1.Value.ToShortDateString());
                u.Parameters.AddWithValue("@babaAdi", textBox5.Text);
                u.Parameters.AddWithValue("@anneAdi", textBox6.Text);
                u.Parameters.AddWithValue("@cepTel", textBox7.Text);
                u.Parameters.AddWithValue("@eposta", textBox8.Text);
                u.Parameters.AddWithValue("@parola", textBox9.Text);
                formlar.veri_ekle(u);
                MessageBox.Show("Başarıyla kayıt oldunuz!");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = "";
            }
            else
            {
                MessageBox.Show("Lütfen eksik yer bırakmayınız!");
            }
        }
    }
}
