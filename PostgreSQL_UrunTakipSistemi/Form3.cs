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

namespace PostgreSQL_UrunTakipSistemi
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            FormClosing +=Form3_FormClosing;
        }
         //bu form kapatıldıktan sonra form2nin yani eleman ekleme formunun yeniden ekrana gelmesi için oluşturulmuş bir çağrı.
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form pop = new Form2();
            pop.ShowDialog();
        }
        //Veritabanına bağlanmak için bağlantı anahtarı oluşturuldu.
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; " +
           "Database=dburunler; user Id=postgres; password=*****");

        
        private void Form3_Load(object sender, EventArgs e)
        {

            Listele();
        }

        //silme/güncelleme/ekleme işlemlerinden sonra güncel tabloyu listelemek için.
        //ayrıca form loadda çağrılıyor yani form açılırken kategoriler tablosunun ögeleri datagridview üstünde gösteriliyor.
        private void Listele()
        {
            string sorgu = "select * from kategoriler order by kategoriid";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource= ds.Tables[0];
            dataGridView1.Columns["kategoriid"].ReadOnly=true; //kategoriid otomatik arttırıldığı için kullanıcının onun üstünde işlem yapmasını istemiyorum. Bu sebeple sutun sadece readonly.
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            
            
        }
        
        //kategori ekleme işlemi textbox üzerinden yapılıyor.Bunun için Ekle butonu dolduruldu.
        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand veriEkle = new NpgsqlCommand("insert into kategoriler (kategoriad) values (@p1)", baglanti);
            
            if (txtKategoriAd.Text =="")
            {
                MessageBox.Show("Kategori adı boş olamaz!");
            }
            else
            {
                veriEkle.Parameters.AddWithValue("@p1", txtKategoriAd.Text);
                veriEkle.ExecuteNonQuery();
            }
            
            baglanti.Close();
            txtKategoriAd.Clear();
            Listele();          
           
        }
  
        
        //Silme işlemlerini yapan fonksiyonlar tanımlandı ayrıca idsi 20 olan bilinmeyen kategorinin silinmesi engellendi bu sayede programın istikrarı sağlandı.
        private void btnSil_Click(object sender, EventArgs e)
        {
            void KategoriSil(int selectedId)
            {
                NpgsqlCommand kategoriSil = new NpgsqlCommand("delete from kategoriler where kategoriid=@p1", baglanti);
                kategoriSil.Parameters.AddWithValue("@p1", selectedId);
                baglanti.Open();
                kategoriSil.ExecuteNonQuery();
                baglanti.Close();
            }
            void KategoriUpdate(int selectedId)
            {
                NpgsqlCommand guncelle = new NpgsqlCommand("update urunler set kategori=20 where kategori=@p1", baglanti);

                guncelle.Parameters.AddWithValue("@p1", selectedId);
               
                baglanti.Open();
                guncelle.ExecuteNonQuery();
                baglanti.Close();
            }

            int selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {

                DialogResult dialogresult = MessageBox.Show("kategoriyi silmek istediğinize emin misiniz?"
                    , "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (dialogresult == DialogResult.Yes)
                {

                    foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  
                    {
                        int selectedId= Convert.ToInt32(drow.Cells[0].Value);
                        if (selectedId != 20)
                        {
                            KategoriUpdate(selectedId);
                            KategoriSil(selectedId);
                        }
                        else
                        {
                            MessageBox.Show("Bilinmeyen kategorisi silinemez!");
                        }  
                        
                    }  
                    Listele();
                }

                else if (dialogresult == DialogResult.No)
                {
                    MessageBox.Show("Silme işlemi iptal edildi");
                }

            }
            else if (selectedRowCount<=0)
            {
                MessageBox.Show("Lütfen tablodan silinecek elemanı/elemanları seçin");
            }


        }
         // Burada diğer kısımlardan farklı olarak çoklu güncelleme özelliği koyulmadı. Kategori güncellenirken tek tek güncelleme yapılabilir.
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand kategoriadDBGuncelle = new NpgsqlCommand("update kategoriler set kategoriad=@p1 where kategoriid=@p2", baglanti);
            
            foreach (DataGridViewRow upt3 in dataGridView1.SelectedRows)
            {   
                int id = Convert.ToInt32(upt3.Cells[0].Value);
                if (id !=20)
                {
                    if (upt3.Cells[0].Value != null)
                    {
                        kategoriadDBGuncelle.Parameters.AddWithValue("@p2", upt3.Cells[0].Value);
                    }
                    else
                    {
                        MessageBox.Show("Hatalı seçim", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                
                    if(upt3.Cells[1].Value != null)
                    {
                        kategoriadDBGuncelle.Parameters.AddWithValue("@p1", upt3.Cells[1].Value);
                    }
                    else
                    {
                        MessageBox.Show("Kategori ismi boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                    baglanti.Open();
                    kategoriadDBGuncelle.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Güncelleme Başarılı");
                    Listele();

                }
                else
                {
                    MessageBox.Show("Bilinmeyen Kategorisine müdahale edilemez!");
                    Listele();
                }

                
                
            }
        }
    }
}
