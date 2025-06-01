<%@ Page Title="" Language="C#" MasterPageFile="~/src/trangchu admin/admin.Master" AutoEventWireup="true" CodeBehind="DatHang.aspx.cs" Inherits="doan.src.trangchu_admin.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="dathang.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="gvdatHang" runat="server" AutoGenerateColumns="False" CssClass="Donhang" DataKeyNames="MaDH" 
     OnRowEditing="gvDonHang_RowEditing"
     OnRowUpdating="gvDonHang_RowUpdating"
     OnRowCancelingEdit="gvDonHang_RowCancelingEdit"
    OnRowDeleting="gvDonHang_RowDeleting" Width="638px">
    <Columns>
        <asp:BoundField DataField="MaDH" HeaderText="Mã ĐH" ReadOnly="True" />
        <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" />
        <asp:BoundField DataField="MaND" HeaderText="Mã người dùng" />
        <asp:BoundField DataField="MaSP" HeaderText="Mã sản phẩm" />
        <asp:BoundField DataField="NgayDatHang" HeaderText="Ngày đặt hàng" />

        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
    </asp:GridView>
   <hr />
<div class="add">
<h3>Thêm đơn hàng mới</h3>
<asp:TextBox ID="txtSoLuong" runat="server" Placeholder="Số lượng" />
<asp:TextBox ID="txtMaND" runat="server" Placeholder="Mã người dùng" />
<asp:TextBox ID="txtMaSP" runat="server" Placeholder="Mã sản phẩm" />
<asp:Button ID="btnAdd" runat="server" Text="Thêm" OnClick="btnAdd_Click" />
</div>
</asp:Content>
