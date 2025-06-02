<%@ Page Title="" Language="C#" MasterPageFile="~/src/trangchu admin/admin.Master" AutoEventWireup="true" CodeBehind="DatHang.aspx.cs" Inherits="doan.src.trangchu_admin.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link rel="stylesheet" href="han.css" />
    <asp:GridView ID="gvdatHang" runat="server" AutoGenerateColumns="False" CssClass="Donhang" DataKeyNames="MaDH" 
     OnRowEditing="gvDonHang_RowEditing"
     OnRowUpdating="gvDonHang_RowUpdating"
     OnRowCancelingEdit="gvDonHang_RowCancelingEdit"
    OnRowDeleting="gvDonHang_RowDeleting" Width="100%">
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
<div class="search-box">
    <h3>Tìm kiếm đơn hàng</h3>
    <div class="form-search">
        <asp:TextBox ID="txtTimMaSP" runat="server" CssClass="form-control" Placeholder="Nhập mã đơn hàng cần tìm" />
        <asp:Button ID="btnTimKiem" runat="server" Text="Tìm" CssClass="btn-search" OnClick="btnTimKiem_Click" />
    </div>
    <asp:Label ID="lblThongBao" runat="server" CssClass="search-message" />
</div>
</asp:Content>