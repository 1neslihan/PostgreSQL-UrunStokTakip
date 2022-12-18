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

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form pop = new Form2();
            pop.ShowDialog();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; " +
           "Database=dburunler; user Id=postgres; password=*****");

        private void Form3_Load(object sender, EventArgs e)
        {
            //baglanti.Open();
            //NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategoriler", baglanti);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //baglanti.Close();
            string sorgu = "select * from kategoriler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource= ds.Tables[0];
            dataGridView1.Columns["kategoriid"].ReadOnly=true;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            

        }

        
        private void Listele()
        {
            string sorgu = "select * from kategoriler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource= ds.Tables[0];
            dataGridView1.Columns["kategoriid"].ReadOnly=true;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            
            
        }
        
        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into kategoriler (kategoriad) values (@p1)", baglanti);
            
            if (txtKategoriAd.Text =="")
            {
                MessageBox.Show("Kategori adı boş olamaz!");
            }
            else
            {
                komut.Parameters.AddWithValue("@p1", txtKategoriAd.Text);
                komut.ExecuteNonQuery();
            }
            
            baglanti.Close();
            txtKategoriAd.Clear();
            Listele();          
           
        }
  
        

        private void btnSil_Click(object sender, EventArgs e)
        {
            void KategoriSil(int selectedId)
            {
                NpgsqlCommand komut3 = new NpgsqlCommand("delete from kategoriler where kategoriid=@p1", baglanti);
                komut3.Parameters.AddWithValue("@p1", selectedId);
                baglanti.Open();
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }
            void KategoriUpdate(int selectedId)
            {
                NpgsqlCommand komut2 = new NpgsqlCommand("update urunler set kategori=20 where kategori=@p1", baglanti);

                komut2.Parameters.AddWithValue("@p1", selectedId);
               
                baglanti.Open();
                komut2.ExecuteNonQuery();
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

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand kategoriadDBGuncelle = new NpgsqlCommand("update kategoriler set kategoriad=@p1 where kategoriid=@p2", baglanti);
           
            foreach(DataGridViewRow upt3 in dataGridView1.SelectedRows)
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
                Listele();
            }
        }
    }
}
