using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSQL_UrunTakipSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
       
        }
        //private Form4 filtreleForm = null;
        //public Form1(Form callingForm)
        //{
        //    filtreleForm = callingForm as Form4;
        //    InitializeComponent();
        //}

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; " +
            "Database=dburunler; user Id=postgres; password=*****");
        
        
        private void Form1_Load(object sender, EventArgs e)
        {
          
           btnListele_Click(sender, e);

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select urunler.urunid,urunler.urunad As Urun_Ad, urunler.stok, urunler.alisfiyat As Alis_Fiyat," +
                "urunler.satisfiyat As Satis_Fiyat,urunler.gorsel As Gorsel,urunler.kategori As kategori_kod,kategoriler.kategoriad from urunler " +
                "inner join kategoriler " +
                "on " +
                "urunler.kategori=kategoriler.kategoriid " +
                "order by urunid";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource= ds.Tables[0];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns["kategoriad"].ReadOnly=true;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            string sorgu2 = "select kategoriid As Kategori_Kod,kategoriad from kategoriler order by kategoriid";
            NpgsqlDataAdapter da2=new NpgsqlDataAdapter(sorgu2, baglanti);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.DataSource= ds2.Tables[0];
            dataGridView2.Columns["Kategori_Kod"].ReadOnly=true;
            dataGridView2.Columns["kategoriad"].ReadOnly=true;
            dataGridView2.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            string sorgu3 = "select * from silinenurunler";
            NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sorgu3, baglanti);
            DataSet ds3 = new DataSet();
            da3.Fill(ds3);
            dataGridView3.DataSource=ds3.Tables[0];
            dataGridView3.Columns[6].Visible = false;
            dataGridView3.RowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

         
            

        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            Form2 frm= new Form2();
            frm.ShowDialog();
            btnListele_Click(sender,e);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            void KayıtSil(int selectedId)
            {
                NpgsqlCommand komut3 = new NpgsqlCommand("delete from urunler where urunid=@p1", baglanti);
                komut3.Parameters.AddWithValue("@p1", selectedId);
                baglanti.Open();
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }
            void KayıtEkle(string selectedAd, int selectedStok, int selectedAlisFiyat, int selectedSatisFiyat,
                string selectedGorsel, int selectedKategori)
            {
                NpgsqlCommand komut2 = new NpgsqlCommand("insert into silinenUrunler (" +
                    "silinenurunad," +
                    "silinenstok," +
                    "silinenalisfiyat," +
                    "silinensatisfiyat," +
                    "silinengorsel," +
                    "silinenkategori ) " +
                    "values (" +
                    "@p1," +
                    "@p2," +
                    "@p3," +
                    "@p4," +
                    "@p5," +
                    "@p6 )", baglanti);
                
                komut2.Parameters.AddWithValue("@p1", selectedAd);
                komut2.Parameters.AddWithValue("@p2", selectedStok);
            
                if (selectedAlisFiyat==-1)
                {
                    komut2.Parameters.AddWithValue("@p3", DBNull.Value);
                }
                else
                {
                    komut2.Parameters.AddWithValue("@p3", selectedAlisFiyat);
                }

                if (selectedSatisFiyat==-1)
                {
                    komut2.Parameters.AddWithValue("@p4", DBNull.Value);
                }
                else
                {
                    komut2.Parameters.AddWithValue("@p4", selectedSatisFiyat);
                }

                komut2.Parameters.AddWithValue("@p5", selectedGorsel);
                komut2.Parameters.AddWithValue("@p6", selectedKategori);
                baglanti.Open();
                komut2.ExecuteNonQuery();
                baglanti.Close();
            }
            
            int selectedRowCount =dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {

                DialogResult dialogresult = MessageBox.Show("ürünü silmek istediğinize emin misiniz?"
                    , "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (dialogresult == DialogResult.Yes)
                {
                    
                    foreach (DataGridViewRow del in dataGridView1.SelectedRows)  //Seçili Satırları Silme ve Yeni Tabloya ekleme
                    {
                        int selectedId = Convert.ToInt32(del.Cells[0].Value);
                        string selectedAd = Convert.ToString(del.Cells[1].Value);
                        int selectedStok = Convert.ToInt32(del.Cells[2].Value);     
                        int selectedAlisFiyat;// int selectedAlisFiyat = Convert.ToInt32(drow.Cells[3].Value);
                        if (del.Cells[3].Value==DBNull.Value)
                        {
                            selectedAlisFiyat=-1;
                        }
                        else
                        {
                            selectedAlisFiyat=Convert.ToInt32(del.Cells[3].Value);
                        }

                        int selectedSatisFiyat;//int selectedSatisFiyat = Convert.ToInt32(drow.Cells[4].Value);
                        if(del.Cells[4].Value== DBNull.Value)
                        {
                            selectedSatisFiyat=-1;
                        }
                        else
                        {
                            selectedSatisFiyat = Convert.ToInt32(del.Cells[4].Value);
                        }
                     
                        
                        string selectedGorsel = Convert.ToString(del.Cells[5].Value);
                        int selectedKategori = Convert.ToInt32(del.Cells[6].Value);

                        KayıtEkle(selectedAd, selectedStok, selectedAlisFiyat, selectedSatisFiyat, selectedGorsel, selectedKategori);
                        KayıtSil(selectedId);
                    }

                    MessageBox.Show("Ürün silme başarılı");
                    btnListele_Click(sender, e);
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

            foreach (DataGridViewRow upt in dataGridView1.SelectedRows)
            {
                NpgsqlCommand komut4 = new NpgsqlCommand("Update urunler set urunad=@p1, stok=@p2, alisfiyat=@p3," +
                    "satisfiyat=@p4, gorsel=@p5, kategori=@p6 where urunid=@p7",baglanti);

                if (upt.Cells[1].Value !=null)
                {
                    komut4.Parameters.AddWithValue("@p1", upt.Cells[1].Value);
                }
                else
                {
                    komut4.Parameters.AddWithValue("@p1",DBNull.Value);
                }

                if (upt.Cells[2].Value !=null)
                {
                    komut4.Parameters.AddWithValue("@p2", upt.Cells[2].Value);
                }
                else
                {
                    komut4.Parameters.AddWithValue("@p2", DBNull.Value);
                }

                if (upt.Cells[3].Value !=null)
                {
                    komut4.Parameters.AddWithValue("@p3", upt.Cells[3].Value);
                }
                else
                {
                    komut4.Parameters.AddWithValue("@p3", DBNull.Value);
                }

                if (upt.Cells[4].Value !=null)
                {
                    komut4.Parameters.AddWithValue("@p4", upt.Cells[4].Value);
                }
                else
                {
                    komut4.Parameters.AddWithValue("@p4", DBNull.Value);
                }

                if (upt.Cells[5].Value !=null)
                {
                    komut4.Parameters.AddWithValue("@p5", upt.Cells[5].Value);
                }
                else
                {
                    komut4.Parameters.AddWithValue("@p5", DBNull.Value);
                }

                if (upt.Cells[6].Value !=null)
                {
                    komut4.Parameters.AddWithValue("@p6", upt.Cells[6].Value);
                }
                else
                {
                    komut4.Parameters.AddWithValue("@p6", DBNull.Value);
                }

                if (upt.Cells[0].Value !=null)
                {
                    komut4.Parameters.AddWithValue("@p7", upt.Cells[0].Value);
                }
                else
                {
                    komut4.Parameters.AddWithValue("@p7", DBNull.Value);
                }
                baglanti.Open();
                komut4.ExecuteNonQuery();
                baglanti.Close();
            }
            
            MessageBox.Show("işlem başarılı");
            btnListele_Click(sender, e);
           
        }

        

       

        private void btnCopBosalt_Click(object sender, EventArgs e)
        {
            DialogResult dialogresult = MessageBox.Show("Çöp kutusunu boşaltmak istediğinize emin misiniz? Bu işlem geri alınamaz!"
                    , "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (dialogresult==DialogResult.Yes)
            {
                NpgsqlCommand copbosalt = new NpgsqlCommand("truncate table silinenurunler restart identity",baglanti);
                baglanti.Open();
                copbosalt.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Çöp kutusu boşaltıldı.");
                btnListele_Click(sender, e);
            }
            else if (dialogresult==DialogResult.No)
            {
                MessageBox.Show("Silme işlemi iptal edildi.");
            }
            

        }

        private void btnKaliciSilme_Click(object sender, EventArgs e)
        {
            void KayıtSil(int selectedId)
            {
                NpgsqlCommand komut4 = new NpgsqlCommand("delete from silinenurunler where silinenurunid=@p1", baglanti);
                komut4.Parameters.AddWithValue("@p1", selectedId);
                baglanti.Open();
                komut4.ExecuteNonQuery();
                baglanti.Close();
            }
            int selectedRowCount = dataGridView3.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                DialogResult dialogresult = MessageBox.Show("ürünü silmek istediğinize emin misiniz?" +
                    "Bu işlem geri alınamaz."
                    , "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (dialogresult==DialogResult.Yes)
                {
                    foreach (DataGridViewRow harddel in dataGridView3.SelectedRows)  //Seçili Satırları Silme ve Yeni Tabloya ekleme
                    {
                        int selectedId = Convert.ToInt32(harddel.Cells[6].Value);
                        KayıtSil(selectedId);

                    }
                    MessageBox.Show("Silme işlemi başarılı");
                    btnListele_Click(sender, e);
                }
                else if (dialogresult==DialogResult.No)
                {
                    MessageBox.Show("Silme işlemi iptal edildi");
                }

            }

            else
            {
                MessageBox.Show("Silinecek satır/satırlar seçiniz!");
            }

        }

        private void btnGeriYukle_Click(object sender, EventArgs e)
        {
            void KayıtSil(int selectedId)
            {
                NpgsqlCommand komut6 = new NpgsqlCommand("delete from silinenurunler where silinenurunid=@p1", baglanti);
                komut6.Parameters.AddWithValue("@p1", selectedId);
                baglanti.Open();
                komut6.ExecuteNonQuery();
                baglanti.Close();
            }
            void KayıtEkle(string selectedAd, int selectedStok, int selectedAlisFiyat, int selectedSatisFiyat,
                string selectedGorsel, int selectedKategori)
            {
                NpgsqlCommand komut5 = new NpgsqlCommand("insert into urunler (" +
                    "urunad," +
                    "stok," +
                    "alisfiyat," +
                    "satisfiyat," +
                    "gorsel," +
                    "kategori ) " +
                    "values (" +
                    "@p1," +
                    "@p2," +
                    "@p3," +
                    "@p4," +
                    "@p5," +
                    "@p6)", baglanti);

                komut5.Parameters.AddWithValue("@p1", selectedAd);
                komut5.Parameters.AddWithValue("@p2", selectedStok);

                if (selectedAlisFiyat==-1)
                {
                    komut5.Parameters.AddWithValue("@p3", DBNull.Value);
                }
                else
                {
                    komut5.Parameters.AddWithValue("@p3", selectedAlisFiyat);
                }

                if (selectedSatisFiyat==-1)
                {
                    komut5.Parameters.AddWithValue("@p4", DBNull.Value);
                }
                else
                {
                    komut5.Parameters.AddWithValue("@p4", selectedSatisFiyat);
                }

                komut5.Parameters.AddWithValue("@p5", selectedGorsel);
                komut5.Parameters.AddWithValue("@p6", selectedKategori);

                baglanti.Open();
                komut5.ExecuteNonQuery(); 
                baglanti.Close();
            }

            int kategoriKarsilastir(int selectedKategori)
            {
                NpgsqlCommand karsilastir = new NpgsqlCommand("select silinenurunad from silinenurunler s " +
                        "where exists(select 1 from kategoriler k where k.kategoriid=s.silinenkategori and silinenkategori=@d)", baglanti);
                karsilastir.Parameters.AddWithValue("@d", selectedKategori);
                baglanti.Open();

                karsilastir.ExecuteScalar();
                if ((string)karsilastir.ExecuteScalar()==null)
                    selectedKategori=20;
                baglanti.Close();
                return selectedKategori;
            }

            int selectedRowCount = dataGridView3.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                foreach (DataGridViewRow restore in dataGridView3.SelectedRows)  //Seçili Satırları ana tabloya geri ekleme
                {
                    int selectedId = Convert.ToInt32(restore.Cells[6].Value);
                    string selectedAd = Convert.ToString(restore.Cells[0].Value);
                    int selectedStok = Convert.ToInt32(restore.Cells[1].Value);
                    int selectedAlisFiyat;  
                    if (restore.Cells[2].Value==DBNull.Value)
                    {
                        selectedAlisFiyat=-1;
                    }
                    else
                    {
                        selectedAlisFiyat=Convert.ToInt32(restore.Cells[2].Value);
                    }

                    int selectedSatisFiyat;  
                    if (restore.Cells[3].Value== DBNull.Value)
                    {
                        selectedSatisFiyat=-1;
                    }
                    else
                    {
                        selectedSatisFiyat = Convert.ToInt32(restore.Cells[3].Value);
                    }


                    string selectedGorsel = Convert.ToString(restore.Cells[4].Value);

                    
                    int selectedKategori = Convert.ToInt32(restore.Cells[5].Value);
                    
                    selectedKategori=kategoriKarsilastir(selectedKategori);
                    KayıtEkle(selectedAd,selectedStok,selectedAlisFiyat,selectedSatisFiyat,selectedGorsel,selectedKategori);
                    KayıtSil(selectedId);
                      
                    
                }
                MessageBox.Show("Geri yükleme tamamlandı.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnListele_Click(sender, e);
            }
            else if(selectedRowCount==0)
            {
                MessageBox.Show("Geri yüklemek istediğiniz satır veya satırları seçiniz.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }


        }

        public string alisFiyatBaslangic { get; set; }
        public string alisFiyatBitis { get; set; }
        public string satisFiyatBaslangic { get; set; }
        public string satisFiyatBitis { get; set; }
        public string kategoriBilgisi { get; set; }
        public string urunAdı { get; set; }
        public bool secili { get; set; }
        

      
        public void filtre()
        {
           
            string sorgu = "select urunler.urunid,urunler.urunad As Urun_Ad, urunler.stok, urunler.alisfiyat As Alis_Fiyat," +
                "urunler.satisfiyat As Satis_Fiyat,urunler.gorsel As Gorsel,urunler.kategori As kategori_kod,kategoriler.kategoriad from urunler " +
                "inner join kategoriler " +
                "on " +
                "urunler.kategori=kategoriler.kategoriid";
            int sayac = 0;
            if(urunAdı !=string.Empty)
            {
                sayac++;
                if (sayac==1)
                {
                    sorgu=sorgu+" where";
                }
                sorgu =sorgu+" urunler.urunad=@p1";

            }

            if(alisFiyatBaslangic !=string.Empty)
            {
                sayac++;
                if (sayac==1)
                {
                    sorgu=sorgu+" where";
                }
                if (sayac>1)
                {
                    sorgu=sorgu+" and";
                }
                sorgu=sorgu+" urunler.alisfiyat>=@p2";

            }

            if(alisFiyatBitis !=string.Empty)
            {
                sayac++;
                if (sayac==1)
                {
                    sorgu=sorgu+" where";
                }
                if (sayac>1)
                {
                    sorgu=sorgu+" and";
                }

                sorgu=sorgu+" urunler.alisfiyat<=@p3";
            }

            if (satisFiyatBaslangic !=string.Empty)
            {
                sayac++;
                if (sayac==1)
                {
                    sorgu=sorgu+" where";
                }
                if (sayac>1)
                {
                    sorgu=sorgu+" and";
                }
                sorgu=sorgu+" urunler.satisfiyat>=@p4";

            }

            if (satisFiyatBitis !=string.Empty)
            {
                sayac++;
                if (sayac==1)
                {
                    sorgu=sorgu+" where";
                }
                if (sayac>1)
                {
                    sorgu=sorgu+" and";
                }
                sorgu=sorgu+" urunler.satisfiyat>=@p5";

            }

            if (kategoriBilgisi !="-1")
            {
                sayac++;
                if (sayac==1)
                {
                    sorgu=sorgu+" where";
                }
                if (sayac>1)
                {
                    sorgu=sorgu+" and";
                }
                sorgu=sorgu+" urunler.kategori=@p6";
            }

            if(secili)
            {
                sayac++;
                if (sayac==1)
                {
                    sorgu=sorgu+" where";
                }
                if (sayac>1)
                {
                    sorgu=sorgu+" and";
                }
                sorgu=sorgu+" urunler.stok> @p7";
            }

            sorgu=sorgu+ " order by urunler.urunad asc";


            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            da.SelectCommand.Parameters.AddWithValue("@p1", urunAdı);

            if (alisFiyatBaslangic!=string.Empty)
            {
                da.SelectCommand.Parameters.AddWithValue("@p2", int.Parse(alisFiyatBaslangic));
            }

            if (alisFiyatBitis!=string.Empty) 
            { 
                da.SelectCommand.Parameters.AddWithValue("@p3", int.Parse(alisFiyatBitis));
            }
           
            if(satisFiyatBaslangic!=string.Empty)
            {
                da.SelectCommand.Parameters.AddWithValue("@p4", int.Parse(satisFiyatBaslangic));
            }
            
            if(satisFiyatBitis!=string.Empty)
            {
                da.SelectCommand.Parameters.AddWithValue("@p5", int.Parse(satisFiyatBitis));
            }

            if (kategoriBilgisi!="-1")
            {
                da.SelectCommand.Parameters.AddWithValue("@p6", int.Parse(kategoriBilgisi));
            }
            if (secili)
            {
                da.SelectCommand.Parameters.AddWithValue("@p7", 0);
            }
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource= ds.Tables[0];

            
            
        }
        

        private void btnFiltre_Click(object sender, EventArgs e)
        {

          Form frm4=new Form4(this);
            frm4.Show();
           

        }
        
    }
}
