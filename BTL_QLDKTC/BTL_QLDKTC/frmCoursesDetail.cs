using BTL_QLDKTC.Utilities;
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

namespace BTL_QLDKTC
{
    public partial class frmCoursesDetail : Form
    {
        //Biến thành viên để lưu trữ tham số truyền vào
        private string _maMonHoc;
        private string _maCTDT;

        //thông tin kỳ học hiện tại
        private string _namHocHienTai;
        private int _hocKyHienTai;

        // 2. Lưu trữ kỳ học được truyền vào (thay vì tính toán lại)
        

        //Biến kết nối và lấy dữ liệu
        SqlConnection conn;
        string strConn;

        // Thêm biến thành viên mới
        private string _maSV;
        private int _soTinChiMonHoc;

        // Hàm này sẽ được gọi khi Form chính mở Form chi tiết
        //public frmCoursesDetail(string maMonHoc, string maCTDT, string maSV, int soTinChi)
        //{
        //    InitializeComponent();

        //    // 1. Lưu trữ tham số
        //    _maMonHoc = maMonHoc;
        //    _maCTDT = maCTDT;
        //    _maSV = maSV; // Gán Mã SV
        //    _soTinChiMonHoc = soTinChi; // Gán Số tín chỉ

        //    // 2. Xác định Năm học/Kỳ học động
        //    var semesterInfo = SemesterDeterminer.GetCurrentSemester();
        //    _namHocHienTai = semesterInfo.NamHoc;
        //    _hocKyHienTai = semesterInfo.HocKy;

        //    // Thiết lập kết nối
        //    strConn = "Data Source=DESKTOP-I4D6NFT\\SQLEXPRESS;" +
        //              "Initial Catalog=QuanLySinhVien;" +
        //              "Integrated Security=True;" +
        //              "Encrypt=True;" +
        //              "TrustServerCertificate=True;";
        //    conn = new SqlConnection(strConn);

        //    // Tải dữ liệu ngay lập tức
        //    LoadCourseDetails();
        //    this.grdDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //}

        // Add this constructor to your frmCoursesDetail class
        public frmCoursesDetail(string maMonHoc, string maCTDT, string maSV, int soTinChi)
        {
            InitializeComponent();

            // 1. Lưu trữ tham số
            _maMonHoc = maMonHoc;
            _maCTDT = maCTDT;
            _maSV = maSV; // Gán Mã SV
            _soTinChiMonHoc = soTinChi; // Gán Số tín chỉ

            // 2. Xác định Năm học/Kỳ học động (CHỈ GỌI MỘT LẦN)
            var semesterInfo = SemesterDeterminer.GetCurrentSemester();
            _namHocHienTai = semesterInfo.NamHoc;
            _hocKyHienTai = semesterInfo.HocKy;

            // Thiết lập kết nối
            strConn = "Data Source=DESKTOP-I4D6NFT\\SQLEXPRESS;" +
                      "Initial Catalog=QuanLySinhVien;" +
                      "Integrated Security=True;" +
                      "Encrypt=True;" +
                      "TrustServerCertificate=True;";
            conn = new SqlConnection(strConn);

            // Thiết lập Selection Mode (Rất quan trọng)
            this.grdDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Tải dữ liệu ngay lập tức
            LoadCourseDetails();
        }

