using System;
using System.Data;
using System.Data.SqlClient; // Thư viện để làm việc với SQL
using System.Text.RegularExpressions; // Thư viện để kiểm tra định dạng (Regex)
using System.Windows.Forms;

namespace QuanLyOrganization // Đảm bảo tên này khớp với tên Project của bạn
{
    public partial class Form1 : Form
    {
        // ====================================================
        // KHU VỰC CẤU HÌNH KẾT NỐI (SỬA DÒNG NÀY NẾU LỖI)
        // Data Source=.; nghĩa là lấy server nội bộ. Nếu lỗi, thử thay dấu chấm bằng .\SQLEXPRESS
        // ====================================================
        string connectionString = @"Data Source=.;Initial Catalog=QuanLyToChuc;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        // 1. SỰ KIỆN BẤM NÚT SAVE
        private void btnSave_Click(object sender, EventArgs e)
        {
            // --- BƯỚC 1: VALIDATE (KIỂM TRA DỮ LIỆU ĐẦU VÀO) ---
            string name = txtOrgName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            // Kiểm tra tên rỗng
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Organization Name không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrgName.Focus();
                return;
            }

            // Kiểm tra độ dài tên (3 - 255 ký tự)
            if (name.Length < 3 || name.Length > 255)
            {
                MessageBox.Show("Organization Name phải từ 3 đến 255 ký tự!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrgName.Focus();
                return;
            }

            // Kiểm tra số điện thoại (Nếu có nhập thì phải là số, 9-12 ký tự)
            if (!string.IsNullOrEmpty(phone))
            {
                // Dùng Regex: Chỉ chấp nhận số (\d), độ dài {9,12}
                if (!Regex.IsMatch(phone, @"^\d{9,12}$"))
                {
                    MessageBox.Show("Phone chỉ được chứa số và dài từ 9-12 ký tự!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }
            }

            // Kiểm tra Email (Đơn giản: Có nhập thì phải có @ và dấu chấm)
            if (!string.IsNullOrEmpty(email))
            {
                if (!email.Contains("@") || !email.Contains("."))
                {
                    MessageBox.Show("Email không đúng định dạng!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }
            }

            // --- BƯỚC 2: TƯƠNG TÁC VỚI DATABASE ---
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 2.1 Kiểm tra trùng tên (OrgName)
                    string checkSql = "SELECT COUNT(*) FROM ORGANIZATION WHERE OrgName = @Name";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Name", name);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Organization Name already exists (Tên đã tồn tại)!", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Dừng lại, không lưu
                        }
                    }

                    // 2.2 Lưu dữ liệu (Insert)
                    string insertSql = "INSERT INTO ORGANIZATION (OrgName, Address, Phone, Email) VALUES (@Name, @Addr, @Phone, @Email)";
                    using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Name", name);
                        // Nếu address rỗng thì lưu DBNull (NULL trong SQL), ngược lại lưu giá trị
                        insertCmd.Parameters.AddWithValue("@Addr", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);
                        insertCmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                        insertCmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);

                        insertCmd.ExecuteNonQuery(); // Thực thi lệnh Insert
                    }
                }

                // --- BƯỚC 3: XỬ LÝ SAU KHI LƯU THÀNH CÔNG ---
                MessageBox.Show("Save successfully (Lưu thành công)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở khóa nút Director theo yêu cầu đề bài
                btnDirector.Enabled = true;
            }
            catch (Exception ex)
            {
                // Bắt lỗi kết nối SQL hoặc lỗi hệ thống
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. SỰ KIỆN BẤM NÚT BACK
        private void btnBack_Click(object sender, EventArgs e)
        {
            // Đóng form hiện tại
            this.Close();
        }

        // 3. SỰ KIỆN BẤM NÚT DIRECTOR
        private void btnDirector_Click(object sender, EventArgs e)
        {
            // Theo đề bài: Mở form Director Management.
            // Vì chưa làm form đó nên tạm thời hiện thông báo đã.
            MessageBox.Show("Đang chuyển sang màn hình Director Management...", "Chuyển trang");

            // Code mở form mới sau này sẽ là:
            // FormDirector frm = new FormDirector();
            // frm.Show();
        }
    }
}