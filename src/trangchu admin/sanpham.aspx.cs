using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace doan.src.trangchu_admin
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        private void LoadProducts(string searchTerm = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT * FROM SanPham";
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    sql += " WHERE TenSP LIKE @SearchTerm OR LoaiSP LIKE @SearchTerm";
                }
                sql += " ORDER BY NgayTao DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
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
                    ShowMessage("Lỗi: " + ex.Message, false);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadProducts(searchTerm);
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
                        LoadProducts();

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
            if (e.CommandName == "ViewDetails")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                // Show product details in a modal via Javascript
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
                            string hinhAnh = reader["HinhAnhChinh"].ToString();

                            // Format the detailed information with HTML
                            string detailsHtml = $@"
                                <div class='card'>
                                    <div class='card-body'>
                                        <h5 class='card-title'>{tenSP}</h5>
                                        <img src='{hinhAnh}' class='img-fluid mb-3' style='max-height: 200px;'>
                                        <p><strong>Mô tả:</strong> {moTa}</p>
                                    </div>
                                </div>";

                            // Display details in a Bootstrap modal using JavaScript
                            string script = $@"
                                var detailsModal = new bootstrap.Modal(document.getElementById('productDetailsModal'), {{
                                    keyboard: false
                                }});
                                document.getElementById('modalProductDetails').innerHTML = `{detailsHtml}`;
                                detailsModal.show();";

                            // Register the script
                            ScriptManager.RegisterStartupScript(this, GetType(), "showDetailsModal", script, true);
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
            LoadProducts(txtSearch.Text.Trim());
        }

        protected void gvSanPham_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSanPham.EditIndex = -1;
            LoadProducts(txtSearch.Text.Trim());
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
            LoadProducts(txtSearch.Text.Trim());
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

            LoadProducts(txtSearch.Text.Trim());
        }

        protected void gvSanPham_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSanPham.PageIndex = e.NewPageIndex;
            LoadProducts(txtSearch.Text.Trim());
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