        //Hàm tải chi tiết lớp học phần
        private void LoadCourseDetails()
        {
            // Truy vấn lấy chi tiết LHP theo Mã Môn học, CTDT, và Kỳ học hiện tại
            string sql = @"
                WITH temp_table AS (
                    -- Tính toán số lượng còn trống
                    SELECT lhp.MaLopHP, lhp.GioiHanSL,
                        CASE WHEN lhp.GioiHanSL - COUNT(dk.MaSV) < 0 THEN 0
                             ELSE lhp.GioiHanSL - COUNT(dk.MaSV) END AS SoLuongConTrong,
                        COUNT(dk.MaSV) AS SoLuongDaDangKy
                    FROM LopHocPhan lhp 
                    LEFT JOIN DangKy dk ON lhp.MaLopHP = dk.MaLopHP 
                    GROUP BY lhp.MaLopHP, lhp.GioiHanSL
                )
                SELECT 
                    mh.TenMonHoc, mh.SoTinChi,
                    lhp.MaLopHP, 
                    gv.TenGiangVien, 
                    lh.LoaiTiet,
                    lhp.GioiHanSL, 
                    t.SoLuongDaDangKy,
                    t.SoLuongConTrong, 
                    lh.PhongHoc,
                    CONCAT(N'Thứ ', lh.Thu, N' Tiết ', lh.TietBD, N'-', lh.TietKT) AS LichHoc 
                FROM LopHocPhan lhp
                JOIN MonHoc mh ON lhp.MaMonHoc = mh.MaMonHoc
                LEFT JOIN GiangVien gv ON lhp.MaGiangVien = gv.MaGiangVien
                JOIN Lichhoc lh ON lhp.MaLopHP = lh.MaLopHP
                LEFT JOIN temp_table t ON lhp.MaLopHP = t.MaLopHP
                WHERE 
                    lhp.MaMonHoc = @MaMonHoc AND 
                    lhp.MaCTDT = @MaCTDT AND
                    lhp.NamHoc = @NamHoc AND 
                    lhp.HocKy = @HocKy
                ORDER BY lhp.MaLopHP";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Thêm tham số lọc
                cmd.Parameters.AddWithValue("@MaMonHoc", _maMonHoc);
                cmd.Parameters.AddWithValue("@MaCTDT", _maCTDT);
                cmd.Parameters.AddWithValue("@NamHoc", _namHocHienTai);
                cmd.Parameters.AddWithValue("@HocKy", _hocKyHienTai);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // --- BƯỚC 1: Hiển thị Tiêu đề Môn học ---
                if (dt.Rows.Count > 0)
                {
                    string tenMon = dt.Rows[0]["TenMonHoc"].ToString();
                    string soTinChi = dt.Rows[0]["SoTinChi"].ToString();
                    lblTitle.Text = $"Chi tiết lớp học phần môn \"{tenMon}\" (Số tín chỉ: {soTinChi})";
                }

                else
                {
                    lblTitle.Text = $"Không tìm thấy Lớp Học Phần nào cho Mã Môn: {_maMonHoc}";
                    btnRegister.Enabled = false;
                }

                // --- BƯỚC 2: Gán dữ liệu vào DataGridView ---
                grdDetails.AutoGenerateColumns = false;

                // Gán DataPropertyName cho các cột đã định nghĩa trong Designer
                grdDetails.Columns["colLoaiTiet"].DataPropertyName = "LoaiTiet";
                grdDetails.Columns["colLopHP"].DataPropertyName = "MaLopHP";
                grdDetails.Columns["colGV"].DataPropertyName = "TenGiangVien";
                grdDetails.Columns["colGioiHan"].DataPropertyName = "GioiHanSL";
                grdDetails.Columns["colSLConTrong"].DataPropertyName = "SoLuongConTrong";
                grdDetails.Columns["colPhongHoc"].DataPropertyName = "PhongHoc";
                grdDetails.Columns["colLichHoc"].DataPropertyName = "LichHoc"; // Là chuỗi CONCAT

                grdDetails.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra sự cố khi tải chi tiết lớp học phần: " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // BƯỚC 1: LẤY THÔNG TIN LỚP ĐƯỢC CHỌN VÀ KIỂM TRA
            if (grdDetails.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một Lớp Học Phần để đăng ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = grdDetails.SelectedRows[0];
            string selectedMaLopHP = selectedRow.Cells["colLopHP"].Value.ToString();
            // Lấy SL còn trống (Dùng cho kiểm tra UI/thông báo)
            int remainingSeats = Convert.ToInt32(selectedRow.Cells["colSLConTrong"].Value);

            // Kiểm tra nhanh sĩ số (Client-side)
            if (remainingSeats <= 0)
            {
                MessageBox.Show($"Lớp {selectedMaLopHP} đã đầy. Vui lòng chọn lớp khác.", "Không thể Đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // 2. Kiểm tra tất cả các ràng buộc nghiệp vụ
            if (ValidateRegistration(_maSV, selectedMaLopHP, _maMonHoc, _soTinChiMonHoc))
            {
                // 3. Nếu hợp lệ, tiến hành Đăng ký (Giao dịch SQL)
                if (ExecuteRegistrationTransaction(_maSV, selectedMaLopHP))
                {
                    MessageBox.Show("Đăng ký môn học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Báo hiệu về Form cha (frmCourses) rằng đã có thay đổi và đóng Form Modal
                    this.DialogResult = DialogResult.OK;
                    
                    this.Close();
                    
                }
                // else: Lỗi đã được xử lý và thông báo trong hàm ExecuteRegistrationTransaction
            }
            // else: Ràng buộc thất bại (ValidateRegistration đã hiển thị thông báo lỗi)
        }

        //hàm 1: kiểm tra sĩ số hiện tại của lớp học phần
        private bool CheckMaxCapacity(string maLopHP)
        {
            // Cảnh báo: Sử dụng TOP 1 để tối ưu truy vấn (Giả định mỗi LHP chỉ có một dòng)
            string sql = @"
        SELECT 
            (lhp.GioiHanSL - COUNT(dk.MaSV)) AS SoLuongConTrong
        FROM LopHocPhan lhp
        LEFT JOIN DangKy dk ON lhp.MaLopHP = dk.MaLopHP 
        AND dk.TrangThaiDangKy = 'DK'
        WHERE lhp.MaLopHP = @MaLopHP
        GROUP BY lhp.GioiHanSL, lhp.MaLopHP";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLopHP", maLopHP);

                    // ExecuteScalar chỉ trả về giá trị đầu tiên của cột đầu tiên
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        // Không tìm thấy Lớp HP, coi như lỗi
                        MessageBox.Show("Lỗi: Không tìm thấy lớp học phần này trong CSDL.", "Lỗi Dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    int soLuongConTrong = Convert.ToInt32(result);

                    if (soLuongConTrong <= 0)
                    {
                        MessageBox.Show($"Lớp học phần hiện tại đã đầy ({soLuongConTrong} suất còn trống).", "Lỗi Sĩ số", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra sĩ số: " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //hàm 2: kiểm tra sinh viên đã đăng ký môn học trong kỳ chưa
        private bool IsAlreadyRegistered(string maSV, string maMonHoc, string namHoc, int hocKy)
        {
            string sql = @"
        SELECT COUNT(dk.MaSV)
        FROM DangKy dk
        JOIN LopHocPhan lhp ON dk.MaLopHP = lhp.MaLopHP
        WHERE dk.MaSV = @MaSV 
          AND lhp.MaMonHoc = @MaMonHoc 
          AND lhp.NamHoc = @NamHoc 
          AND lhp.HocKy = @HocKy AND dk.TrangThaiDangKy = 'DK'";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSV", maSV);
                cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                cmd.Parameters.AddWithValue("@NamHoc", namHoc);
                cmd.Parameters.AddWithValue("@HocKy", hocKy);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        //hàm 3: kiểm tra sinh viên đã qua môn học chưa
        private bool IsCoursePassed(string maSV, string maMonHoc)
        {
            // Giả định conn là SqlConnection đã được mở và không cần đóng ở đây

            // Truy vấn SQL: Kiểm tra xem sinh viên đã có bất kỳ lần đăng ký nào cho môn này
            // mà TrangThai là "Dat" hay chưa.
            string sql = @"
                SELECT COUNT(dk.MaDK)
                FROM DangKy dk
                JOIN LopHocPhan lhp ON dk.MaLopHP = lhp.MaLopHP
                WHERE dk.MaSV = @MaSV 
                  AND lhp.MaMonHoc = @MaMonHoc 
                  AND dk.TrangThai = 'Dat'
                   ;";
            
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Thêm tham số
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);

                    // ExecuteScalar trả về giá trị đầu tiên của cột đầu tiên (COUNT)
                    int count = (int)cmd.ExecuteScalar();

                    // Nếu count > 0, nghĩa là đã tìm thấy bản ghi "Đạt" cho môn học này
                    return count > 0;
                }
            
            
        }
        //hàm 4: kiểm tra môn tiên quyết
        private bool CheckPrerequisites(string maSV, string maMonHoc)
        {
            // Giả định conn là SqlConnection đã được mở

            // Bước 1: Truy vấn CSDL để lấy tất cả Mã môn tiên quyết bắt buộc
            string sqlGetPrereqs = @"
            SELECT MaMonTienQuyet 
            FROM MonTienQuyet 
            WHERE MaMonHoc = @MaMonHoc;";

            // Danh sách các Mã Môn Tiên Quyết cần phải Đạt
            List<string> requiredPrereqs = new List<string>();

            
                using (SqlCommand cmd = new SqlCommand(sqlGetPrereqs, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requiredPrereqs.Add(reader["MaMonTienQuyet"].ToString());
                        }
                    }
                }

                // Nếu không có môn tiên quyết nào (danh sách rỗng), coi như hợp lệ
                if (requiredPrereqs.Count == 0)
                {
                    return true;
                }

                // Bước 2: Kiểm tra từng môn tiên quyết trong bảng DangKy của sinh viên

                // Tạo một chuỗi SQL để kiểm tra kết quả Đạt cho TẤT CẢ các môn tiên quyết cùng lúc
                // Sử dụng IN (@PrereqsList) là không an toàn vì SQL Injection, ta dùng vòng lặp hoặc tạo chuỗi tham số an toàn.

                // Cách an toàn: Tạo bảng tạm hoặc kiểm tra từng môn (tùy thuộc vào hiệu suất, ta chọn kiểm tra từng môn nếu danh sách ngắn)

                foreach (string prereqMaMon in requiredPrereqs)
                {
                    // Tương tự như hàm IsCoursePassed, nhưng kiểm tra cho môn tiên quyết
                    if (!HasStudentPassed(maSV, prereqMaMon))
                    {
                        MessageBox.Show($"Bạn chưa đạt môn tiên quyết: {prereqMaMon}. Vui lòng hoàn thành môn này trước.",
                                        "Chưa học môn tiên quyết!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }

                return true; // Đã đạt tất cả các môn tiên quyết
            
            
        }
        // Hàm hỗ trợ: Kiểm tra xem sinh viên đã đạt một môn cụ thể hay chưa
        // (Cần định nghĩa hàm này trong frmCoursesDetail.cs)
        private bool HasStudentPassed(string maSV, string maMonHoc)
        {
            // Cần đảm bảo kết nối vẫn đang mở hoặc mở lại nếu bạn không muốn phụ thuộc vào hàm cha.
            // Vì LoadStudentData() gọi các hàm này, ta giả định conn đã mở và sẽ được đóng trong ValidateRegistration().
            string sql = @"
        SELECT COUNT(dk.MaDK)
        FROM DangKy dk
        JOIN LopHocPhan lhp ON dk.MaLopHP = lhp.MaLopHP
        WHERE dk.MaSV = @MaSV 
          AND lhp.MaMonHoc = @MaMonHoc 
          AND dk.TrangThai = 'Dat';";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSV", maSV);
                cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        //hàm 5: kiểm tra giới hạn tín chỉ tối đa
        private bool CheckCreditLimit(string maSV, string namHoc, int hocKy, int soTinChiMoi)
        {
            // Giả định: Giới hạn tín chỉ tối đa cho một học kỳ
            const int MAX_CREDITS = 25;
            int totalRegisteredCredits = 0;

            // Truy vấn SQL: Tính tổng số tín chỉ mà sinh viên đã đăng ký trong kỳ này
            // COUNT(dk.MaDK) ở đây là không đúng, phải là SUM(mh.SoTinChi)
            string sql = @"
            SELECT SUM(mh.SoTinChi) AS TotalCredits
            FROM DangKy dk
            JOIN LopHocPhan lhp ON dk.MaLopHP = lhp.MaLopHP
            JOIN MonHoc mh ON lhp.MaMonHoc = mh.MaMonHoc
            WHERE dk.MaSV = @MaSV 
              AND lhp.NamHoc = @NamHoc 
              AND lhp.HocKy = @HocKy
        AND dk.TrangThaiDangKy = 'DK'";

            // Lưu ý: Đảm bảo kết nối conn đã được mở trước khi gọi hàm này

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    cmd.Parameters.AddWithValue("@NamHoc", namHoc);
                    cmd.Parameters.AddWithValue("@HocKy", hocKy);

                    // ExecuteScalar trả về giá trị SUM
                    object result = cmd.ExecuteScalar();

                    // Xử lý trường hợp không tìm thấy (trả về DBNull hoặc null)
                    if (result != DBNull.Value && result != null)
                    {
                        totalRegisteredCredits = Convert.ToInt32(result);
                    }
                }

                int potentialTotal = totalRegisteredCredits + soTinChiMoi;

                // --- Kiểm tra Ràng buộc ---
                if (potentialTotal > MAX_CREDITS)
                {
                    MessageBox.Show($"Tổng tín chỉ đã đăng ký ({totalRegisteredCredits}) và môn mới ({soTinChiMoi}) là {potentialTotal} tín chỉ. Đã vượt quá giới hạn tối đa cho phép là {MAX_CREDITS} tín chỉ.",
                                    "Lỗi Giới hạn Tín chỉ",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                    return false;
                }

                return true; // Hợp lệ
            
            
        }

       
        // HÀM 6: kiểm tra trùng lịch học
        private bool IsScheduleConflict(string maSV, string newMaLopHP, string namHoc, int hocKy) // <<< BỎ THAM SỐ SqlTransaction
        {
            string sql = @"
        SELECT COUNT(*)
        FROM LichHoc NewLH
        JOIN LopHocPhan NewLHP ON NewLH.MaLopHP = NewLHP.MaLopHP
        JOIN DangKy dk ON dk.MaSV = @MaSV
        JOIN LopHocPhan OldLHP ON dk.MaLopHP = OldLHP.MaLopHP
        JOIN LichHoc OldLH ON OldLHP.MaLopHP = OldLH.MaLopHP
        WHERE 
            NewLHP.MaLopHP = @NewMaLopHP
            AND OldLHP.NamHoc = @NamHoc AND OldLHP.HocKy = @HocKy -- Lọc LHP đã đăng ký
            AND NewLHP.NamHoc = @NamHoc AND NewLHP.HocKy = @HocKy -- Lọc LHP mới (Dùng để kiểm tra dữ liệu đầu vào)
            and dk.TrangThaiDangKy = 'DK'  -- Chỉ kiểm tra các đăng ký còn hiệu lực 
            
            -- ĐIỀU KIỆN XUNG ĐỘT: Cùng Thứ VÀ thời gian chồng chéo
            AND NewLH.Thu = OldLH.Thu 
            AND (
                (NewLH.TietBD <= OldLH.TietKT) 
                AND (NewLH.TietKT >= OldLH.TietBD) 
            )";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn)) // <<< KHÔNG TRUYỀN TRANSACTION
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    cmd.Parameters.AddWithValue("@NewMaLopHP", newMaLopHP);
                    cmd.Parameters.AddWithValue("@NamHoc", namHoc);
                    cmd.Parameters.AddWithValue("@HocKy", hocKy);

                    int conflictCount = (int)cmd.ExecuteScalar();
                    return conflictCount > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra trùng lịch: " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }

        // --- HÀM THỰC HIỆN GIAO DỊCH SQL (TRANSACTION) VÀ GHI DỮ LIỆU ---
        private bool ExecuteRegistrationTransaction(string maSV, string maLopHP)
        {
            // BẮT BUỘC: Sử dụng Giao dịch và Khóa hàng để tránh Race Condition (nhiều người đăng ký suất cuối cùng)
            SqlTransaction transaction = null;
           

            if (conn.State != ConnectionState.Open) conn.Open();
            transaction = conn.BeginTransaction();

            try
            {
                // BƯỚC 1: KIỂM TRA SĨ SỐ LẦN CUỐI VÀ KHÓA HÀNG (Sử dụng UPDLOCK)
                string sqlCheckCapacityAndLock = @"
                    SELECT GioiHanSL - COUNT(dk.MaSV) AS Remaining
                    FROM LopHocPhan lhp WITH (UPDLOCK) -- Khóa hàng để tránh xung đột
                    LEFT JOIN DangKy dk ON lhp.MaLopHP = dk.MaLopHP
                    WHERE lhp.MaLopHP = @MaLopHP
                    GROUP BY lhp.GioiHanSL;
                ";
                using (SqlCommand cmdLock = new SqlCommand(sqlCheckCapacityAndLock, conn, transaction))
                {
                    cmdLock.Parameters.AddWithValue("@MaLopHP", maLopHP);
                    int remaining = Convert.ToInt32(cmdLock.ExecuteScalar() ?? 0);

                    if (remaining <= 0)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lớp đã đầy! Thao tác đăng ký của bạn bị từ chối.", "Lỗi Giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }

                // BƯỚC 2: THỰC HIỆN GHI ĐĂNG KÝ
                string sqlInsert = @"
                    INSERT INTO DangKy (MaSV, MaLopHP, NgayDK, TrangThaiDangKy) 
                    VALUES (@MaSV, @MaLopHP, GETDATE(), 'DK');
                ";
                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, transaction))
                {
                    cmdInsert.Parameters.AddWithValue("@MaSV", maSV);
                    cmdInsert.Parameters.AddWithValue("@MaLopHP", maLopHP);
                    cmdInsert.ExecuteNonQuery();
                }

                // BƯỚC 3: COMMIT GIAO DỊCH
                transaction.Commit();
                return true;
            }

            catch (Exception ex)
            {
                // Nếu có lỗi, ROLLBACK giao dịch
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MessageBox.Show("Đăng ký thất bại do lỗi hệ thống: " + ex.Message, "Lỗi Giao dịch CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private bool ValidateRegistration(string maSV, string selectedMaLopHP, string selectedMaMonHoc, int soTinChiMoi)
        {
            // Lấy thông tin Kỳ học hiện tại từ biến thành viên đã lưu trong Constructor
            string namHoc = _namHocHienTai;
            int hocKy = _hocKyHienTai;

            // Kiểm tra kết nối CSDL
            if (conn.State != ConnectionState.Open)
            {
                try { conn.Open(); }
                catch (Exception)
                {
                    MessageBox.Show("Không thể kết nối CSDL để kiểm tra ràng buộc.", "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            try
            {
                // --- BƯỚC 1: KIỂM TRA SĨ SỐ TỐI ĐA (Kiểm tra lại từ CSDL là tốt nhất) ---
                // Để đơn giản, ta dựa vào việc kiểm tra đã thực hiện trong btnRegister_Click,
                // nơi giá trị còn trống (SoLuongConTrong) đã được kiểm tra > 0.
                // Tuy nhiên, nếu bạn muốn kiểm tra tuyệt đối, bạn sẽ cần truy vấn lại tại đây.
                // Giả định nếu code chạy tới đây, số lượng còn trống ban đầu là > 0.
                if (!CheckMaxCapacity(selectedMaLopHP))
                {
                    // Thông báo lỗi đã được xử lý trong hàm CheckMaxCapacity
                    return false;
                }

                // --- BƯỚC 2: KIỂM TRA ĐÃ ĐĂNG KÝ TRONG KỲ CHƯA (Trùng MaMonHoc) ---
                if (IsAlreadyRegistered(maSV, selectedMaMonHoc, namHoc, hocKy))
                {
                    MessageBox.Show($"Bạn đã đăng ký môn này ({selectedMaMonHoc}) trong kỳ {hocKy}/{namHoc}.", "Đã Đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                // --- BƯỚC 3: KIỂM TRA ĐÃ QUA MÔN (Đã Đạt) CHƯA ---
                if (IsCoursePassed(maSV, selectedMaMonHoc))
                {
                    MessageBox.Show("Bạn đã đạt môn học này và không cần đăng ký lại.", "Đã Đạt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                // --- BƯỚC 4: KIỂM TRA MÔN TIÊN QUYẾT (Pre-requisite) ---
                if (!CheckPrerequisites(maSV, selectedMaMonHoc))
                {
                    // Thông báo chi tiết được xử lý bên trong hàm CheckPrerequisites
                    return false;
                }

                // --- BƯỚC 5: KIỂM TRA GIỚI HẠN TÍN CHỈ TỐI ĐA (Max Credit Limit) ---
                if (!CheckCreditLimit(maSV, namHoc, hocKy, soTinChiMoi))
                {
                    // Thông báo chi tiết được xử lý bên trong hàm CheckCreditLimit
                    return false;
                }

                // --- BƯỚC 6: KIỂM TRA TRÙNG LỊCH HỌC (Schedule Conflict) ---
                if (IsScheduleConflict(maSV, selectedMaLopHP, namHoc, hocKy))
                {
                    MessageBox.Show("Lớp học phần này bị trùng lịch học với môn bạn đã đăng ký. Vui lòng chọn lớp khác.", "Trùng Lịch học", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                return true; // TẤT CẢ CÁC RÀNG BUỘC ĐÃ ĐƯỢC THỎA MÃN
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình kiểm tra ràng buộc: " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                // Đóng kết nối
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        //đóng form
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}