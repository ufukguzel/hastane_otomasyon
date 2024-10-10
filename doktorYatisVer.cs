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
    public partial class doktorYatisVer : Form
    {
        public doktorYatisVer()
        {
            InitializeComponent();
        }

        private void doktorYatisVer_FormClosing(object sender, FormClosingEventArgs e)
        { // yakış ver formu çarpıdan kapatılırsa.
            e.Cancel = true; // bu formu kapatmayı iptal ediyoruz.
            formlar.formAnaEkran.Show(); // anaekranı gösterdik.
            this.Hide(); // formu gizledik.
        }

        private void button2_Click(object sender, EventArgs e)
        { // geri butonu
            formlar.formDoktorGiris.Show();// formDoktorGirisi gösterdik.
            this.Hide();// formu gizledik.
        }

        private void button3_Click(object sender, EventArgs e)
        {// bul
            if (textBox1.Text != "") // eğer textbox1 boş değisle tc yani
            {
                try  // try kodumuz önce kodları deniyor hata bulursa alttaki catch kodu içerisinde ki kodları çalıştırıyor.
                {
                    // hastalar tablosuna bakıyoruz ve orada tc 
                    SqlCommand c = new SqlCommand("select * from hastalar where TC=@tc", formlar.baglanti);
                    c.Parameters.AddWithValue("@tc", textBox1.Text);
                    formlar.veri_getir(c);
                    while (formlar.dr.Read())
                    {
                        textBox2.Text = formlar.dr["adi"].ToString();
                        textBox3.Text = formlar.dr["soyadi"].ToString();
                        textBox4.Text = formlar.dr["DogumTarihi"].ToString();
                        textBox5.Text = formlar.dr["DogumYeri"].ToString();
                    }
                    formlar.baglanti.Close();
                }
                catch (Exception) { }
            }
            else
            {
                MessageBox.Show("Lütfen TC giriniz.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//Yatış ver
            SqlCommand c;
            if (textBox1.Text != "")
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
                        if (hastaNakilOldu == false)
                        {
                            bool yatisVerildi = false; // sata daha önce taburcu oldu mu kontrol yapıyoruz.
                            c = new SqlCommand("select * from yatisverilenler where yatanTC=@tc", formlar.baglanti); // taburcular tablosunu konytöl ettik
                            c.Parameters.AddWithValue("@tc", textBox1.Text); // eğer tc si varsa taburcu olmuştur.
                            formlar.veri_getir(c);
                            if (formlar.dr.Read()) // sonra getirilen bilgileri okuyoruz eğer okunuyorsa taburcu olmuştur.
                            {
                                yatisVerildi = true;
                            }
                            formlar.baglanti.Close();
                            if (yatisVerildi == false)
                            {
                                try
                                { // insert into komutu ileyatışı verilen kişileri yatisverilenler tablosuna ekliyoruz.
                                    c = new SqlCommand("insert into yatisverilenler(yatisTarihi,yatanTC) values(@yt,@tc)", formlar.baglanti);
                                    c.Parameters.AddWithValue("@yt", dateTimePicker2.Value.ToShortDateString());
                                    c.Parameters.AddWithValue("@tc", textBox1.Text);
                                    formlar.veri_ekle(c);
                                    MessageBox.Show("Yatış verildi!");
                                }
                                catch (Exception hata) { MessageBox.Show("Daha önceden yatış verildi!\n" + hata); }
                            }
                            else
                            {
                                MessageBox.Show("Daha önce yatış verildi.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hasta taburcu olmuştur.Tekrardan Yatış veremezsiniz.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hasta nakil olmuştur.Tekrardan Yatış veremezsiniz.");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen TC giriniz.");
                }
            }
        }
    }
}
