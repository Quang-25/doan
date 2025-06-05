<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Thanhtoan.aspx.cs" Inherits="doan.src.Thanhtoan.Thanhtoan" %>
<%@ Register Src="~/src/headerr/headerr.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/src/footerr/footerr.ascx" TagPrefix="ux" TagName="FooterHome" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../home/trangchu.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <style type="text/css">
        body{
            font-family: Arial, sans-serif;
            background: #f5f5f5;
            margin: 0;
            height: 100%;

            background-image: url('https://demo037102.web30s.vn/datafiles/web30s/upload/images/7101-7200/30S-03-7102/slide2.jpg');
            background-size: cover;      /* ảnh phủ đầy, không bị lặp */
            background-position: center; /* căn giữa ảnh */
            background-repeat: no-repeat;
            background-attachment: fixed; /* ảnh cố định khi cuộn */
        }
        .form-container{
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 40px 20px;
        }

        .form-section {
             background: #fff;    /* nền trắng cho rõ */
             padding: 30px 50px; /* tăng padding để form thoáng */
             width: 600px;
             box-sizing: border-box;
             border-radius: 10px;
             box-shadow: 0 8px 16px #000000; /* bóng mờ đậm hơn */
             transition: box-shadow 0.3s ease;
        }

        .form-section:hover {
             box-shadow: 0 12px 24px  #00000033; /* hiệu ứng khi hover */
        }

        .form-group {
             margin-bottom: 20px;
        }

        .form-group label {
             font-weight: 600;
             font-size: 14px;
             margin-bottom: 6px;
             display: block;
             color: #333;
        }

        .form-group input, 
        .form-group textarea {
             width: 100%;
             padding: 10px 12px;
             font-size: 14px;
             border: 1.5px solid #ccc;
             border-radius: 6px;
             transition: border-color 0.3s ease;
             resize: vertical;
        }

        .form-group input:focus, 
        .form-group textarea:focus {
              border-color: #28a745;
              outline: none;
              box-shadow: 0 0 5px #28a745;
        }

        .summary {
              margin-top: 25px;
              font-size: 16px;
              margin-bottom: 20px
        }
        .summary p {
            margin-bottom: 10px
        }
        .total {
              color: #d9534f;
              font-weight: 700;
        }

        .btn {
              background: #28a745;
              color: #FFFFFF;
              padding: 12px 20px;
              font-weight: 600;
              border: none;
              border-radius: 6px;
              cursor: pointer;
              font-size: 16px;
              width: 100%;
              transition: background-color 0.3s ease;
        }

        .btn:hover {
              background: #218838;
        }

</style>
</head>
<body>
    <form id="form1" runat="server">
         <uc:Header runat="server" ID="headerHome"></uc:Header>
        <div class="form-container">
             <div class="form-section">
            <h2 style="text-align: center; margin-bottom: 30px">THÔNG TIN THANH TOÁN</h2>
            <div class="form-group">
                <asp:Label ID="lblHoTen" runat="server"  >
                   <span style="color:red">*</span> Họ tên
                </asp:Label>
                <asp:TextBox ID="txtHoTen" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblDiaChi" runat="server"  >
                    <span style="color:red">*</span> Địa chỉ
                </asp:Label>
                <asp:TextBox ID="txtDiaChi" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblSDT" runat="server">
                    <span style="color:red">*</span> Số điện thoại
                </asp:Label>
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
        <ux:FooterHome runat="server" />
    </form>
    
</body>
</html>
