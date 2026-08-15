# Phần Mềm Quản Lý Đăng Ký Tín Chỉ


## 📋 Mục Lục

- [Giới Thiệu](#giới-thiệu)
- [Tính Năng Chính](#tính-năng-chính)
- [Cài Đặt](#cài-đặt)
- [Cấu Trúc Dự Án](#cấu-trúc-dự-án)
- [Công Nghệ Sử Dụng](#công-nghệ-sử-dụng)

## Giới Thiệu


Hệ thống đăng ký tín chỉ trực tuyến giúp sinh viên quản lý việc đăng ký môn học một cách thuận tiện.

### Lợi ích chính

- ✅ Giao diện Windows Forms thân thiện, dễ sử dụng
- ✅ Kiểm tra xung đột lịch học tự động
- ✅ Theo dõi tổng số tín chỉ đã đăng ký
- ✅ Xem lịch sử đăng ký các học kỳ trước
- ✅ Quản lý hiệu quả bằng SQL Server

## Tính Năng Chính

### 1. Đăng Ký Tín Chỉ
- Chọn môn học theo chương trình đào tạo (CTDT)
- Xem danh sách lớp học phần có sẵn
- Kiểm tra số chỗ trống trực tuyến

### 2. Quản Lý Lớp Học Phần
- Hiển thị lịch học chi tiết (thứ, tiết học, phòng)
- Thông tin giảng viên phụ trách
- Sức chứa lớp và số chỗ trống
- Phân loại loại tiết học (lý thuyết, thực hành)

### 3. Kiểm Tra Tự Động
- Xung đột lịch học
- Giới hạn tín chỉ (min 14, max 25 tín chỉ)
- Điều kiện tiên quyết môn học

### 4. Lịch Sử & Thống Kê
- Xem tất cả đăng ký trong các học kỳ trước
- Thống kê số môn và tín chỉ đã hoàn thành
- Theo dõi trạng thái (Đạt/Trượt)

## Cài Đặt

### Bước 1: Clone Repository

### Bước 2: Tạo Database
1. Mở SQL Server Management Studio
2. Chạy script `Database/CreateDatabase.sql` để tạo database
3. Chạy script `Database/InsertSampleData.sql` (nếu có) để nhập dữ liệu mẫu

### Bước 3: Cấu Hình Connection String
Mở file `frmCourses.cs` và cập nhật connection string:
strConn = "Data Source=YOUR_SERVER\SQLEXPRESS;" + "Initial Catalog=QuanLySinhVien;" + "Integrated Security=True;" + "Encrypt=True;" + "TrustServerCertificate=True;";
### Bước 4: Biên Dịch & Chạy
1. Mở project với Visual Studio 2022
2. Nhấn __Ctrl+Shift+B__ để biên dịch
3. Nhấn __F5__ để chạy ứng dụng


## Cấu Trúc Dự Án

```text
BTL_QLDKTC/
├── Models/
├── Utilities/
├── frmLogin.cs
├── frmCourses.cs
├── frmCoursesDetail.cs
├── frmHelp.cs
├── Program.cs
└── App.config
```

### Mô tả các thành phần chính

| Thành phần | Chức năng |
|------------|------------|
| `Models/SinhVienInfo.cs` | Lưu trữ thông tin sinh viên |
| `Utilities/SemesterDeterminer.cs` | Xác định năm học và học kỳ hiện tại |
| `frmLogin.cs` | Giao diện đăng nhập hệ thống |
| `frmCourses.cs` | Giao diện chính quản lý đăng ký tín chỉ |
| `frmCoursesDetail.cs` | Hiển thị chi tiết lớp học phần |
| `frmHelp.cs` | Hướng dẫn sử dụng phần mềm |
| `Program.cs` | Điểm khởi động của ứng dụng |
| `App.config` | Chứa các cấu hình của ứng dụng |
## Công Nghệ Sử Dụng

- **Ngôn Ngữ:** C# 7.3
- **Framework:** .NET Framework 4.8
- **UI:** Windows Forms
- **Database:** SQL Server
- **IDE:** Visual Studio 2022

## ⚠️ Disclaimer / Lưu ý
  
Sản phẩm này được phát triển như một dự án học tập / minh họa. Mục đích chính là để học và demo chức năng; do đó mã nguồn có thể còn thiếu sót, chưa được tối ưu hoặc chưa phù hợp để triển khai trực tiếp vào môi trường sản xuất. 
