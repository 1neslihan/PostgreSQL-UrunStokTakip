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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using TextBox = System.Windows.Forms.TextBox;
using ToolTip = System.Windows.Forms.ToolTip;

namespace PostgreSQL_UrunTakipSistemi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; " +
           "Database=dburunler; user Id=postgres; password=*****");
   
        

        private void Form2_Load(object sender, EventArgs e)
        {
            
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategoriler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comKategori.DisplayMember="kategoriad";
            comKategori.ValueMember="kategoriid";
            comKategori.DataSource= dt;
            
            ToolTip toolTip1=new ToolTip();
            toolTip1.AutoPopDelay = 8000;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.button1, "Yeni kategori eklemek için tıklayın");
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.IsBalloon= true;
            toolTip1.ToolTipTitle = "Nasıl Kategori Düzenlerim?";           
       
        }
        

        private void btnEkle_Click(object sender, EventArgs e)
        {
            
            NpgsqlCommand komut = new NpgsqlCommand("insert into urunler (urunad,stok,alisfiyat," +
                "satisfiyat,gorsel,kategori) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            
       

            if (txtAd.Text != null)
            {
                komut.Parameters.AddWithValue("@p1", txtAd.Text);

            }
            else
            {
                komut.Parameters.AddWithValue("@p1", DBNull.Value);
            }

            komut.Parameters.AddWithValue("@p2", int.Parse(numStok.Value.ToString()));

            if (string.IsNullOrEmpty(txtAlisFiyat.Text))
            {
                komut.Parameters.AddWithValue("@p3", DBNull.Value);
            }
            else
            {
                komut.Parameters.AddWithValue("@p3", double.Parse(txtAlisFiyat.Text));
            }

            if (string.IsNullOrEmpty(txtSatisFiyat.Text))
            {
                komut.Parameters.AddWithValue("@p4", DBNull.Value);
            }
            else
            {
                komut.Parameters.AddWithValue("@p4", double.Parse(txtSatisFiyat.Text));
            }

            komut.Parameters.AddWithValue("@p5", txtGorsel.Text);
            komut.Parameters.AddWithValue("@p6", int.Parse(comKategori.SelectedValue.ToString()));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            DialogResult dialogresult = MessageBox.Show("Veri ekleme başarılı.Veri eklemeye devam etmek iste misiniz?"
                   , "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogresult == DialogResult.Yes)
            {
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        (item as TextBox).Clear();
                    }
                }
                numStok.ResetText();
            }
            else if (dialogresult == DialogResult.No)
            {
                //MessageBox.Show("Silme işlemi iptal edildi");
                this.Close();
            }
            

        }
        
        
   

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm3=new Form3();
            this.Close();
            frm3.ShowDialog();
            

        }
    }
}
