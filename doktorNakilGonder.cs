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
    public partial class doktorNakilGonder : Form
    {
        public doktorNakilGonder()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        { // Geri butonuna tıklandığında
            formlar.formDoktorGiris.Show(); //formDoktorGiris formunu gösterdik.
            this.Hide(); // bu formu gizledik.
        }

        private void button1_Click(object sender, EventArgs e)
        { // Nakil gönder butonu kodları
            if (textBox1.Text != "") // eğer textboxlar boş değilse
            {
                bool hastaNakilOldu = false; // sata daha önce taburcu oldu mu kontrol yapıyoruz.
                SqlCommand c;
                try // try kodumuz önce kodları deniyor hata bulursa alttaki catch kodu içerisinde ki kodları çalıştırıyor.
                {
                    c = new SqlCommand("select * from nakiller where nakilTC=@tc", formlar.baglanti); // taburcular tablosunu konytöl ettik
                    c.Parameters.AddWithValue("@tc", textBox1.Text); // eğer tc si varsa taburcu olmuştur.
                    formlar.veri_getir(c);
                    if (formlar.dr.Read()) // sonra getirilen bilgileri okuyoruz eğer okunuyorsa taburcu olmuştur.
                    {
                        hastaNakilOldu = true;
                    }
                    formlar.baglanti.Close();
                    if (hastaNakilOldu == false)
                    {
                        c = new SqlCommand("insert into nakiller(nakilTC,nakiledilenHastane,doktorID) values(@ntc,@neh,@did)", formlar.baglanti);
                        // c mysqlCommand nesnesi oluşturduk ve insert into komutu muzu ekledik.
                        c.Parameters.AddWithValue("@ntc", textBox1.Text);
                        c.Parameters.AddWithValue("@neh", comboBox1.Text);
                        c.Parameters.AddWithValue("@did", formlar.girisYapanKisi);
                        formlar.veri_ekle(c);
                        // d mysql command nesnesi
                        SqlCommand d = new SqlCommand("delete from yatisverilenler where yatanTC=@tc", formlar.baglanti);
                        // tc si textboxta yazan kişiyi yatisverilenler tablosundan siliyoruz.
                        d.Parameters.AddWithValue("@tc", textBox1.Text);
                        formlar.veri_ekle(d);
                        MessageBox.Show("Nakil edildi!");
                    }
                    else
                    {
                        MessageBox.Show("Hasta zaten nakil edildi.");
                    }
                }
                catch (Exception hata) { MessageBox.Show("Beklenmedik bir hata!\n" + hata); }// herhangi bir hata olursa bu çalışıyor.
            }
            else
            {
                MessageBox.Show("Lütfen TC giriniz.");
            }
        }

        private void doktorNakilGonder_FormClosing(object sender, FormClosingEventArgs e)
        { // form çarpı butonundan kapatılırsa
            formlar.formDoktorGiris.Show(); // doktor giriş formunu göster
            this.Hide(); // bu formu gizle
            e.Cancel = true; // kapatma işlemini iptal et
        }

        private void doktorNakilGonder_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
