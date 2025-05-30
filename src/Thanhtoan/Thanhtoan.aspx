<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Thanhtoan.aspx.cs" Inherits="doan.src.Thanhtoan.Thanhtoan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <h3>Thanh toan</h3>
           <div class="don-hang">
            <asp:Image ID="img" runat="server" />
            </div>
             <asp:Label ID="lblTenSp" runat="server" Text="Tong tien hang" CssClass="hang"></asp:Label>
             <asp:Label ID="lblTen" runat="server" Text="tam tinh" CssClass="hang"></asp:Label>
             <asp:Label ID="lblsanpham" runat="server" Text="Thanh tien" CssClass="hang"></asp:Label>
            <div>
             
            </div>
        </div>
    </form>
</body>
</html>
