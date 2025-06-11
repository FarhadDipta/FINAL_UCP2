using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace PosyanduBalita
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void LoadReportData()
        {
            // Ganti connectionString sesuai dengan milik Anda
            string connectionString = "Data Source=FARHAD-DIPTA\\FARHADDIPTA;Initial Catalog=PosyanduBalita;Integrated Security=True";
            string query = @"SELECT RekamMedisID, BalitaID, OrangTuaID, PerawatID, Nama, TanggalLahir, JenisKelamin, TinggiBadan, BeratBadan, TanggalPemeriksaan, NoTelepon FROM RekamMedis";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }

            // Pastikan "DataSet1" sesuai dengan nama DataSet di RDLC Anda
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Ganti path berikut dengan path file .rdlc Anda
            reportViewer1.LocalReport.ReportPath = "C:\\Users\\ASUS\\Desktop\\Kuliah\\Semester 4\\PABD\\UCP1\\UCP1_PABD\\PosyanduBalita\\Final_UCP1\\PosyanduBalita\\PosyanduReport.rdlc";
            reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            // Tidak perlu diisi jika tidak ada logic khusus
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }
    }
}
