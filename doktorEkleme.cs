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
    public partial class doktorEkleme : Form
    {
        public doktorEkleme()
        {
            InitializeComponent();
        }

        private void doktorEkleme_FormClosing(object sender, FormClosingEventArgs e)
        { // Doktor Ekleme Formu sağ üst çarpıdan kapatılırsa
            e.Cancel = true; // kapatmayı iptal ediyoruz.
            formlar.formAdminGirisPaneli.Show(); // formAdminGirisPaneli formumuzu gösteriyoruz.
            this.Hide(); // Doktor ekleme formunu gizliyoruz.
        }
        void doktorlar() // doktorları yenilemek için değer döndermeyen fonksiyon kodumuz.
        {// Bu kodu form içerisinde herhangi bir yerde kullanabiliriz.Örneğin yeni kayıt yaptık ardından foktorlar listesini yeniliyoruz bu kodla.
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = ""; // Her seferinde textboxları temizliyoruz.
            // doktorlar fonksiyonumuzda
            SqlDataAdapter mda = new SqlDataAdapter("select * from doktorlar", formlar.baglanti); // Verileri dataadapter ile sorguladık.
            formlar.dataGridVeri(mda, dataGridView1); // dataGridVeriGetir fonksiyonuna MySqlDataAdapter ve datagridviewimizi gösterdik.
            SqlCommand c = new SqlCommand("select * from klinikler", formlar.baglanti); // c isimli command nesnemize klinikleri getiren sql kodumuzu ekledik.
            formlar.combo(c, "klinikID", "klinikAdi", comboBox1); // ve comboboxa kliniklerin id sini ve adını atadık.
        }

        private void doktorEkleme_Load(object sender, EventArgs e)
        { // Doktor ekleme sayfası ekrana geldiğinde
            formlar.baglan();  // formlar klasın da ki bağlan fonksiyonunu çağırdık.
            doktorlar(); // doktorları ve klinikleri listeledik.
        }

        private void button1_Click(object sender, EventArgs e)
        {// Ekle Butonu kodları
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")  // textboxlar boş değilse işlemlerimizi yapalım.
            {
                try // try kodumuz önce kodları deniyor hata bulursa alttaki catch kodu içerisinde ki kodları çalıştırıyor.
                {// Bulamazsa kendi kodlarını çalıştırıyor.
                    SqlCommand u = new SqlCommand("insert into doktorlar(kullaniciAdi,Sifre,doktorAdiSoyadi,klinikID)values(@kadi,@sifre,@adsoyad,@klinikid)", formlar.baglanti);
                    // insert into ile veritabanında doktorlar tablosuna veriler ekledik.Parametre kodları ile hangi değerleri ekleyeceğimizi belirledik.
                    u.Parameters.AddWithValue("@kadi", textBox1.Text);
                    u.Parameters.AddWithValue("@sifre", textBox2.Text);
                    u.Parameters.AddWithValue("@adsoyad", textBox3.Text);
                    u.Parameters.AddWithValue("@klinikid", comboBox1.SelectedValue.ToString());
                    formlar.veri_ekle(u);
                    MessageBox.Show("Başarıyla kayıt edildi!");
                    textBox1.Text = textBox2.Text = textBox3.Text = ""; // Textboxları temizledik.
                }
                catch (Exception) // herhangi bir hata olursa bu çalışıyor.
                {
                    MessageBox.Show("Doktor zaten kayıtlı!");
                }
            }
            else  // textboxlar boşsa hata mesajımızı veriyoruz.
            {
                MessageBox.Show("Lütfen eksik yer bırakmayınız!");
            }
            doktorlar(); // doktorları ve klinikleri listeledik.
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { // dataGridView de herhangi bir sütuna tıkladığımız da.
            try // try kodumuz önce kodları deniyor hata bulursa alttaki catch kodu içerisinde ki kodları çalıştırıyor.
            { // c MySqlCommand nesnesi oluşturduk ve 
                SqlCommand c = new SqlCommand("select * from doktorlar where doktorID=@d_id", formlar.baglanti);
                // data gridde seçilen doktorun ilk sütununda id si yer alıyor bu id ye göre o doktorun bilgilerini getirmiş olduk
                c.Parameters.AddWithValue("@d_id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                formlar.veri_getir(c);
                while (formlar.dr.Read())
                { // getirilen bilgileri textboxlara ekledik.
                    textBox4.Text = formlar.dr["doktorID"].ToString(); 
                    textBox1.Text = formlar.dr["kullaniciAdi"].ToString();
                    textBox2.Text = formlar.dr["Sifre"].ToString();
                    textBox3.Text = formlar.dr["doktorAdiSoyadi"].ToString();
                    comboBox1.SelectedValue = formlar.dr["klinikID"].ToString();
                }
                formlar.baglanti.Close();// Textboxları temizledik.
            }
            catch (Exception){ } // herhangi bir hata olursa bu çalışıyor.
        }

        private void button2_Click(object sender, EventArgs e)
        {// sil butonu
            try // try kodumuz önce kodları deniyor hata bulursa alttaki catch kodu içerisinde ki kodları çalıştırıyor.
            {
                SqlCommand c = new SqlCommand("delete from doktorlar where doktorID=@d_id", formlar.baglanti);
                // delete sorgusu ile textbox4 te yazan id ye göre doktoru sildik.
                c.Parameters.AddWithValue("@d_id", textBox4.Text);
                formlar.veri_ekle(c); // veri ekle fonksiyonumuz anı sql kodunun çalışmasını sağlıyor yan, aynı zamanda silebilir.
                MessageBox.Show("Başarıyla silindi!");
            }
            catch (Exception) { } // herhangi bir hata olursa bu çalışıyor.
            doktorlar(); // doktorları ve klinikleri listeledik.
        }
        
        private void button4_Click(object sender, EventArgs e)
        {//düzenle butonu
            try// try kodumuz önce kodları deniyor hata bulursa alttaki catch kodu içerisinde ki kodları çalıştırıyor.
            {
                SqlCommand c = new SqlCommand("update doktorlar set doktorID=@d_id,kullaniciAdi=@kadi,Sifre=@sifre,doktorAdiSoyadi=@adsoyad,klinikID=@klinikid where doktorID=@d_id", formlar.baglanti);
                // update doktorlar sorgumuz.Set ile yeni bilgiler ekledik yeni bilgileri command a atadğımız parametrs lardan aldık.
                c.Parameters.AddWithValue("@kadi", textBox1.Text);
                c.Parameters.AddWithValue("@sifre", textBox2.Text);
                c.Parameters.AddWithValue("@adsoyad", textBox3.Text);
                c.Parameters.AddWithValue("@klinikid", comboBox1.SelectedValue.ToString());
                c.Parameters.AddWithValue("@d_id", textBox4.Text);
                formlar.veri_ekle(c);
                MessageBox.Show("Başarıyla güncellendi!");
            }
            catch (Exception) { }// herhangi bir hata olursa bu çalışıyor.
            doktorlar(); // doktorları ve klinikleri listeledik.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formlar.formAdminGirisPaneli.Show();// formAdminGirisPaneli formumuzu gösteriyoruz.
            this.Hide(); // Doktor ekleme formunu gizliyoruz.

        }
    }
}
