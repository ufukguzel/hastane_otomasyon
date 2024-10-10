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
    public partial class RandevuAl : Form
    {
        public RandevuAl()
        {
            InitializeComponent();
        }
        public static string girisYapanTC = "0";
        private void RandevuAl_FormClosing(object sender, FormClosingEventArgs e)
        {// randevuAl formu çarpı butonunda kapatılırsa sağ üst
            e.Cancel = true; // form kapatmayı iptal ediyoruz
            formlar.formAnaEkran.Show(); // aanekran formumuzu açıyoruz
            this.Hide();// bu formu gizliyoruz.
        }

        private void button22_Click(object sender, EventArgs e)
        {// geri butonu
            formlar.formAnaEkran.Show(); // aanekran formumuzu açıyoruz
            this.Hide();// bu formu gizliyoruz.
        }
        void klinkgetir() // klinkgetir fonksiyonu
        { // comboboxa klinği getiriyoruz.
            SqlCommand c = new SqlCommand("select * from klinikler",formlar.baglanti);
            formlar.combo(c,"klinikID","klinikAdi",comboBox1);
        }
        bool randevualabilir = true;
        void randevuGetir() // randevu getirme fonksiyonu
        {
            if (comboBox1.SelectedIndex != 0 && comboBox2.SelectedIndex != 0) // eğer comboboxlar boş değilse
            {
                try
                {
                    foreach (Control item in panel1.Controls) // panel1 de ki butonların arkaplanını yeşil yapıyoruz.
                        if (item is Button) item.BackColor = Color.Green;
                    randevualabilir = true; // randevu alabilirmiyi true olarak değiştirdik.
                    panel1.Enabled = true; // panel1 i aktif ettik
                    SqlCommand c = new SqlCommand("select * from randevular where klinikID=@kid and doktorID=@did and tarih=@tarih", formlar.baglanti);
                    // tarih,doktor ve seçilen kliniğe göre randevuları getirdik ama bir yerde göstermedik.
                    c.Parameters.AddWithValue("@kid", comboBox1.SelectedValue.ToString()); // kliniğin id sini aldık
                    c.Parameters.AddWithValue("@did", comboBox2.SelectedValue.ToString()); // doktorun id si
                    c.Parameters.AddWithValue("@tarih", dateTimePicker1.Value.ToShortDateString()); // tarih değeri
                    formlar.veri_getir(c);
                    while (formlar.dr.Read()) // sonra getirilen bilgileri okuyoruz 
                    {
                        foreach (Control item in panel1.Controls) // tüm butonları tek tek kontro lediyoruz
                        {
                            // eğer butonun üstündeki yazi gelen o tarihte o klinikte o doktora gelen randevuya eşitse arkaplanı kırmızı oluyor
                            if (item is Button && item.Text == formlar.dr["saat"].ToString()) 
                            {
                                item.BackColor = Color.Red; // kırmızı yapıyoruz.
                                if (girisYapanTC == formlar.dr["TC"].ToString())
                                {// eğer giriş yapan kişi o randevuya sahip olan kişiyse
                                    MessageBox.Show("Bu tarihte bir randevunuz var lütfen başka tarih seçiniz!");
                                    randevualabilir = false;// randevu almasını engelliyoruz çünkü aynı tarih saat ve doktora aynı kişi birden fazla randevu alamamalı.
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                formlar.baglanti.Close();
            }
            else
            {
                panel1.Enabled = false;
            }
        }

        private void RandevuAl_Load(object sender, EventArgs e)
        {
            klinkgetir();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {//  klinik seçildiye doktor getir	
            if (comboBox1.SelectedIndex != 0) // klinik comboboxı seçiniz değilse
                {
                    SqlCommand d = new SqlCommand("SELECT klinikler.klinikID,doktorlar.doktorID,doktorlar.doktorAdiSoyadi FROM doktorlar INNER JOIN klinikler ON doktorlar.klinikID = klinikler.klinikID where doktorlar.klinikID=@kid", formlar.baglanti);
                    d.Parameters.Add("@kid", comboBox1.SelectedValue.ToString()); //  kliniğe göre doktor getirdik doktor getirirken inner join kullanarak seçili kliniğin doktorunu getirdik.
                    formlar.combo(d, "doktorID", "doktorAdiSoyadi", comboBox2);
                }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            randevuGetir(); // eğer tarih değiştiyse randevuları tekrar getirdik.
        }
        string saat = "";
        Button btn;
        private void b_click(object sender, EventArgs e)
        {//butonları herhangi birine tıklandığında hangisi olduğunu buluyoruz.
            btn = sender as Button; // seçilen butonu bulduk
            saat = btn.Text; // butonun üstünde ki saati saat stringine atadık.
        }

        private void button1_Click(object sender, EventArgs e)
        {// Randevu al butonumuz
            if (comboBox1.SelectedIndex != 0 && comboBox2.SelectedIndex != 0 && btn.BackColor != Color.Red && randevualabilir == true)
            {// insert into ile randevuları randevular tablosuna ekledik.
                SqlCommand c = new SqlCommand("insert into randevular(TC,klinikID,doktorID,tarih,saat)values(@tc,@kid,@did,@tarih,@saat)", formlar.baglanti);
                c.Parameters.AddWithValue("@tc", girisYapanTC);
                c.Parameters.AddWithValue("@kid", comboBox1.SelectedValue.ToString());
                c.Parameters.AddWithValue("@did", comboBox2.SelectedValue.ToString());
                c.Parameters.AddWithValue("@tarih", dateTimePicker1.Value.ToShortDateString());
                c.Parameters.AddWithValue("@saat", saat);
                formlar.veri_ekle(c);
                MessageBox.Show(girisYapanTC + " TC'li kişiye " + dateTimePicker1.Value.ToShortDateString() + " " + saat + "'ine randevu alınmıştır.");
                comboBox1.SelectedIndex = comboBox2.SelectedIndex = 0;
                randevuGetir();
            }
            // randevu al
        }

        private void button23_Click(object sender, EventArgs e)
        {// güncelle butonu
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" || radioButton1.Checked == true || radioButton2.Checked == true)
            {// uptade ile randevualmak için giriş yapan kişi bilgilerii değiştirmek isterse diye.
                SqlCommand u = new SqlCommand("UPDATE hastalar set adi=@adi,soyadi=@soyadi,Cinsiyeti=@cinsiyeti,DogumYeri=@dogumYeri,babaAdi=@babaAdi,anneAdi=@anneAdi,CepTel=@cepTel,Eposta=@eposta,Parola=@parola where TC=@tc", formlar.baglanti);
                u.Parameters.AddWithValue("@tc", textBox1.Text);
                u.Parameters.AddWithValue("@adi", textBox2.Text);
                u.Parameters.AddWithValue("@soyadi", textBox3.Text);
                u.Parameters.AddWithValue("@cinsiyeti", (radioButton1.Checked == true) ? "Bay" : "Bayan");
                u.Parameters.AddWithValue("@dogumYeri", textBox4.Text);
                u.Parameters.AddWithValue("@babaAdi", textBox5.Text);
                u.Parameters.AddWithValue("@anneAdi", textBox6.Text);
                u.Parameters.AddWithValue("@cepTel", textBox7.Text);
                u.Parameters.AddWithValue("@eposta", textBox8.Text);
                u.Parameters.AddWithValue("@parola", textBox9.Text);
                formlar.veri_ekle(u);
                MessageBox.Show("Başarıyla güncellendi!");
             }
            else
            {
                MessageBox.Show("Lütfen eksik yer bırakmayınız!");
            }
        }

        private void button24_Click(object sender, EventArgs e)
        { // randevu iptal butonu
            SqlDataAdapter mda = new SqlDataAdapter("SELECT randevular.randevuID,klinikler.klinikAdi, randevular.tarih, randevular.saat, hastalar.TC, hastalar.adi, hastalar.soyadi FROM randevular INNER JOIN hastalar ON randevular.TC = hastalar.TC INNER JOIN klinikler ON randevular.klinikID = klinikler.klinikID where hastalar.TC=@tc", formlar.baglanti);
            mda.SelectCommand.Parameters.AddWithValue("@tc",textBox1.Text);
            formlar.dataGridVeri(mda, formlar.formRandevuiPtal.dataGridView1); // formlar tablosunda dataGridVeri isimli fonksiyonu
            // çağırdık ve o fonksiyon verdiğimiz sql cümlesi ile formlar.formtaburcuOlanlar.dataGridView1'i istediğimiz bilgilerle doldurdu..
            formlar.formRandevuiPtal.Show(); // formlar klasında tanımladığımız formRandevuiPtal formumuzu göstertiyoruz.
            this.Hide(); // Bu ana ekran formumuzu gizledik.
        }
    }
}
