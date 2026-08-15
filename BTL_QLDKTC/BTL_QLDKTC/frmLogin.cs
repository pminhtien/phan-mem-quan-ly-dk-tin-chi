using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLDKTC
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void lblExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassWord.Text.Trim();
            string role = "";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblErrorLogin.Text = "Vui lòng nhập đầy đủ thông tin.";
                lblErrorLogin.Visible = true;
                return;
            }
            else
            {
                lblErrorLogin.Visible = false;
            }

            if (rbStudent.Checked)
            {
                role = "SinhVien";
            }
            else if (rbManager.Checked)
            {
                role = "QuanLy";
            }

            if (string.IsNullOrEmpty(role))
            {
                lblErrorRole.Text = "Vui lòng chọn vai trò đăng nhập.";
                lblErrorRole.ForeColor = Color.Red;
                lblErrorRole.Visible = true;
                return;
            }



            string connStr = "Data Source=DESKTOP-I4D6NFT\\SQLEXPRESS;" +
              "Initial Catalog=QuanLySinhVien;" +
              "Integrated Security=True;" +
              "Encrypt=True;" +
              "TrustServerCertificate=True;";

            string sql = @"select count(*)
                            from TaiKhoan
                            where TenDangNhap = @username
                            and MatKhau = @password
                            and VaiTro = @role";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@role", role);
                try
                {
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count == 0)
                    {
                        lblErrorLogin.Text = "Tên đăng nhập hoặc mật khẩu không chính xác!";
                        lblErrorLogin.ForeColor = Color.Red;
                        lblErrorLogin.Visible = true;
                    }
                    else
                    {
                        // Đăng nhập thành công
                        MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        
                        if (role == "SinhVien")
                        {
                            // Truyền Tên Đăng Nhập (username) vào Constructor của Form Sinh Viên
                            frmMainStudent mainStudent = new frmMainStudent(username);
                            mainStudent.ShowDialog();
                            
                        }
                        else if (role == "QuanLy")
                        {
                            // Truyền Tên Đăng Nhập (username) vào Constructor của Form Quản Lý
                            //frmMainManager mainManager = new frmMainStudent(username);
                            frmMainManager mainManager = new frmMainManager(username);
                            mainManager.ShowDialog();

                        }
                        //this.Close();
                        this.Show();
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi kết nối/CSDL
                    lblErrorLogin.Text = "Lỗi hệ thống: Không thể kết nối đến Cơ sở dữ liệu. Vui lòng thử lại.";
                    // Để debug: MessageBox.Show(ex.Message);
                    lblErrorLogin.ForeColor = Color.Red;
                    lblErrorLogin.Visible = true;
                }

            }
        }

        private void rbStudent_CheckedChanged_1(object sender, EventArgs e)
        {
            // Nếu người dùng chọn vai trò thì ẩn label lỗi
            if (rbStudent.Checked)
            {
                lblErrorRole.Visible = false;
            }
        }

        private void rbManager_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbManager.Checked)
            {
                lblErrorRole.Visible = false;
            }
        }
    }
}