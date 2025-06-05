<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="giohang.aspx.cs" Inherits="doan.src.Giohang.giohang" %>
<%@ Register Src="~/src/headerr/headerr.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/src/footerr/footerr.ascx" TagPrefix="ux" TagName="FooterHome" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="../home/trang.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

    <style type="text/css">
        body {
            font-family:Arial, Helvetica, sans-serif;
            background-color: #f0f0f0;
           
        }
        .name{
            
            font-size: 28px;
            font-weight: bold;
           
        }
        
        .content {
            color: #333;
            font-size: 16px;
            margin-bottom: 195px;
        }
        .total {
            color: #dc3545;
            font-size: 16px;
            font-weight: bold;
            text-align: right;
            margin-right: 84px;
            margin-bottom: 20px;
        }
        .buttons {
            display: flex;
            justify-content: flex-end;
            gap: 10px;
            margin-top: 20px;
            text-align: right;
            margin-right: 29px;
        }
        .aspButton {
            background-color: #4CAF50;
            color: white;
            padding: 8px 4px;
            border: none;
            border-radius: 10px;
            margin-left: 0px;
            cursor: pointer;
            font-size: 12px;
        }
        .aspButton:hover {
            background-color: #45a049;
        }
        .cart{
            background-image:url(https://demo037102.web30s.vn/datafiles/34058/upload/images/banner/slide2.jpg?t=1636600660);
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            width:100%;
            height: 190px;
            display: flex;
            align-items: center;
            justify-content: center;
            color:white;
            margin:0px 0px 5px;
        }
        .cart-details {
          flex: 1;
           font-size: 16px;
        }
        .cart-details div {
      
         padding: 6px 10px;       
        
        }
        .cart-details button {
         background-color: #fff;
            border: 1px solid #ccc;
         padding: 3px 10px;
        margin: 0 5px;
        cursor: pointer;
        font-weight: bold;
        font-size: 14px;
        }
        .bold {
            font-weight: bold;
            color: red;
        }


        .giohang-item {
        display: flex;
        align-items: center;
        padding: 15px 0;
         border-bottom: 1px dashed #ccc;
        }

        .giohang-item img {
         width: 120px;
        height: auto;
        margin-right: 20px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
            <uc:Header runat="server" ID="headerHome"></uc:Header>
        <div>

     
        <div class="cart">
        <h3 class="name">Giỏ Hàng</h3>
        </div>
        <asp:Repeater ID="rptgiohang" runat="server" OnItemCommand="rptgiohang_ItemCommand">
        <ItemTemplate>
        <div class="giohang-item">
        <img src='<%# Eval("HinhAnhChinh") %>' />
        <div class="cart-details">
         <div><%# Eval("TenSP") %></div>
         <div>Đơn giá: <%# Eval("Gia", "{0:N0}") %> đ</div>
        <div>
            Số lượng:
            <asp:LinkButton ID="btnGiam" runat="server" 
                CommandName="GiamSoLuong" 
                CommandArgument='<%# Eval("MaSP") %>'></asp:LinkButton>

            <%# Eval("SoLuong") %>

            <asp:LinkButton ID="btnTang" runat="server" 
                CommandName="TangSoLuong" 
                CommandArgument='<%# Eval("MaSP") %>'></asp:LinkButton>
        </div>
        <div class="bold">
            Thành tiền:<%# (Convert.ToInt32(Eval("Gia"))* Convert.ToInt32(Eval("SoLuong"))).ToString("N0") %>đ
        </div>
        </div>
        </div>
        </ItemTemplate>
        </asp:Repeater>
        <div class="total">
            <asp:Label ID="lblTotal" runat="server"></asp:Label>
        </div>
        <div class="buttons">
            <asp:Button ID="btnViewMore" runat="server" Text="XEM THÊM SẢN PHẨM" CssClass="aspButton" />
            <asp:Button ID="btnCheckout" runat="server" Text="THANH TOÁN" CssClass="aspButton" OnClick="btnCheckout_Click" />
        </div>
               </div>
          <ux:FooterHome runat="server" />

    </form>
</body>
</html>
