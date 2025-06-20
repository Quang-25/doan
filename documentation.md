# Tài liệu dự án ASP.NET Web App - Hệ thống bán hàng trà sữa

## Tổng quan

Dự án này là một ứng dụng web ASP.NET Web Forms được phát triển để quản lý hệ thống bán trà sữa trực tuyến. Ứng dụng cung cấp các tính năng cho cả người dùng thông thường (khách hàng) và quản trị viên (admin), bao gồm đăng ký, đăng nhập, xem sản phẩm, đặt hàng, quản lý sản phẩm, và quản lý khách hàng.

## Kiến trúc hệ thống

### Công nghệ sử dụng
- **ASP.NET Web Forms** (.NET Framework 4.8)
- **MS SQL Server** (Azure Database)
- **Bootstrap** (v5.2.3) cho giao diện người dùng
- **jQuery** (v3.7.0) cho tương tác phía client
- **AJAX** cho cập nhật không đồng bộ
- **User Controls (ASCX)** cho các thành phần tái sử dụng

### Cấu trúc thư mục
```
- App_Start/         (Cấu hình ban đầu cho ứng dụng)
- bin/               (Các assembly đã được biên dịch)
- Content/           (CSS và các tài nguyên tĩnh)
- obj/               (Các file tạm thời trong quá trình biên dịch)
- packages/          (Các thư viện NuGet)
- Properties/        (Thông tin assembly)
- Scripts/           (Javascript)
- src/               (Source code chính của ứng dụng)
  - dangky/          (Trang đăng ký)
  - dangnhap/        (Trang đăng nhập)
  - footerr/         (UserControl footer)
  - Giohang/         (Trang giỏ hàng)
  - headerr/         (UserControl header)
  - home/            (Trang chủ)
  - Liên Hệ/         (Trang liên hệ)
  - logout/          (Xử lý đăng xuất)
  - Thanhtoan/       (Trang thanh toán)
  - trangchu admin/  (Các trang quản trị)
- About.aspx         (Trang giới thiệu)
- Contact.aspx       (Trang liên hệ mặc định)
- Default.aspx       (Trang chủ mặc định)
- Global.asax        (Xử lý sự kiện toàn cục)
- Site.Master        (Master page chính)
- Site.Mobile.Master (Master page cho thiết bị di động)
- ViewSwitcher.ascx  (UserControl để chuyển đổi giao diện)
- Web.config         (Cấu hình ứng dụng)
```

## Cơ sở dữ liệu

### Sơ đồ cơ sở dữ liệu
Database được lưu trữ trên Azure SQL Server với các bảng chính:

#### NguoiDung
- `MaND` (Primary Key): ID người dùng
- `TenND`: Tên người dùng
- `Sdt`: Số điện thoại
- `Email`: Email người dùng
- `TenDangNhap`: Tên đăng nhập
- `MatKhau`: Mật khẩu
- `ChucVu`: Vai trò (admin hoặc user)
- `DiaChi`: Địa chỉ người dùng
- `SoDu`: Số dư tài khoản
- `SoLanTruyCap`: Số lần truy cập (được sử dụng trong footer)

#### SanPham
- `MaSP` (Primary Key): ID sản phẩm
- `TenSP`: Tên sản phẩm
- `Gia`: Giá sản phẩm
- `KhuyenMai`: Mức khuyến mãi (%)
- `TongSoLuong`: Tổng số lượng hàng
- `SoLuongBan`: Số lượng đã bán
- `LoaiSP`: Loại sản phẩm
- `MoTa`: Mô tả sản phẩm
- `TinhTrang`: Tình trạng ('Còn hàng', 'Hết hàng')
- `HinhAnhChinh`: URL hình ảnh chính
- `HinhAnhPhu`: URL hình ảnh phụ 1
- `HinhAnhPhu2`: URL hình ảnh phụ 2
- `XuatXu`: Xuất xứ sản phẩm
- `NgayTao`: Ngày tạo sản phẩm

#### DonHang
- `MaDH` (Primary Key): ID đơn hàng
- `SoLuong`: Số lượng đặt mua
- `MaND`: ID người dùng (Foreign Key)
- `MaSP`: ID sản phẩm (Foreign Key)
- `NgayDatHang`: Thời điểm đặt hàng

