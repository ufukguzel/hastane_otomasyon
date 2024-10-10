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
    public partial class doktorTaburcuEt : Form
    {
        public doktorTaburcuEt()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        { // Geri butonu
            formlar.formDoktorGiris.Show();// doktor giriş formunu göster
            this.Hide(); // bu formu gizle
        }

        private void doktorTaburcuEt_FormClosing(object sender, FormClosingEventArgs e)
        { // doktorTaburcuEt formu çarpıdan kapatılırsa
            formlar.formDoktorGiris.Show();// doktor giriş formunu göster
            this.Hide(); // bu formu gizle
            e.Cancel = true; // kapatma işlemini iptal et
        }

        private void button1_Click(object sender, EventArgs e)
        { // taburcu et butonu
            SqlCommand c;
            if (textBox1.Text != "") // eğer textbox1 boş değilse yani tc alanı
            {
                bool hastaNakilOldu = false;
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
                    bool hastaTaburcuOldu = false; // sata daha önce taburcu oldu mu kontrol yapıyoruz.
                    try // try kodumuz önce kodları deniyor hata bulursa alttaki catch kodu içerisinde ki kodları çalıştırıyor.
                    {
                        c = new SqlCommand("select * from taburcular where TC=@tc", formlar.baglanti); // taburcular tablosunu konytöl ettik
                        c.Parameters.AddWithValue("@tc", textBox1.Text); // eğer tc si varsa taburcu olmuştur.
                        formlar.veri_getir(c);
                        if (formlar.dr.Read()) // sonra getirilen bilgileri okuyoruz eğer okunuyorsa taburcu olmuştur.
                        {
                            hastaTaburcuOldu = true;
                        }
                        formlar.baglanti.Close();
                        if (hastaTaburcuOldu == false)
                        {
                            c = new SqlCommand("insert into taburcular(TC,cikisTarihi,girisTarihi,ucret) values(@tc,@ct,@gt,@uc)", formlar.baglanti);
                            // nakil gönderirken nakiller listesine kişiyi ekliyoruz.
                            c.Parameters.AddWithValue("@tc", textBox1.Text);
                            c.Parameters.AddWithValue("@ct", dateTimePicker1.Value.ToShortDateString()); // ct çıkış tarihi tarih ayarı dd.mm.yyyy şeklinde kayıt eder.
                            c.Parameters.AddWithValue("@gt", dateTimePicker2.Value.ToShortDateString()); // gt giriş tarihi tarih ayarı dd.mm.yyyy şeklinde kayıt eder.
                            c.Parameters.AddWithValue("@uc", textBox2.Text);
                            formlar.veri_ekle(c);
                            // ve yatış verilenlerden siliyoruz
                            SqlCommand d = new SqlCommand("delete from yatisverilenler where yatanTC=@tc", formlar.baglanti);
                            d.Parameters.AddWithValue("@tc", textBox1.Text);
                            formlar.veri_ekle(d);
                            MessageBox.Show("Taburcu edildi!");
                        }
                        else
                        {
                            MessageBox.Show("Hasta daha öcneden taburcu edildi!", "Hata");
                        }
                    }
                    catch (Exception hata) { MessageBox.Show("Daha önceden yatış verildi!\n" + hata); } // herhangi bir hata olursa böyle hata ver.
                }
                else
                {
                    MessageBox.Show("Hasta nakil oldu taburcu edemezsiniz.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen TC giriniz.");
            }
        }
    }
}
