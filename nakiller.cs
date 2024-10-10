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
    public partial class nakiller : Form
    {
        public nakiller()
        {
            InitializeComponent();
        }

        private void nakiller_FormClosing(object sender, FormClosingEventArgs e)
        {
            formlar.formAnaEkran.Show();
            this.Hide();
            e.Cancel = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            formlar.formAnaEkran.Show();
            this.Hide();
        }
    }
}
