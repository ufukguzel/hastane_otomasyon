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
    public partial class anaEkran : Form
    {
        public anaEkran()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) // button 3 admin girişi yazan butonumuz.
        {
            formlar.formAdminGiris.Show(); // formlar klasında tanımladığımız formAdminGiris formumuzu göstertiyoruz.
            this.Hide(); // Bu ana ekran formumuzu gizledik.
        }

        private void button2_Click(object sender, EventArgs e) // button 2 doktor girişi yazan butonumuz.
        {
            formlar.formDoktorGirisPaneli.Show(); // formlar klasında tanımladığımız formDoktorGirisPaneli formumuzu göstertiyoruz.
            this.Hide(); // Bu ana ekran formumuzu gizledik.
        }

        private void button1_Click(object sender, EventArgs e)// button 1 randevu al yazan butonumuz.
        {
            formlar.formGiris.Show();// formlar klasında tanımladığımız formGiris formumuzu göstertiyoruz.
            this.Hide(); // Bu ana ekran formumuzu gizledik.
        }

        private void anaEkran_FormClosing(object sender, FormClosingEventArgs e)
        { // anaEkran formumuzu kapattığımızda çalışacak kodlar.Sağ üst köşedeki çarpı butonuna basılırsa yani
            Application.ExitThread(); // Tüm gizli formları ve uygulamayı kapatma kodumuz.
        }

        private void anaEkran_Load(object sender, EventArgs e)
        { // anaEkran formu ilk açıldığında
            formlar.baglan(); // formlar klasında ki bağlan fonksiyonunu çalıştırdık.
        }

        private void button6_Click(object sender, EventArgs e)
        {// Hastanede yatanlar isimli butona tıkladığımızda
            formlar.baglan(); // formlar klasında ki bağlan fonksiyonunu çalıştırdık.
            SqlDataAdapter mda = new SqlDataAdapter("SELECT hastalar.adi, hastalar.soyadi, hastalar.DogumYeri, yatisverilenler.yatisTarihi FROM yatisverilenler INNER JOIN hastalar ON yatisverilenler.yatanTC = hastalar.TC",formlar.baglanti);
            // mysqldataadapter oluşturduk ve içerisine sql cümlemizi yazdık.
            formlar.dataGridVeri(mda, formlar.formHastanedeYatanlar.dataGridView1); // formlar tablosunda dataGridVeri isimli fonksiyonu
            // çağırdık ve o fonksiyon verdiğimiz sql cümlesi ile formlar.formHastanedeYatanlar.dataGridView1'i istediğimiz bilgilerle doldurdu..
            formlar.formHastanedeYatanlar.Show(); // formlar klasında tanımladığımız formHastanedeYatanlar formumuzu göstertiyoruz.
            this.Hide(); // Bu ana ekran formumuzu gizledik.
        }

        private void button5_Click(object sender, EventArgs e)
        {// Taburcu olan hastalar isimli buton kodları
            formlar.baglan(); // formlar klasında ki bağlan fonksiyonunu çalıştırdık.
            SqlDataAdapter mda = new SqlDataAdapter("SELECT hastalar.adi,hastalar.soyadi,hastalar.DogumYeri,taburcular.cikisTarihi FROM taburcular INNER JOIN hastalar ON taburcular.TC = hastalar.TC", formlar.baglanti);
            // mysqldataadapter oluşturduk ve içerisine sql cümlemizi yazdık.
            formlar.dataGridVeri(mda, formlar.formtaburcuOlanlar.dataGridView1); // formlar tablosunda dataGridVeri isimli fonksiyonu
            // çağırdık ve o fonksiyon verdiğimiz sql cümlesi ile formlar.formtaburcuOlanlar.dataGridView1'i istediğimiz bilgilerle doldurdu..
            formlar.formtaburcuOlanlar.Show(); // formlar klasında tanımladığımız formtaburcuOlanlar formumuzu göstertiyoruz.
            this.Hide(); // Bu ana ekran formumuzu gizledik.
        }

        private void button4_Click(object sender, EventArgs e)
        {// Nakil giden hastalar isimli buton kodları
            formlar.baglan(); // formlar klasında ki bağlan fonksiyonunu çalıştırdık.
            SqlDataAdapter mda = new SqlDataAdapter("SELECT nakiller.nakiledilenHastane, hastalar.adi, hastalar.soyadi, doktorlar.doktorAdiSoyadi FROM hastalar INNER JOIN nakiller ON nakiller.nakilTC = hastalar.TC INNER JOIN doktorlar ON nakiller.doktorID = doktorlar.doktorID", formlar.baglanti);
            // mysqldataadapter oluşturduk ve içerisine sql cümlemizi yazdık.
            formlar.dataGridVeri(mda, formlar.formNakiller.dataGridView1); // formlar tablosunda dataGridVeri isimli fonksiyonu
            // çağırdık ve o fonksiyon verdiğimiz sql cümlesi ile formlar.formNakiller.dataGridView1'i istediğimiz bilgilerle doldurdu..
            formlar.formNakiller.Show(); // formlar klasında tanımladığımız formNakiller formumuzu göstertiyoruz.
            this.Hide(); // Bu ana ekran formumuzu gizledik.
        }
    }
}
