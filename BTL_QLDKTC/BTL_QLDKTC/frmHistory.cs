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
    public partial class frmHistory : Form
    {
        private DataTable _dtAllSemesters;
        private string _maSV;
        SqlConnection conn;
        string strConn = "Data Source=DESKTOP-I4D6NFT\\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        // Constructor nhận Mã SV
        public frmHistory(string maSV)
        {
            InitializeComponent();
            _maSV = maSV;
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(strConn); // Mở kết nối nếu cần

            // 1. Tải các Năm học và Học kỳ có sẵn cho ComboBox (LoadAvailableSemesters đã đảm bảo SelectedIndex=0)
            LoadAvailableSemesters();

            // 2. Tải lịch sử ban đầu theo kỳ mặc định
            if (comNamHoc.Items.Count > 0 && comHocKy.Items.Count > 0)
            {
                string namHoc = comNamHoc.SelectedItem.ToString();
                // SỬA LỖI: Chuyển đổi HocKy (cần dùng hàm ConvertHocKy)
                int hocKy = ConvertHocKy(comHocKy.SelectedItem.ToString());

                LoadHistoryGrid(namHoc, hocKy); // GỌI HÀM LỌC ĐÚNG
            }
        }

        private void LoadHistoryGrid(string namHoc, int hocKy)
        {
            // Giả định _maSV và strConn đã được khai báo và thiết lập
            string maSV = _maSV;

            // SỬA ĐỔI SQL: Lấy các cột đúng với tên bạn đã định nghĩa trong Designer
            string sql = @"
                SELECT 
                    ROW_NUMBER() OVER (ORDER BY dk.MaDK DESC) AS No,
                    dk.MaLopHP,
                    lhp.MaMonHoc AS CourseID,        -- <<< ĐÃ THÊM LẠI
                    mh.TenMonHoc AS CourseName,
                    mh.SoTinChi AS Credits,

                    convert(varchar, dk.NgayDK,120) AS DateTime,
        
                    CASE 
                        WHEN dk.TrangThaiDangKy = 'DK' OR dk.TrangThaiDangKy = 'X' THEN N'Đăng ký'
                        WHEN dk.TrangThaiDangKy = 'H' THEN N'Hủy'
                        ELSE N'---'
                    END AS Action,
        
                    dk.TrangThai AS KetQuaHocTap 
                FROM DangKy dk
                JOIN LopHocPhan lhp ON dk.MaLopHP = lhp.MaLopHP
                JOIN MonHoc mh ON lhp.MaMonHoc = mh.MaMonHoc
                WHERE 
                    dk.MaSV = @MaSV AND 
                    lhp.NamHoc = @NamHoc AND 
                    lhp.HocKy = @HocKy
                ORDER BY dk.MaDK DESC";

            try
            {
                using (SqlConnection tempConn = new SqlConnection(strConn))
                {
                    tempConn.Open();
                    SqlCommand cmd = new SqlCommand(sql, tempConn);

                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    cmd.Parameters.AddWithValue("@NamHoc", namHoc);
                    cmd.Parameters.AddWithValue("@HocKy", hocKy);

                    SqlDataAdapter daHistory = new SqlDataAdapter(cmd);
                    DataTable dtHistory = new DataTable();
                    daHistory.Fill(dtHistory);

                    // Gán dữ liệu
                    grdRegisteredHistory.AutoGenerateColumns = false;
                    grdRegisteredHistory.DataSource = dtHistory;

                    // --- Mapping DataPropertyName ---
                    // Đảm bảo tên cột trong SQL SELECT khớp với DataPropertyName
                    grdRegisteredHistory.Columns["colNo"].DataPropertyName = "No";
                    grdRegisteredHistory.Columns["colAction"].DataPropertyName = "Action";
                    grdRegisteredHistory.Columns["colCourseID"].DataPropertyName = "CourseID";
                    grdRegisteredHistory.Columns["colCourseName"].DataPropertyName = "CourseName";
                    grdRegisteredHistory.Columns["colCredits"].DataPropertyName = "Credits";
                    grdRegisteredHistory.Columns["colDate"].DataPropertyName = "DateTime";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu lịch sử: " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ConvertHocKy(string hocKyString)
        {
            // Vì ComboBox chứa giá trị 1, 2, ta chỉ cần parse thẳng (hoặc xử lý trường hợp không phải số)
            if (int.TryParse(hocKyString, out int hocKy))
            {
                return hocKy;
            }
            return 0; // Trả về 0 nếu không hợp lệ
        }
        
        // TRONG frmHistory.cs

        // Hàm TẢI dữ liệu Năm học và Học kỳ từ lịch sử đăng ký của sinh viên
        private void LoadAvailableSemesters()
        {
            // Cần phải có biến thành viên private DataTable _dtAllSemesters;
            string sql = @"
        SELECT DISTINCT 
            lhp.NamHoc, 
            lhp.HocKy 
        FROM DangKy dk 
        JOIN LopHocPhan lhp ON dk.MaLopHP = lhp.MaLopHP 
        WHERE dk.MaSV = @MaSV 
        ORDER BY lhp.NamHoc DESC, lhp.HocKy DESC";

            try
            {
                using (SqlConnection tempConn = new SqlConnection(strConn)) // Nên dùng tempConn
                {
                    tempConn.Open();
                    SqlCommand cmd = new SqlCommand(sql, tempConn);
                    cmd.Parameters.AddWithValue("@MaSV", _maSV);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Tải dữ liệu vào biến thành viên _dtAllSemesters
                        _dtAllSemesters = new DataTable();
                        _dtAllSemesters.Load(reader);

                    } // 🛑 SqlDataReader ĐÓNG TẠI ĐÂY (End of using block)

                    // 1. Dọn dẹp ComboBox SAU KHI reader ĐÓNG
                    comNamHoc.Items.Clear();
                    comHocKy.Items.Clear();

                    // 2. Xử lý dữ liệu và đổ vào ComboBox
                    if (_dtAllSemesters != null && _dtAllSemesters.Rows.Count > 0)
                    {
                        var distinctNamHoc = _dtAllSemesters.AsEnumerable()
                                            .Select(r => r.Field<string>("NamHoc")).Distinct().ToList();

                        // 3. Đổ dữ liệu vào comNamHoc
                        foreach (string namHoc in distinctNamHoc)
                        {
                            comNamHoc.Items.Add(namHoc);
                        }

                        // 4. Thiết lập kỳ học mới nhất làm mặc định
                        if (comNamHoc.Items.Count > 0)
                        {
                            comNamHoc.SelectedIndex = 0;

                            // Tải Học kỳ tương ứng
                            LoadHocKyByNamHoc(comNamHoc.SelectedItem.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu Năm học/Học kỳ: " + ex.Message, "Sự cố Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // TRONG frmHistory.cs
        private void LoadHocKyByNamHoc(string namHoc)
        {
            comHocKy.Items.Clear();

            if (_dtAllSemesters != null)
            {
                // Lọc các Học kỳ tương ứng với Năm học đã chọn
                var distinctHocKy = _dtAllSemesters.AsEnumerable()
                    .Where(r => r.Field<string>("NamHoc") == namHoc)
                    .Select(r => r.Field<int>("HocKy")).Distinct().ToList();

                // Đổ dữ liệu vào comHocKy
                foreach (int hocKy in distinctHocKy)
                {
                    comHocKy.Items.Add(hocKy.ToString());
                }

                if (comHocKy.Items.Count > 0)
                {
                    comHocKy.SelectedIndex = 0;
                }
            }
        }

        private void btnSearchHistory_Click(object sender, EventArgs e)
        {
            // ... (Kiểm tra null giữ nguyên)
            if (comNamHoc.SelectedItem == null || comHocKy.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Năm học và Học kỳ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy Năm học (string) và Học kỳ (string)
            string namHoc = comNamHoc.SelectedItem.ToString();
            string hocKyString = comHocKy.SelectedItem.ToString();

            // 🛑 SỬA LỖI: Dùng hàm ConvertHocKy đã đơn giản hóa
            int hocKy = ConvertHocKy(hocKyString);

            // ... (Kiểm tra tính hợp lệ của hocKy giữ nguyên)

            // 4. Nếu mọi thứ hợp lệ, tải dữ liệu
            LoadHistoryGrid(namHoc, hocKy);
        }
    }
}
