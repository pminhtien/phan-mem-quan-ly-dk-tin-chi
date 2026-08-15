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
using BTL_QLDKTC.Utilities;
using BTL_QLDKTC.Models;

namespace BTL_QLDKTC
{
    public partial class frmCourses : Form
    {
        private System.Windows.Forms.Timer statusTimer;
        const int MAX_CREDITS = 25; // Giới hạn tín chỉ tối đa
        //biến kết nối và lấy dữ liệu
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
        string strConn, sql;

        //biến lưu thông tin sinh viên hiện tại
        private SinhVienInfo currentStudent;

        // Trong frmCourses.cs
        // Khai báo biến để lưu tham chiếu đến hàng (row) cuối cùng được chọn
        private DataGridViewRow lastClickedRow = null;

        // Khai báo màu mặc định (0, 0, 255 - Blue)
        private readonly Color DefaultLinkColor = Color.FromArgb(0, 0, 255);

        //Constructor chính
        public frmCourses(SinhVienInfo svInfo)
        {
            InitializeComponent();

            //1. Lưu trữ thông tin sinh viên
            this.currentStudent = svInfo;

            //2. Xác định Năm học và Học kỳ hiện tại
            var SemesterInfo = SemesterDeterminer.GetCurrentSemester();
            string namHoc = SemesterInfo.NamHoc;
            int hocKy = SemesterInfo.HocKy;

            //3. Hiển thị thông tin Năm học và Học kỳ trên Label
            lblSemester.Text = $"Năm học {namHoc} - Học kỳ HK0{hocKy}";

            //Hiển thị chương trình đào tạo của sinh viên
            comCTDT.Items.Clear();
            comCTDT.Items.Add(svInfo.TenCTDT);
            comCTDT.SelectedIndex = 0;
            //comCTDT.Enabled = false;

            //4. Gọi hàm tải dữ liệu với các tham số xác định
            LoadCoursesByCTDT(namHoc, hocKy);
            LoadCoursesByCTDT(namHoc, hocKy); // Tải lưới môn học
            LoadRegisteredCourses(namHoc, hocKy); // Tải lưới đã đăng ký và TÍNH TỔNG HỢP

            // Khởi tạo Timer và thiết lập thời gian
            statusTimer = new System.Windows.Forms.Timer();
            statusTimer.Interval = 3000; // Ẩn sau 3 giây (3000 milliseconds)
            statusTimer.Tick += StatusTimer_Tick;

        }
        //Hàm xử lý sự kiện Timer Tick để ẩn thông báo
        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            // Khi Timer kết thúc (sau 3 giây), dừng Timer và ẩn thông báo
            statusTimer.Stop();
            lblStatusMessage.Text = ""; // Xóa nội dung
            lblStatusMessage.Visible = false;
        }
        private void ShowTemporaryMessage(string message, Color backColor)
        {
            lblStatusMessage.Text = message;
            lblStatusMessage.BackColor = backColor;
            lblStatusMessage.Visible = true;

            // Đảm bảo dừng mọi Timer cũ và khởi động lại
            statusTimer.Stop();
            statusTimer.Start();
        }

