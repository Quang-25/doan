<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Thanhtoan.aspx.cs" Inherits="doan.src.Thanhtoan.Thanhtoan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
     body { font-family: Arial; background: #f5f5f5; padding: 20px; }
    .form-section { background: #f2f2f2; padding: 20px; width: 400px; }
    .form-group { margin-bottom: 15px; }
    .form-group label { font-weight: bold; display: block; }
    .form-group input { width: 100%; padding: 5px; }
    .summary { margin-top: 20px; }
    .total { color: red; font-weight: bold; }
    .btn { background: green; color: white; padding: 10px 15px; border: none; cursor: pointer; }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
             <div class="form-section">
            <h2>Thông tin thanh toán</h2>
            <div class="form-group">
                <label>Họ tên</label>
                <asp:TextBox ID="txtHoTen" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Địa chỉ</label>
                <asp:TextBox ID="txtDiaChi" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Số điện thoại</label>
                <asp:TextBox ID="txtSDT" runat="server"></asp:TextBox>
            </div>

            <hr />
            <asp:Repeater runat="server" ID="rptListoder">

                    <ItemTemplate>

                        <div class="listOrdered">

                            <div class="ordered" style="padding: 5px; border-radius: 5px; border-bottom: 1px gray solid; display: grid; grid-template-columns: 1fr 2fr; align-items: center">

                                <img class="imgOdered" style="width: 70px; height: 70px; border-radius: 5px;" src="<%# Eval("img0") %>" />

                                <div>

                                    <p class="nameOrdered" style="color: #2d2a6e; font-size: 16px; font-weight: 500"><%# Eval("nameItem")%> </p>

                                    <div class="priceAndQuantity" style="display: grid; grid-template-columns: 3fr 1fr; align-items: center">
                                        <p class="priceOrdered" style="color: red"><%# Eval("promotion").ToString()=="0"? Eval("price").ToString(): Eval("promotion").ToString()%>đ</p>

                                        <div style="width: 20px; height: 20px; color: #fff; border-radius: 100%; display: flex; justify-content: center; align-items: center; background-color: #2d2a6e" class="quantityOrdered"><%# Eval("quantity") %></div>

                                    </div>

                                </div>

                            </div>

                        </div>

                    </ItemTemplate>

                </asp:Repeater>
            <asp:Repeater ID="rptDonHang" runat="server">
                <ItemTemplate>
                    <div><%# Eval("TenSP") %> x <%# Eval("SoLuong") %> = <b><%# string.Format("{0:N0} đ", Eval("ThanhTien")) %></b></div>
                </ItemTemplate>
            </asp:Repeater>

            <div class="summary">
                <p><b>Tiền hàng:</b> <asp:Label ID="lblTienHang" runat="server" /></p>
                <p><b>Phí vận chuyển:</b> 20000</p>
                <p class="total">Tổng cộng: <asp:Label ID="lblTongCong" runat="server" CssClass="total" /></p>
            </div>

            <asp:Button ID="btnThanhToan" runat="server" Text="Thanh Toán" CssClass="btn" OnClick="btnThanhToan_Click" />
        </div>
       </div>
    </form>
</body>
</html>
