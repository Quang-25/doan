<%@ Page Title="" Language="C#" MasterPageFile="~/src/trangchu admin/admin.Master" AutoEventWireup="true" CodeBehind="sanpham.aspx.cs" Inherits="doan.src.trangchu_admin.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    <link href="sanpham.css" rel="stylesheet" />
    <style>
        .product-management {
            padding: 20px;
            margin-left: 20px;
            background-color: hotpink;
            background-image: url("https://demo037102.web30s.vn/datafiles/34058/upload/images/banner/slide2.jpg?t=1636600660");
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
            background-color: #f8f9fa;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 20px;
            box-shadow: 0 1px 3px rgba(0,0,0,0.1);
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <asp:GridView ID="gvSanPham" runat="server" AutoGenerateColumns="false"
                CssClass="table table-striped table-bordered table-hover"
                DataKeyNames="MaSP" OnRowCommand="gvSanPham_RowCommand"
                OnRowEditing="gvSanPham_RowEditing"
                OnRowCancelingEdit="gvSanPham_RowCancelingEdit"
                OnRowUpdating="gvSanPham_RowUpdating"
                OnRowDeleting="gvSanPham_RowDeleting"
                OnRowDataBound="gvSanPham_RowDataBound"
                AllowPaging="true" PageSize="10" 
                OnPageIndexChanging="gvSanPham_PageIndexChanging">
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="pagination-container" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="MaSP" HeaderText="Mã SP" ReadOnly="true" />
                    <asp:TemplateField HeaderText="Tên Sản Phẩm">
                        <ItemTemplate>
                            <strong><%# Eval("TenSP") %></strong>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTenSP" runat="server" Text='<%# Bind("TenSP") %>' CssClass="form-control" />
                        </EditItemTemplate>
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
                        <EditItemTemplate>
                            <div class="mb-2">
                                <label>Giá gốc:</label>
                                <asp:TextBox ID="txtGia" runat="server" Text='<%# Bind("Gia") %>' CssClass="form-control" />
                            </div>
                            <div>
                                <label>Khuyến mãi (%):</label>
                                <asp:TextBox ID="txtKhuyenMai" runat="server" Text='<%# Bind("KhuyenMai") %>' CssClass="form-control" />
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Số Lượng">
                        <ItemTemplate>
                            <div class="stock-info">
                                <span class="stock-badge bg-primary">Tổng: <%# Eval("TongSoLuong") %></span>
                                <span class="stock-badge bg-success">Đã bán: <%# Eval("SoLuongBan") %></span>
                                <span class="stock-badge bg-info">Trong kho: <%# Eval("TongSoLuong") %></span>
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <div class="mb-2">
                                <label>Tổng số lượng:</label>
                                <asp:TextBox ID="txtTongSoLuong" runat="server" Text='<%# Bind("TongSoLuong") %>' CssClass="form-control" />
                            </div>
                            <div>
                                <label>Số lượng đã bán:</label>
                                <asp:TextBox ID="txtSoLuongBan" runat="server" Text='<%# Bind("SoLuongBan") %>' CssClass="form-control" />
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Loại SP">
                        <ItemTemplate>
                            <%# Eval("LoaiSP") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLoaiSP" runat="server" Text='<%# Bind("LoaiSP") %>' CssClass="form-control" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Xuất xứ">
                        <ItemTemplate>
                            <%# Eval("XuatXu") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtXuatXu" runat="server" Text='<%# Bind("XuatXu") %>' CssClass="form-control" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tình Trạng">
                        <ItemTemplate>
                            <span class='<%# Convert.ToString(Eval("TinhTrang")) == "Còn hàng" ? "product-status-available" : "product-status-unavailable" %>'>
                                <%# Eval("TinhTrang") %>
                            </span>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTinhTrang" runat="server" CssClass="form-select" SelectedValue='<%# Bind("TinhTrang") %>'>
                                <asp:ListItem Text="Còn hàng" Value="Còn hàng" />
                                <asp:ListItem Text="Hết hàng" Value="Hết hàng" />
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hình Ảnh">
                        <ItemTemplate>
                            <img src='<%# Eval("HinhAnhChinh") %>' alt="Product" class="img-thumbnail img-preview" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtHinhAnhChinh" runat="server" Text='<%# Bind("HinhAnhChinh") %>' CssClass="form-control" />
                            <div class="mt-2">
                                <img src='<%# Eval("HinhAnhChinh") %>' alt="Preview" class="img-thumbnail img-preview" />
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Thao Tác">
                        <ItemTemplate>
                            <div class="d-flex flex-column gap-1">
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-edit btn-action">
                                    <i class="bi bi-pencil-square"></i> Sửa
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-delete btn-action" 
                                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa sản phẩm này không?');">
                                    <i class="bi bi-trash"></i> Xóa
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnView" runat="server" CommandName="ViewDetails" CommandArgument='<%# Eval("MaSP") %>' CssClass="btn btn-sm btn-view btn-action">
                                    <i class="bi bi-eye"></i> Chi tiết
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <div class="d-flex flex-column gap-1">
                                <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success btn-action">
                                    <i class="bi bi-check-lg"></i> Cập nhật
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary btn-action">
                                    <i class="bi bi-x-lg"></i> Hủy
                                </asp:LinkButton>
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info text-center">
                        <i class="bi bi-info-circle me-2"></i> Không có sản phẩm nào. Hãy thêm sản phẩm mới!
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
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

    <!-- Bootstrap Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