        // Hàm tải dữ liệu lớp học phần, lọc theo CTDT, Năm học và Học kỳ
        private void LoadCoursesByCTDT(string namHoc, int hocKy)
        {
            string maCTDT = this.currentStudent.MaCTDT;

            strConn = "Data Source=DESKTOP-I4D6NFT\\SQLEXPRESS;" +
                      "Initial Catalog=QuanLySinhVien;" +
                      "Integrated Security=True;" +
                      "Encrypt=True;" +
                      "TrustServerCertificate=True;";

            conn = new SqlConnection(strConn);

            // Truy vấn SQL: Lấy các LHP thuộc CTDT, Năm học, Học kỳ hiện tại, và tính số lượng còn trống
            //string sql = @"
            //    WITH temp_table AS (
            //        SELECT lhp.MaLopHP, lhp.GioiHanSL,
            //            CASE 
            //                WHEN lhp.GioiHanSL - COUNT(dk.MaSV) < 0 THEN 0
            //                ELSE lhp.GioiHanSL - COUNT(dk.MaSV)
            //            END AS SoLuongConTrong
            //        FROM LopHocPhan lhp 
            //        LEFT JOIN DangKy dk ON lhp.MaLopHP = dk.MaLopHP 
            //        GROUP BY lhp.MaLopHP, lhp.GioiHanSL
            //    )
            //    SELECT 
            //        lhp.MaMonHoc, mh.TenMonHoc, mh.SoTinChi, gv.TenGiangVien, lhp.MaLopHP, lh.LoaiTiet, lhp.GioiHanSL,
            //        t.SoLuongConTrong, lh.PhongHoc,
            //        CONCAT(N'Thứ ', lh.Thu, N' Tiết ', lh.TietBD, N'-', lh.TietKT) AS LichHoc 
            //    FROM LopHocPhan lhp
            //    JOIN MonHoc mh ON lhp.MaMonHoc = mh.MaMonHoc
            //    LEFT JOIN GiangVien gv ON lhp.MaGiangVien = gv.MaGiangVien
            //    JOIN Lichhoc lh ON lhp.MaLopHP = lh.MaLopHP
            //    LEFT JOIN temp_table t ON lhp.MaLopHP = t.MaLopHP
            //    WHERE 
            //        lhp.MaCTDT = @MaCTDT AND 
            //        lhp.NamHoc = @NamHoc AND 
            //        lhp.HocKy = @HocKy";
            string sql = @"
                with temp_table AS (
                SELECT  
                    lhp.MaMonHoc,
                    mh.TenMonHoc,
                    mh.SoTinChi,
                    COUNT(lhp.MaLopHP) AS SoLopHocPhanDangMo  -- <<< Cột mới đếm số lượng LHP
                FROM LopHocPhan lhp
                JOIN MonHoc mh ON lhp.MaMonHoc = mh.MaMonHoc
                WHERE 
                    lhp.MaCTDT = @MaCTDT AND 
                    lhp.NamHoc = @NamHoc AND 
                    lhp.HocKy = @HocKy
                GROUP BY lhp.MaMonHoc, mh.TenMonHoc, mh.SoTinChi -- <<< Nhóm theo Môn học
                )
                select 
                    ROW_NUMBER() OVER (ORDER BY tt.MaMonHoc) AS STT,
                    tt.MaMonHoc,
                    tt.TenMonHoc,
                    tt.SoTinChi,
                    tt.SoLopHocPhanDangMo
                from temp_table tt";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Thêm tham số lọc SQL (MaCTDT, NamHoc, HocKy)
                cmd.Parameters.AddWithValue("@MaCTDT", maCTDT);
                cmd.Parameters.AddWithValue("@NamHoc", namHoc);
                cmd.Parameters.AddWithValue("@HocKy", hocKy);

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                // Gán dữ liệu cho DataGridView
                grdCourses.AutoGenerateColumns = false;
                grdCourses.Columns["col1No"].DataPropertyName = "STT";
                grdCourses.Columns["col1CourseID"].DataPropertyName = "MaMonHoc";
                grdCourses.Columns["col1CourseName"].DataPropertyName = "TenMonHoc";
                grdCourses.Columns["col1Credits"].DataPropertyName = "SoTinChi";
                grdCourses.Columns["col1OpenClasses"].DataPropertyName = "SoLopHocPhanDangMo";

                //grdCourses.Columns["col1Lecturer"].DataPropertyName = "TenGiangVien";
                //grdCourses.Columns["col1ClassID"].DataPropertyName = "MaLopHP";
                //grdCourses.Columns["col1ClassType"].DataPropertyName = "LoaiTiet";
                //grdCourses.Columns["col1Capacity"].DataPropertyName = "GioiHanSL";
                //grdCourses.Columns["col1AvailableSeats"].DataPropertyName = "SoLuongConTrong";
                //grdCourses.Columns["col1Room"].DataPropertyName = "PhongHoc";
                //grdCourses.Columns["col1Schedule"].DataPropertyName = "LichHoc";
                grdCourses.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lớp học phần: " + ex.Message, "Không thể tải thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void frmCourses_Load(object sender, EventArgs e)
        {
            //this.grdCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        
        private void btnChoose_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có hàng nào được chọn hay không
            if (grdCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một môn học để xem chi tiết.", "Chưa chọn môn học", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Lấy hàng đang được chọn (chỉ lấy hàng đầu tiên nếu chọn nhiều)
                DataGridViewRow selectedRow = grdCourses.SelectedRows[0];

                // 3. Lấy Mã Môn học từ DataPropertyName
                // Cần đảm bảo rằng cột MaMonHoc (col1CourseID) đã được tải dữ liệu
                //string selectedMaMonHoc = selectedRow.Cells["col1CourseID"].Value.ToString();
                //string maCTDT = this.currentStudent.MaCTDT;
                //string maSV = this.currentStudent.MaSV;
                //var SemesterInfo = SemesterDeterminer.GetCurrentSemester();
                //string namHoc = SemesterInfo.NamHoc;
                //int hocKy = SemesterInfo.HocKy;

                // Get required values from the selected row
                string selectedMaMonHoc = selectedRow.Cells["col1CourseID"].Value.ToString();
                string maCTDT = this.currentStudent.MaCTDT;
                string maSV = this.currentStudent.MaSV;
                var SemesterInfo = SemesterDeterminer.GetCurrentSemester();
                string namHoc = SemesterInfo.NamHoc;
                int hocKy = SemesterInfo.HocKy;

                // Get SoTinChi from the selected row
                int soTinChi = Convert.ToInt32(selectedRow.Cells["col1Credits"].Value);

                // 4. Tạo Form chi tiết và truyền tham số (MaMonHoc, MaCTDT, MaSV, NamHoc, HocKy)
                frmCoursesDetail detailForm = new frmCoursesDetail(
                    selectedMaMonHoc,
                    maCTDT,
                    maSV,
                    soTinChi
                );

                // Mở Form Modal
                detailForm.ShowDialog();
                // 2. Tải lại danh sách môn học/lớp HP có sẵn
                LoadCoursesByCTDT(namHoc, hocKy);

                // 3. Tải lại danh sách môn đã đăng ký và cập nhật tổng hợp
                LoadRegisteredCourses(namHoc, hocKy);

                // Sau khi Form chi tiết đóng, bạn có thể muốn tải lại lưới grdCourses nếu có thay đổi
                // Tải lại lưới môn học
                // LoadCoursesByCTDT(namHoc, hocKy); // Cần gọi lại với tham số kỳ học hiện tại

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xem chi tiết môn học: " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRegisteredCourses(string namHoc, int hocKy)
        {
            // Lấy mã SV từ biến đã lưu
            string maSV = this.currentStudent.MaSV;

            // Truy vấn SQL: Lấy các LHP sinh viên đã đăng ký trong kỳ hiện tại
            string sql = @"
        SELECT 
            ROW_NUMBER() OVER (ORDER BY dk.MaDK) AS STT,
            mh.TenMonHoc,
            lhp.MaLopHP,
            mh.SoTinChi,
            gv.TenGiangVien,
            CONCAT(N'Thứ ', lh.Thu, N' Tiết ', lh.TietBD, N'-', lh.TietKT) AS LichHoc,
            dk.TrangThai -- Thêm trạng thái (Đạt, Trượt, NULL)
        FROM DangKy dk
        JOIN LopHocPhan lhp ON dk.MaLopHP = lhp.MaLopHP
        JOIN MonHoc mh ON lhp.MaMonHoc = mh.MaMonHoc
        LEFT JOIN GiangVien gv ON lhp.MaGiangVien = gv.MaGiangVien
        JOIN LichHoc lh ON lhp.MaLopHP = lh.MaLopHP
        WHERE 
            dk.MaSV = @MaSV AND 
            lhp.NamHoc = @NamHoc AND 
            lhp.HocKy = @HocKy and 
            dk.TrangThaiDangKy = 'DK'
        ORDER BY STT";

            try
            {
                // Sử dụng using để đảm bảo đối tượng được giải phóng (conn đã được khai báo ở trên)
                using (SqlConnection tempConn = new SqlConnection(strConn))
                {
                    tempConn.Open();
                    SqlCommand cmd = new SqlCommand(sql, tempConn);

                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    cmd.Parameters.AddWithValue("@NamHoc", namHoc);
                    cmd.Parameters.AddWithValue("@HocKy", hocKy);

                    SqlDataAdapter daRegistered = new SqlDataAdapter(cmd);
                    DataTable dtRegistered = new DataTable();
                    daRegistered.Fill(dtRegistered);

                    // Gán dữ liệu cho grdRegisteredCourses
                    grdRegisteredCourses.AutoGenerateColumns = false;
                    grdRegisteredCourses.DataSource = dtRegistered;

                    // Mapping DataPropertyName (Dựa trên Designer của bạn)
                    grdRegisteredCourses.Columns["col2TT"].DataPropertyName = "STT";
                    grdRegisteredCourses.Columns["col2TenMon"].DataPropertyName = "TenMonHoc";
                    grdRegisteredCourses.Columns["col2MaLopHP"].DataPropertyName = "MaLopHP";
                    grdRegisteredCourses.Columns["col2STC"].DataPropertyName = "SoTinChi";
                    grdRegisteredCourses.Columns["col2GV"].DataPropertyName = "TenGiangVien";
                    grdRegisteredCourses.Columns["col2LichHoc"].DataPropertyName = "LichHoc";
                    // Cột trạng thái (Chưa có trong designer của bạn, nhưng cần thiết)
                    // grdRegisteredCourses.Columns["col2TrangThai"].DataPropertyName = "TrangThai"; 

                    // Cập nhật tóm tắt sau khi tải dữ liệu
                    UpdateRegistrationSummary(dtRegistered);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lớp đã đăng ký: " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRegistrationSummary(DataTable dtRegistered)
        {
            int totalCredits = 0;
            int totalCourses = dtRegistered.Rows.Count;

            // Tính tổng số tín chỉ
            foreach (DataRow row in dtRegistered.Rows)
            {
                // Đảm bảo cột SoTinChi (tên là "SoTinChi" trong SQL SELECT) là số nguyên
                if (int.TryParse(row["SoTinChi"].ToString(), out int credits))
                {
                    totalCredits += credits;
                }
            }

            // Hiển thị kết quả lên lblRegisteredResult
            lblRegisteredResult.Text = $"Kết quả đăng ký: {totalCourses} môn học, {totalCredits} tín chỉ";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy Năm học và Học kỳ hiện tại
                var semesterInfo = SemesterDeterminer.GetCurrentSemester();
                string namHoc = semesterInfo.NamHoc;
                int hocKy = semesterInfo.HocKy;

                // 2. Tải lại danh sách môn học/lớp HP có sẵn
                LoadCoursesByCTDT(namHoc, hocKy);

                // 3. Tải lại danh sách môn đã đăng ký và cập nhật tổng hợp
                LoadRegisteredCourses(namHoc, hocKy);

                //MessageBox.Show("Dữ liệu đã được cập nhật thành công!", "Làm mới thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // THAY THẾ MESSAGEBOX BẰNG THÔNG BÁO TẠM THỜI
                ShowTemporaryMessage("✅ Dữ liệu đã được cập nhật thành công!", Color.LightGreen);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Không thể làm mới dữ liệu: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // THAY THẾ MESSAGEBOX BẰNG THÔNG BÁO LỖI TẠM THỜI
                ShowTemporaryMessage("❌ Lỗi hệ thống: Không thể làm mới dữ liệu.", Color.LightCoral);
            }
        }

        

        private void grdCourses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đảm bảo chỉ xử lý khi nhấp đúp vào một hàng dữ liệu hợp lệ (không phải hàng tiêu đề)
            if (e.RowIndex < 0) return;

            try
            {
                // 1. Lấy dữ liệu cần thiết từ hàng được nhấp đúp
                DataGridViewRow selectedRow = grdCourses.Rows[e.RowIndex];

                // Lấy các tham số cần thiết
                string selectedMaMonHoc = selectedRow.Cells["col1CourseID"].Value.ToString();

                // Lấy thông tin Sinh viên (Giả định this.currentStudent đã được khởi tạo)
                // Thay thế bằng cách lấy từ biến thành viên của Form cha
                string maCTDT = this.currentStudent.MaCTDT;
                string maSV = this.currentStudent.MaSV;

                // Lấy Số Tín chỉ 
                int soTinChi = Convert.ToInt32(selectedRow.Cells["col1Credits"].Value);

                // Lấy Năm học và Học kỳ hiện tại (nên được tính toán hoặc lấy từ biến thành viên)
                var SemesterInfo = SemesterDeterminer.GetCurrentSemester();
                string namHoc = SemesterInfo.NamHoc;
                int hocKy = SemesterInfo.HocKy;

                // 2. Tạo Form chi tiết và truyền tham số
                frmCoursesDetail detailForm = new frmCoursesDetail(
                        selectedMaMonHoc,
                        maCTDT,
                        maSV,
                        soTinChi
                );

                // 3. Mở Form MODAL và kiểm tra kết quả
                DialogResult dialogResult = detailForm.ShowDialog();

                // 4. Tải lại dữ liệu nếu có đăng ký thành công
                if (dialogResult == DialogResult.OK)
                {
                    // Tải lại lưới môn học tổng quan và lưới đã đăng ký
                    LoadCoursesByCTDT(namHoc, hocKy); // Cập nhật sĩ số lớp có sẵn
                    LoadRegisteredCourses(namHoc, hocKy); // Tải lại lưới đã đăng ký

                    // THÔNG BÁO TẠM THỜI: (Nếu bạn đã cài đặt hàm ShowTemporaryMessage)
                    // ShowTemporaryMessage("Đã đăng ký thành công. Dữ liệu đã được làm mới.", Color.LightGreen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở chi tiết hoặc tải dữ liệu (Nhấp đúp): " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //sử dụng khi cột cuối là đăng ký ( link )
        //private void grdCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    // Đảm bảo chỉ xử lý khi nhấp vào cột Link và không phải hàng tiêu đề (header)
        //    // Giả định tên cột Link của bạn trong Designer là "col1Register"
        //    if (e.ColumnIndex == grdCourses.Columns["col1Register"].Index && e.RowIndex >= 0)
        //    {
        //        try
        //        {
        //            // 1. Lấy dữ liệu cần thiết từ hàng được click
        //            DataGridViewRow selectedRow = grdCourses.Rows[e.RowIndex];
        //            DataGridViewCell linkCell = selectedRow.Cells["col1Register"];

        //            // Lưu trữ hàng này để sử dụng sau khi Form con đóng
        //            lastClickedRow = selectedRow;

        //            // Lấy các tham số cần thiết
        //            string selectedMaMonHoc = selectedRow.Cells["col1CourseID"].Value.ToString();
        //            string maCTDT = this.currentStudent.MaCTDT;
        //            string maSV = this.currentStudent.MaSV;

        //            // Lấy Số Tín chỉ 
        //            int soTinChi = Convert.ToInt32(selectedRow.Cells["col1Credits"].Value);

        //            // Lấy Năm học và Học kỳ hiện tại (cần thiết cho logic tải lại)
        //            var SemesterInfo = SemesterDeterminer.GetCurrentSemester();
        //            string namHoc = SemesterInfo.NamHoc;
        //            int hocKy = SemesterInfo.HocKy;

        //            // 2. Tạo Form chi tiết và truyền 4 tham số lõi
        //            frmCoursesDetail detailForm = new frmCoursesDetail(
        //                selectedMaMonHoc,
        //                maCTDT,
        //                namHoc,
        //                hocKy,
        //                maSV,
        //                soTinChi
        //            );

        //            // 3. Mở Form MODAL và kiểm tra kết quả
        //            DialogResult dialogResult = detailForm.ShowDialog();
        //            // 4. SAU KHI FORM CHI TIẾT ĐÓNG: Khôi phục màu sắc

        //            linkCell.Style.ForeColor = DefaultLinkColor;
        //            // 6. Tải lại dữ liệu nếu có đăng ký thành công
        //            //if (dialogResult == DialogResult.OK)
        //            //{
        //                LoadCoursesByCTDT(namHoc, hocKy);
        //            //    // LoadRegisteredCourses(namHoc, hocKy); 
        //            //}
        //        }
        //        catch (Exception ex)
        //        {
        //            // Đảm bảo khôi phục màu cho đường link nếu có lỗi xảy ra
        //            if (lastClickedRow != null)
        //            {
        //                lastClickedRow.Cells["col1Register"].Style.ForeColor = DefaultLinkColor;
        //            }
        //            MessageBox.Show("Lỗi khi mở chi tiết hoặc tải dữ liệu: " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //}
        //}
        // TRONG frmCourses.cs

        private bool ExecuteCancelTransaction(string maSV, string maLopHP)
        {
            SqlTransaction transaction = null;

            using (SqlConnection tempConn = new SqlConnection(strConn)) // Sử dụng connection string strConn
            {
                try
                {
                    tempConn.Open();
                    transaction = tempConn.BeginTransaction();

                    // 1. VÔ HIỆU HÓA bản ghi ĐĂNG KÝ đang hoạt động ('DK' -> 'X')
                    string sqlDeactivate = @"
                UPDATE DangKy
                SET TrangThaiDangKy = 'X' 
                WHERE MaSV = @MaSV AND MaLopHP = @MaLopHP AND TrangThaiDangKy = 'DK';
            ";
                    using (SqlCommand cmdDeactivate = new SqlCommand(sqlDeactivate, tempConn, transaction))
                    {
                        cmdDeactivate.Parameters.AddWithValue("@MaSV", maSV);
                        cmdDeactivate.Parameters.AddWithValue("@MaLopHP", maLopHP);
                        if (cmdDeactivate.ExecuteNonQuery() == 0)
                        {
                            // Không tìm thấy bản ghi đang 'DK', rollback và thoát
                            transaction.Rollback();
                            MessageBox.Show("Không tìm thấy đăng ký có hiệu lực để hủy.", "Lỗi Dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // 2. CHÈN bản ghi HỦY ('H') mới vào làm LOG
                    string sqlInsertLog = @"
                INSERT INTO DangKy (MaSV, MaLopHP, NgayDK, TrangThaiDangKy)
                VALUES (@MaSV, @MaLopHP, GETDATE(), 'H'); 
            ";
                    using (SqlCommand cmdInsertLog = new SqlCommand(sqlInsertLog, tempConn, transaction))
                    {
                        cmdInsertLog.Parameters.AddWithValue("@MaSV", maSV);
                        cmdInsertLog.Parameters.AddWithValue("@MaLopHP", maLopHP);
                        cmdInsertLog.ExecuteNonQuery();
                    }

                    // 3. COMMIT giao dịch
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback(); // Hoàn tác nếu có lỗi
                    }
                    MessageBox.Show("Hủy đăng ký thất bại: " + ex.Message, "Lỗi Giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            } // Connection tự đóng/giải phóng sau khối using
        }
        private void grdRegisteredCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Kiểm tra xem người dùng có click vào cột "Xóa" và không phải hàng tiêu đề không
            if (e.ColumnIndex == grdRegisteredCourses.Columns["col2Xoa"].Index && e.RowIndex >= 0)
            {
                // Yêu cầu xác nhận từ người dùng
                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn hủy đăng ký lớp học phần này không?",
                    "Xác nhận Hủy Đăng ký",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow row = grdRegisteredCourses.Rows[e.RowIndex];

                        // Lấy các tham số cần thiết
                        string maLopHP = row.Cells["col2MaLopHP"].Value.ToString();
                        string maSV = this.currentStudent.MaSV; // Lấy Mã SV đang đăng nhập

                        // Lấy thông tin kỳ học hiện tại (cho logic tải lại)
                        var semesterInfo = SemesterDeterminer.GetCurrentSemester();
                        string namHoc = semesterInfo.NamHoc;
                        int hocKy = semesterInfo.HocKy;

                        // 2. Thực hiện giao dịch Hủy
                        if (ExecuteCancelTransaction(maSV, maLopHP))
                        {
                            MessageBox.Show("Hủy đăng ký thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 3. Tải lại dữ liệu để phản ánh thay đổi
                            LoadCoursesByCTDT(namHoc, hocKy); // Cập nhật sĩ số lớp có sẵn
                            LoadRegisteredCourses(namHoc, hocKy); // Tải lại lưới đã đăng ký
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi hủy đăng ký: " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}