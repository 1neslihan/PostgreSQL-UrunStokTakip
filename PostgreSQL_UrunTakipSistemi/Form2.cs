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
        //Veritabanına bağlanmak için bağlantı anahtarı oluşturuldu.
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; " +
           "Database=dburunler; user Id=postgres; password=*****");
   
        

        private void Form2_Load(object sender, EventArgs e)
        {
            //burada kategorilerimizi bir comboboxta dropdown olarak göstermek için kategoriler tablosundan değerlerimizi çekiyoruz.
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategoriler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comKategori.DisplayMember="kategoriad"; //kategori adları ekranda gösterilecek şekilde ayarlandı.
            comKategori.ValueMember="kategoriid"; //kategoriidleri ise seçtiğimiz adın değeri olarak atandı.
            comKategori.DataSource= dt;
            
            //kullanıcıya butonu ne amaçla kullanacağını açıklayan bir tooltip eklendi bunu nuget managerdan kütüphane olarak eklemelisiniz.
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
        
        //veritabanına eleman eklemek için Ekle butonunun içi dolduruldu. Veriler textboxlardan,numericupdowndan ve comboboxdan alındı.
        private void btnEkle_Click(object sender, EventArgs e)
        {
            
            NpgsqlCommand veriEkle= new NpgsqlCommand("insert into urunler (urunad,stok,alisfiyat," +
                "satisfiyat,gorsel,kategori) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            
            if (txtAd.Text != null)
            {
                veriEkle.Parameters.AddWithValue("@p1", txtAd.Text);

            }
            else
            {
                veriEkle.Parameters.AddWithValue("@p1", DBNull.Value);
            }

            veriEkle.Parameters.AddWithValue("@p2", int.Parse(numStok.Value.ToString())); //bu bir numericupdown olduğu için default değeri 0 o yüzden direkt olarak eklenebilir.

            if (string.IsNullOrEmpty(txtAlisFiyat.Text))
            {
                veriEkle.Parameters.AddWithValue("@p3", DBNull.Value);
            }
            else
            {
                veriEkle.Parameters.AddWithValue("@p3", double.Parse(txtAlisFiyat.Text));
            }

            if (string.IsNullOrEmpty(txtSatisFiyat.Text))
            {
                veriEkle.Parameters.AddWithValue("@p4", DBNull.Value);
            }
            else
            {
                veriEkle.Parameters.AddWithValue("@p4", double.Parse(txtSatisFiyat.Text));
            }

            veriEkle.Parameters.AddWithValue("@p5", txtGorsel.Text);
            veriEkle.Parameters.AddWithValue("@p6", int.Parse(comKategori.SelectedValue.ToString()));
            baglanti.Open();
            veriEkle.ExecuteNonQuery();
            baglanti.Close();

            DialogResult dialogresult = MessageBox.Show("Veri ekleme başarılı.Veri eklemeye devam etmek iste misiniz?"
                   , "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogresult == DialogResult.Yes)
            {
                //eleman eklemesi yapıldıktan sonra textboxların içini temizliyoruz.
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
        
        
   
        //Yeni kategori ekleme/silme/güncelleme işlemleri için bizi ilgili forma götürecek butonun gövdesi dolduruldu.
        private void button1_Click(object sender, EventArgs e)
        {
            Form frm3=new Form3();
            this.Close();
            frm3.ShowDialog();
            

        }
    }
}
