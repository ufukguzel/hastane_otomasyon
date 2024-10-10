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
    public partial class doktorGirisPaneli : Form
    {
        public doktorGirisPaneli()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {// Geri butonu kodları
            formlar.formAnaEkran.Show();// formAnaEkran formunu gösterdik
            this.Hide(); // bu formu gizledik
        }

        private void doktorGirisPaneli_FormClosing(object sender, FormClosingEventArgs e)
        { // bu form çarpı işaretinden tıklanıp kapatmaya kalkılırsa
            e.Cancel = true; // formu kapatmayı iptal ediyoruz
            formlar.formAnaEkran.Show();// formAnaEkran formunu gösterdik
            this.Hide(); // bu formu gizledik
        }

        private void button1_Click(object sender, EventArgs e)
        { // Giriş butonuna tıklandıysa
            formlar.baglan(); // formlar klasında ki bağlan fonksiyonunu çalıştırdık.
            doktorGiris.randevular(); // randevuları listeliyoruz.
            if (textBoxKadi.Text != "" && textBoxSifre.Text != "") // ve eğer textboxlar boş değilse
            {
                try // try kodumuz önce kodları deniyor hata bulursa alttaki catch kodu içerisinde ki kodları çalıştırıyor.
                {// Bulamazsa kendi kodlarını çalıştırıyor.
                    formlar.baglanti.Open();
                    SqlCommand doktorSorgusu = new SqlCommand("select * from doktorlar where kullaniciAdi=@kadi and Sifre=@sifre", formlar.baglanti);
                    // doktorlar için sorgu yaptık
                    doktorSorgusu.Parameters.Add("@kadi", textBoxKadi.Text); // kullanıcı adı 
                    doktorSorgusu.Parameters.Add("@sifre", textBoxSifre.Text); // ve şifreyi veri tabanında aradık
                    formlar.veri_getir(doktorSorgusu); // yöneticiler tablosudna ki verileri getirdik
                    if (formlar.dr.Read()) // eğer bulduysa okuma başarılıysa
                    {
                        textBoxKadi.Text = textBoxSifre.Text = ""; // textboxları temizledik
                        formlar.girisYapanKisi = formlar.dr["doktorID"].ToString(); // formlar klasında girisYapanKisi isimli stringe doktorun id sini atıyoruz.
                        formlar.formDoktorGiris.Show(); //formDoktorGiris formunu gösterdik
                        this.Hide(); // bu formu gizledik
                    }
                    else // eğer yanlışsa 
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre yanlış!"); // Hata mesajı verdik
                    }
                    formlar.baglanti.Close();// veri tabanı bağlantısını kapadık.
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Bilinmeyen bir hata gerçekleşti hatnın sebebi:\n" + hata); // bilinmeyen bir hata
                }
            }
        }

        private void textBoxSifre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
