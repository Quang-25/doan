<%@ Page Title="" Language="C#" MasterPageFile="~/src/trangchu admin/admin.Master" AutoEventWireup="true" CodeBehind="lienhe.aspx.cs" Inherits="doan.src.trangchu_admin.lienhe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .search-container {
        display: flex;
        gap: 10px;
        padding: 12px;
        background-color: #f7f7f8;
        border-radius: 8px;
        margin-bottom: 10px;
        align-items: center;
    }

    .form-container {
        background: #fff;
        padding: 20px;
        border-radius: 12px;
        box-shadow: 0 2px 8px #0000001A;
        width: 200px;
        flex-shrink: 0;
    }

    .table {
        border-collapse: separate !important;
        border-spacing: 0 8px;
        width: 100%;
        border: none !important;
        background: #fff;
    }

    .table th, .table td {
        border: none !important;
        padding: 12px 16px;
        vertical-align: middle;
    }

    .table th {
        background: #f7f7f8;
        font-weight: 600;
    }

    .table td {
        background: #fff;
        border-bottom: 1px solid #eee;
        border-radius: 6px;
    }

    .gv-pagination {
        margin-top: 20px;
        text-align: center;
    }

    .gv-pagination a, .gv-pagination span {
        margin: 0 4px;
        padding: 6px 12px;
        border: 1px solid #ccc;
        border-radius: 6px;
        text-decoration: none;
        color: #333;
    }

    .gv-pagination .aspNetDisabled {
        color: #999;
        pointer-events: none;
    }

    .btn {
        border-radius: 6px !important;
        padding: 8px 16px !important;
        font-size: 14px !important;
    }

    .btn-success {
        background-color: #28a745 !important;
        border: none !important;
    }

    .btn-primary {
        background-color: #32CD32 !important;
        border: none !important;
    }

    .btn-secondary {
        background-color: #32CD32 !important;
        border: none !important;
    }

    .form-control {
        margin-bottom: 10px;
        padding: 10px;
        border-radius: 6px;
        border: 1px solid #ccc;
        font-size: 14px;
    }

    /* Label thông báo */
    .message-label {
        display: block;
        margin-top: 10px;
        font-weight: bold;
        color: #008000;
    }
    .text-danger {
        color: #ff0000;
        font-size: 14px;
        margin-top: 5px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <h2 class="header"></h2>
    
   <!-- Cột trái: Bảng liên hệ -->
<div style="flex: 1;">
    <%--<!-- Thanh tìm kiếm nằm trong bảng -->--%>
    <div class="search-container" style="margin-bottom: 10px;">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control search-box" Placeholder="🔍 Tìm theo họ tên hoặc email hoặc user..." />
        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary search-button" OnClick="btnSearch_Click" CausesValidation="false"/>
        <asp:Button ID="btnReset" runat="server" Text="Làm mới" CssClass="btn btn-secondary" OnClick="btnReset_Click" CausesValidation="false"/>
    </div>

    <asp:GridView ID="gvLienHe" runat="server" AutoGenerateColumns="False" DataKeyNames="MaLH"
        CssClass="table" AllowPaging="true" PageSize="10"
        PagerSettings-Mode="Numeric" PagerStyle-CssClass="gv-pagination"
        OnRowEditing="gvLienHe_RowEditing"
        OnRowUpdating="gvLienHe_RowUpdating"
        OnRowCancelingEdit="gvLienHe_RowCancelingEdit"
        OnRowDeleting="gvLienHe_RowDeleting"
        OnPageIndexChanging="gvLienHe_PageIndexChanging">
    <Columns>
        <asp:TemplateField HeaderText="Họ tên">
            <ItemTemplate>
                <%# Eval("HoTen") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtHoTen" runat="server" Text='<%# Bind("HoTen") %>' CssClass="form-control" />
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Email">
            <ItemTemplate>
                <%# Eval("Email") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' CssClass="form-control" />
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="SĐT">
            <ItemTemplate>
                <%# Eval("DienThoai") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtSDT" runat="server" Text='<%# Bind("DienThoai") %>' CssClass="form-control" />
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Địa chỉ">
            <ItemTemplate>
                <%# Eval("DiaChi") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtDiaChi" runat="server" Text='<%# Bind("DiaChi") %>' CssClass="form-control" />
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Nội dung">
            <ItemTemplate>
                <%# Eval("MoTa") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtNoiDung" runat="server" Text='<%# Bind("MoTa") %>' CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="User">
            <ItemTemplate>
                <%# Eval("UserName") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtUserName" runat="server" Text='<%# Bind("UserName") %>' CssClass="form-control" />
            </EditItemTemplate>
        </asp:TemplateField>
                <asp:BoundField DataField="NgayGui" HeaderText="Ngày gửi" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:CommandField ShowEditButton="true" EditText="✏️ Sửa" UpdateText="💾 Lưu" CancelText="❌ Hủy" CausesValidation="false" />
                <asp:CommandField ShowDeleteButton="true" DeleteText="🗑️ Xóa" />
     </Columns>
     </asp:GridView>
</div>

       <div class="form-container">
        <h3 style="margin-top: 0;">Thêm liên hệ mới</h3>
        <asp:Panel ID="pnlAdd" runat="server">
            <asp:TextBox ID="txtHoTen" runat="server" Placeholder="Họ tên" CssClass="form-control" /><br />
            <asp:RequiredFieldValidator ID="rfvHoten" runat="server" ControlToValidate="txtHoTen" ErrorMessage="Bạn vui lòng nhập họ tên!" CssClass="text-danger" Display="Dynamic" />
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email" CssClass="form-control" /><br />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Bạn vui lòng nhập email!" CssClass="text-danger" Display="Dynamic" AutoPostBack="True" OnTextChanged="txtEmail_TextChanged"/>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ErrorMessage="Email không hợp lệ!" CssClass="text-danger" Display="Dynamic" />

            <asp:TextBox ID="txtSDT" runat="server" Placeholder="Số điện thoại" CssClass="form-control" /><br />
            <asp:RequiredFieldValidator ID="rfvSDT" runat="server" ControlToValidate="txtSDT" ErrorMessage="Bạn vui lòng nhập số điện thoại!" CssClass="text-danger" Display="Dynamic" AutoPostBack="True" OnTextChanged="txtSDT_TextChanged" />
            <asp:RegularExpressionValidator ID="revSDT" runat="server" ControlToValidate="txtSDT" ValidationExpression="^(0|\+84|84)(3|5|7|8|9)\d{8}$" ErrorMessage="Số điện thoại chưa đúng định dạng" CssClass="text-danger" Display="Dynamic" />

            <asp:TextBox ID="txtDiaChi" runat="server" Placeholder="Địa chỉ" CssClass="form-control" /><br />
            <asp:RequiredFieldValidator ID="rfvDiachi" runat="server" ControlToValidate="txtDiachi" ErrorMessage="Bạn vui lòng nhập địa chỉ!" CssClass="text-danger" Display="Dynamic" />

            <asp:TextBox ID="txtNoiDung" runat="server" Placeholder="Nội dung" CssClass="form-control" TextMode="MultiLine" Rows="3" /><br />
            <asp:RequiredFieldValidator ID="rfvNoidung" runat="server" ControlToValidate="txtNoidung" ErrorMessage="Bạn vui lòng nhập nội dung!" CssClass="text-danger" Display="Dynamic" />

            <asp:TextBox ID="txtUserName" runat="server" Placeholder="UserName (nếu có)" CssClass="form-control" /><br />
            <asp:Button ID="btnThem" runat="server" Text="Thêm mới" CssClass="btn btn-success" OnClick="btnThem_Click"/>
        </asp:Panel>
        <asp:Label ID="lblMessage" runat="server" CssClass="message-label" />
    </div>
</asp:Content>


