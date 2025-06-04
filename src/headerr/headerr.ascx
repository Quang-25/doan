<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="headerr.ascx.cs" Inherits="doan.src.headerr.headerr" %>
 <div>
     <div class="header">
         <div class="logo-box">
             <img src="https://demo037102.web30s.vn/datafiles/34058/upload/images/logo.png" alt="Trà sữa" />
         </div>

         <div class="account-menu">
         <span class="account-label">Tài khoản</span>
          <div class="dropdown-content">
         <a href="/src/dangnhap/login">Đăng nhập</a>
         <a href="/src/dangky/dangky">Đăng ký</a>
         <a href="/src/logout">Đăng xuất</a>
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