#### LienHe
- `MaLH` (Primary Key): ID liên hệ
- `HoTen`: Tên người liên hệ
- `Email`: Email người liên hệ
- `DienThoai`: Số điện thoại
- `DiaChi`: Địa chỉ
- `MoTa`: Nội dung liên hệ
- `MaND`: ID người dùng (Foreign Key)
- `UserName`: Tên người dùng
- `NgayGui`: Ngày gửi liên hệ

### Connection String
```xml
<add name="DefaultConnection" connectionString="Server=tcp:milkstores.database.windows.net,1433;Initial Catalog=Data_Web_Nang_Cao;Persist Security Info=False;User ID=Quang25;Password=cohoi2512@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Pooling=true;" providerName="System.Data.SqlClient"/>
```

## Thành phần chính của ứng dụng

### User Controls (ASCX)

#### headerr.ascx
Đây là thành phần header xuất hiện ở đầu các trang người dùng. Chứa:
- Logo trang web
- Menu điều hướng (Trang chủ, Giới thiệu, Menu Trà Sữa, Dịch vụ, Tin Tức, Thư Viện, Liên hệ)
- Các liên kết tài khoản (Đăng nhập, Đăng ký, Đăng xuất)
- Giỏ hàng và thông tin số lượng sản phẩm, tổng tiền

#### footerr.ascx
Thành phần footer xuất hiện ở cuối các trang người dùng. Hiển thị:
- Giờ mở cửa
- Thông tin liên hệ (địa chỉ, hotline)
- Các liên kết mạng xã hội
- Số lần truy cập của người dùng (chức năng ghi nhận lượt truy cập)

### Master Pages

#### Site.Master
Master page mặc định cho các trang dành cho người dùng, sử dụng Bootstrap để tạo giao diện responsive.

#### Site.Mobile.Master
Master page dành cho thiết bị di động, được tối ưu hóa hiển thị trên màn hình nhỏ.

#### admin.Master
Master page cho khu vực quản trị, bao gồm:
- Menu điều hướng bên trái
- Các liên kết tới các trang quản lý (Khách hàng, Sản phẩm, Đặt hàng, Liên hệ)

### Các trang chính

#### Trang người dùng

1. **home.aspx**
   - Trang chủ hiển thị danh sách sản phẩm
   - Hiển thị sản phẩm mới
   - Cho phép thêm sản phẩm vào giỏ hàng

2. **login.aspx**
   - Xác thực người dùng
   - Lưu thông tin đăng nhập vào session và cookie
   - Kiểm tra quyền truy cập và chuyển hướng tương ứng

3. **dangky.aspx**
   - Đăng ký tài khoản mới
   - Kiểm tra tính duy nhất của email và số điện thoại
   - Xác thực dữ liệu người dùng

4. **LienHe.aspx**
   - Form liên hệ với captcha
   - Lưu thông tin liên hệ vào database
   - Kiểm tra tính hợp lệ của email và số điện thoại

5. **giohang.aspx**
   - Hiển thị các sản phẩm trong giỏ hàng
   - Tính tổng tiền
   - Chức năng thanh toán

6. **Thanhtoan.aspx**
   - Xác nhận đơn hàng
   - Hiển thị chi tiết đơn hàng và tổng tiền

#### Trang quản trị (Admin)

1. **KhachHang.aspx**
   - Hiển thị danh sách khách hàng
   - Thêm, sửa, xóa thông tin khách hàng
   - Tìm kiếm khách hàng

2. **sanpham.aspx**
   - Quản lý sản phẩm (thêm, sửa, xóa)
   - Tìm kiếm và lọc sản phẩm theo danh mục
   - Cập nhật tình trạng sản phẩm

3. **DatHang.aspx**
   - Quản lý đơn hàng
   - Xem chi tiết đơn hàng
   - Cập nhật trạng thái đơn hàng

4. **lienhe.aspx**
   - Xem và quản lý tin nhắn liên hệ từ khách hàng
   - Sắp xếp theo ngày gửi

## Luồng xử lý chính

### Xử lý đăng nhập và phân quyền
1. Người dùng nhập thông tin đăng nhập (username, password)
2. Hệ thống xác thực thông tin với CSDL
3. Lưu thông tin đăng nhập vào session và cookie (nếu thành công)
4. Kiểm tra vai trò người dùng (admin hoặc user)
5. Chuyển hướng đến trang tương ứng (admin panel hoặc trang chủ)

