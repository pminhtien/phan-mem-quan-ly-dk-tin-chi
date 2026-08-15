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
    public partial class frmMainManager : Form
    {
        private string _mql;

        //fields
        private Form activeForm;
        // Constructor Mặc định (giữ lại nếu cần) -> dùng khi không truyền tham số
        public frmMainManager()
        {
            InitializeComponent();
        }
        // 2. Constructor Mới: Chấp nhận tên đăng nhập làm tham số
        public frmMainManager(string username) : this()
        {
            // Gán giá trị tên đăng nhập được truyền vào cho biến cục bộ
            _mql = username;
        }



        private void btnLogout_Click_1(object sender, EventArgs e)
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

        private void btnExit_Click_1(object sender, EventArgs e)
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
