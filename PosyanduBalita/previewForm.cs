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

namespace PosyanduBalita
{
    public partial class previewForm : Form
    {
        private string connectionString = "Data Source=FARHAD-DIPTA\\FARHADDIPTA;Initial Catalog=PosyanduBalita;Integrated Security=True";
        private DataTable data;
        private string tableName;

        public previewForm(DataTable dt, string tableName)
        {
            InitializeComponent();
            data = dt;
            this.tableName = tableName;
            dgvPreview.DataSource = data;
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            dgvPreview.AutoResizeColumns();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda ingin mengimpor data ini ke database?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ImportDataToDatabase();
            }
        }

        private bool ValidateRow(DataRow row)
        {
            // Contoh validasi: Nama tidak boleh kosong
            if (row.Table.Columns.Contains("Nama"))
            {
                string nama = row["Nama"].ToString();
                if (string.IsNullOrWhiteSpace(nama))
                {
                    MessageBox.Show("Nama tidak boleh kosong.", "Kesalahan Validasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void ImportDataToDatabase()
        {
            try
            {
                DataTable dt = (DataTable)dgvPreview.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    if (!ValidateRow(row))
                        continue;

                    string query = "";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;

                            if (tableName == "Balita")
                            {
                                query = "INSERT INTO Balita (Nama, TanggalLahir, JenisKelamin) " +
                                        "VALUES (@Nama, @TanggalLahir, @JenisKelamin)";
                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@Nama", row["Nama"]);
                                cmd.Parameters.AddWithValue("@TanggalLahir", row["TanggalLahir"]);
                                cmd.Parameters.AddWithValue("@JenisKelamin", row["JenisKelamin"]);
                            }
                            else if (tableName == "OrangTua")
                            {
                                query = "INSERT INTO OrangTua (Nama, NoTelepon, Alamat) " +
                                        "VALUES (@Nama, @NoTelepon, @Alamat)";
                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@Nama", row["Nama"]);
                                cmd.Parameters.AddWithValue("@NoTelepon", row["NoTelepon"]);
                                cmd.Parameters.AddWithValue("@Alamat", row["Alamat"]);
                            }
                            else if (tableName == "Perawat")
                            {
                                query = "INSERT INTO Perawat (Nama, NoTelepon) VALUES (@Nama, @NoTelepon)";
                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@Nama", row["Nama"]);
                                cmd.Parameters.AddWithValue("@NoTelepon", row["NoTelepon"]);
                            }
                            else if (tableName == "RekamMedis")
                            {
                                query = "INSERT INTO RekamMedis (BalitaID, OrangTuaID, PerawatID, Nama, TanggalLahir, JenisKelamin, TinggiBadan, BeratBadan, TanggalPemeriksaan, NoTelepon) " +
                                        "VALUES (@BalitaID, @OrangTuaID, @PerawatID, @Nama, @TanggalLahir, @JenisKelamin, @TinggiBadan, @BeratBadan, @TanggalPemeriksaan, @NoTelepon)";
                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@BalitaID", row["BalitaID"]);
                                cmd.Parameters.AddWithValue("@OrangTuaID", row["OrangTuaID"]);
                                cmd.Parameters.AddWithValue("@PerawatID", row["PerawatID"]);
                                cmd.Parameters.AddWithValue("@Nama", row["Nama"]);
                                cmd.Parameters.AddWithValue("@TanggalLahir", row["TanggalLahir"]);
                                cmd.Parameters.AddWithValue("@JenisKelamin", row["JenisKelamin"]);
                                cmd.Parameters.AddWithValue("@TinggiBadan", row["TinggiBadan"]);
                                cmd.Parameters.AddWithValue("@BeratBadan", row["BeratBadan"]);
                                cmd.Parameters.AddWithValue("@TanggalPemeriksaan", row["TanggalPemeriksaan"]);
                                cmd.Parameters.AddWithValue("@NoTelepon", row["NoTelepon"]);
                            }
                            // Tambahkan else if untuk tabel lain jika diperlukan

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Data berhasil diimpor ke database.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mengimpor data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
