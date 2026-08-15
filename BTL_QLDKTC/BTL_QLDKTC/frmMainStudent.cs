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
using BTL_QLDKTC.Models;
using System.Security.Cryptography;
using BTL_QLDKTC.Utilities;


namespace BTL_QLDKTC
{
    public partial class frmMainStudent : Form
    {
        // 1. Khai báo biến Private để lưu trữ Tên đăng nhập và thông tin sinh viên
        //private string _msv; //tên đăng nhập = mã sinh viên
        private SinhVienInfo loggedInStudent; //<<< BIẾN MỚI: Lưu trữ toàn bộ thông tin SV


        //fields
        private Form activeForm;

        //constructor 
        // Constructor Mặc định (giữ lại nếu cần) -> dùng khi không truyền tham số
        public frmMainStudent()
        {
            InitializeComponent();
        }

        // 2. Constructor Mới: Chấp nhận tên đăng nhập làm tham số
        public frmMainStudent(string maSV) : this()
        {
            // Gán giá trị tên đăng nhập được truyền vào cho biến cục bộ
            //_msv = username;
            LoadStudentData(maSV);
        }

        private void LoadStudentData(string maSV)
        {
            // 1. Kết nối CSDL
            string connStr = "Data Source=DESKTOP-I4D6NFT\\SQLEXPRESS;" +
              "Initial Catalog=QuanLySinhVien;" +
              "Integrated Security=True;" +
              "Encrypt=True;" +
              "TrustServerCertificate=True;";

            //2. truy vấn lấy tên và msv từ bảng sinh viên
            string sql = @"SELECT sv.MaSV, sv.TenSV, sv.MaCTDT, ctdt.TenCTDT
                            FROM SinhVien sv
                            JOIN ChuongTrinhDT ctdt ON sv.MaCTDT = ctdt.MaCTDT
                            WHERE sv.MaSV = @MaSV"; //truy vấn có tham số
            using (SqlConnection conn = new SqlConnection(connStr))

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSV", maSV);

                try
                {
                    conn.Open();
                    //sử dụng SqlDataReader để đọc dữ liệu
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //3. Lấy dữ liệu và gán vào label trên form
                            //string maSV = reader["MaSV"].ToString();
                            string tenSV = reader["TenSV"].ToString();
                            string maCTDT = reader["MaCTDT"].ToString();
                            string tenCTDT = reader["TenCTDT"].ToString();


                            //4. Gán vào label trên form
                            lblStudentID.Text = "MSV: " + maSV;
                            lblStudentName.Text = tenSV;

                            //5. Lưu trữ vào đối tượng LoggedInStudent
                            loggedInStudent = new SinhVienInfo
                            {
                                MaSV = maSV,
                                TenSV = tenSV,
                                MaCTDT = maCTDT,
                                TenCTDT = tenCTDT // Lưu trữ Tên CTDT
                            };
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên tương ứng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }
                }
                catch (Exception ex)
                {
                    //Xu ly loi ket noi CSDL
                    MessageBox.Show("Lỗi CSDL khi tải thông tin: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    loggedInStudent = null;
                }
            }

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        // Đặt lại trạng thái của tất cả các nút về mặc định
        private void ResetButtonStates()
        {
            // Đặt tất cả 3 nút về trạng thái màu mặc định (ví dụ: nền trắng, chữ đen)
            //Color defaultColor = Color.Transparent; // Hoặc Color.White
            Color defaultColor = panelMenu.BackColor;

            btnRegister.BackColor = defaultColor;
            btnHistory.BackColor = defaultColor;
            btnHelp.BackColor = defaultColor;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (loggedInStudent == null)
            {
                MessageBox.Show("Không thể tải thông tin sinh viên để đăng ký. Vui lòng thử lại.", "Lỗi Dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenChildForm(new frmCourses(loggedInStudent), sender);
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            // Kiểm tra để đảm bảo dữ liệu sinh viên đã được tải thành công
            if (this.loggedInStudent == null)
            {
                MessageBox.Show("Không thể tải thông tin sinh viên. Vui lòng thử đăng nhập lại.", "Lỗi Dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Khai báo và truyền Mã SV vào Constructor của frmHistory
            // Mã SV được lấy từ đối tượng đã lưu trữ (this.loggedInStudent)
            frmHistory historyForm = new frmHistory(this.loggedInStudent.MaSV);

            // Mở Form con
            OpenChildForm(historyForm, sender);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHelp(), sender);
        }

        //dùng để mở form con bên trong panel body
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                // Bước 1: Đặt lại trạng thái của TẤT CẢ các nút về MẶC ĐỊNH
                ResetButtonStates();

                // Bước 2: Nổi bật CHỈ nút vừa được click
                // 'btnSender' là đối tượng (Button) đã kích hoạt sự kiện này
                Button clickedButton = btnSender as Button;

                // Kiểm tra để đảm bảo nó là một Button
                if (clickedButton != null)
                {
                    // Thiết lập màu nổi bật cho nút vừa được bấm
                    clickedButton.BackColor = Color.FromArgb(43, 87, 154); // Ví dụ: Màu xanh đậm
                    //clickedButton.BackColor = Color.FromArgb(0, 120, 215); // Ví dụ: Màu xanh dương
                    
                }

                // *Tùy chọn: Gọi hàm chuyển đổi UserControl tại đây
                // ChangeUserControl(clickedButton.Name); 
            }
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            // Đóng form con hiện tại nếu có
            if (activeForm != null) {
                activeForm.Close();
            }
            ActivateButton(btnSender); 
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelBody.Controls.Add(childForm);
            this.panelBody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void frmMainStudent_Load(object sender, EventArgs e)
        {
            
            // 1. Kết nối CSDL
            //string connStr = "Data Source=DESKTOP-I4D6NFT\\SQLEXPRESS;" +
            //  "Initial Catalog=QuanLySinhVien;" +
            //  "Integrated Security=True;" +
            //  "Encrypt=True;" +
            //  "TrustServerCertificate=True;";

            ////2. truy vấn lấy tên và msv từ bảng sinh viên
            //string sql = @"SELECT sv.MaSV, sv.TenSV, sv.MaCTDT, ctdt.TenCTDT
            //                FROM SinhVien sv
            //                JOIN ChuongTrinhDaoTao ctdt ON sv.MaCTDT = ctdt.MaCTDT
            //                WHERE sv.MaSV = @MaSV"; //truy vấn có tham số

            //using (SqlConnection conn = new SqlConnection(connStr))

            //using (SqlCommand cmd = new SqlCommand(sql, conn))
            //{
            //thêm tham số: chống SQL Injection
            //cmd.Parameters.AddWithValue("@MaSV", _msv);
            //try
            //{
            //    conn.Open();
            //    //sử dụng SqlDataReader để đọc dữ liệu
            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        if (reader.Read())
            //        {
            //            //3. Lấy dữ liệu và gán vào label trên form
            //            string maSV = reader["MaSV"].ToString();
            //            string tenSV = reader["TenSV"].ToString();
            //            string maCTDT = reader["MaCTDT"].ToString();
            //            string tenCTDT = reader["TenCTDT"].ToString();


            //            //4. Gán vào label trên form
            //            lblStudentID.Text = "MSV: " + maSV;
            //            lblStudentName.Text = tenSV;

            //            //5. Lưu trữ vào đối tượng LoggedInStudent
            //            loggedInStudent = new SinhVienInfo
            //            {
            //                MaSV = maSV,
            //                TenSV = tenSV,
            //                MaCTDT = maCTDT,
            //                TenCTDT = tenCTDT // Lưu trữ Tên CTDT
            //            };
            //        }
            //        else
            //        {
            //            MessageBox.Show("Không tìm thấy sinh viên tương ứng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //Xu ly loi ket noi CSDL
            //    MessageBox.Show("Lỗi CSDL khi tải thông tin: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //}
        }

        //Đăng xuất 
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // 1. Hỏi người dùng xác nhận
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Xác nhận Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                
                this.Close();

                // 1. Đóng Form hiện tại (frmMainStudent).
                // 2. Vì frmMainStudent được gọi bằng ShowDialog() từ frmLogin,
                // luồng chương trình sẽ tự động trở về frmLogin và hiển thị lại.

                Form loginForm = Application.OpenForms["frmLogin"];

            }
        }
        //Thoát ứng dụng
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Bạn có chắc chắn muốn thoát không?",
            "Xác nhận thoát",
            MessageBoxButtons.YesNoCancel,    // thêm nút Cancel để xử lý khi bấm X
            MessageBoxIcon.Question
            );

            // Kiểm tra kết quả trả về
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {
                // Không thoát, có thể ghi log hoặc giữ nguyên
            }
            else if (result == DialogResult.Cancel)
            {
                // Người dùng bấm "X" hoặc "Cancel" => không làm gì
            }
        }

        
    }
}