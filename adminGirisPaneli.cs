using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hastane_otomasyon
{
    public partial class adminGirisPaneli : Form
    {
        public adminGirisPaneli()
        {
            InitializeComponent();
        }

        private void adminGirisPaneli_FormClosing(object sender, FormClosingEventArgs e)
        {//Giriş paneli kapatılırken yani sağ üstten çarpıya basılırsa
            e.Cancel = true;// Formun çarpıdan kapanmasını iptal ettik.
            formlar.formAnaEkran.Show();// formlar klasında tanımladığımız formAnaEkran formumuzu göstertiyoruz.
            this.Hide(); // Bu adminGirisPaneli formumuzu gizledik.
        }

        private void button1_Click(object sender, EventArgs e)
        {// Geri isimli butona tıklandığında
            formlar.formAnaEkran.Show();// formlar klasında tanımladığımız formAnaEkran formumuzu göstertiyoruz.
            this.Hide(); // Bu adminGirisPaneli formumuzu gizledik.
        }

        private void button2_Click(object sender, EventArgs e)
        {// Doktor Ekleme - Silme isimli butona tıklandığında
            formlar.formDoktorEkleme.Show();// formlar klasında tanımladığımız formDoktorEkleme formumuzu göstertiyoruz.
            this.Hide(); // Bu adminGirisPaneli formumuzu gizledik.
        }

        private void button3_Click(object sender, EventArgs e)
        {// Klinik Ekleme - Silme isimli butona tıklandığında
            formlar.formKlinikEkleme.Show();// formlar klasında tanımladığımız formKlinikEkleme formumuzu göstertiyoruz.
            this.Hide(); // Bu adminGirisPaneli formumuzu gizledik.
        }
    }
}
