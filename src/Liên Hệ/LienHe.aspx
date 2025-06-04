<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LienHe.aspx.cs" Inherits="doan.src.Liên_Hệ.LienHe" %>
<%@ Register Src="~/src/headerr/headerr.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/src/footerr/footerr.ascx" TagPrefix="ux" TagName="FooterHome" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Liên hệ</title>
     <link rel="stylesheet" href="../home/trang.css" />
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <style type="text/css">
    body{
        font-family: Arial,sans-serif;
        margin: 0;
        padding: 40px;
        color: #333;
    }
    /* Grid wrapper */
    .wrapper {
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 20px;
        max-width: 1200px;
        margin: 0 auto;
    }
    /*Info (cột trái)*/
    .info{
        grid-column: 1 / 2;
    }
    .info-h1{
        font-size:  28px;
        border-bottom: 1px solid #999;
        padding-bottom: 8px;
        margin-bottom: 12px;
    }
    .info p {
        margin: 8px 0;
        line-height: 1.5;
    }
    /* Map (cột phải) */
    .map{
        grid-column: 2 / 3;
        border: 2px solid lightgreen;
        padding: 5px;
    }
    .map iframe{
        width: 100%;
        height: 300px;
        border: none;
    }
    /* Form phần dưới*/
    .form-section{
        grid-column: 1 / -1;
        margin-top: 20px;
    }
    .form-section h2 {
        font-size: 24px;
        margin-bottom: 20px;
    }
    .form-section hr {
        border: none;
        border-top: 1px solid #ddd;
        margin-bottom: 20px;
    }
    .form-row {
        display: flex;
        flex-wrap: wrap;
        gap: 20px;
        margin-bottom: 20px;
    }
    .form-group {
        flex: 1 1 ;
        display: flex;
        flex-direction: column;
    }
    .form-label {
        font-weight: 500;
        margin-bottom: 6px;
    }
    .required {
        color: red;
        margin-right: 4px;
    }
    .form-control{
        padding: 10px ;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
        font-size: 1rem;
    }
    .form-control:focus {
        outline: none;
        border-color: black;        

    }
    .email-hot{  color:black;  transition: color 0.3s ease;}
    .email-hot:hover{
        color:#47b65c;
    }
    .textarea-control {
        min-height: 120px;
        resize:vertical;
    }
    .text-danger {
        color: red;
        font-size: 0.9rem;
        margin-top: 5px;
    }
    /*Captcha nhúng trong input*/
    .captcha-box {
        position: relative;
        width: 100%;
    }
    .captcha-input{
        width: 100%;
        padding: 10px; padding-right:120px; /*Chừa chỗ cho mã + nút */; border: 1px solid #ccc;
        border-radius: 4px; box-sizing: border-box;
    }
    .captcha-code {
        position: absolute; top:50%; right: 40px; transform: translateY(-50%);font-weight: bold; color:green; user-select: none;
    }
    .refresh-btn{
        position: absolute; top:50%; right:10px; transform: translateY(-50%); background: none; border: none; font-size: 1.2rem; cursor: pointer;
    }

    /* Buttons */
    .btn-row {
        display:flex;
        gap: 10px;
    }
    .btn-row input[type="submit"], .btn-row input[type="button"]{
        padding: 10px 25px; background: green; color: white; border: none; border-radius: 3px; cursor: pointer; font-size: 1rem;
    }
    .success {
    color: green;
    font-weight: bold;
    margin-top: 10px;
    }
    .fail {
    color: red;
    font-weight: bold;
    margin-top: 10px;
    }
    .form-control,
    .textarea-control,
    .captcha-input {
    width: 100%;
    }
    .form-group {
     width: 100%;
    }
    .section-2 {
    grid-column: 1 / -1;
    }

