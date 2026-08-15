using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_QLDKTC.Models // Đặt trong Namespace chuyên biệt
{
    // Lớp chứa các thông tin tối thiểu cần thiết cho luồng làm việc
    public class SinhVienInfo
    {
        public string MaSV { get; set; }
        public string TenSV { get; set; }
        public string MaCTDT { get; set; }
        public string TenCTDT { get; set; } // Tên CTDT để hiển thị
        // Thêm các thuộc tính khác như LopGoc, NgaySinh, v.v. nếu cần hiển thị
    }
}
