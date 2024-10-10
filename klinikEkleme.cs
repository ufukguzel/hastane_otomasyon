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
    public partial class klinikEkleme : Form
    {
        public klinikEkleme()
        {
            InitializeComponent();
        }

        private void klinikEkleme_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            formlar.formAdminGirisPaneli.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formlar.formAdminGirisPaneli.Show();
            this.Hide();
        }

        void klinikler() // klinkler fonksiyonu
        {
            textBox1.Text = "";
            SqlDataAdapter mda = new SqlDataAdapter("select * from klinikler", formlar.baglanti); // Verileri dataadapter ile sorguladık.
            formlar.dataGridVeri(mda, dataGridView1); // dataGridVeriGetir fonksiyonuna MySqlDataAdapter ve datagridviewimizi gösterdik.
        }

        private void klinikEkleme_Load(object sender, EventArgs e)
        {
            klinikler();
        }

        private void button1_Click(object sender, EventArgs e)
        {// ekleme komutu
            try
            {
                if (textBox1.Text != "")
                {
                    SqlCommand c = new SqlCommand("insert into klinikler(klinikAdi) values(@kadi)", formlar.baglanti);// klinikadına @kadiyi ekledik
                    c.Parameters.AddWithValue("@kadi", textBox1.Text); // @kadi textboxa yazdığımz klinik adıdır.
                    formlar.veri_ekle(c); // veriyi klinikler tablosuna ekledik fonksiyonumuzla
                    MessageBox.Show("Klinik eklendi!"); // mesajımız
                }
                else
                {
                    MessageBox.Show("Lütfen boş bırakmayınız.");
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bilinmeyen bir hata gerçekleşti.\n" + hata);
            }
            klinikler(); // klinikleri yeniledik.
        }

        private void button2_Click(object sender, EventArgs e)
        {// sil butonu
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                try
                {
                    SqlCommand c = new SqlCommand("DELETE from klinikler where klinikID=@kid", formlar.baglanti);// klinikadına @kadiyi ekledik
                    c.Parameters.AddWithValue("@kid", dataGridView1.CurrentRow.Cells[0].Value.ToString()); // @kadi textboxa yazdığımz klinik adıdır.
                    formlar.veri_ekle(c); // veriyi klinikler tablosuna ekledik fonksiyonumuzla
                    MessageBox.Show("Klinik silindi!"); // mesajımız
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Bilinmeyen bir hata gerçekleşti.\n" + hata);
                }

            }
            else MessageBox.Show("Lüyfen bir klinik seçiniz!");
            klinikler(); // klinikleri yeniledik.
        }
    }
}
