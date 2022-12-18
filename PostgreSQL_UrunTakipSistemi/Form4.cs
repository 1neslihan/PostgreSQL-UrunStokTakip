using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSQL_UrunTakipSistemi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }
        private Form1 mainForm = null;
        public Form4(Form callingForm)
        {
            mainForm = callingForm as Form1;
            InitializeComponent();
        }

        private void btnFiltre_Click(object sender, EventArgs e)
        {
            //Form1 frm1=new Form1();
            //Form pop=new Form1();
            //pop.Hide();
            //frm1.Hide();
            //frm1.Show();
            //frm1.ShowInTaskbar=true;
            this.mainForm.urunAdı=txtUrunAd.Text.Trim().ToLower();
            this.mainForm.kategoriBilgisi=comKategori.SelectedText.Trim().ToLower();
            this.mainForm.alisFiyatBaslangic=Convert.ToInt32(txtAlisBaslangic.Text.Trim());
            this.mainForm.alisFiyatBitis=Convert.ToInt32(txtAlisBitis.Text.Trim());
            this.mainForm.satisFiyatBaslangic=Convert.ToInt32(txtSatisBaslangic.Text.Trim());
            this.mainForm.satisFiyatBitis=Convert.ToInt32(txtSatisBitis.Text.Trim());
            mainForm.filtre();

        }
    }
}
