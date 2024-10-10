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
    public partial class randevuiPtal : Form
    {
        public randevuiPtal()
        {
            InitializeComponent();
        }

        private void randevuiPtal_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            formlar.formRandevuAl.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        { // randevuyu iptal et butonu
            try
            {
                SqlCommand c = new SqlCommand("DELETE from randevular where randevuID=@rid", formlar.baglanti);
                c.Parameters.AddWithValue("@rid", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                formlar.veri_ekle(c);
                MessageBox.Show("Seçlen randevu başarıyla silindi.");
            }
            catch (Exception)
            {
                MessageBox.Show("Randevu zaten silindi veya randevu seçmediniz.");
            }
            SqlDataAdapter mda = new SqlDataAdapter("SELECT randevular.randevuID,klinikler.klinikAdi, randevular.tarih, randevular.saat, hastalar.TC, hastalar.adi, hastalar.soyadi FROM randevular INNER JOIN hastalar ON randevular.TC = hastalar.TC INNER JOIN klinikler ON randevular.klinikID = klinikler.klinikID where hastalar.TC=@tc", formlar.baglanti);
            mda.SelectCommand.Parameters.AddWithValue("@tc", formlar.formRandevuAl.textBox1.Text);
            formlar.dataGridVeri(mda, formlar.formRandevuiPtal.dataGridView1); // formlar tablosunda dataGridVeri isimli fonksiyonu
        }

        private void button22_Click(object sender, EventArgs e)
        {
            formlar.formRandevuAl.Show();
            this.Hide();
        }
    }
}
