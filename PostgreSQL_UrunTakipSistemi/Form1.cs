using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
        //Datagridviewlarımızın tümünde property üzerinden SelectMode=FullRowSelect,AutoSizeColumnsMode=Fill olarak seçildi.
        
        //Veri tabanına bağlanmak için bağlanti anahtarı oluşturuldu.
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; " +
            "Database=dburunler; user Id=postgres; password=*****");
        
        //Program başlangıcında çalışmasını istediğimiz ögeleri load event'a atabiliriz. 
        //Ben listele butonunun içindeki kodları pek çok yerde kullandığım için diretk olarak load eventda onu çağırdım.
        private void Form1_Load(object sender, EventArgs e)
        {
          
           btnListele_Click(sender, e);

        }

        //Veri tabanındaki tabloları datagridviewler üzerinde göstermek için Listele butonunun click eventını dolduruyorum.
        private void btnListele_Click(object sender, EventArgs e)
        {
            //urunler isimli tablodan urunlerinid'si,adı,stok miktarı,alış ve satış fiyatları varsa resimlerinin yolları,
            //kategori kodları ve ilişkili olduğu kategoriler tablosundanda kategori adlarını listeliyorum.
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
            dataGridView1.Columns[0].Visible = false; //urunid bloğunun datagridviewda görünmesini istemediğim için false yaptım.
            dataGridView1.Columns["kategoriad"].ReadOnly=true; //Ayrıca güncelleme yaparken kategoriad bloğunun değiştirilmemesi için bu blok sadece readonly olarak ayarlandı.
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque; //Biraz görsellik eklemek için satırların arka planını renklendirdim.
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;


            //kategoriler isimli tablodan kategoriid ve kategoriad alanlarını listeliyorum.
            string sorgu2 = "select kategoriid As Kategori_Kod,kategoriad from kategoriler order by kategoriid";
            NpgsqlDataAdapter da2=new NpgsqlDataAdapter(sorgu2, baglanti);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.DataSource= ds2.Tables[0];
            dataGridView2.Columns["Kategori_Kod"].ReadOnly=true; //Kategori güncelleme işlemleri buradaki datagridview üzerinden yapılmayacağı için satırları readonly olarak ayarladım.
            dataGridView2.Columns["kategoriad"].ReadOnly=true;
            dataGridView2.RowsDefaultCellStyle.BackColor = Color.Bisque; 
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;


            //silinen urunler adlı tablodan listeleme yapıyoruz. Bu benim çöp kutusu listem.
            string sorgu3 = "select * from silinenurunler";
            NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sorgu3, baglanti);
            DataSet ds3 = new DataSet();
            da3.Fill(ds3);
            dataGridView3.DataSource=ds3.Tables[0];
            dataGridView3.Columns[6].Visible = false;
            dataGridView3.RowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
        

        }

        //Yeni eleman eklemek için Ekle butonunun içi doldurulacak bu işlem yeni bir formda yapılıyor o yüzden burda form çağırıldı.
        private void btnEkle_Click(object sender, EventArgs e)
        {
            Form2 frm= new Form2();
            frm.ShowDialog();
            btnListele_Click(sender,e);
        }

        //Silme işlemlerimiz için Sil butonunun içi dolduruluyor.
        private void btnSil_Click(object sender, EventArgs e)
        {
            //Silinecek ögeler urunidlerine göre belirleniyor bunuda kayıtSil isimli fonksiyonumuzla gerçekleştiriyorum.
            void KayıtSil(int selectedId)
            {
                NpgsqlCommand sil = new NpgsqlCommand("delete from urunler where urunid=@p1", baglanti);
                sil.Parameters.AddWithValue("@p1", selectedId);
                baglanti.Open();
                sil.ExecuteNonQuery();
                baglanti.Close();
            }

            //Kayıtlarımız silinmeden önce birer kopyaları çöp kutusuna taşınıyor böylece yanlışlıkla sildiğimiz ürünleri geri getirebiliriz.
            void KayıtEkle(string selectedAd, int selectedStok, int selectedAlisFiyat, int selectedSatisFiyat,
                string selectedGorsel, int selectedKategori)
            {
                NpgsqlCommand copEkle = new NpgsqlCommand("insert into silinenUrunler (" +
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
                
                copEkle.Parameters.AddWithValue("@p1", selectedAd);

                if (selectedStok==-1)
                {
                    copEkle.Parameters.AddWithValue("@p2", DBNull.Value);
                }
                else
                {
                    copEkle.Parameters.AddWithValue("@p2", selectedStok);
                }
                
            
                if (selectedAlisFiyat==-1)
                {
                    copEkle.Parameters.AddWithValue("@p3", DBNull.Value);
                }
                else
                {
                    copEkle.Parameters.AddWithValue("@p3", selectedAlisFiyat);
                }

                if (selectedSatisFiyat==-1)
                {
                    copEkle.Parameters.AddWithValue("@p4", DBNull.Value);
                }
                else
                {
                    copEkle.Parameters.AddWithValue("@p4", selectedSatisFiyat);
                }

                copEkle.Parameters.AddWithValue("@p5", selectedGorsel);
                copEkle.Parameters.AddWithValue("@p6", selectedKategori);
                baglanti.Open();
                copEkle.ExecuteNonQuery();
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
                        int selectedStok;
                        if (del.Cells[2].Value==DBNull.Value) //int ögeler null değer alamayacağı ilgili alanın veri tabanında null olduğu durumlar için -1 ataması yapıyorum. 
                        {                                     //benim verilerin negatif değer alamayacağından fonksiyonda -1 tespit edilen yer veri tabanında null olacak şekilde dolduruluyor.
                            selectedStok=-1;
                        }
                        else
                        {
                            selectedStok=Convert.ToInt32(del.Cells[2].Value);
                        }
                             
                        int selectedAlisFiyat;
                        if (del.Cells[3].Value==DBNull.Value)
                        {
                            selectedAlisFiyat=-1;
                        }
                        else
                        {
                            selectedAlisFiyat=Convert.ToInt32(del.Cells[3].Value);
                        }

                        int selectedSatisFiyat;
                        if(del.Cells[4].Value== DBNull.Value)
                        {
                            selectedSatisFiyat=-1;
                        }
                        else
                        {
                            selectedSatisFiyat = Convert.ToInt32(del.Cells[4].Value);
                        }
                     
                        
                        string selectedGorsel = Convert.ToString(del.Cells[5].Value);
                        int selectedKategori;
                        if (del.Cells[6].Value==DBNull.Value)
                        {
                            selectedKategori=20;
                        }
                        else
                        {
                            selectedKategori=Convert.ToInt32(del.Cells[6].Value);
                        }
                        

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

        //verileri güncellemek için Guncelle butonunun içi dolduruluyor.
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Kayıtları fonkyion yardımıyla güncelliyorum.
            void kayitGuncelle(int selectedId,string selectedUrunad,int selectedStok,
                int selectedAlisFiyat,int selectedSatisFiyat,string selectedGorsel,int selectedKategori)
            {
                NpgsqlCommand guncelle = new NpgsqlCommand("Update urunler set urunad=@p1, stok=@p2, alisfiyat=@p3," +
                    "satisfiyat=@p4, gorsel=@p5, kategori=@p6 where urunid=@p7",baglanti);

                guncelle.Parameters.AddWithValue("@p7", selectedId);
                guncelle.Parameters.AddWithValue("@p1", selectedUrunad);

                if (selectedStok==-1)
                {
                    guncelle.Parameters.AddWithValue("@p2", DBNull.Value);
                }
                else
                {
                    guncelle.Parameters.AddWithValue("@p2", selectedStok);
                }

                if (selectedAlisFiyat==-1)
                {
                    guncelle.Parameters.AddWithValue("@p3", DBNull.Value);
                }
                else
                {
                    guncelle.Parameters.AddWithValue("@p3", selectedAlisFiyat);
                }

                if (selectedSatisFiyat==-1)
                {
                    guncelle.Parameters.AddWithValue("@p4", DBNull.Value);
                }
                else
                {
                    guncelle.Parameters.AddWithValue("@p4", selectedSatisFiyat);
                }

                guncelle.Parameters.AddWithValue("@p5", selectedGorsel);
                guncelle.Parameters.AddWithValue("@p6", selectedKategori);

                baglanti.Open();
                //burada try-catch kullanma sebebim kullanıcı kategoriler tablosunda olmayan bir kategori girişi yapmaya çalışırsa sistemin onu durdurmasını sağlamak için.
                try
                {
                    guncelle.ExecuteNonQuery();
                    MessageBox.Show("işlem başarılı");
                }
                catch(NpgsqlException)
                {
                    MessageBox.Show("Geçersiz kategori kodu girildi. Güncelleme işlemi iptal edildi.");
                }

                baglanti.Close();

            }


            foreach (DataGridViewRow upt in dataGridView1.SelectedRows)
            {
                int selectedId =Convert.ToInt32(upt.Cells[0].Value);
                string selectedUrunAd =Convert.ToString(upt.Cells[1].Value);
                int selectedStok;
                if (upt.Cells[2].Value==DBNull.Value)
                {
                    selectedStok=-1;
                }
                else
                {
                    selectedStok=Convert.ToInt32(upt.Cells[2].Value);
                }
                
                int selectedAlisFiyat;
                if (upt.Cells[3].Value==DBNull.Value)
                {
                    selectedAlisFiyat=-1;
                }
                else
                {
                    selectedAlisFiyat=Convert.ToInt32(upt.Cells[3].Value);
                }
                int selectedSatisFiyat;
                if (upt.Cells[4].Value==DBNull.Value)
                {
                    selectedSatisFiyat=-1;
                }
                else
                {
                    selectedSatisFiyat=Convert.ToInt32(upt.Cells[4].Value);
                }

                string selectedGorsel = Convert.ToString(upt.Cells[5].Value);
                int selectedKategori;

                if (upt.Cells[6].Value==DBNull.Value)
                {
                    selectedKategori=20;
                }
                else
                {
                    selectedKategori=Convert.ToInt32(upt.Cells[6].Value);

                }

                kayitGuncelle(selectedId, selectedUrunAd, selectedStok, selectedAlisFiyat, selectedSatisFiyat, selectedGorsel, selectedKategori);
            }
            
            btnListele_Click(sender, e); //güncelleme tamamlandığında yeni durum listelenmesi için listele butonu aktifleştirildi.
           
        }
       
        //Çöp kutusundaki tüm ögeleri silmek için Copbosalt butonunun içi dolduruldu.
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

        //Seçilen satırların cop kutusundan silinmesi için kaliciSilme butonunun içi dolduruldu.
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

        //Urunler listesine geri yüklemek istediğimiz ögeler için geri yükleme butonunun içi dolduruldu.
        private void btnGeriYukle_Click(object sender, EventArgs e)
        {
            void KayıtSil(int selectedId)
            {
                NpgsqlCommand sil = new NpgsqlCommand("delete from silinenurunler where silinenurunid=@p1", baglanti);
                sil.Parameters.AddWithValue("@p1", selectedId);
                baglanti.Open();
                sil.ExecuteNonQuery();
                baglanti.Close();
            }
            void KayıtEkle(string selectedAd, int selectedStok, int selectedAlisFiyat, int selectedSatisFiyat,
                string selectedGorsel, int selectedKategori)
            {
                NpgsqlCommand geriYukle = new NpgsqlCommand("insert into urunler (" +
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

                geriYukle.Parameters.AddWithValue("@p1", selectedAd);

                if (selectedStok==-1)
                {
                    geriYukle.Parameters.AddWithValue("@p2", DBNull.Value);
                }

                else
                {
                    geriYukle.Parameters.AddWithValue("@p2", selectedStok);
                }
                
                if (selectedAlisFiyat==-1)
                {
                    geriYukle.Parameters.AddWithValue("@p3", DBNull.Value);
                }
                else
                {
                    geriYukle.Parameters.AddWithValue("@p3", selectedAlisFiyat);
                }

                if (selectedSatisFiyat==-1)
                {
                    geriYukle.Parameters.AddWithValue("@p4", DBNull.Value);
                }
                else
                {
                    geriYukle.Parameters.AddWithValue("@p4", selectedSatisFiyat);
                }

                geriYukle.Parameters.AddWithValue("@p5", selectedGorsel);
                geriYukle.Parameters.AddWithValue("@p6", selectedKategori);

                baglanti.Open();
                geriYukle.ExecuteNonQuery(); 
                baglanti.Close();
            }

            //Sildiğimiz bir ürünü geri yüklerken onun kategorisinin hala sistemde olup olmadığını kontrol ediyoruz. Eğer kategori artık bulunmuyorsa ürün bilinmeyen kategorisi altında geri yükleniyor.
            int kategoriKarsilastir(int selectedKategori)
            {
                NpgsqlCommand karsilastir = new NpgsqlCommand("select silinenurunad from silinenurunler s " +
                        "where exists(select 1 from kategoriler k where k.kategoriid=s.silinenkategori and silinenkategori=@d)", baglanti);
                karsilastir.Parameters.AddWithValue("@d", selectedKategori);
                baglanti.Open();

                karsilastir.ExecuteScalar();
                if ((string)karsilastir.ExecuteScalar()==null)
                    selectedKategori=20; //bilinmeyen kategorisinin id'si
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

                    int selectedStok;
                    if (restore.Cells[1].Value==DBNull.Value)
                    {
                        selectedStok=-1;
                    }
                    else
                    {
                        selectedStok=Convert.ToInt32(restore.Cells[1].Value);
                    }
                    
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

        //Filtreleme işlemi yaparken başka bir form içindeki değişkenlerden değer alıyoruz bunun için public değişkenler tanımladım.
        public string alisFiyatBaslangic { get; set; }
        public string alisFiyatBitis { get; set; }
        public string satisFiyatBaslangic { get; set; }
        public string satisFiyatBitis { get; set; }
        public string kategoriBilgisi { get; set; }
        public string urunAdı { get; set; }
        public bool secili { get; set; }
        

      //filtre fonksiyonu form4 de doldurulan verilere göre sql sorgusunu oluşturuyor. Daha sonra oluşturulan sorguya göre parametleri atayarak filtreleme sorgusunu çalıştırıyor.
        public void filtre()
        {
           
            string sorgu = "select urunler.urunid,urunler.urunad As Urun_Ad, urunler.stok, urunler.alisfiyat As Alis_Fiyat," +
                "urunler.satisfiyat As Satis_Fiyat,urunler.gorsel As Gorsel,urunler.kategori As kategori_kod,kategoriler.kategoriad from urunler " +
                "inner join kategoriler " +
                "on " +
                "urunler.kategori=kategoriler.kategoriid";
            int sayac = 0; //and ve where gibi sorguyu oluşturacak ifadelerin eklenmesi için oluşturulan sayac.
            if(urunAdı !=string.Empty) //örneğin form4de urun adına göre filtre yapılacaksa sorguya yeni alan ekleniyor.
            {
                sayac++;
                if (sayac==1)
                {
                    sorgu=sorgu+" where";
                }
                sorgu =sorgu+" urunler.urunad=@p1";

            }

            if(alisFiyatBaslangic !=string.Empty) //Verilen değerden büyük olanları listeler. Belirli bir aralıkta listeleme yapmak için hem başlangıç hemde bitiş değeri doldurulmalı.
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

            if(alisFiyatBitis !=string.Empty) //Verilen değerden küçük olanları listeler.Belirli bir aralıkta listeleme yapmak için hem başlangıç hemde bitiş değeri doldurulmalı.
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

            if (kategoriBilgisi !="-1") //bir dropdown listten çekilen eleman olduğu için -1 gelmesi durumu seçili eleman olmadığına işaret eder. Bu durumda sorguya ona göre ekleme yapılır.
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

            if(secili) //bu bir checkbox için oluşturulmuş bool yapısı true dönmesi durumunda sorguya ilgili ekleme yapılır.
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


            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti); //dataadaptera oluşturulan sorgu veriliyor.Hiç bir filtre yoksa ürünler alfabetik olarak sıralı şekilde listelenir.
            if (urunAdı!=string.Empty)
            {
                da.SelectCommand.Parameters.AddWithValue("@p1", urunAdı);
            }
            
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
        
        //filtreleme butonunun içi dolduruluyor. Form4 çağırılıyor bu sayede içindeki verileri doldurup çekebiliriz.
        private void btnFiltre_Click(object sender, EventArgs e)
        {

          Form frm4=new Form4(this);
            frm4.Show();
        }
        
    }
}
