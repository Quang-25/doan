using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace doan.src.trangchu_admin
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "utf-8";
            if (!IsPostBack)
            {
                // Register AJAX script for async updates
                ScriptManager.RegisterStartupScript(this, GetType(), "RegisterAsyncPostBackControl",
                    "Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function() { $('.modal').modal('hide'); });", true);

                LoadProductCategories();
                LoadProducts();
            }
        }

        private void LoadProductCategories()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT DISTINCT LoaiSP FROM SanPham ORDER BY LoaiSP";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string category = reader["LoaiSP"].ToString();
                        ddlLoaiSP.Items.Add(new ListItem(category, category));
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Lỗi khi tải danh mục: " + ex.Message, false);
                }
            }
        }

        private void LoadProducts(string searchTerm = "", string categoryFilter = "", string statusFilter = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT * FROM SanPham WHERE 1=1";

                // Apply search filter
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    sql += " AND (TenSP LIKE @SearchTerm OR LoaiSP LIKE @SearchTerm)";
                }

                // Apply category filter
                if (!string.IsNullOrEmpty(categoryFilter))
                {
                    sql += " AND LoaiSP = @CategoryFilter";
                }

                // Apply status filter
                if (!string.IsNullOrEmpty(statusFilter))
                {
                    sql += " AND TinhTrang = @StatusFilter";
                }

                sql += " ORDER BY NgayTao DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                // Add parameters
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                }

                if (!string.IsNullOrEmpty(categoryFilter))
                {
                    cmd.Parameters.AddWithValue("@CategoryFilter", categoryFilter);
                }

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    cmd.Parameters.AddWithValue("@StatusFilter", statusFilter);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);
                    gvSanPham.DataSource = dt;
                    gvSanPham.DataBind();
                }
                catch (Exception ex)
                {
                    ShowMessage("Lỗi khi tải sản phẩm: " + ex.Message, false);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            string categoryFilter = ddlLoaiSP.SelectedValue;
            string statusFilter = ddlTinhTrangLoc.SelectedValue;

            // Reset to first page when searching
            gvSanPham.PageIndex = 0;

            LoadProducts(searchTerm, categoryFilter, statusFilter);
        }

        protected void ddlLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            string categoryFilter = ddlLoaiSP.SelectedValue;
            string statusFilter = ddlTinhTrangLoc.SelectedValue;

            // Reset to first page when filtering
            gvSanPham.PageIndex = 0;

            LoadProducts(searchTerm, categoryFilter, statusFilter);
        }

        protected void ddlTinhTrangLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            string categoryFilter = ddlLoaiSP.SelectedValue;
            string statusFilter = ddlTinhTrangLoc.SelectedValue;

            // Reset to first page when filtering
            gvSanPham.PageIndex = 0;

            LoadProducts(searchTerm, categoryFilter, statusFilter);
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            gvSanPham.PageSize = pageSize;

            // Reset to first page when changing page size
            gvSanPham.PageIndex = 0;

            string searchTerm = txtSearch.Text.Trim();
            string categoryFilter = ddlLoaiSP.SelectedValue;
            string statusFilter = ddlTinhTrangLoc.SelectedValue;

            LoadProducts(searchTerm, categoryFilter, statusFilter);
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"INSERT INTO SanPham 
                                  (TenSP, Gia, KhuyenMai, TongSoLuong, SoLuongBan, LoaiSP, MoTa, TinhTrang, 
                                   HinhAnhChinh, HinhAnhPhu, HinhAnhPhu2, XuatXu, NgayTao)
                                  VALUES 
                                  (@TenSP, @Gia, @KhuyenMai, @TongSoLuong, @SoLuongBan, @LoaiSP, @MoTa, 
                                   @TinhTrang, @HinhAnhChinh, @HinhAnhPhu, @HinhAnhPhu2, @XuatXu, @NgayTao)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TenSP", txtTenSPMoi.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gia", Convert.ToDouble(txtGiaMoi.Text.Trim()));

                    // Handle nullable parameters
                    if (string.IsNullOrEmpty(txtKhuyenMaiMoi.Text.Trim()))
                        cmd.Parameters.AddWithValue("@KhuyenMai", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@KhuyenMai", Convert.ToInt32(txtKhuyenMaiMoi.Text.Trim()));

                    cmd.Parameters.AddWithValue("@TongSoLuong", Convert.ToInt32(txtTongSoLuongMoi.Text.Trim()));

                    if (string.IsNullOrEmpty(txtSoLuongBanMoi.Text.Trim()))
                        cmd.Parameters.AddWithValue("@SoLuongBan", 0);
                    else
                        cmd.Parameters.AddWithValue("@SoLuongBan", Convert.ToInt32(txtSoLuongBanMoi.Text.Trim()));

                    cmd.Parameters.AddWithValue("@LoaiSP", txtLoaiSPMoi.Text.Trim());
                    cmd.Parameters.AddWithValue("@MoTa", txtMoTaMoi.Text.Trim());
                    cmd.Parameters.AddWithValue("@TinhTrang", ddlTinhTrangMoi.SelectedValue);
                    cmd.Parameters.AddWithValue("@HinhAnhChinh", txtHinhAnhChinhMoi.Text.Trim());

                    if (string.IsNullOrEmpty(txtHinhAnhPhuMoi.Text.Trim()))
                        cmd.Parameters.AddWithValue("@HinhAnhPhu", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@HinhAnhPhu", txtHinhAnhPhuMoi.Text.Trim());

                    if (string.IsNullOrEmpty(txtHinhAnhPhu2Moi.Text.Trim()))
                        cmd.Parameters.AddWithValue("@HinhAnhPhu2", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@HinhAnhPhu2", txtHinhAnhPhu2Moi.Text.Trim());

                    if (string.IsNullOrEmpty(txtXuatXuMoi.Text.Trim()))
                        cmd.Parameters.AddWithValue("@XuatXu", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@XuatXu", txtXuatXuMoi.Text.Trim());

                    cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        ShowMessage("✅ Thêm sản phẩm thành công!", true);
                        ClearForm();

                        // Reload with current filters
                        string searchTerm = txtSearch.Text.Trim();
                        string categoryFilter = ddlLoaiSP.SelectedValue;
                        string statusFilter = ddlTinhTrangLoc.SelectedValue;
                        LoadProducts(searchTerm, categoryFilter, statusFilter);

                        // Reset modal by JavaScript
                        ScriptManager.RegisterStartupScript(this, GetType(), "hideModal",
                            "$('#addProductModal').modal('hide');", true);
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("❌ Lỗi: " + ex.Message, false);
                    }
                }
            }
        }

        private void ClearForm()
        {
            txtTenSPMoi.Text = string.Empty;
            txtGiaMoi.Text = string.Empty;
            txtKhuyenMaiMoi.Text = string.Empty;
            txtTongSoLuongMoi.Text = string.Empty;
            txtSoLuongBanMoi.Text = string.Empty;
            txtLoaiSPMoi.Text = string.Empty;
            txtMoTaMoi.Text = string.Empty;
            ddlTinhTrangMoi.SelectedIndex = 0;
            txtHinhAnhChinhMoi.Text = string.Empty;
            txtHinhAnhPhuMoi.Text = string.Empty;
            txtHinhAnhPhu2Moi.Text = string.Empty;
            txtXuatXuMoi.Text = string.Empty;
        }

        protected void gvSanPham_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails" || e.CommandName == "EditModal" || e.CommandName == "DeleteModal")
            {
                int productId = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "SELECT * FROM SanPham WHERE MaSP = @MaSP";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSP", productId);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string tenSP = reader["TenSP"].ToString();
                            string moTa = reader["MoTa"].ToString();
                            string hinhAnhChinh = reader["HinhAnhChinh"].ToString();
                            string hinhAnhPhu = reader["HinhAnhPhu"] != DBNull.Value ? reader["HinhAnhPhu"].ToString() : string.Empty;
                            string hinhAnhPhu2 = reader["HinhAnhPhu2"] != DBNull.Value ? reader["HinhAnhPhu2"].ToString() : string.Empty;
                            string loaiSP = reader["LoaiSP"].ToString();
                            string tinhTrang = reader["TinhTrang"].ToString();
                            string xuatXu = reader["XuatXu"] != DBNull.Value ? reader["XuatXu"].ToString() : string.Empty;
                            double gia = Convert.ToDouble(reader["Gia"]);
                            int tongSoLuong = Convert.ToInt32(reader["TongSoLuong"]);
                            int soLuongBan = Convert.ToInt32(reader["SoLuongBan"]);
                            string khuyenMai = reader["KhuyenMai"] != DBNull.Value ? reader["KhuyenMai"].ToString() : string.Empty;

                            if (e.CommandName == "ViewDetails")
                            {
                                // Format the detailed information with HTML
                                string giaHienThi = khuyenMai != string.Empty ?
                                    $"{gia * (1 - Convert.ToDouble(khuyenMai) / 100):N0}đ <del>{gia:N0}đ</del> (-{khuyenMai}%)" :
                                    $"{gia:N0}đ";

                                string detailsHtml = $@"
                                    <div class='row'>
                                        <div class='col-md-5'>
                                            <img src='{hinhAnhChinh}' class='img-fluid mb-3' alt='{tenSP}'>
                                            <div class='row'>
                                                {(!string.IsNullOrEmpty(hinhAnhPhu) ? $@"<div class='col-6'><img src='{hinhAnhPhu}' class='img-thumbnail' alt='Hình ảnh phụ 1'></div>" : "")}
                                                {(!string.IsNullOrEmpty(hinhAnhPhu2) ? $@"<div class='col-6'><img src='{hinhAnhPhu2}' class='img-thumbnail' alt='Hình ảnh phụ 2'></div>" : "")}
                                            </div>
                                        </div>
                                        <div class='col-md-7'>
                                            <h4 class='mb-3'>{tenSP}</h4>
                                            <p class='fs-5 fw-bold text-primary'>{giaHienThi}</p>
                                            <div class='mb-3'>
                                                <span class='badge bg-secondary'>{loaiSP}</span>
                                                <span class='badge {(tinhTrang == "Còn hàng" ? "bg-success" : "bg-danger")}'>{tinhTrang}</span>
                                                {(!string.IsNullOrEmpty(xuatXu) ? $@"<span class='badge bg-info'>Xuất xứ: {xuatXu}</span>" : "")}
                                            </div>
                                            <div class='mb-3'>
                                                <p><strong>Tổng số lượng:</strong> {tongSoLuong + soLuongBan}</p>
                                                <p><strong>Số lượng đã bán:</strong> {soLuongBan}</p>
                                                <p><strong>Tồn kho:</strong> {tongSoLuong}</p>
                                            </div>
                                            <div class='mb-3'>
                                                <h5>Mô tả:</h5>
                                                <p>{moTa}</p>
                                            </div>
                                        </div>
                                    </div>";

                                // Display details in a Bootstrap modal using JavaScript
                                string script = $@"
                                    document.getElementById('modalProductDetails').innerHTML = `{detailsHtml}`;
                                    var detailsModal = new bootstrap.Modal(document.getElementById('productDetailsModal'));
                                    detailsModal.show();";

                                // Register the script
                                ScriptManager.RegisterStartupScript(this, GetType(), "showDetailsModal", script, true);
                            }
                            else if (e.CommandName == "EditModal")
                            {
                                // Fill edit modal with product data
                                hdnProductId.Value = productId.ToString();
                                txtTenSPEdit.Text = tenSP;
                                txtGiaEdit.Text = gia.ToString();
                                txtKhuyenMaiEdit.Text = khuyenMai;
                                txtTongSoLuongEdit.Text = tongSoLuong.ToString();
                                txtSoLuongBanEdit.Text = soLuongBan.ToString();
                                txtLoaiSPEdit.Text = loaiSP;
                                txtXuatXuEdit.Text = xuatXu;
                                ddlTinhTrangEdit.SelectedValue = tinhTrang;
                                txtHinhAnhChinhEdit.Text = hinhAnhChinh;
                                txtHinhAnhPhuEdit.Text = hinhAnhPhu;
                                txtHinhAnhPhu2Edit.Text = hinhAnhPhu2;
                                txtMoTaEdit.Text = moTa;

                                // Set image previews
                                imgHinhAnhChinhEditPreview.Src = hinhAnhChinh;
                                imgHinhAnhPhuEditPreview.Src = hinhAnhPhu;
                                imgHinhAnhPhu2EditPreview.Src = hinhAnhPhu2;

                                // Show edit modal
                                ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal",
                                    "var editModal = new bootstrap.Modal(document.getElementById('editProductModal')); editModal.show();", true);
                            }
                            else if (e.CommandName == "DeleteModal")
                            {
                                // Set delete confirmation values
                                hdnDeleteProductId.Value = productId.ToString();
                                spnDeleteProductName.InnerText = tenSP;

                                // Show delete confirmation modal
                                ScriptManager.RegisterStartupScript(this, GetType(), "showDeleteModal",
                                    "var deleteModal = new bootstrap.Modal(document.getElementById('deleteProductModal')); deleteModal.show();", true);
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("❌ Lỗi: " + ex.Message, false);
                    }
                }
            }
        }

        protected void gvSanPham_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSanPham.EditIndex = e.NewEditIndex;
            string searchTerm = txtSearch.Text.Trim();
            string categoryFilter = ddlLoaiSP.SelectedValue;
            string statusFilter = ddlTinhTrangLoc.SelectedValue;
            LoadProducts(searchTerm, categoryFilter, statusFilter);
        }

        protected void gvSanPham_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSanPham.EditIndex = -1;
            string searchTerm = txtSearch.Text.Trim();
            string categoryFilter = ddlLoaiSP.SelectedValue;
            string statusFilter = ddlTinhTrangLoc.SelectedValue;
            LoadProducts(searchTerm, categoryFilter, statusFilter);
        }

        protected void gvSanPham_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int productId = Convert.ToInt32(gvSanPham.DataKeys[e.RowIndex].Value);

            // Get values from the GridView EditItemTemplate
            TextBox txtTenSP = (TextBox)gvSanPham.Rows[e.RowIndex].FindControl("txtTenSP");
            TextBox txtGia = (TextBox)gvSanPham.Rows[e.RowIndex].FindControl("txtGia");
            TextBox txtKhuyenMai = (TextBox)gvSanPham.Rows[e.RowIndex].FindControl("txtKhuyenMai");
            TextBox txtTongSoLuong = (TextBox)gvSanPham.Rows[e.RowIndex].FindControl("txtTongSoLuong");
            TextBox txtSoLuongBan = (TextBox)gvSanPham.Rows[e.RowIndex].FindControl("txtSoLuongBan");
            TextBox txtLoaiSP = (TextBox)gvSanPham.Rows[e.RowIndex].FindControl("txtLoaiSP");
            TextBox txtXuatXu = (TextBox)gvSanPham.Rows[e.RowIndex].FindControl("txtXuatXu");
            DropDownList ddlTinhTrang = (DropDownList)gvSanPham.Rows[e.RowIndex].FindControl("ddlTinhTrang");
            TextBox txtHinhAnhChinh = (TextBox)gvSanPham.Rows[e.RowIndex].FindControl("txtHinhAnhChinh");

            if (txtTenSP == null || txtGia == null || txtTongSoLuong == null ||
                txtSoLuongBan == null || txtLoaiSP == null || ddlTinhTrang == null ||
                txtHinhAnhChinh == null)
            {
                ShowMessage("❌ Lỗi: Không thể tìm thấy các điều khiển trong GridView.", false);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"UPDATE SanPham 
                              SET TenSP = @TenSP, 
                                  Gia = @Gia, 
                                  KhuyenMai = @KhuyenMai, 
                                  TongSoLuong = @TongSoLuong, 
                                  SoLuongBan = @SoLuongBan, 
                                  LoaiSP = @LoaiSP, 
                                  XuatXu = @XuatXu,
                                  TinhTrang = @TinhTrang, 
                                  HinhAnhChinh = @HinhAnhChinh
                              WHERE MaSP = @MaSP";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", productId);
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text.Trim());
                cmd.Parameters.AddWithValue("@Gia", Convert.ToDouble(txtGia.Text.Trim()));

                if (string.IsNullOrEmpty(txtKhuyenMai.Text.Trim()))
                    cmd.Parameters.AddWithValue("@KhuyenMai", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@KhuyenMai", Convert.ToInt32(txtKhuyenMai.Text.Trim()));

                cmd.Parameters.AddWithValue("@TongSoLuong", Convert.ToInt32(txtTongSoLuong.Text.Trim()));
                cmd.Parameters.AddWithValue("@SoLuongBan", Convert.ToInt32(txtSoLuongBan.Text.Trim()));
                cmd.Parameters.AddWithValue("@LoaiSP", txtLoaiSP.Text.Trim());

                if (string.IsNullOrEmpty(txtXuatXu.Text.Trim()))
                    cmd.Parameters.AddWithValue("@XuatXu", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@XuatXu", txtXuatXu.Text.Trim());

                cmd.Parameters.AddWithValue("@TinhTrang", ddlTinhTrang.SelectedValue);
                cmd.Parameters.AddWithValue("@HinhAnhChinh", txtHinhAnhChinh.Text.Trim());

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    ShowMessage("✅ Cập nhật sản phẩm thành công!", true);
                }
                catch (Exception ex)
                {
                    ShowMessage("❌ Lỗi: " + ex.Message, false);
                }
            }

            gvSanPham.EditIndex = -1;
            string searchTerm = txtSearch.Text.Trim();
            string categoryFilter = ddlLoaiSP.SelectedValue;
            string statusFilter = ddlTinhTrangLoc.SelectedValue;
            LoadProducts(searchTerm, categoryFilter, statusFilter);
        }

        protected void gvSanPham_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productId = Convert.ToInt32(gvSanPham.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", productId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    ShowMessage("✅ Xóa sản phẩm thành công!", true);
                }
                catch (Exception ex)
                {
                    ShowMessage("❌ Lỗi: " + ex.Message + " Sản phẩm này có thể đã được đặt hàng, không thể xóa.", false);
                }
            }

            string searchTerm = txtSearch.Text.Trim();
            string categoryFilter = ddlLoaiSP.SelectedValue;
            string statusFilter = ddlTinhTrangLoc.SelectedValue;
            LoadProducts(searchTerm, categoryFilter, statusFilter);
        }

        protected void gvSanPham_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSanPham.PageIndex = e.NewPageIndex;

            string searchTerm = txtSearch.Text.Trim();
            string categoryFilter = ddlLoaiSP.SelectedValue;
            string statusFilter = ddlTinhTrangLoc.SelectedValue;

            LoadProducts(searchTerm, categoryFilter, statusFilter);
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int productId;
                if (!int.TryParse(hdnProductId.Value, out productId))
                {
                    ShowMessage("❌ Lỗi: ID sản phẩm không hợp lệ.", false);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"UPDATE SanPham 
                                  SET TenSP = @TenSP, 
                                      Gia = @Gia, 
                                      KhuyenMai = @KhuyenMai, 
                                      TongSoLuong = @TongSoLuong, 
                                      SoLuongBan = @SoLuongBan, 
                                      LoaiSP = @LoaiSP, 
                                      XuatXu = @XuatXu,
                                      TinhTrang = @TinhTrang,
                                      HinhAnhChinh = @HinhAnhChinh,
                                      HinhAnhPhu = @HinhAnhPhu,
                                      HinhAnhPhu2 = @HinhAnhPhu2,
                                      MoTa = @MoTa
                                  WHERE MaSP = @MaSP";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSP", productId);
                    cmd.Parameters.AddWithValue("@TenSP", txtTenSPEdit.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gia", Convert.ToDouble(txtGiaEdit.Text.Trim()));

                    if (string.IsNullOrEmpty(txtKhuyenMaiEdit.Text.Trim()))
                        cmd.Parameters.AddWithValue("@KhuyenMai", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@KhuyenMai", Convert.ToInt32(txtKhuyenMaiEdit.Text.Trim()));

                    cmd.Parameters.AddWithValue("@TongSoLuong", Convert.ToInt32(txtTongSoLuongEdit.Text.Trim()));
                    cmd.Parameters.AddWithValue("@SoLuongBan", Convert.ToInt32(txtSoLuongBanEdit.Text.Trim()));
                    cmd.Parameters.AddWithValue("@LoaiSP", txtLoaiSPEdit.Text.Trim());

                    if (string.IsNullOrEmpty(txtXuatXuEdit.Text.Trim()))
                        cmd.Parameters.AddWithValue("@XuatXu", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@XuatXu", txtXuatXuEdit.Text.Trim());

                    cmd.Parameters.AddWithValue("@TinhTrang", ddlTinhTrangEdit.SelectedValue);
                    cmd.Parameters.AddWithValue("@HinhAnhChinh", txtHinhAnhChinhEdit.Text.Trim());

                    if (string.IsNullOrEmpty(txtHinhAnhPhuEdit.Text.Trim()))
                        cmd.Parameters.AddWithValue("@HinhAnhPhu", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@HinhAnhPhu", txtHinhAnhPhuEdit.Text.Trim());

                    if (string.IsNullOrEmpty(txtHinhAnhPhu2Edit.Text.Trim()))
                        cmd.Parameters.AddWithValue("@HinhAnhPhu2", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@HinhAnhPhu2", txtHinhAnhPhu2Edit.Text.Trim());

                    cmd.Parameters.AddWithValue("@MoTa", txtMoTaEdit.Text.Trim());

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        ShowMessage("✅ Cập nhật sản phẩm thành công!", true);

                        // Reload the products
                        string searchTerm = txtSearch.Text.Trim();
                        string categoryFilter = ddlLoaiSP.SelectedValue;
                        string statusFilter = ddlTinhTrangLoc.SelectedValue;
                        LoadProducts(searchTerm, categoryFilter, statusFilter);

                        // Hide modal
                        ScriptManager.RegisterStartupScript(this, GetType(), "hideEditModal",
                            "$('#editProductModal').modal('hide');", true);
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("❌ Lỗi: " + ex.Message, false);
                    }
                }
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int productId;
            if (!int.TryParse(hdnDeleteProductId.Value, out productId))
            {
                ShowMessage("❌ Lỗi: ID sản phẩm không hợp lệ.", false);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", productId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    ShowMessage("✅ Xóa sản phẩm thành công!", true);

                    // Reload the products
                    string searchTerm = txtSearch.Text.Trim();
                    string categoryFilter = ddlLoaiSP.SelectedValue;
                    string statusFilter = ddlTinhTrangLoc.SelectedValue;
                    LoadProducts(searchTerm, categoryFilter, statusFilter);

                    // Hide modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideDeleteModal",
                        "$('#deleteProductModal').modal('hide');", true);
                }
                catch (Exception ex)
                {
                    ShowMessage("❌ Lỗi: " + ex.Message + " Sản phẩm này có thể đã được đặt hàng, không thể xóa.", false);

                    // Hide modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideDeleteModal",
                        "$('#deleteProductModal').modal('hide');", true);
                }
            }
        }

        protected void gvSanPham_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Customize row appearance based on data
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv != null)
                {
                    string tinhTrang = drv["TinhTrang"].ToString();
                    int tongSoLuong = Convert.ToInt32(drv["TongSoLuong"]);
                    int soLuongBan = Convert.ToInt32(drv["SoLuongBan"]);

                    if (tongSoLuong <= soLuongBan || tinhTrang == "Hết hàng")
                    {
                        e.Row.CssClass = e.Row.CssClass + " table-danger";
                    }
                }
            }
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            string cssClass = isSuccess ? "success-message" : "error-message";
            lblMessage.Text = $"<div class='{cssClass}'>{message}</div>";
        }
    }
}