</style>
</head>
<body>
    <form id="form1" runat="server">
         <uc:Header runat="server" ID="headerHome"></uc:Header>
         <div class="wrapper">

     <!--THÔNG TIN LIÊN HỆ -->
         <div class="info">
             <h1>LIÊN HỆ</h1>
             <p><strong>MilkStores</strong>hiện là chuỗi cửa hàng trà sữa lớn nhất tại Việt Nam. Những sản phẩm từ MilkStores đang như một cơn bão rất nhanh chóng lan rộng ra xung quanh Châu Á.</p>
             <p><strong>Địa chỉ:</strong>196 Nguyễn Đình Chiểu, P.Võ Thị Sáu, Q.3, TP.HCM</p>
             
             <div class="email-hot">
                 <p><b>Hotline:</b><span class="email-hot">19009477</span></p>
                 <div><b>Email:</b> <a href="mailto:admin@demo037102.web30s.vn">admin@demo037102.web30s.vn</a></div>
             </div>
         </div>

     <!--Bản đồ-->
     <div class="map">
     <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.466325587861!2d106.68564687480691!3d10.776887859199286!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752edfa80f3673%3A0x8e2355b91a36090c!2zMTk2IE5ndXnhu4VuIMSQw6xuaCBDaGl14buvLCBQaMaw4budbmcgNiwgUXXhuq1uIDMsIFRow6BuaCBwaOG7kSBI4buTIENow60gTWluaCwgVMOibiBI4buTIENow60gTWluaA!5e0!3m2!1svi!2s!4v1716549122544!5m2!1svi!2s"
   allowfullscreen></iframe>
     </div>

     <!--FORM GỬI THÔNG TIN-->
     <div class="form-section">
         <h2>GỬI THÔNG TIN CHO CHÚNG TÔI</h2>
         <div class="form-row">
             <div class="form-group">
                 <asp:Label ID="lblHoten" runat="server" CssClass="form-label"></asp:Label>
                 <asp:TextBox ID="txtHoTen" runat="server" Placeholder="*Họ tên" Height="30px"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvHoten" runat="server" ControlToValidate="txtHoTen" ErrorMessage="Bạn vui lòng nhập họ tên!" CssClass="text-danger" Display="Dynamic" />
             </div>
             <div class="form-group">
                 <asp:Label ID="lblEmail" runat="server" CssClass="form-label"></asp:Label>
                 <asp:TextBox ID="txtEmail" runat="server" CssClass="required:*"  Placeholder="*Email" Height="30px" > 
                 </asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Bạn vui lòng nhập email!" CssClass="text-danger" Display="Dynamic" AutoPostBack="True" OnTextChanged="txtEmail_TextChanged"/>
                 <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ErrorMessage="Email không hợp lệ!" CssClass="text-danger" Display="Dynamic" />
                 <asp:Label ID="lblLoiEmail" runat="server" CssClass="text-danger" />
             </div>
         </div>

         <div class="form-row">
             <div class="form-group">
                 <asp:Label ID="lblSDT" runat="server" CssClass="form-label"></asp:Label>
                 <asp:TextBox ID="txtSDT" runat="server" Placeholder="*Số điện thoại" height="30px"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvSDT" runat="server" ControlToValidate="txtSDT" ErrorMessage="Bạn vui lòng nhập số điện thoại!" CssClass="text-danger" Display="Dynamic" AutoPostBack="True" OnTextChanged="txtSDT_TextChanged" />
                 <asp:RegularExpressionValidator ID="revSDT" runat="server" ControlToValidate="txtSDT" ValidationExpression="^(0|\+84|84)(3|5|7|8|9)\d{8}$" ErrorMessage="Số điện thoại chưa đúng định dạng" CssClass="text-danger" Display="Dynamic" />
                 <asp:Label ID="lblLoiSDT" runat="server" CssClass="text-danger" />
              </div>
             
             <div class="form-group">
                 <asp:Label ID="lblDiachi" runat="server" CssClass="form-label"></asp:Label>
                 <asp:TextBox ID="txtDiachi" runat="server" Placeholder="*Địa chỉ" Height="30px"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvDiachi" runat="server" ControlToValidate="txtDiachi" ErrorMessage="Bạn vui lòng nhập địa chỉ!" CssClass="text-danger" Display="Dynamic" />
             </div>
         </div>
         </div>
     <div class="section-2">
    <div class="mota-capcha">
         <div class="form-row">
             <div class="form-group" style="flex:1 1 100%;">
                 <asp:Label ID="lblNoidung" runat="server" CssClass="form-label"></asp:Label>
                 <asp:TextBox ID="txtNoidung" runat="server" CssClass="form-control textarea-control" TextMode="MultiLine" Placeholder="*Nội dung"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvNoidung" runat="server" ControlToValidate="txtNoidung" ErrorMessage="Bạn vui lòng nhập nội dung!" CssClass="text-danger" Display="Dynamic" />
             </div>
         </div>

         <div class="form-row">
             <div class="form-group" style="flex: 1 1 100%;">
           <div class="captcha-box">
                     <asp:TextBox ID="txtcaptcha" runat="server" CssClass="captcha-input" Placeholder="*Mã bảo mật"></asp:TextBox>
                     <asp:Label ID="lblCapcha" runat="server" CssClass="captcha-code" Text="AB123" />
                     <asp:Button ID="btnTaoLaiMa" runat="server" CssClass="refresh-btn" Text="↻" OnClick="btnTaoLaiCaptcha_Click" CausesValidation="false" />
           </div>
                 <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ControlToValidate="txtcaptcha" ErrorMessage="Bạn vui lòng nhập mã bảo mật!" CssClass="text-danger" Display="Dynamic" />
          </div>
             </div>
            
         </div>
     <div class="inner-button">
         <div class="btn-row">
             <asp:Label ID="lblMessage" runat="server"></asp:Label>
             <asp:Button ID="btnGui" type="submit" runat="server" Text="Gửi" OnClick="btnGui_Click" />
             <asp:Button ID="btnNhaplai" type="button" runat="server" Text="Nhập lại" onclick="btnNhaplai_Click" />
         </div>
    </div>
         </div>
 </div>
        <ux:FooterHome runat="server" />
    </form>
</body>
</html>
