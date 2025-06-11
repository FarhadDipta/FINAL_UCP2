namespace PosyanduBalita
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
        private System.Windows.Forms.TextBox txtBalitaID;
        private System.Windows.Forms.TextBox txtOrangTuaID;
        private System.Windows.Forms.TextBox txtPerawatID;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBalitaID = new System.Windows.Forms.TextBox();
            this.txtOrangTuaID = new System.Windows.Forms.TextBox();
            this.txtPerawatID = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtJenisKelamin = new System.Windows.Forms.TextBox();
            this.txtTinggiBadan = new System.Windows.Forms.TextBox();
            this.txtBeratBadan = new System.Windows.Forms.TextBox();
            this.txtNomorTelepon = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvRekamMedis = new System.Windows.Forms.DataGridView();
            this.dtpTanggalLahir = new System.Windows.Forms.DateTimePicker();
            this.dtpTanggalPemeriksaan = new System.Windows.Forms.DateTimePicker();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnAnalisis = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRekamMedis)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nama";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tanggal Lahir";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Jenis Kelamin";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tinggi Badan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Berat Badan";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Nomor Telepon";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(52, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Tanggal Pemeriksaan";
            // 
            // txtBalitaID
            // 
            this.txtBalitaID.Location = new System.Drawing.Point(168, 12);
            this.txtBalitaID.Name = "txtBalitaID";
            this.txtBalitaID.Size = new System.Drawing.Size(430, 20);
            this.txtBalitaID.TabIndex = 35;
            // 
            // txtOrangTuaID
            // 
            this.txtOrangTuaID.Location = new System.Drawing.Point(168, 38);
            this.txtOrangTuaID.Name = "txtOrangTuaID";
            this.txtOrangTuaID.Size = new System.Drawing.Size(430, 20);
            this.txtOrangTuaID.TabIndex = 36;
            // 
            // txtPerawatID
            // 
            this.txtPerawatID.Location = new System.Drawing.Point(168, 64);
            this.txtPerawatID.Name = "txtPerawatID";
            this.txtPerawatID.Size = new System.Drawing.Size(430, 20);
            this.txtPerawatID.TabIndex = 37;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(168, 91);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(430, 20);
            this.txtNama.TabIndex = 11;
            this.txtNama.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtJenisKelamin
            // 
            this.txtJenisKelamin.Location = new System.Drawing.Point(168, 143);
            this.txtJenisKelamin.Name = "txtJenisKelamin";
            this.txtJenisKelamin.Size = new System.Drawing.Size(430, 20);
            this.txtJenisKelamin.TabIndex = 13;
            // 
            // txtTinggiBadan
            // 
            this.txtTinggiBadan.Location = new System.Drawing.Point(168, 169);
            this.txtTinggiBadan.Name = "txtTinggiBadan";
            this.txtTinggiBadan.Size = new System.Drawing.Size(430, 20);
            this.txtTinggiBadan.TabIndex = 14;
            // 
            // txtBeratBadan
            // 
            this.txtBeratBadan.Location = new System.Drawing.Point(168, 195);
            this.txtBeratBadan.Name = "txtBeratBadan";
            this.txtBeratBadan.Size = new System.Drawing.Size(430, 20);
            this.txtBeratBadan.TabIndex = 15;
            this.txtBeratBadan.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // txtNomorTelepon
            // 
            this.txtNomorTelepon.Location = new System.Drawing.Point(168, 221);
            this.txtNomorTelepon.Name = "txtNomorTelepon";
            this.txtNomorTelepon.Size = new System.Drawing.Size(430, 20);
            this.txtNomorTelepon.TabIndex = 16;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(604, 94);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 22;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(685, 94);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 23);
            this.btnHapus.TabIndex = 23;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(604, 123);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(75, 23);
            this.btnUbah.TabIndex = 24;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(685, 123);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvRekamMedis
            // 
            this.dgvRekamMedis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRekamMedis.Location = new System.Drawing.Point(52, 278);
            this.dgvRekamMedis.Name = "dgvRekamMedis";
            this.dgvRekamMedis.Size = new System.Drawing.Size(708, 167);
            this.dgvRekamMedis.TabIndex = 26;
            this.dgvRekamMedis.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPosyandu_CellClick);
            this.dgvRekamMedis.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPosyandu_CellClick);
            // 
            // dtpTanggalLahir
            // 
            this.dtpTanggalLahir.Location = new System.Drawing.Point(168, 117);
            this.dtpTanggalLahir.Name = "dtpTanggalLahir";
            this.dtpTanggalLahir.Size = new System.Drawing.Size(430, 20);
            this.dtpTanggalLahir.TabIndex = 27;
            // 
            // dtpTanggalPemeriksaan
            // 
            this.dtpTanggalPemeriksaan.Location = new System.Drawing.Point(168, 247);
            this.dtpTanggalPemeriksaan.Name = "dtpTanggalPemeriksaan";
            this.dtpTanggalPemeriksaan.Size = new System.Drawing.Size(430, 20);
            this.dtpTanggalPemeriksaan.TabIndex = 28;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(2, 4);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(75, 23);
            this.btnDashboard.TabIndex = 29;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(604, 152);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 30;
            this.btnImport.Text = "Import Data";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnAnalisis
            // 
            this.btnAnalisis.Location = new System.Drawing.Point(685, 152);
            this.btnAnalisis.Name = "btnAnalisis";
            this.btnAnalisis.Size = new System.Drawing.Size(75, 23);
            this.btnAnalisis.TabIndex = 34;
            this.btnAnalisis.Text = "Analisis";
            this.btnAnalisis.UseVisualStyleBackColor = true;
            this.btnAnalisis.Click += new System.EventHandler(this.btnAnalisis_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(643, 181);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 38;
            this.btnExport.Text = "Export Data";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnAnalisis);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnDashboard);
            this.Controls.Add(this.dtpTanggalPemeriksaan);
            this.Controls.Add(this.dtpTanggalLahir);
            this.Controls.Add(this.dgvRekamMedis);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtNomorTelepon);
            this.Controls.Add(this.txtBeratBadan);
            this.Controls.Add(this.txtTinggiBadan);
            this.Controls.Add(this.txtJenisKelamin);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBalitaID);
            this.Controls.Add(this.txtOrangTuaID);
            this.Controls.Add(this.txtPerawatID);
            this.Name = "Form1";
            this.Text = "Rekam Medis";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRekamMedis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtJenisKelamin;
        private System.Windows.Forms.TextBox txtTinggiBadan;
        private System.Windows.Forms.TextBox txtBeratBadan;
        private System.Windows.Forms.TextBox txtNomorTelepon;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvRekamMedis;
        private System.Windows.Forms.DateTimePicker dtpTanggalLahir;
        private System.Windows.Forms.DateTimePicker dtpTanggalPemeriksaan;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnAnalisis;
        private System.Windows.Forms.Button btnExport;
    }
}
