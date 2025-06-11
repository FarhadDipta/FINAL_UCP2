using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace PosyanduBalita
{
    public partial class Form1: Form
    {
        private string connectionString = "Data Source=FARHAD-DIPTA\\FARHADDIPTA;Initial Catalog=PosyanduBalita;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            txtNama.Clear();
            txtJenisKelamin.Clear();
            txtTinggiBadan.Clear();
            txtBeratBadan.Clear();
            txtNomorTelepon.Clear();
            dtpTanggalLahir.Value = DateTime.Today;
            dtpTanggalPemeriksaan.Value = DateTime.Today;
            txtNama.Focus();
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private int GetNextRekamMedisID()
        {
            int nextID = 1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ISNULL(MAX(RekamMedisID), 0) + 1 FROM RekamMedis";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        nextID = (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mendapatkan ID baru: " + ex.Message);
                }
            }
            return nextID;
        }


        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT RekamMedisID, BalitaID, OrangTuaID, PerawatID, Nama, TanggalLahir, JenisKelamin, TinggiBadan, BeratBadan, TanggalPemeriksaan, NoTelepon FROM RekamMedis";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRekamMedis.AutoGenerateColumns = true;
                    dgvRekamMedis.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message);
                }
            }
        }

        private bool ValidateForm()
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNama.Text.Trim(), @"^[A-Za-z\s]+$"))
            {
                MessageBox.Show("Nama tidak valid. Silakan masukkan hanya huruf A–Z tanpa angka atau simbol.");
                txtNama.Focus();
                return false;
            }

            string noHP = txtNomorTelepon.Text.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(noHP, @"^08\d{8,11}$"))
            {
                MessageBox.Show("Nomor telepon tidak valid. Masukkan nomor yang diawali 08 dan terdiri dari 10–13 digit.");
                txtNomorTelepon.Focus();
                return false;
            }

            DateTime tanggalLahir = dtpTanggalLahir.Value.Date;
            DateTime today = DateTime.Today;
            if (tanggalLahir > today || tanggalLahir < today.AddYears(-5))
            {
                MessageBox.Show("Tanggal lahir tidak valid. Masukkan tanggal antara hari ini dan maksimal 5 tahun yang lalu.");
                dtpTanggalLahir.Focus();
                return false;
            }

            if (!decimal.TryParse(txtTinggiBadan.Text, out decimal tinggiBadan) || tinggiBadan < 40 || tinggiBadan > 120)
            {
                MessageBox.Show("Tinggi badan tidak valid. Masukkan angka antara 40 cm hingga 120 cm.");
                txtTinggiBadan.Focus();
                return false;
            }

            if (!decimal.TryParse(txtBeratBadan.Text, out decimal beratBadan) || beratBadan < 2 || beratBadan > 40)
            {
                MessageBox.Show("Berat badan tidak valid. Masukkan angka antara 2 kg hingga 40 kg.");
                txtBeratBadan.Focus();
                return false;
            }

            string jenisKelamin = txtJenisKelamin.Text.Trim().ToUpper();
            if (jenisKelamin != "L" && jenisKelamin != "P")
            {
                MessageBox.Show("Jenis kelamin tidak valid. Masukkan 'L' untuk Laki-laki atau 'P' untuk Perempuan.");
                txtJenisKelamin.Focus();
                return false;
            }

            DateTime tanggalPemeriksaan = dtpTanggalPemeriksaan.Value.Date;
            if (tanggalPemeriksaan > today || tanggalPemeriksaan < today.AddMonths(-1))
            {
                MessageBox.Show("Tanggal pemeriksaan tidak valid. Masukkan tanggal antara hari ini dan maksimal 1 bulan yang lalu.");
                dtpTanggalPemeriksaan.Focus();
                return false;
            }

            return true;
        }


        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("AddRekamMedis", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BalitaID", int.Parse(txtBalitaID.Text));
                        cmd.Parameters.AddWithValue("@OrangTuaID", int.Parse(txtOrangTuaID.Text));
                        cmd.Parameters.AddWithValue("@PerawatID", int.Parse(txtPerawatID.Text));
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value);
                        cmd.Parameters.AddWithValue("@JenisKelamin", txtJenisKelamin.Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@TinggiBadan", decimal.Parse(txtTinggiBadan.Text));
                        cmd.Parameters.AddWithValue("@BeratBadan", decimal.Parse(txtBeratBadan.Text));
                        cmd.Parameters.AddWithValue("@TanggalPemeriksaan", dtpTanggalPemeriksaan.Value);
                        cmd.Parameters.AddWithValue("@NoTelepon", txtNomorTelepon.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            transaction.Rollback();
                            MessageBox.Show("Data tidak berhasil ditambahkan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    try { transaction.Rollback(); } catch { }
                    MessageBox.Show("Kesalahan database: " + ex.Message, "Kesalahan SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    try { transaction.Rollback(); } catch { }
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvRekamMedis.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();
                        try
                        {
                            int rekamMedisID = Convert.ToInt32(dgvRekamMedis.SelectedRows[0].Cells["RekamMedisID"].Value);
                            using (SqlCommand cmd = new SqlCommand("DeleteRekamMedis", conn, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@RekamMedisID", rekamMedisID);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    transaction.Commit();
                                    MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                    ClearForm();
                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Data tidak ditemukan atau gagal dihapus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            try { transaction.Rollback(); } catch { }
                            MessageBox.Show("Kesalahan database: " + ex.Message, "Kesalahan SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            try { transaction.Rollback(); } catch { }
                            MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            if (dgvRekamMedis.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateRekamMedis", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            int rekamMedisID = Convert.ToInt32(dgvRekamMedis.SelectedRows[0].Cells["RekamMedisID"].Value);
                            cmd.Parameters.AddWithValue("@RekamMedisID", rekamMedisID);
                            cmd.Parameters.AddWithValue("@BalitaID", int.Parse(txtBalitaID.Text));
                            cmd.Parameters.AddWithValue("@OrangTuaID", int.Parse(txtOrangTuaID.Text));
                            cmd.Parameters.AddWithValue("@PerawatID", int.Parse(txtPerawatID.Text));
                            cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                            cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value);
                            cmd.Parameters.AddWithValue("@JenisKelamin", txtJenisKelamin.Text.Trim().ToUpper());
                            cmd.Parameters.AddWithValue("@TinggiBadan", decimal.Parse(txtTinggiBadan.Text));
                            cmd.Parameters.AddWithValue("@BeratBadan", decimal.Parse(txtBeratBadan.Text));
                            cmd.Parameters.AddWithValue("@TanggalPemeriksaan", dtpTanggalPemeriksaan.Value);
                            cmd.Parameters.AddWithValue("@NoTelepon", txtNomorTelepon.Text.Trim());

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                transaction.Commit();
                                MessageBox.Show("Data berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                                ClearForm();
                            }
                            else
                            {
                                transaction.Rollback();
                                MessageBox.Show("Data tidak ditemukan atau gagal diperbarui!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        try { transaction.Rollback(); } catch { }
                        MessageBox.Show("Kesalahan database: " + ex.Message, "Kesalahan SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        try { transaction.Rollback(); } catch { }
                        MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang akan diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Data berhasil diperbarui.");
        }

        private void dgvPosyandu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRekamMedis.Rows[e.RowIndex];

                txtNama.Text = row.Cells["Nama"].Value?.ToString();
                txtJenisKelamin.Text = row.Cells["JenisKelamin"].Value?.ToString();
                txtTinggiBadan.Text = row.Cells["TinggiBadan"].Value?.ToString();
                txtBeratBadan.Text = row.Cells["BeratBadan"].Value?.ToString();
                txtNomorTelepon.Text = row.Cells["NoTelepon"].Value?.ToString();

                dtpTanggalLahir.Value = Convert.ToDateTime(row.Cells["TanggalLahir"].Value);
                dtpTanggalPemeriksaan.Value = Convert.ToDateTime(row.Cells["TanggalPemeriksaan"].Value);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                PreviewData(filePath);
            }
        }

        private void PreviewData(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheetAt(0);
                    DataTable dt = new DataTable();

                    IRow headerRow = sheet.GetRow(0);
                    foreach (var cell in headerRow.Cells)
                    {
                        dt.Columns.Add(cell.ToString());
                    }

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow dataRow = sheet.GetRow(i);
                        if (dataRow == null) continue;
                        DataRow newRow = dt.NewRow();
                        int cellIndex = 0;
                        foreach (var cell in dataRow.Cells)
                        {
                            newRow[cellIndex] = cell.ToString();
                            cellIndex++;
                        }
                        dt.Rows.Add(newRow);
                    }

                    string tableName = "ImportedData"; // Provide a valid table name here
                    previewForm PreviewForm = new previewForm(dt, tableName);
                    PreviewForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading the Excel file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    COUNT(*) AS JumlahPemeriksaan,
                    AVG(BeratBadan) AS RataRataBerat,
                    AVG(TinggiBadan) AS RataRataTinggi,
                    SUM(CASE WHEN JenisKelamin = 'L' THEN 1 ELSE 0 END) AS JumlahLakiLaki,
                    SUM(CASE WHEN JenisKelamin = 'P' THEN 1 ELSE 0 END) AS JumlahPerempuan
                FROM RekamMedis
                WHERE TanggalPemeriksaan >= DATEADD(MONTH, -1, GETDATE())";
                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hasil = $"Jumlah Pemeriksaan (1 bulan terakhir): {reader["JumlahPemeriksaan"]}\n" +
                                           $"Rata-rata Berat Badan: {reader["RataRataBerat"]:0.00} kg\n" +
                                           $"Rata-rata Tinggi Badan: {reader["RataRataTinggi"]:0.00} cm\n" +
                                           $"Jumlah Laki-laki: {reader["JumlahLakiLaki"]}\n" +
                                           $"Jumlah Perempuan: {reader["JumlahPerempuan"]}";
                            MessageBox.Show(hasil, "Analisis Rekam Medis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal melakukan analisis: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string _role;

        public Form1(string role) // Ganti X sesuai nomor form
        {
            InitializeComponent();
            _role = role;
        }

        private void txtBalitaID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }
    }
}
