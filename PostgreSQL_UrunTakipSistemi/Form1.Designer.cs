using System.Windows.Forms;

namespace PostgreSQL_UrunTakipSistemi
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAra = new System.Windows.Forms.Button();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnListele = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.btnKaliciSilme = new System.Windows.Forms.Button();
            this.btnCopBosalt = new System.Windows.Forms.Button();
            this.btnGeriYukle = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1290, 504);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.btnAra);
            this.tabPage1.Controls.Add(this.btnGuncelle);
            this.tabPage1.Controls.Add(this.btnSil);
            this.tabPage1.Controls.Add(this.btnEkle);
            this.tabPage1.Controls.Add(this.btnListele);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1282, 471);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ürün";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(791, 46);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(321, 341);
            this.dataGridView2.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(301, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 25);
            this.label8.TabIndex = 20;
            this.label8.Text = "Veri Tabanı";
            // 
            // btnAra
            // 
            this.btnAra.Location = new System.Drawing.Point(1134, 352);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(126, 32);
            this.btnAra.TabIndex = 19;
            this.btnAra.Text = "Ara";
            this.btnAra.UseVisualStyleBackColor = true;
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Location = new System.Drawing.Point(1134, 282);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(126, 32);
            this.btnGuncelle.TabIndex = 18;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = true;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(1134, 207);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(126, 32);
            this.btnSil.TabIndex = 17;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(1134, 132);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(126, 32);
            this.btnEkle.TabIndex = 16;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnListele
            // 
            this.btnListele.Location = new System.Drawing.Point(1134, 57);
            this.btnListele.Name = "btnListele";
            this.btnListele.Size = new System.Drawing.Size(126, 32);
            this.btnListele.TabIndex = 15;
            this.btnListele.Text = "Listele";
            this.btnListele.UseVisualStyleBackColor = true;
            this.btnListele.Click += new System.EventHandler(this.btnListele_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(824, 4);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(235, 25);
            this.label6.TabIndex = 6;
            this.label6.Text = "Kategori Kodları Listesi";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 46);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(758, 341);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnGeriYukle);
            this.tabPage2.Controls.Add(this.btnCopBosalt);
            this.tabPage2.Controls.Add(this.btnKaliciSilme);
            this.tabPage2.Controls.Add(this.dataGridView3);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1282, 471);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Kategori";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(26, 35);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(787, 327);
            this.dataGridView3.TabIndex = 0;
            // 
            // btnKaliciSilme
            // 
            this.btnKaliciSilme.Location = new System.Drawing.Point(929, 55);
            this.btnKaliciSilme.Name = "btnKaliciSilme";
            this.btnKaliciSilme.Size = new System.Drawing.Size(238, 52);
            this.btnKaliciSilme.TabIndex = 1;
            this.btnKaliciSilme.Text = "Kalıcı Sil";
            this.btnKaliciSilme.UseVisualStyleBackColor = true;
            this.btnKaliciSilme.Click += new System.EventHandler(this.btnKaliciSilme_Click);
            // 
            // btnCopBosalt
            // 
            this.btnCopBosalt.Location = new System.Drawing.Point(929, 185);
            this.btnCopBosalt.Name = "btnCopBosalt";
            this.btnCopBosalt.Size = new System.Drawing.Size(238, 52);
            this.btnCopBosalt.TabIndex = 2;
            this.btnCopBosalt.Text = "Çöp Boşalt";
            this.btnCopBosalt.UseVisualStyleBackColor = true;
            this.btnCopBosalt.Click += new System.EventHandler(this.btnCopBosalt_Click);
            // 
            // btnGeriYukle
            // 
            this.btnGeriYukle.Location = new System.Drawing.Point(929, 331);
            this.btnGeriYukle.Name = "btnGeriYukle";
            this.btnGeriYukle.Size = new System.Drawing.Size(238, 52);
            this.btnGeriYukle.TabIndex = 3;
            this.btnGeriYukle.Text = "Geri Yükle";
            this.btnGeriYukle.UseVisualStyleBackColor = true;
            this.btnGeriYukle.Click += new System.EventHandler(this.btnGeriYukle_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 504);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAra;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnListele;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnKaliciSil;
        private System.Windows.Forms.DataGridView dataGridView3;
        private Button btnKaliciSilme;
        private Button btnCopBosalt;
        private Button btnGeriYukle;
    }
}

