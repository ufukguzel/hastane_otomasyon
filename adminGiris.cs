using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Mysql kodlarını kullanmak için Referanslardan mysql.data referansını eklemek ve bu isim uzayını eklemeniz gerekiyor.

namespace hastane_otomasyon
{
    public partial class adminGiris : Form
    {
        public adminGiris()
        {
            InitializeComponent();
        }

        private void adminGiris_FormClosing(object sender, FormClosingEventArgs e) // admin giriş formu yukardaki çarpı işaretine
        {// basıldığında 
            e.Cancel = true; // kapanmasını iptal ediyoruz.
            formlar.formAnaEkran.Show(); // anaekran formumuzu gösterip
            this.Hide(); // şuan bulunan admin giriş formumuzu gizliyoruz.
        }

        private void button2_Click(object sender, EventArgs e) // Geri dön butonumuza tıklantığında
        {
            formlar.formAnaEkran.Show(); // anaekran formumuzu gösterip
            this.Hide(); // şuan bulunan admin giriş formumuzu gizliyoruz.
        }

        private void button1_Click(object sender, EventArgs e) // giriş butonuna tıklandığında
        {
            if (textBoxKadi.Text != "" && textBoxSifre.Text != "") // eğer textboxlar boş değilse textBoxKadi dediğimiz kullanıcı adının kısaltması
            {
                try // try kodumuz önce kodları deniyor hata bulursa alttaki catch içerisinde ki kodları çalıştırıyor.
                {
                    SqlCommand yoneticiSorgusu = new SqlCommand("select * from yoneticiler where kullaniciAdi=@kadi and sifre=@sifre", formlar.baglanti);
                    // yönetici için sorgu yaptık
                    yoneticiSorgusu.Parameters.Add("@kadi", textBoxKadi.Text); // kullanıcı adı 
                    yoneticiSorgusu.Parameters.Add("@sifre", textBoxSifre.Text); // ve şifreyi veri tabanında aradık
                    formlar.veri_getir(yoneticiSorgusu); // yöneticiler tablosudna ki verileri getirdik
                    if (formlar.dr.Read()) // eğer bulduysa okuma başarılıysa
                    {
                        textBoxKadi.Text = textBoxSifre.Text = ""; // textboxları temizledik
                        formlar.formAdminGirisPaneli.Show(); // formAdminGirisPaneli formumuzu gösterip
                        this.Hide(); // şuan bulunan admin giriş formumuzu gizliyoruz.
                    }
                    else // eğer yanlışsa yani veri gelmediyse demek ki kullanıcı adı veya şifre yanlıştır.Veya veritabanında kayıtlı değildir.
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre yanlış!"); // Hata mesajıöızı verdik
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
