<%@ Page Title="" Language="C#" MasterPageFile="~/src/trangchu admin/admin.Master" AutoEventWireup="true" CodeBehind="sanpham.aspx.cs" Inherits="doan.src.trangchu_admin.WebForm2" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    <link href="sanpham.css" rel="stylesheet" />
    <style>
        .product-management {
            padding: 20px;
/*            margin-left: 20px;*/
            width: 100%;
            background-color: hotpink;
            background-image: url("https://demo037102.web30s.vn/datafiles/34058/upload/images/banner/slide2.jpg?t=1636600660");
            background-size: cover;  
            background-position: center;
/*            background-repeat: no-repeat;*/
            min-height: 500px;
            position: relative;
            /* background-attachment: fixed; */
            
        }
          .table {
      background-color: transparent;
      border-radius: 8px;
  }
  
        .product-form {
            
            border-radius: 8px;
            padding: 20px;
            margin-bottom: 30px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        
        .form-group {
            margin-bottom: 15px;
        }
        
        .btn-action {
            margin-right: 5px;
            color: #fff !important;
        }
        
        .product-grid {
            overflow-x: auto;
        }
        
        .gridview-container {
            width: 100%;
            overflow-x: auto;
        }
        
        .table-hover tbody tr:hover {
            background-color: rgba(0,123,255,0.1);
        }
        
        .message-container {
            margin: 10px 0;
        }
        
        .success-message {
            color: green;
            padding: 10px;
            background-color: #d4edda;
            border-radius: 4px;
        }
        
        .error-message {
            color: #721c24;
            padding: 10px;
            background-color: #f8d7da;
            border-radius: 4px;
        }
        
        /* Image preview styles */
        .img-preview {
            max-width: 100px;
            max-height: 100px;
            margin-top: 10px;
            object-fit: contain;
        }
        
        .product-status-available {
            background-color: #d4edda;
            color: #155724;
            padding: 5px 10px;
            border-radius: 4px;
            font-weight: 500;
        }
        
        .product-status-unavailable {
            background-color: #f8d7da;
            color: #721c24;
            padding: 5px 10px;
            border-radius: 4px;
            font-weight: 500;
        }
        
        .stock-info {
            display: flex;
            flex-direction: column;
            gap: 6px;
        }
        
        .stock-badge {
            padding: 5px 10px;
            border-radius: 4px;
            font-size: 0.9rem;
            display: inline-block;
            width: fit-content;
            font-weight: 500;
        }
        
        .search-container {
/*            background-color: #f8f9fa;*/
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 20px;
/*            box-shadow: 0 1px 3px rgba(0,0,0,0.1);*/
        }
        
        .table-header {
            background-color: #343a40;
            color: white;
            font-weight: 500;
        }
        
        .discounted-price {
            color: #dc3545;
            font-weight: bold;
        }
        
        .original-price {
            text-decoration: line-through;
            color: #6c757d;
            font-size: 0.9rem;
        }
        
        .btn-edit {
            background-color: #0d6efd;
        }
        
        .btn-delete {
            background-color: #dc3545;
        }
        
        .btn-view {
            background-color: #17a2b8;
        }
        
        .pagination-container .pagination-link {
            padding: 5px 10px;
            margin: 0 2px;
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            color: #0d6efd;
            border-radius: 4px;
            text-decoration: none;
        }
        
        .pagination-container .pagination-link.active {
            background-color: #0d6efd;
            color: white;
            border-color: #0d6efd;
        }
        
/*.table {
    --bs-table-bg: rgba(248, 215, 218, 0.8) !important;
    --bs-table-accent-bg: transparent !important;
    background-color: transparent !important;
}*/

.table-danger {
    --bs-table-bg: rgb(251, 232, 233) !important;
    --bs-table-striped-bg: rgb(255, 220, 223) !important;
    --bs-table-hover-bg: rgba(229, 199, 202, 1) !important;
    --bs-table-border-color: rgba(223, 194, 196, 1) !important;
}


/*.table-striped > tbody > tr:nth-of-type(odd) > * {
    --bs-table-accent-bg: rgba(0, 0, 0, 0.02) !important;
}


.table-striped, .table-bordered, .table-hover {
    background-color: transparent !important;
}*/

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="product-management">
        <h2 class="mb-4">Quản Lý Sản Phẩm</h2>
        
        <div class="message-container">
            <asp:Label ID="lblMessage" runat="server" CssClass="w-100"></asp:Label>
        </div>

        <div class="search-container">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Tìm kiếm theo tên hoặc loại sản phẩm..."></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Tìm Kiếm" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="col-md-6 text-md-end">
                    <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#addProductModal">
                        <i class="bi bi-plus-circle me-1"></i> Thêm Sản Phẩm Mới
                    </button>
                </div>
            </div>
        </div>

        <div class="gridview-container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlLoaiSP" runat="server" CssClass="form-select" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlLoaiSP_SelectedIndexChanged">
                                <asp:ListItem Text="-- Tất cả loại sản phẩm --" Value="" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlTinhTrangLoc" runat="server" CssClass="form-select" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlTinhTrangLoc_SelectedIndexChanged">
                                <asp:ListItem Text="-- Tất cả tình trạng --" Value="" />
                                <asp:ListItem Text="Còn hàng" Value="Còn hàng" />
                                <asp:ListItem Text="Hết hàng" Value="Hết hàng" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-select" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem Text="-- Số sản phẩm/trang --" Value="" />
                                <asp:ListItem Text="5 sản phẩm/trang" Value="5" />
                                <asp:ListItem Text="10 sản phẩm/trang" Value="10" />
                                <asp:ListItem Text="20 sản phẩm/trang" Value="20" />
                                <asp:ListItem Text="Tất cả" Value="999999" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <asp:GridView ID="gvSanPham" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered table-hover"
                        DataKeyNames="MaSP" OnRowCommand="gvSanPham_RowCommand"
                        OnRowDataBound="gvSanPham_RowDataBound"
                        AllowPaging="true" PageSize="10" 
                        OnPageIndexChanging="gvSanPham_PageIndexChanging">
                        <HeaderStyle CssClass="table-header" />
                        <PagerSettings Mode="NumericFirstLast" FirstPageText="Đầu" LastPageText="Cuối" 
                            PageButtonCount="5" Position="Bottom" />
                        <PagerStyle CssClass="pagination-container" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="MaSP" HeaderText="Mã SP" ReadOnly="true" />
                            <asp:TemplateField HeaderText="Tên Sản Phẩm">
                                <ItemTemplate>
                                    <strong><%# Eval("TenSP") %></strong>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Giá & Khuyến Mãi">
                                <ItemTemplate>
                                    <div>
                                        <%# Eval("KhuyenMai") != DBNull.Value && Convert.ToInt32(Eval("KhuyenMai")) > 0 ? 
                                            $"<div class='discounted-price'>{string.Format("{0:N0}đ", Convert.ToDouble(Eval("Gia")) * (1 - Convert.ToDouble(Eval("KhuyenMai")) / 100))}</div>" + 
                                            $"<div class='original-price'>{string.Format("{0:N0}đ", Eval("Gia"))}</div>" + 
                                            $"<span class='text-success'>(-{Eval("KhuyenMai")}%)</span>" : 
                                            string.Format("<div>{0:N0}đ</div>", Eval("Gia")) %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Số Lượng">
                                <ItemTemplate>
                                    <div class="stock-info">
                                        <span class="stock-badge bg-primary">Tổng: <%# Convert.ToInt32(Eval("TongSoLuong")) + Convert.ToInt32(Eval("SoLuongBan")) %></span>
                                        <span class="stock-badge bg-success">Đã bán: <%# Eval("SoLuongBan") %></span>
                                        <span class="stock-badge bg-info">Trong kho: <%# Eval("TongSoLuong") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Loại SP">
                                <ItemTemplate>
                                    <%# Eval("LoaiSP") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Xuất xứ">
                                <ItemTemplate>
                                    <%# Eval("XuatXu") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tình Trạng">
                                <ItemTemplate>
                                    <span class='<%# Convert.ToString(Eval("TinhTrang")) == "Còn hàng" ? "product-status-available" : "product-status-unavailable" %>'>
                                        <%# Eval("TinhTrang") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hình Ảnh">
                                <ItemTemplate>
                                    <img src='<%# Eval("HinhAnhChinh") %>' alt="Product" class="img-thumbnail img-preview" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thao Tác">
                                <ItemTemplate>
                                    <div class="d-flex flex-column gap-1">
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditModal" CommandArgument='<%# Eval("MaSP") %>' CssClass="btn btn-sm btn-edit btn-action">
                                            <i class="bi bi-pencil-square"></i> Sửa
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteModal" CommandArgument='<%# Eval("MaSP") %>' CssClass="btn btn-sm btn-delete btn-action">
                                            <i class="bi bi-trash"></i> Xóa
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnView" runat="server" CommandName="ViewDetails" CommandArgument='<%# Eval("MaSP") %>' CssClass="btn btn-sm btn-view btn-action">
                                            <i class="bi bi-eye"></i> Chi tiết
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info text-center">
                                <i class="bi bi-info-circle me-2"></i> Không có sản phẩm nào. Hãy thêm sản phẩm mới!
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlLoaiSP" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlTinhTrangLoc" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlPageSize" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    
    <!-- Modal thêm sản phẩm mới -->
    <div class="modal fade" id="addProductModal" tabindex="-1" aria-labelledby="addProductModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title" id="addProductModalLabel"><i class="bi bi-plus-circle me-2"></i>Thêm Sản Phẩm Mới</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="txtTenSPMoi" class="form-label">Tên Sản Phẩm</label>
                                <asp:TextBox ID="txtTenSPMoi" runat="server" CssClass="form-control" placeholder="Nhập tên sản phẩm"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTenSP" runat="server" ControlToValidate="txtTenSPMoi"
                                    ErrorMessage="Vui lòng nhập tên sản phẩm" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="AddProduct"></asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtGiaMoi" class="form-label">Giá</label>
                                <asp:TextBox ID="txtGiaMoi" runat="server" CssClass="form-control" TextMode="Number" placeholder="Nhập giá sản phẩm"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvGia" runat="server" ControlToValidate="txtGiaMoi"
                                    ErrorMessage="Vui lòng nhập giá sản phẩm" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="AddProduct"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rvGia" runat="server" ControlToValidate="txtGiaMoi"
                                    MinimumValue="0" MaximumValue="9999999999" Type="Double"
                                    ErrorMessage="Giá phải là số dương" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="AddProduct"></asp:RangeValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtKhuyenMaiMoi" class="form-label">Khuyến Mãi (%)</label>
                                <asp:TextBox ID="txtKhuyenMaiMoi" runat="server" CssClass="form-control" TextMode="Number" placeholder="Nhập % khuyến mãi"></asp:TextBox>
                                <asp:RangeValidator ID="rvKhuyenMai" runat="server" ControlToValidate="txtKhuyenMaiMoi"
                                    MinimumValue="0" MaximumValue="100" Type="Integer"
                                    ErrorMessage="Khuyến mãi phải từ 0% đến 100%" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="AddProduct"></asp:RangeValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtTongSoLuongMoi" class="form-label">Tổng Số Lượng</label>
                                <asp:TextBox ID="txtTongSoLuongMoi" runat="server" CssClass="form-control" TextMode="Number" placeholder="Nhập tổng số lượng"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTongSoLuong" runat="server" ControlToValidate="txtTongSoLuongMoi"
                                    ErrorMessage="Vui lòng nhập tổng số lượng" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="AddProduct"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rvTongSoLuong" runat="server" ControlToValidate="txtTongSoLuongMoi"
                                    MinimumValue="0" MaximumValue="9999" Type="Integer"
                                    ErrorMessage="Số lượng phải lớn hơn hoặc bằng 0" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="AddProduct"></asp:RangeValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtSoLuongBanMoi" class="form-label">Số Lượng Đã Bán</label>
                                <asp:TextBox ID="txtSoLuongBanMoi" runat="server" CssClass="form-control" TextMode="Number" placeholder="Nhập số lượng đã bán"></asp:TextBox>
                                <asp:RangeValidator ID="rvSoLuongBan" runat="server" ControlToValidate="txtSoLuongBanMoi"
                                    MinimumValue="0" MaximumValue="9999" Type="Integer"
                                    ErrorMessage="Số lượng phải lớn hơn hoặc bằng 0" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="AddProduct"></asp:RangeValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="txtLoaiSPMoi" class="form-label">Loại Sản Phẩm</label>
                                <asp:TextBox ID="txtLoaiSPMoi" runat="server" CssClass="form-control" placeholder="Nhập loại sản phẩm"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLoaiSP" runat="server" ControlToValidate="txtLoaiSPMoi"
                                    ErrorMessage="Vui lòng nhập loại sản phẩm" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="AddProduct"></asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtXuatXuMoi" class="form-label">Xuất Xứ</label>
                                <asp:TextBox ID="txtXuatXuMoi" runat="server" CssClass="form-control" placeholder="Nhập xuất xứ"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="ddlTinhTrangMoi" class="form-label">Tình Trạng</label>
                                <asp:DropDownList ID="ddlTinhTrangMoi" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Còn hàng" Value="Còn hàng" />
                                    <asp:ListItem Text="Hết hàng" Value="Hết hàng" />
                                </asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <label for="txtHinhAnhChinhMoi" class="form-label">Hình Ảnh Chính (URL)</label>
                                <asp:TextBox ID="txtHinhAnhChinhMoi" runat="server" CssClass="form-control" placeholder="Nhập URL hình ảnh chính"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvHinhAnhChinh" runat="server" ControlToValidate="txtHinhAnhChinhMoi"
                                    ErrorMessage="Vui lòng nhập URL hình ảnh chính" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="AddProduct"></asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtHinhAnhPhuMoi" class="form-label">Hình Ảnh Phụ 1 (URL)</label>
                                <asp:TextBox ID="txtHinhAnhPhuMoi" runat="server" CssClass="form-control" placeholder="Nhập URL hình ảnh phụ 1 (tùy chọn)"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="txtHinhAnhPhu2Moi" class="form-label">Hình Ảnh Phụ 2 (URL)</label>
                                <asp:TextBox ID="txtHinhAnhPhu2Moi" runat="server" CssClass="form-control" placeholder="Nhập URL hình ảnh phụ 2 (tùy chọn)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="txtMoTaMoi" class="form-label">Mô Tả</label>
                        <asp:TextBox ID="txtMoTaMoi" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Nhập mô tả sản phẩm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMoTa" runat="server" ControlToValidate="txtMoTaMoi"
                            ErrorMessage="Vui lòng nhập mô tả sản phẩm" Display="Dynamic" ForeColor="Red"
                            ValidationGroup="AddProduct"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        <i class="bi bi-x-circle me-1"></i> Hủy
                    </button>
                    <asp:Button ID="btnAddProduct" runat="server" Text="Thêm Sản Phẩm" CssClass="btn btn-success" 
                        OnClick="btnAddProduct_Click" ValidationGroup="AddProduct" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal xem chi tiết sản phẩm -->
    <div class="modal fade" id="productDetailsModal" tabindex="-1" aria-labelledby="productDetailsModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-info text-white">
                    <h5 class="modal-title" id="productDetailsModalLabel"><i class="bi bi-info-circle me-2"></i>Chi Tiết Sản Phẩm</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="modalProductDetails" runat="server">
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        <i class="bi bi-x-circle me-1"></i> Đóng
                    </button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal sửa sản phẩm -->
    <div class="modal fade" id="editProductModal" tabindex="-1" aria-labelledby="editProductModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title" id="editProductModalLabel"><i class="bi bi-pencil-square me-2"></i>Sửa Sản Phẩm</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hdnProductId" runat="server" />
                    <div class="row">
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="txtTenSPEdit" class="form-label">Tên Sản Phẩm</label>
                                <asp:TextBox ID="txtTenSPEdit" runat="server" CssClass="form-control" placeholder="Nhập tên sản phẩm"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTenSPEdit" runat="server" ControlToValidate="txtTenSPEdit"
                                    ErrorMessage="Vui lòng nhập tên sản phẩm" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="EditProduct"></asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtGiaEdit" class="form-label">Giá</label>
                                <asp:TextBox ID="txtGiaEdit" runat="server" CssClass="form-control" TextMode="Number" placeholder="Nhập giá sản phẩm"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvGiaEdit" runat="server" ControlToValidate="txtGiaEdit"
                                    ErrorMessage="Vui lòng nhập giá sản phẩm" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="EditProduct"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rvGiaEdit" runat="server" ControlToValidate="txtGiaEdit"
                                    MinimumValue="0" MaximumValue="9999999999" Type="Double"
                                    ErrorMessage="Giá phải là số dương" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="EditProduct"></asp:RangeValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtKhuyenMaiEdit" class="form-label">Khuyến Mãi (%)</label>
                                <asp:TextBox ID="txtKhuyenMaiEdit" runat="server" CssClass="form-control" TextMode="Number" placeholder="Nhập % khuyến mãi"></asp:TextBox>
                                <asp:RangeValidator ID="rvKhuyenMaiEdit" runat="server" ControlToValidate="txtKhuyenMaiEdit"
                                    MinimumValue="0" MaximumValue="100" Type="Integer"
                                    ErrorMessage="Khuyến mãi phải từ 0% đến 100%" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="EditProduct"></asp:RangeValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtTongSoLuongEdit" class="form-label">Tổng Số Lượng</label>
                                <asp:TextBox ID="txtTongSoLuongEdit" runat="server" CssClass="form-control" TextMode="Number" placeholder="Nhập tổng số lượng"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTongSoLuongEdit" runat="server" ControlToValidate="txtTongSoLuongEdit"
                                    ErrorMessage="Vui lòng nhập tổng số lượng" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="EditProduct"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rvTongSoLuongEdit" runat="server" ControlToValidate="txtTongSoLuongEdit"
                                    MinimumValue="0" MaximumValue="9999" Type="Integer"
                                    ErrorMessage="Số lượng phải lớn hơn hoặc bằng 0" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="EditProduct"></asp:RangeValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtSoLuongBanEdit" class="form-label">Số Lượng Đã Bán</label>
                                <asp:TextBox ID="txtSoLuongBanEdit" runat="server" CssClass="form-control" TextMode="Number" placeholder="Nhập số lượng đã bán"></asp:TextBox>
                                <asp:RangeValidator ID="rvSoLuongBanEdit" runat="server" ControlToValidate="txtSoLuongBanEdit"
                                    MinimumValue="0" MaximumValue="9999" Type="Integer"
                                    ErrorMessage="Số lượng phải lớn hơn hoặc bằng 0" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="EditProduct"></asp:RangeValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="txtLoaiSPEdit" class="form-label">Loại Sản Phẩm</label>
                                <asp:TextBox ID="txtLoaiSPEdit" runat="server" CssClass="form-control" placeholder="Nhập loại sản phẩm"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLoaiSPEdit" runat="server" ControlToValidate="txtLoaiSPEdit"
                                    ErrorMessage="Vui lòng nhập loại sản phẩm" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="EditProduct"></asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <label for="txtXuatXuEdit" class="form-label">Xuất Xứ</label>
                                <asp:TextBox ID="txtXuatXuEdit" runat="server" CssClass="form-control" placeholder="Nhập xuất xứ"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="ddlTinhTrangEdit" class="form-label">Tình Trạng</label>
                                <asp:DropDownList ID="ddlTinhTrangEdit" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Còn hàng" Value="Còn hàng" />
                                    <asp:ListItem Text="Hết hàng" Value="Hết hàng" />
                                </asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <label for="txtHinhAnhChinhEdit" class="form-label">Hình Ảnh Chính (URL)</label>
                                <asp:TextBox ID="txtHinhAnhChinhEdit" runat="server" CssClass="form-control" placeholder="Nhập URL hình ảnh chính"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvHinhAnhChinhEdit" runat="server" ControlToValidate="txtHinhAnhChinhEdit"
                                    ErrorMessage="Vui lòng nhập URL hình ảnh chính" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="EditProduct"></asp:RequiredFieldValidator>
                                <div class="mt-2">
                                    <img id="imgHinhAnhChinhEditPreview" runat="server" class="img-thumbnail img-preview" alt="Preview" />
                                </div>
                            </div>
                            <div class="mb-3">
                                <label for="txtHinhAnhPhuEdit" class="form-label">Hình Ảnh Phụ 1 (URL)</label>
                                <asp:TextBox ID="txtHinhAnhPhuEdit" runat="server" CssClass="form-control" placeholder="Nhập URL hình ảnh phụ 1 (tùy chọn)"></asp:TextBox>
                                <div class="mt-2">
                                    <img id="imgHinhAnhPhuEditPreview" runat="server" class="img-thumbnail img-preview" alt="Preview" />
                                </div>
                            </div>
                            <div class="mb-3">
                                <label for="txtHinhAnhPhu2Edit" class="form-label">Hình Ảnh Phụ 2 (URL)</label>
                                <asp:TextBox ID="txtHinhAnhPhu2Edit" runat="server" CssClass="form-control" placeholder="Nhập URL hình ảnh phụ 2 (tùy chọn)"></asp:TextBox>
                                <div class="mt-2">
                                    <img id="imgHinhAnhPhu2EditPreview" runat="server" class="img-thumbnail img-preview" alt="Preview" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="txtMoTaEdit" class="form-label">Mô Tả</label>
                        <asp:TextBox ID="txtMoTaEdit" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Nhập mô tả sản phẩm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMoTaEdit" runat="server" ControlToValidate="txtMoTaEdit"
                            ErrorMessage="Vui lòng nhập mô tả sản phẩm" Display="Dynamic" ForeColor="Red"
                            ValidationGroup="EditProduct"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        <i class="bi bi-x-circle me-1"></i> Hủy
                    </button>
                    <asp:Button ID="btnUpdateProduct" runat="server" Text="Cập Nhật" CssClass="btn btn-primary" 
                        OnClick="btnUpdateProduct_Click" ValidationGroup="EditProduct" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal xóa sản phẩm -->
    <div class="modal fade" id="deleteProductModal" tabindex="-1" aria-labelledby="deleteProductModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title" id="deleteProductModalLabel"><i class="bi bi-exclamation-triangle me-2"></i>Xác Nhận Xóa</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hdnDeleteProductId" runat="server" />
                    <p>Bạn có chắc chắn muốn xóa sản phẩm <strong><span id="spnDeleteProductName" runat="server"></span></strong> không?</p>
                    <p class="text-danger">Hành động này không thể hoàn tác!</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        <i class="bi bi-x-circle me-1"></i> Hủy
                    </button>
                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Xóa" CssClass="btn btn-danger" 
                        OnClick="btnConfirmDelete_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
