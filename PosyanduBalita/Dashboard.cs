using System;
using System.Windows.Forms;

namespace PosyanduBalita
{
    public partial class Dashboard : Form
    {
        private string _role;

        public Dashboard(string role)
        {
            _role = role;
            InitializeComponent();
        }

        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnBalita_Click(object sender, EventArgs e)
        {
            Form3 formBalita = new Form3(_role);
            formBalita.Show();
            this.Hide();
        }

        private void btnOrangTua_Click(object sender, EventArgs e)
        {
            Form2 formOrangTua = new Form2(_role);
            formOrangTua.Show();
            this.Hide();
        }

        private void btnPerawat_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(_role);
            form4.Show();
            this.Hide();
        }

        private void btnRekamMedis_Click(object sender, EventArgs e)
        {
            Form1 formRekamMedis = new Form1(_role);
            formRekamMedis.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Login loginForm = new Login();
                loginForm.Show();
                this.Hide();
            }
        }

    }
}