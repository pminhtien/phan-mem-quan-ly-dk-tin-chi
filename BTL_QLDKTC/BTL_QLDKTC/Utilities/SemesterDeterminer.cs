using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace chứa các tiện ích chung cho ứng dụng
namespace BTL_QLDKTC.Utilities
{
    // dùng để xác định Năm học và Học kỳ hiện tại
    /// <summary>
    /// Lớp tĩnh cung cấp logic để xác định Năm học và Học kỳ hiện tại dựa trên ngày hệ thống.
    /// Giả định: Tháng 7 trở đi là Học kỳ 1 (cho Năm học [Hiện tại]-[Hiện tại + 1]).
    ///           Tháng 1-6 là Học kỳ 2 (cho Năm học [Trước đó]-[Hiện tại]).
    /// </summary>
    public static class SemesterDeterminer
    {
        /// <summary>
        /// Xác định Năm học và Học kỳ hiện tại.
        /// </summary>
        /// <returns>Một Tuple chứa NamHoc (string) và HocKy (int).</returns>
        public static (string NamHoc, int HocKy) GetCurrentSemester()
        {
            //int currentYear = DateTime.Now.Year;
            int currentYear = 2025;
            //int currentMonth = DateTime.Now.Month;
            int currentMonth = 11;

            string namHoc;
            int hocKy;

            // --- Logic Xác định Kỳ học ---

            // Nếu là tháng 7 (Tháng 7, 8, 9, 10, 11, 12): Bắt đầu Học kỳ 1 của năm học mới
            if (currentMonth >= 7)
            {
                // Ví dụ: Tháng 11/2025 -> Năm học 2025-2026, Học kỳ 1
                namHoc = $"{currentYear}-{currentYear + 1}";
                hocKy = 1;
            }
            // Nếu là tháng 1 đến tháng 6: Đang trong Học kỳ 2 của năm học cũ
            else
            {
                // Ví dụ: Tháng 3/2026 -> Năm học 2025-2026, Học kỳ 2
                namHoc = $"{currentYear - 1}-{currentYear}";
                hocKy = 2;
            }

            return (namHoc, hocKy);
        }
    }
}