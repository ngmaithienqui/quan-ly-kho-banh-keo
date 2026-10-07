# Quản lý kho bánh kẹo

Ứng dụng quản lý kho bánh kẹo được xây dựng bằng **C# WinForms** và **SQL Server**, hỗ trợ quản lý hàng hóa, nhập xuất kho, kiểm kê và thống kê.

## Chức năng chính

- Quản lý sản phẩm và danh mục
- Quản lý nhập hàng và xuất hàng
- Quản lý kiểm kê kho
- Quản lý khách hàng và nhà cung cấp
- Theo dõi công nợ và lịch sử giao dịch
- Thống kê doanh thu
- Xuất báo cáo Excel
- Phân quyền người dùng

## Công nghệ sử dụng

- C#
- .NET Framework 4.8
- Windows Forms
- SQL Server
- ADO.NET
- ClosedXML

## Cấu trúc dự án

```text
DAO/              Các lớp truy xuất dữ liệu
DTO/              Các lớp đối tượng dữ liệu
Database/         File cơ sở dữ liệu
Properties/       Cấu hình project
Resources/        Tài nguyên giao diện
App.config        Cấu hình kết nối SQL Server
DBConnection.cs   Xử lý kết nối cơ sở dữ liệu
Program.cs        Điểm khởi chạy chương trình
```

## Cài đặt và chạy

### 1. Clone repository
git clone https://github.com/ngmaithienqui/quan-ly-kho-banh-keo.git

### 2. Mở project
Mở file: 
QLCuaHangBanhKeo.sln 
bằng Visual Studio.

### 3. Cấu hình cơ sở dữ liệu
Tạo database `CuaHangBanhKeo` bằng file SQL có trong project.
Sau đó kiểm tra chuỗi kết nối trong `App.config`:
connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=CuaHangBanhKeo;Integrated Security=True;"
Thay `.\SQLEXPRESS` bằng tên SQL Server trên máy nếu cần.

### 4. Chạy chương trình
- Restore NuGet Packages
- Build Solution
- Nhấn `F5` để chạy

## Lưu ý
Các file và thư mục sau không nên đưa lên GitHub:

```gitignore
.vs/
.vscode/
bin/
obj/
packages/

*.user
*.suo
*.bak
```
