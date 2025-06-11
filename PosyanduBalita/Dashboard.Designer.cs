namespace PosyanduBalita
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnBalita = new System.Windows.Forms.Button();
            this.btnOrangTua = new System.Windows.Forms.Button();
            this.btnPerawat = new System.Windows.Forms.Button();
            this.btnRekamMedis = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBalita
            // 
            this.btnBalita.Location = new System.Drawing.Point(100, 50);
            this.btnBalita.Name = "btnBalita";
            this.btnBalita.Size = new System.Drawing.Size(200, 50);
            this.btnBalita.TabIndex = 0;
            this.btnBalita.Text = "Balita";
            this.btnBalita.UseVisualStyleBackColor = true;
            this.btnBalita.Click += new System.EventHandler(this.btnBalita_Click);
            // 
            // btnOrangTua
            // 
            this.btnOrangTua.Location = new System.Drawing.Point(100, 120);
            this.btnOrangTua.Name = "btnOrangTua";
            this.btnOrangTua.Size = new System.Drawing.Size(200, 50);
            this.btnOrangTua.TabIndex = 1;
            this.btnOrangTua.Text = "Orang Tua";
            this.btnOrangTua.UseVisualStyleBackColor = true;
            this.btnOrangTua.Click += new System.EventHandler(this.btnOrangTua_Click);
            // 
            // btnPerawat
            // 
            this.btnPerawat.Location = new System.Drawing.Point(100, 190);
            this.btnPerawat.Name = "btnPerawat";
            this.btnPerawat.Size = new System.Drawing.Size(200, 50);
            this.btnPerawat.TabIndex = 2;
            this.btnPerawat.Text = "Perawat";
            this.btnPerawat.UseVisualStyleBackColor = true;
            this.btnPerawat.Click += new System.EventHandler(this.btnPerawat_Click);
            // 
            // btnRekamMedis
            // 
            this.btnRekamMedis.Location = new System.Drawing.Point(100, 260);
            this.btnRekamMedis.Name = "btnRekamMedis";
            this.btnRekamMedis.Size = new System.Drawing.Size(200, 50);
            this.btnRekamMedis.TabIndex = 3;
            this.btnRekamMedis.Text = "Rekam Medis";
            this.btnRekamMedis.UseVisualStyleBackColor = true;
            this.btnRekamMedis.Click += new System.EventHandler(this.btnRekamMedis_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(1, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // Dashboard
            // 
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnRekamMedis);
            this.Controls.Add(this.btnPerawat);
            this.Controls.Add(this.btnOrangTua);
            this.Controls.Add(this.btnBalita);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnBalita;
        private System.Windows.Forms.Button btnOrangTua;
        private System.Windows.Forms.Button btnPerawat;
        private System.Windows.Forms.Button btnRekamMedis;
        private System.Windows.Forms.Button btnLogout;
    }
}