```csharp
// login.aspx.cs
protected void btnLogin_Click(object sender, EventArgs e)
{
    // Xác thực người dùng
    // ...
    
    // Lưu session và cookie
    Session["idUser"] = userId;
    Session["UserName"] = tenND;
    Session["ChucVu"] = chucVu;

    HttpCookie userCookie = new HttpCookie("UserInfo");
    userCookie["UserID"] = userId.ToString();
    userCookie["UserName"] = tenND;
    userCookie["UserRole"] = chucVu;
    userCookie["TenDangNhap"] = username;
    userCookie.Expires = DateTime.Now.AddDays(7);
    Response.Cookies.Add(userCookie);

    // Chuyển hướng dựa trên vai trò
    if (chucVu.ToLower() == "admin")
    {
        Response.Redirect("/src/trangchu admin/KhachHang.aspx");
    }
    else
    {
        Response.Redirect("/src/home/home.aspx");
    }
}
```

### Đặt hàng và giỏ hàng
1. Người dùng xem sản phẩm trên trang home.aspx
2. Thêm sản phẩm vào giỏ hàng (lưu thông tin vào bảng DonHang)
3. Xem giỏ hàng (giohang.aspx)
4. Tiến hành thanh toán (Thanhtoan.aspx)

```csharp
// home.aspx.cs - Thêm vào giỏ hàng
protected void rptSanPham_ItemCommand(object source, RepeaterCommandEventArgs e)
{
    if (e.CommandName == "AddToCart")
    {
        string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string userId = GetUserIdFromCookie();
            string query = @"INSERT INTO DonHang (Soluong,MaND,MaSP) VALUES (@soluong,@userId,@idSp)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@soluong", 1);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@idSp", e.CommandArgument.ToString());
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        pageload();
        headerHome.pageload();
    }
}
```

### Quản lý sản phẩm (Admin)
1. Admin đăng nhập và truy cập sanpham.aspx
2. Hiển thị danh sách sản phẩm từ CSDL
3. Thêm, sửa, xóa sản phẩm
4. Lưu thay đổi vào CSDL

## Các tính năng nổi bật

1. **Phân quyền người dùng**
   - Phân biệt quyền admin và user thông thường
   - Điều hướng tới các trang phù hợp với quyền

2. **User Controls (ASCX)**
   - Tái sử dụng header và footer trên nhiều trang
   - Đảm bảo tính đồng nhất của giao diện

3. **Lưu trữ phiên đăng nhập**
   - Sử dụng cả Session và Cookie
   - Tự động khôi phục phiên làm việc

4. **Quản lý sản phẩm**
   - Phân loại, tìm kiếm sản phẩm
   - Hỗ trợ khuyến mãi, giảm giá

5. **Quản lý đơn hàng**
   - Theo dõi đơn hàng
   - Xem chi tiết đơn hàng

6. **Thống kê số lần truy cập**
   - Theo dõi hoạt động người dùng
   - Hiển thị trong footer

## Cấu hình hệ thống

### Web.config
- Cấu hình ConnectionString để kết nối cơ sở dữ liệu Azure SQL
- Cấu hình ASP.NET Framework 4.8
- Cấu hình globalization và encoding UTF-8
- Cấu hình bundle scripts và styles

### BundleConfig.cs
Cấu hình nhóm các file CSS và JavaScript để tối ưu hóa hiệu suất.

### RouteConfig.cs
Cấu hình Friendly URLs để tạo URL thân thiện với SEO.

## Kết luận

Dự án ASP.NET Web App này thể hiện một hệ thống bán hàng trà sữa trực tuyến hoàn chỉnh, với đầy đủ chức năng cơ bản cho cả người dùng thông thường và quản trị viên. Ứng dụng sử dụng ASP.NET Web Forms và các User Controls (ASCX) để xây dựng giao diện người dùng tái sử dụng, cùng với MS SQL Server (Azure) để lưu trữ dữ liệu.

Các thành phần chính bao gồm trang người dùng (home, giỏ hàng, thanh toán), trang quản trị (quản lý khách hàng, sản phẩm, đơn hàng), và các thành phần tái sử dụng (header, footer). Hệ thống có tính năng quản lý phiên đăng nhập, phân quyền người dùng, và quản lý sản phẩm/đơn hàng đầy đủ.
