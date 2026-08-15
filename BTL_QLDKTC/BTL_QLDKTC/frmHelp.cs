using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLDKTC
{
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
        }



        //pt1: định dạng tiêu đề
        private void AppendHeading(string text, float fontSize, FontStyle style)
        {
            // Đưa con trỏ về cuối văn bản (để áp dụng định dạng mới)
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;
            //richTextBox1.SelectionColor = Color.SteelBlue;
            //richTextBox1.SelectionColor = Color.Teal;
            //richTextBox1.SelectionColor = Color.RoyalBlue;
            // Thiết lập Font và Style
            //richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, fontSize, style); //1

            richTextBox1.SelectionFont = new Font("Segoe UI", fontSize, style); //2
            // Thêm văn bản, có ngắt dòng
            richTextBox1.AppendText(text + Environment.NewLine);

            // Đặt lại font về mặc định cho văn bản tiếp theo
            richTextBox1.SelectionFont = richTextBox1.Font;
        }

        //pt2: Định dạng Văn bản Thường
        private void AppendNormalText(string text)
        {
            // Đưa con trỏ về cuối văn bản (để áp dụng định dạng mới)
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;

            // Đảm bảo font là font mặc định
            richTextBox1.SelectionFont = richTextBox1.Font;

            richTextBox1.AppendText(text);
        }

        //pt3: Tạo Danh sách Gạch đầu dòng
        private void AppendBulletList(string[] items)
        {
            // Đưa con trỏ về cuối văn bản
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;

            // Bật dấu đầu dòng và đặt lề
            richTextBox1.SelectionBullet = true;
            richTextBox1.SelectionIndent = 24; // Khoảng 24 pixel là lề đẹp
            richTextBox1.SelectionFont = richTextBox1.Font;

            foreach (string item in items)
            {
                // Thêm từng mục (RichTextBox sẽ tự thêm dấu chấm đầu dòng)
                richTextBox1.AppendText(item + Environment.NewLine);
            }

            // Tắt dấu đầu dòng và xóa lề sau khi hoàn tất danh sách
            richTextBox1.SelectionBullet = false;
            richTextBox1.SelectionIndent = 0;

            // Thêm ngắt dòng kép để tạo khoảng trống cho phần tiếp theo
            richTextBox1.AppendText(Environment.NewLine);
        }


        private void LoadRichTextContent()
        {
            // Giả sử tên RichTextBox của bạn là 'richTextBox1'

            // Xóa nội dung cũ
            richTextBox1.Clear();

            // Đảm bảo chế độ chỉnh sửa (nếu cần)
            richTextBox1.ReadOnly = true;

            // --- 1. TIÊU ĐỀ CHÍNH: "Giới thiệu phần mềm" ---
            AppendHeading("  📘 Giới thiệu phần mềm", 16, FontStyle.Bold);
            AppendNormalText("  Tổng quan về hệ thống đăng ký tín chỉ" + Environment.NewLine + Environment.NewLine);

            // --- 2. TIÊU ĐỀ PHỤ: "Về hệ thống" ---
            AppendHeading("  Về hệ thống", 14, FontStyle.Bold);
            AppendNormalText("  Hệ thống đăng ký tín chỉ là một nền tảng trực tuyến giúp sinh viên dễ dàng đăng ký các môn học theo học kỳ. " +
                "\n  Hệ thống được thiết kế với giao diện thân thiện, dễ sử dụng và đảm bảo quy trình đăng ký nhanh chóng, chính xác." + Environment.NewLine + Environment.NewLine);

            // --- 3. TIÊU ĐỀ PHỤ: "Tính năng chính" ---
            AppendHeading("  Tính năng chính", 14, FontStyle.Bold);
            AppendBulletList(new string[]
            {
        "Đăng ký tín chỉ trực tuyến theo học kỳ",
        "Chọn lớp học phần với thời gian biểu rõ ràng",
        "Tự động kiểm tra xung đột lịch học",
        "Hiển thị số tín chỉ đã đăng ký và giới hạn tín chỉ",
        "Xem lịch sử đăng ký tín chỉ các học kỳ trước",
        "Tính toán tự động học phí"
            });

            // --- 4. TIÊU ĐỀ PHỤ: "Lợi ích" ---
            AppendHeading("  Lợi ích", 14, FontStyle.Bold);
            AppendBulletList(new string[]
            {
        "Tiết kiệm thời gian đăng ký",
        "Giảm thiểu sai sót trong quá trình đăng ký",
        "Theo dõi tiến độ học tập dễ dàng",
        "Truy cập mọi lúc, mọi nơi"
            });
        }

        // Đảm bảo các hàm này áp dụng cho richTextBox2
       
        private void AppendHeading2(string text, float fontSize, FontStyle style)
        {
            richTextBox2.SelectionStart = richTextBox2.TextLength;
            richTextBox2.SelectionLength = 0;
            richTextBox2.SelectionFont = new Font("Segoe UI", fontSize, style);

            richTextBox2.AppendText(text + Environment.NewLine);
            richTextBox2.SelectionFont = richTextBox2.Font;
        }

        private void AppendNormalText2(string text)
        {
            richTextBox2.SelectionStart = richTextBox2.TextLength;
            richTextBox2.SelectionLength = 0;
            richTextBox2.SelectionFont = richTextBox2.Font;
            richTextBox2.AppendText(text);
        }

        // Phương thức mới để tạo danh sách bước (Bước 1, Bước 2,...)
        private void AppendNumberedSteps(string[] steps)
        {
            for (int i = 0; i < steps.Length; i++)
            {
                string stepNumber = $"  Bước {i + 1}: ";
                string stepContent = steps[i];

                // 1. In đậm phần "Bước X:"
                richTextBox2.SelectionStart = richTextBox2.TextLength;
                richTextBox2.SelectionLength = 0;
                richTextBox2.SelectionFont = new Font(richTextBox2.Font.FontFamily, richTextBox2.Font.Size, FontStyle.Bold);
                richTextBox2.AppendText(stepNumber);

                // 2. Định dạng thường cho nội dung
                richTextBox2.SelectionStart = richTextBox2.TextLength;
                richTextBox2.SelectionLength = 0;
                richTextBox2.SelectionFont = richTextBox2.Font; // Đặt lại về font thường
                richTextBox2.AppendText(stepContent + Environment.NewLine);
            }
            richTextBox2.AppendText(Environment.NewLine); // Ngắt dòng để cách đoạn
        }
        private void LoadRichTextContent2()
        {
            // Giả sử tên RichTextBox của bạn là 'richTextBox2'
            richTextBox2.Clear();
            richTextBox2.ReadOnly = true;

            // --- 1. TIÊU ĐỀ CHÍNH: "Hướng dẫn sử dụng" ---
            AppendHeading2("  🛠️ Hướng dẫn sử dụng", 16, FontStyle.Bold);
            AppendNormalText2("  Hướng dẫn chi tiết các chức năng của hệ thống" + Environment.NewLine + Environment.NewLine);

            // --- 2. MỤC LỚN: "1. Đăng ký tín chỉ" ---
            AppendHeading2("  1. Đăng ký tín chỉ", 14, FontStyle.Bold);

            // Danh sách các bước (Được định dạng in đậm)
            AppendNumberedSteps(new string[]
            {
                "Chọn chương trình đào tạo và học kỳ muốn đăng ký",
                "Chọn các môn học cần đăng ký bằng cách tick vào ô checkbox",
                "Kiểm tra tổng số tín chỉ (tối thiểu 14, tối đa 35 tín chỉ)",
                "Nhấn nút \"Đăng ký lớp tín chỉ\" để chọn lớp học phần cụ thể",
                "Chọn lớp học phần cho từng môn (hệ thống sẽ tự động kiểm tra xung đột lịch)",
                "Xác nhận đăng ký và hoàn tất"
            });

            // --- 3. MỤC LỚN: "2. Kiểm tra xung đột lịch học" ---
            AppendHeading2(Environment.NewLine + "  2. Kiểm tra xung đột lịch học", 14, FontStyle.Bold); // Thêm ngắt dòng để cách đoạn
            AppendNormalText2("  Hệ thống tự động kiểm tra và cảnh báo nếu bạn chọn các lớp có lịch học trùng nhau. " +
                "\n  Khi chọn lớp mới, nếu có xung đột với lớp đã chọn trước đó, bạn sẽ nhận được thông báo và không thể chọn lớp đó. " +
                "\n  Hãy chọn lớp học phần khác có lịch học phù hợp." + Environment.NewLine);

            // --- 4. MỤC LỚN: "3. Lịch sử đăng ký tín chỉ" ---
            AppendHeading2(Environment.NewLine + "  3. Lịch sử đăng ký tín chỉ", 14, FontStyle.Bold); // Thêm ngắt dòng để cách đoạn
            AppendNormalText2("  Truy cập mục \"Lịch sử đăng ký tín chỉ\" để xem các môn học đã đăng ký trong các học kỳ trước. " +
                "\n  Tại đây bạn có thể xem chi tiết thông tin môn học, số tín chỉ, và trạng thái đăng ký." + Environment.NewLine);

            // --- 5. MỤC LỚN: "4. Lưu ý quan trọng" ---
            AppendHeading2(Environment.NewLine + "  4. Lưu ý quan trọng", 14, FontStyle.Bold); // Thêm ngắt dòng để cách đoạn
            AppendBulletList2_Fallback(new string[]
            {
                "Số tín chỉ tối thiểu: 14 tín chỉ",
                "Số tín chỉ tối đa: 25 tín chỉ",
                "Không được chọn các lớp có lịch học trùng nhau",
                "Kiểm tra kỹ thông tin trước khi xác nhận đăng ký",
                "Liên hệ phòng đào tạo nếu gặp vấn đề kỹ thuật"
            });
        }
        private void frmHelp_Load(object sender, EventArgs e)
        {
            // Ép tạo controls của cả hai tab bằng cách tạm set SelectedIndex
            int originalIndex = tabControl1.SelectedIndex;
            tabControl1.SelectedIndex = 1; // tạo handle cho tabPage2/richTextBox2
            tabControl1.SelectedIndex = originalIndex; // quay về tab ban đầu

            LoadRichTextContent();
            LoadRichTextContent2();
        }

        // Phương thức 4: Tạo Danh sách Gạch đầu dòng cho richTextBox2
        private void AppendBulletList2_Fallback(string[] items)
        {
            // Ép tạo handle
            var h = richTextBox2.Handle;

            // Lưu focus cũ
            Control prev = this.ActiveControl;

            // Đặt caret ở cuối
            richTextBox2.Select(richTextBox2.TextLength, 0);

            // Thiết lập indent: SelectionIndent là tổng lề trái, SelectionHangingIndent là treo
            int indent = 24;
            int hanging = 12;
            richTextBox2.SelectionIndent = indent;
            // Một vài version expose SelectionHangingIndent - nếu không có, ta mô phỏng bằng padding trong chuỗi
            try
            {
                // Nếu property tồn tại
                dynamic dt = richTextBox2;
                dt.SelectionHangingIndent = hanging;
            }
            catch
            {
                // ignore nếu không tồn tại
            }

            // Font cho toàn danh sách
            richTextBox2.SelectionFont = richTextBox2.Font;
            richTextBox2.SelectionColor = Color.Black;

            // Thêm bullet bằng ký tự '•' + một space
            foreach (var item in items)
            {
                richTextBox2.AppendText("• " + item + Environment.NewLine);
            }

            // Reset indent
            richTextBox2.SelectionIndent = 0;
            try { dynamic dt = richTextBox2; dt.SelectionHangingIndent = 0; } catch { }

            // Thêm ngắt dòng
            richTextBox2.AppendText(Environment.NewLine);

            // Khôi phục focus
            if (prev != null && prev != richTextBox2) prev.Focus();
        }

    }
}
