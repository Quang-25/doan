<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footerr.ascx.cs" Inherits="doan.src.footerr.footerr" %>
<link rel="stylesheet" href="footer.css" />
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
       <asp:Label ID="lblnguoidung" runat="server" ForeColor="White" Font-Size="18px" Style="margin-top:42px" />
   </div>