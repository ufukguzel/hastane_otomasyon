using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace hastane_otomasyon
{
    class formlar
    {
        public static anaEkran formAnaEkran = new anaEkran();
        public static giris formGiris = new giris();
        public static adminGiris formAdminGiris = new adminGiris();
        public static adminGirisPaneli formAdminGirisPaneli = new adminGirisPaneli();
        public static doktorEkleme formDoktorEkleme = new doktorEkleme();
        public static doktorGirisPaneli formDoktorGirisPaneli = new doktorGirisPaneli();
        public static doktorGiris formDoktorGiris = new doktorGiris();
        public static doktorYatisVer formDoktorYatisVer = new doktorYatisVer();
        public static doktorNakilGonder formDoktorNakilGonder = new doktorNakilGonder();
        public static klinikEkleme formKlinikEkleme = new klinikEkleme();
        public static RandevuAl formRandevuAl = new RandevuAl();
        public static randevuiPtal formRandevuiPtal = new randevuiPtal();
        public static doktorYatisVer formYatisVer = new doktorYatisVer();
        public static doktorTaburcuEt formTaburcuEt = new doktorTaburcuEt();
        public static hastanedeYatanlar formHastanedeYatanlar = new hastanedeYatanlar();
        public static taburcuOlanlar formtaburcuOlanlar = new taburcuOlanlar();
        public static nakiller formNakiller = new nakiller();
        public static uyeol formUyeOl = new uyeol();

        public static SqlConnectionStringBuilder csb; // Global veritabanı değişkenlerimiz
        public static SqlDataAdapter da;
        public static SqlConnection baglanti;
        public static SqlCommand komut;
        public static DataTable dt;
        public static SqlDataReader dr;

        public static string girisYapanKisi = "";
        public static void baglan()
        {
            string baglanticumlesi = @"Data Source=DESKTOP-KU0N70B\SQLEXPRESS;Initial Catalog=hastane_otomasyonu1;Integrated Security=True";
            baglanti = new SqlConnection(baglanticumlesi);
        }

        public static void veri_getir(SqlCommand cmd) // veri getirme işlemimiz
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            baglanti.Open();
            komut = cmd;
            dr = komut.ExecuteReader();
        }

        public static void veri_ekle(SqlCommand cmd) // veri getirme işlemimiz
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            baglanti.Open();
            komut = cmd;
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public static void combo(SqlCommand cmd, string id, string ad, ComboBox cb)
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            baglanti.Open();
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.NewRow();
            dr[id] = 0;
            dr[ad] = "Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            cb.DataSource = dt;
            cb.DisplayMember = ad;
            cb.ValueMember = id;
            baglanti.Close();
        }
        public static void dataGridVeri(SqlDataAdapter mda, DataGridView dgv)
        { // datagridview verileri doldurma işlemi
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            baglanti.Open();
            da = mda;
            dt = new DataTable();
            da.Fill(dt);
            dgv.DataSource = dt;
            baglanti.Close();
        }
    }
}
