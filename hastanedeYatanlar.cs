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
    public partial class hastanedeYatanlar : Form
    {
        public hastanedeYatanlar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            formlar.formAnaEkran.Show();
        }

        private void hastanedeYatanlar_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            formlar.formAnaEkran.Show();
            e.Cancel = true;
        }

        private void hastanedeYatanlar_Load(object sender, EventArgs e)
        {

        }
    }
}
