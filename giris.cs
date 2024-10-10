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
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        { // üye ola tıkalntıysa
            formlar.formUyeOl.Show(); // üye ol formumuzu gösteriyoruz.
            this.Hide(); // bu formu gizledik.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formlar.formAnaEkran.Show(); // formAnaEkran formumuzu gösteriyoruz.
            this.Hide();// bu formu gizledik.
        }

        private void giris_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // formu kapatmayı iptal ediyoruz.
            formlar.formAnaEkran.Show(); // formAnaEkran formumuzu gösteriyoruz.
            this.Hide(); // bu formu gizledik.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") // textboclar boş dğeilse
            {
                try
                { // giriş butonu kodları
                    SqlCommand hastaSorgusu = new SqlCommand("select * from hastalar where TC=@kadi and Parola=@sifre", formlar.baglanti);
                    // doktorlar için sorgu yaptık
                    hastaSorgusu.Parameters.Add("@kadi", textBox1.Text); // kullanıcı adı 
                    hastaSorgusu.Parameters.Add("@sifre", textBox2.Text); // ve şifreyi veri tabanında aradık
                    formlar.veri_getir(hastaSorgusu); // yöneticiler tablosudna ki verileri getirdik
                    if (formlar.dr.Read()) // eğer bulduysa okuma başarılıysa
                    {
                        textBox1.Text = textBox2.Text = ""; // textboxları temizledik
                        RandevuAl.girisYapanTC = formlar.formRandevuAl.textBox1.Text = formlar.dr["TC"].ToString();
                        formlar.formRandevuAl.textBox1.Text = formlar.dr["TC"].ToString();
                        formlar.formRandevuAl.textBox2.Text = formlar.dr["adi"].ToString();
                        formlar.formRandevuAl.textBox3.Text = formlar.dr["soyadi"].ToString();
                        if(formlar.dr["Cinsiyeti"].ToString() == "Bay")
                        { formlar.formRandevuAl.radioButton1.Checked = true; }
                        else formlar.formRandevuAl.radioButton2.Checked = true;
                        formlar.formRandevuAl.textBox4.Text = formlar.dr["DogumYeri"].ToString();
                        formlar.formRandevuAl.textBox5.Text = formlar.dr["babaAdi"].ToString();
                        formlar.formRandevuAl.textBox6.Text = formlar.dr["anneAdi"].ToString();
                        formlar.formRandevuAl.textBox7.Text = formlar.dr["CepTel"].ToString();
                        formlar.formRandevuAl.textBox8.Text = formlar.dr["Eposta"].ToString();
                        formlar.formRandevuAl.textBox9.Text = formlar.dr["Parola"].ToString();
                        formlar.formRandevuAl.Show(); // formu gösterdik
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

        private void giris_Load(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
