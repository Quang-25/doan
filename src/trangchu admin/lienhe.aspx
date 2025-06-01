<%@ Page Title="" Language="C#" MasterPageFile="~/src/trangchu admin/admin.Master" AutoEventWireup="true" CodeBehind="lienhe.aspx.cs" Inherits="doan.src.trangchu_admin.lienhe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
         body {
            font-family: Arial;
            background-color: lightblue;
            padding: 20px;
        }

        h2 {
            color: navy;
        }

        .form-group {
            margin-bottom: 10px;
        }

        label {
            display: block;
            font-weight: bold;
            color: black;
        }

        input[type="text"], textarea {
            width: 100%;
            padding: 8px;
            border: 1px solid gray;
            border-radius: 4px;
        }

        .btn {
            background-color: green;
            color: white;
            padding: 8px 15px;
            margin-right: 5px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .btn-danger {
            background-color: red;
        }

        .search-bar {
            margin-bottom: 15px;
        }

        .grid {
            margin-top: 20px;
            background-color: white;
            border: 1px solid #ccc;
            padding: 10px;
        }

        .message {
            margin-top: 10px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <h2>Quản lý Liên hệ</h2>

        <div class="form-group">
            <asp:Label ID="lblHoTen" runat="server" />
            <asp:TextBox ID="txtHoTen" runat="server" />
        </div>
        <div class="form-group">
            <asp:Label ID="lblEmail" runat="server" />
            <asp:TextBox ID="txtEmail" runat="server" />
        </div>
        <div class="form-group">
            <asp:Label ID="lblSDT" runat="server" />
            <asp:TextBox ID="txtSDT" runat="server" />
        </div>
        <div class="form-group">
            <asp:Label ID="lblDiaChi" runat="server" />
            <asp:TextBox ID="txtDiaChi" runat="server" />
        </div>
        <div class="form-group">
            <asp:Label ID="lblNoiDung" runat="server" />
            <asp:TextBox ID="txtMoTa" runat="server" TextMode="MultiLine" Rows="3" />
        </div>

        <div class="form-container">
        <asp:HiddenField ID="hdID" runat="server" />
        <div class="form-group">
        <asp:Button ID="btnThem" runat="server" CssClass="btn" Text="Thêm" OnClick="btnThem_Click" />
        <asp:Button ID="btnSua" runat="server" CssClass="btn" Text="Sửa" OnClick="btnSua_Click" />
        <asp:Button ID="btnXoa" runat="server" CssClass="btn btn-danger" Text="Xóa" OnClick="btnXoa_Click" />
        </div>
            </div>

        <div class="search-bar">
            <asp:TextBox ID="txtTimKiem" runat="server" Placeholder="Tìm theo họ tên..." />
            <asp:Button ID="btnTim" runat="server" CssClass="btn" Text="Tìm" OnClick="btnTim_Click" />
        </div>

        <asp:Label ID="lblMessage" runat="server" CssClass="message" />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="MaLH" CssClass="grid"
            OnRowEditing="GridView1_RowEditing"
            OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowUpdating="GridView1_RowUpdating"
            OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="MaLH" HeaderText="Mã" ReadOnly="True" />
                <asp:BoundField DataField="HoTen" HeaderText="Họ tên" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="DienThoai" HeaderText="SĐT" />
                <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" />
                <asp:BoundField DataField="MoTa" HeaderText="Mô tả" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

        <asp:Literal ID="Literal1" runat="server" />
    
</asp:Content>

