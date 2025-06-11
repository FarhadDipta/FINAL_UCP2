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
    public partial class Form3 : Form
    {
        private string connectionString = "Data Source=FARHAD-DIPTA\\FARHADDIPTA;Initial Catalog=PosyanduBalita;Integrated Security=True";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            txtNama.Clear();
            dtpTanggalLahir.Value = DateTime.Today;
            txtJenisKelamin.Clear();
            txtNama.Focus();
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private bool ValidateForm()
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNama.Text.Trim(), @"^[A-Za-z\s]+$"))
            {
                MessageBox.Show("Nama tidak valid. Silakan masukkan hanya huruf A–Z tanpa angka atau simbol.");
                txtNama.Focus();
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

            string jenisKelamin = txtJenisKelamin.Text.Trim().ToUpper();
            if (jenisKelamin != "L" && jenisKelamin != "P")
            {
                MessageBox.Show("Jenis kelamin tidak valid. Masukkan 'L' untuk Laki-laki atau 'P' untuk Perempuan.");
                txtJenisKelamin.Focus();
                return false;
            }

            return true;
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Tambahkan BalitaID pada SELECT
                    string query = "SELECT BalitaID, Nama, TanggalLahir, JenisKelamin FROM Balita";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvBalita.AutoGenerateColumns = true;
                    dgvBalita.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message);
                }
            }
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
                    using (SqlCommand cmd = new SqlCommand("AddBalita", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value);
                        cmd.Parameters.AddWithValue("@JenisKelamin", txtJenisKelamin.Text.Trim().ToUpper());

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
            if (dgvBalita.SelectedRows.Count > 0)
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
                            int balitaID = Convert.ToInt32(dgvBalita.SelectedRows[0].Cells["BalitaID"].Value);
                            using (SqlCommand cmd = new SqlCommand("DeleteBalita", conn, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@BalitaID", balitaID);

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

            if (dgvBalita.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateBalita", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            int balitaID = Convert.ToInt32(dgvBalita.SelectedRows[0].Cells["BalitaID"].Value);
                            cmd.Parameters.AddWithValue("@BalitaID", balitaID);
                            cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                            cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value);
                            cmd.Parameters.AddWithValue("@JenisKelamin", txtJenisKelamin.Text.Trim().ToUpper());

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

        private void dgvBalita_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBalita.Rows[e.RowIndex];

                txtNama.Text = row.Cells["Nama"].Value?.ToString();
                dtpTanggalLahir.Text = row.Cells["TanggalLahir"].Value?.ToString();
                txtJenisKelamin.Text = row.Cells["JenisKelamin"].Value?.ToString();
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

                    string tableName = "Balita"; // Specify the table name here
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
                    COUNT(*) AS JumlahBalita,
                    SUM(CASE WHEN JenisKelamin = 'L' THEN 1 ELSE 0 END) AS JumlahLakiLaki,
                    SUM(CASE WHEN JenisKelamin = 'P' THEN 1 ELSE 0 END) AS JumlahPerempuan
                FROM Balita";
                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hasil = $"Jumlah Balita: {reader["JumlahBalita"]}\n" +
                                           $"Laki-laki: {reader["JumlahLakiLaki"]}\n" +
                                           $"Perempuan: {reader["JumlahPerempuan"]}";
                            MessageBox.Show(hasil, "Analisis Balita", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public Form3(string role) // Ganti X sesuai nomor form
        {
            InitializeComponent();
            _role = role;
        }

    }
}
