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
    public partial class doktorGiris : Form
    {
        public doktorGiris()
        {
            InitializeComponent();
        }

        private void doktorGiris_FormClosing(object sender, FormClosingEventArgs e)
        {// Doktor Ekleme Formu sağ üst çarpıdan kapatılırsa
            randevular(); // randevular fonksiyonunu çalıştırdık.
            formlar.formAnaEkran.Show(); // anaekran formumuzu gösterdik.
            this.Hide(); // bu formu gizledik.
            e.Cancel = true; // kapatmayı iptal ediyoruz.
        }

        private void button4_Click(object sender, EventArgs e)
        { // Geri butonu kodları
            formlar.formAnaEkran.Show(); // anaekran formumuzu gösterdik.
            this.Hide(); // bu formu gizledik.
        }

        private void button1_Click(object sender, EventArgs e)
        {//nakil gönder butonu kodları
            formlar.formDoktorNakilGonder.Show();// formDoktorNakilGonder formumuzu gösterdik.
            this.Hide(); // bu formu gizledik.
        }

        private void button2_Click(object sender, EventArgs e)
        {// Hastaya yatış ver butonu kodları
            formlar.formDoktorYatisVer.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); // 1.satırda tc olduğu için
            // formDoktorYatisVerformunda ki modifiersi public olan textbox1 e değer atadık. atanana değer seçilen satırın 1. alanı oda tc
            formlar.formDoktorYatisVer.Show();// formDoktorYatisVer formumuzu gösterdik.
            this.Hide(); // bu formu gizledik.
        }
        public static void randevular()
        { // randevuları listeledik.
            formlar.baglan(); // formlar klasında ki bağlan fonksiyonunu çalıştırdık.
            SqlDataAdapter d = new SqlDataAdapter("select * from randevular", formlar.baglanti);
            formlar.dataGridVeri(d,formlar.formDoktorGiris.dataGridView1);
        }
        private void doktorGiris_Load(object sender, EventArgs e)
        {//doktorGiris formu açıldığında çalışacak kod
            randevular(); // randevuları listeliyoruz.
        }

        private void button3_Click(object sender, EventArgs e)
        {// Hastayı taburcu et butonu kodları
            formlar.formTaburcuEt.Show();// formTaburcuEt formumuzu gösterdik.
            this.Hide(); // bu formu gizledik.
        }
    }
}
