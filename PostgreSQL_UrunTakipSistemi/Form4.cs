using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PostgreSQL_UrunTakipSistemi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        //Veritabanına bağlanmak için bağlantı anahtarı oluşturuldu.
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; " +
            "Database=dburunler; user Id=postgres; password=*****");

        //Kategoriler form açıldığında listelenmesi için bilgiler tablodan comboboxa çekiliyor.
        private void Form4_Load(object sender, EventArgs e)
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategoriler order by kategoriid", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comKategori.DisplayMember="kategoriad";
            comKategori.ValueMember="kategoriid";
            comKategori.DataSource= dt;
            comKategori.SelectedIndex=-1; //otomatik olarak hiçbir elemanın seçili olmaması için -1 yapıldı.
        }

        //Bu form açıldığı zaman her filtrele butonuna bastığımızda yeni bir form1 açmasını önlemek için tanımlanan değişken ve fonksiyon.
        private Form1 mainForm = null;
        public Form4(Form callingForm)
        {
            mainForm = callingForm as Form1;
            InitializeComponent();
        }

        //form4 de textbox, combobox ve checkboxdaki verileri form1'e gönderen fonksiyon.
        private void btnFiltre_Click(object sender, EventArgs e)
        {
            this.mainForm.urunAdı=txtUrunAd.Text.Trim();
            if(comKategori.SelectedValue==null)
            {
                this.mainForm.kategoriBilgisi="-1";
            }
            else
            {
                this.mainForm.kategoriBilgisi=comKategori.SelectedValue.ToString();
            } 
            this.mainForm.alisFiyatBaslangic=txtAlisBaslangic.Text.Trim();
            this.mainForm.alisFiyatBitis=txtAlisBitis.Text.Trim();
            this.mainForm.satisFiyatBaslangic=txtSatisBaslangic.Text.Trim();
            this.mainForm.satisFiyatBitis=txtSatisBitis.Text.Trim();
            if(chbox1.Checked) 
            { 
                this.mainForm.secili=true;
            }
            else
                this.mainForm.secili= false;
            
            mainForm.filtre(); //form1deki listeyi güncellemek için form1deki fonksiyon çağırıldı.
            this.Dispose(true);//form4ü kapatıyor.

        }
    }
}
