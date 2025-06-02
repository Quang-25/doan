<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dangky.aspx.cs" Inherits="doan.src.DANGKY.dangky" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng ký tài khoản</title>
    <link href="giaodien.css" rel="stylesheet" />
    <style>
        body {
            font-family: Arial, sans-serif;
        }

        .form-container {
            max-width: 500px;
            margin: 50px auto;
            padding: 20px;
            border-radius: 10px;
            background-color: #f9f9f9;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        }

        .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        table {
            width: 100%;
        }

        td {
            padding: 8px;
            vertical-align: top;
        }

        .textbox {
            width: 100%;
            padding: 6px;
        }

        .button {
            margin-top: 10px;
            padding: 8px 20px;
            border: none;
            border-radius: 5px;
            background-color: #4285f4;
            color: white;
            cursor: pointer;
        }

        .button:hover {
            background-color: #357ae8;
        }

        .error {
            color: red;
            font-size: 0.9em;
        }

        .text-center {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="anhnen">
        <div class="form-container">
            <h2>Đăng ký tài khoản</h2>
            <table>
                <tr>
                    <td>Họ tên:</td>
                    <td>
                        <asp:TextBox ID="txtHoTen" runat="server" CssClass="textbox" />
                        <asp:RequiredFieldValidator ID="rvt1" runat="server" ControlToValidate="txtHoTen" ErrorMessage="* Bắt buộc" CssClass="error" />
                    </td>
                </tr>
                <tr>
                    <td>Tên truy cập:</td>
                    <td>
                        <asp:TextBox ID="txtTenTruyCap" runat="server" CssClass="textbox" />
                        <asp:RequiredFieldValidator ID="rvt2" runat="server" ControlToValidate="txtTenTruyCap" ErrorMessage="* Bắt buộc" CssClass="error" />
                    </td>
                </tr>
                <tr>
                    <td>Điện thoại:</td>
                    <td>
                        <asp:TextBox ID="txtDienThoai" runat="server" CssClass="textbox" />
                        <asp:RequiredFieldValidator ID="vftkiemtra" runat="server" ControlToValidate="txtDienThoai" ErrorMessage="* Bắt buộc" CssClass="error" />
                        <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtDienThoai"
                            ErrorMessage="* SĐT không hợp lệ" ValidationExpression="^(03|05|07|08|09)[0-9]{8}$" CssClass="error" />
                    </td>
                </tr>
                <tr>
                    <td>Email:</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" />
                        <asp:RequiredFieldValidator ID="rfvemail" runat="server" ControlToValidate="txtEmail" ErrorMessage="* Bắt buộc" CssClass="error" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="* Email không hợp lệ" ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w{2,4}$" CssClass="error" />
                    </td>
                </tr>
                <tr>
                    <td>Mật khẩu:</td>
                    <td>
                        <asp:TextBox ID="txtMatKhau" runat="server" CssClass="textbox" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="rvt4" runat="server" ControlToValidate="txtMatKhau" ErrorMessage="* Bắt buộc" CssClass="error" />
                    </td>
                </tr>
                <tr>
                    <td>Xác nhận mật khẩu:</td>
                    <td>
                        <asp:TextBox ID="txtXacNhan" runat="server" CssClass="textbox" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtXacNhan" ErrorMessage="* Bắt buộc" CssClass="error" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="text-center">
                        <asp:Button ID="btnDangKy" runat="server" Text="Đăng ký" CssClass="button" OnClick="btnDangKy_Click" />
                        <asp:Button ID="btnLamLai" runat="server" Text="Làm lại" CssClass="button" OnClick="btnLamLai_Click" CausesValidation="false" />
                        <br />
                        <asp:Label ID="lblthongbao" runat="server" CssClass="error" />
                    </td>
                </tr>
            </table>
        </div>
            </div>
    </form>
</body>
</html>
