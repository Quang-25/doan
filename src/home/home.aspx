<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="doan.src.home.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="trangchu.css" />
    <link rel="icon" type="image/png" href="https://demo037102.web30s.vn/datafiles/34058/upload/images/logo.png" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="header">
                <div class="logo-box">
                    <img src="https://demo037102.web30s.vn/datafiles/34058/upload/images/logo.png" alt="Trà sữa" />
                </div>

                <div class="account-menu">
                <span class="account-label">Tài khoản</span>
                 <div class="dropdown-content">
                <a href="DangNhap.aspx">Đăng nhập</a>
                <a href="DangKy.aspx">Đăng ký</a>
                </div>
                </div>
                <div class="yeuthich">
                <span class="account-label">Yêu thích</span>
                </div>
                <div class="giohang">
                    <div>
                         <a href="/src/Giohang/giohang.aspx" style="text-decoration: none; color: inherit; font-size:14px">GIỎ HÀNG</a>
                    </div>
                    <div class="count">
                        <asp:Label ID="lblgiohang" runat="server"></asp:Label>
                    </div>
                    </div>
                    <span class="basket"><i class="fa fa-shopping-basket"></i></span>
            </div>

            <div class="box-Menu">
                <ul>
                    <li><a class="titleMenu" href="#">Trang chủ</a></li>
                    <li><a class="titleMenu" href="#">Giới thiệu</a></li>
                    <li class="menu-dropdown">
                        <a class="titleMenu" href="#">Menu Trà Sữa</a>
                        <div class="dropdown-container">
                            <div class="dropdown-column">
                                <h5>Trà sữa sinh tố</h5>
                                <a class="detailTraSuaSinhTo" href="#">Trà truyền thống</a>
                                <a class="detailTraSuaSinhTo" href="#">Trà sữa trái cây</a>
                                <a class="detailTraSuaSinhTo" href="#">Trà sữa đài loan</a>
                            </div>
                            <div class="dropdown-column">
                                <h5>Nước ép trái cây</h5>
                                <a class="detailTraSuaSinhTo" href="#">Nước ép chai</a>
                                <a class="detailTraSuaSinhTo" href="#">Nước ép thiên nhiên</a>
                                <a class="detailTraSuaSinhTo" href="#">Nước ép tươi</a>
                            </div>
                            <div class="dropdown-column">
                                <h5>Kem</h5>
                                <a class="detailTraSuaSinhTo" href="#">Kem Gelato, Ý</a>
                                <a class="detailTraSuaSinhTo" href="#">Kem Helado, Argentina</a>
                                <a class="detailTraSuaSinhTo" href="#">Kem Ais Kacang, Malaysia</a>
                                <a class="detailTraSuaSinhTo" href="#">Kem Mochi, Nhật Bản</a>
                                <a class="detailTraSuaSinhTo" href="#">Kem Ben & Jerry's, Vermont, Mỹ</a>
                                <a class="detailTraSuaSinhTo" href="#">Kem Mochi, Nhật Bản</a>
                            </div>
                        </div>
                    </li>
                    <li class=" dichvu-dropdown">
                        <a class="titleMenu" href="#">Dịch vụ</a>
                        <div class="dropdown-dichvu">
                        <ul >
                            <li><a href="#">Hỗ trợ khách hàng</a></li>
                            <li><a href="#">Giao hàng</a></li>
                        </ul>
                        </div>
                      
                    </li>
                    <li><a class="titleMenu" href="#">Tin Tức</a></li>
                    <li><a class="titleMenu" href="#">Thư Viện</a></li>
                    <li><a class="titleMenu" href="/src/Liên%20Hệ/LienHe">Liên hệ</a></li>
                    <li>
                        <div class="search">
                             <asp:TextBox ID="txtSearch" runat="server" CssClass="search-for" Placeholder="Nhap tu khoa..." ></asp:TextBox>
                            <svg xmlns="http://www.w3.org/2000/svg" height="20" width="20" fill="currentColor" viewBox="0 0 24 24">
                                <path d="M15.5 14h-.79l-.28-.27A6.471 6.471 0 0 0 16 9.5 
                                 6.5 6.5 0 1 0 9.5 16c1.61 0 3.09-.59 
                                 4.23-1.57l.27.28v.79l5 4.99L20.49 
                                 19l-4.99-5zm-6 0C8.01 14 6 11.99 
                                 6 9.5S8.01 5 10.5 5 15 7.01 15 
                                 9.5 12.99 14 10.5 14z" />
                            </svg>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="anh">
            <img src="https://demo037102.web30s.vn/datafiles/34058/upload/images/banner/slide1.jpg?t=1636600660" class="meetmore" alt="anh loi" />
        </div>
        <div class="infor-bar">
            <ig class="truck">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512">
                    <path d="M48 0C21.5 0 0 21.5 0 48L0 368c0 26.5 21.5 48 48 48l16 0c0 53 43 96 96 96s96-43 96-96l128 0c0 53 43 96 96 96s96-43 96-96l32 0c17.7 0 32-14.3 32-32s-14.3-32-32-32l0-64 0-32 0-18.7c0-17-6.7-33.3-18.7-45.3L512 114.7c-12-12-28.3-18.7-45.3-18.7L416 96l0-48c0-26.5-21.5-48-48-48L48 0zM416 160l50.7 0L544 237.3l0 18.7-128 0 0-96zM112 416a48 48 0 1 1 96 0 48 48 0 1 1 -96 0zm368-48a48 48 0 1 1 0 96 48 48 0 1 1 0-96z" />
                </svg>
            </ig>
            <div class="freeship">
                <span>Miễn phí vận chuyển</span>
                <span>Đặt hàng trên 100.000đ</span>
            </div>
            <ig class="gioqua">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                    <path d="M190.5 68.8L225.3 128l-1.3 0-72 0c-22.1 0-40-17.9-40-40s17.9-40 40-40l2.2 0c14.9 0 28.8 7.9 36.3 20.8zM64 88c0 14.4 3.5 28 9.6 40L32 128c-17.7 0-32 14.3-32 32l0 64c0 17.7 14.3 32 32 32l448 0c17.7 0 32-14.3 32-32l0-64c0-17.7-14.3-32-32-32l-41.6 0c6.1-12 9.6-25.6 9.6-40c0-48.6-39.4-88-88-88l-2.2 0c-31.9 0-61.5 16.9-77.7 44.4L256 85.5l-24.1-41C215.7 16.9 186.1 0 154.2 0L152 0C103.4 0 64 39.4 64 88zm336 0c0 22.1-17.9 40-40 40l-72 0-1.3 0 34.8-59.2C329.1 55.9 342.9 48 357.8 48l2.2 0c22.1 0 40 17.9 40 40zM32 288l0 176c0 26.5 21.5 48 48 48l144 0 0-224L32 288zM288 512l144 0c26.5 0 48-21.5 48-48l0-176-192 0 0 224z" />
                </svg>
            </ig>
            <div class="Qua">
                <span>Giỏ quà đặc biệt</span>
                <span>Tặng món quà hoàn hảo</span>
            </div>
            <ig class="calender">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                    <path d="M96 32l0 32L48 64C21.5 64 0 85.5 0 112l0 48 448 0 0-48c0-26.5-21.5-48-48-48l-48 0 0-32c0-17.7-14.3-32-32-32s-32 14.3-32 32l0 32L160 64l0-32c0-17.7-14.3-32-32-32S96 14.3 96 32zM448 192L0 192 0 464c0 26.5 21.5 48 48 48l352 0c26.5 0 48-21.5 48-48l0-272z" />
                </svg>
            </ig>
            <div class="calender-1">
                <span>Khuyến mãi đặc biệt</span>
                <span>Mỗi ngày, mỗi giờ </span>
            </div>
            <ig class="comment">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512"></svg>
            </ig>
            <div class="comment-1">
                <span>Chăm sóc khách hàng 24/7</span>
                <span>Gọi ngay: 1900 9477</span>
            </div>
        </div>
        <h3 class="tieude">Đồ uống khuyến mãi</h3>
        <div class="nen"> 
            <asp:Repeater ID="rptSanPham" runat="server" OnItemCommand="rptSanPham_ItemCommand1">
            <ItemTemplate>
            <div class=" juice">
                <div class="anhsanpham">
                <img src='<%# Eval("HinhAnhChinh") %>' alt='<%# Eval("TenSP") %>' />
                    <div class="sale-badge"><%# Eval("KhuyenMai") %></div>
                    <div class="sale-badge">-10%</div>

                  <div class="overlay">
                        <asp:Button ID="btn_sua" runat="server" CssClass="order-btn"
                        Text="Đặt hàng"
                        CommandName="update"
                        CommandArgument='<%# Eval("MaSP") %>'
                        OnCommand="OnClickUpdateItem"
                        CausesValidation="false"
                        UseSubmitBehavior="false" />
                      
                    </div>
                    <div class="name"><%# Eval("TenSP") %></div>
                <div class="price">
                    <del><%# Eval("Gia") %></del>
                    <span class="new-price"><%# Eval("Gia") %></span>
                </div>
                <div class="stars">★ ★ ★ ★ ★</div>
            </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
        </div>
        <div class="footer">
            <div class="footer-column">
                <span class="footer-icon"><i class="fa-solid fa-house"></i></span>
                <h3 class="Home">GIỜ MỞ CỬA</h3>
                <div class="footer-text">Thứ 2-6: 8h00 am - 22h00 pm</div>
                <div class="footer-text">Thứ 7-CN: 9h00 am - 21h00 pm</div>
            </div>
            <div class="footer-column">
                <span class="footer-icon"><i class="fa-solid fa-envelope"></i></span>
                <h3 class="Home">THÔNG TIN LIÊN HỆ</h3>
                <div class="footer-text">Địa chỉ:196 Nguyễn Đình Chiểu,P.Võ Thị Sáu,Q.3,TP.HCM</div>
                <div class="footer-text">Hotline:19009477</div>
            </div>
            <div class="footer-column">
                <span class="footer-icon"><i class="fa-solid fa-share-nodes"></i></></span>
                <h3 class="Home">KẾT NỐI VỚI CHÚNG TÔI</h3>
                <div class="social-network">
                    <span class="social-icon"><i class="fa-brands fa-facebook"></i></span>
                    <span class="social-icon"><i class="fa-solid fa-phone"></i></span>
                    <span class="social-icon"><i class="fa-brands fa-twitter"></i></span>
                    <span class="social-icon"><i class="fa-brands fa-youtube"></i></span>
                    <span class="social-icon"><i class="fa-brands fa-pinterest"></i></span>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
