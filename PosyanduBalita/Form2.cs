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
    public partial class Form2 : Form
    {
        private string connectionString = "Data Source=FARHAD-DIPTA\\FARHADDIPTA;Initial Catalog=PosyanduBalita;Integrated Security=True";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            txtNama.Clear();
            txtNoTelepon.Clear();
            txtAlamat.Clear();
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

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNoTelepon.Text.Trim(), @"^\d{10,13}$"))
            {
                MessageBox.Show("No Telepon tidak valid. Masukkan angka dengan panjang 10 hingga 13 digit.");
                txtNoTelepon.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAlamat.Text))
            {
                MessageBox.Show("Alamat tidak boleh kosong.");
                txtAlamat.Focus();
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtAlamat.Text.Trim(), @"^[A-Za-z0-9\s,\.]+$"))
            {
                MessageBox.Show("Alamat hanya boleh berisi huruf, angka, spasi, koma, dan titik.");
                txtAlamat.Focus();
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
                    string query = "SELECT OrangTuaID, Nama, NoTelepon, Alamat FROM OrangTua";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvOrangTua.AutoGenerateColumns = true;
                    dgvOrangTua.DataSource = dt;
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
                    using (SqlCommand cmd = new SqlCommand("AddOrangTua", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelepon", txtNoTelepon.Text.Trim());
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text.Trim());

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
            if (dgvOrangTua.SelectedRows.Count > 0)
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
                            int orangTuaID = Convert.ToInt32(dgvOrangTua.SelectedRows[0].Cells["OrangTuaID"].Value);
                            using (SqlCommand cmd = new SqlCommand("DeleteOrangTua", conn, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@OrangTuaID", orangTuaID);

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

            if (dgvOrangTua.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateOrangTua", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            int orangTuaID = Convert.ToInt32(dgvOrangTua.SelectedRows[0].Cells["OrangTuaID"].Value);
                            cmd.Parameters.AddWithValue("@OrangTuaID", orangTuaID);
                            cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                            cmd.Parameters.AddWithValue("@NoTelepon", txtNoTelepon.Text.Trim());
                            cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text.Trim());

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

        private void dgvOrangTua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvOrangTua.Rows[e.RowIndex];

                txtNama.Text = row.Cells["Nama"].Value?.ToString();
                txtNoTelepon.Text = row.Cells["NoTelepon"].Value?.ToString();
                txtAlamat.Text = row.Cells["Alamat"].Value?.ToString();
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
                    COUNT(*) AS JumlahOrangTua,
                    COUNT(DISTINCT NoTelepon) AS JumlahNoTeleponUnik,
                    COUNT(DISTINCT Alamat) AS JumlahAlamatUnik
                FROM OrangTua";
                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hasil = $"Jumlah Orang Tua: {reader["JumlahOrangTua"]}\n" +
                                           $"Nomor Telepon Unik: {reader["JumlahNoTeleponUnik"]}\n" +
                                           $"Alamat Unik: {reader["JumlahAlamatUnik"]}";
                            MessageBox.Show(hasil, "Analisis Orang Tua", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public Form2(string role) // Ganti X sesuai nomor form
        {
            InitializeComponent();
            _role = role;
        }
    }
}
