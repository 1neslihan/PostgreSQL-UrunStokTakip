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

        }
    }
}
