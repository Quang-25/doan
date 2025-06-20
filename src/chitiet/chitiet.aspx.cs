using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace doan.src.chitiet
{
    public partial class chitiet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = GetUserIdFromCookie();
                if (userId == null)
                {
                    Response.Redirect("/src/dangnhap/login.aspx");
                    return;
                }

                // Get product ID from the query string
                string productIdString = Request.QueryString["id"];
                if (string.IsNullOrEmpty(productIdString) || !int.TryParse(productIdString, out int productId))
                {
                    Response.Redirect("/src/home/home.aspx");
                    return;
                }

                // Load product details
                LoadProductDetails(productId);

                // Load related products (same category)
                LoadRelatedProducts(productId);
            }
        }

        private string GetUserIdFromCookie()
        {
            // Ưu tiên kiểm tra Session trước
            if (Session["idUser"] != null)
            {
                return Session["idUser"].ToString();
            }

            // Nếu không có Session, kiểm tra Cookie
            HttpCookie userCookie = Request.Cookies["UserInfo"];
            if (userCookie != null && !string.IsNullOrEmpty(userCookie["UserID"]))
            {
                // Phục hồi Session từ Cookie
                Session["idUser"] = userCookie["UserID"];
                Session["ChucVu"] = userCookie["UserRole"];
                Session["UserName"] = userCookie["UserName"];

                return userCookie["UserID"];
            }

            return null;
        }

        private void LoadProductDetails(int productId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT MaSP, TenSP, Gia, KhuyenMai, TongSoLuong, SoLuongBan, LoaiSP, MoTa,
                           TinhTrang, HinhAnhChinh, HinhAnhPhu, HinhAnhPhu2, XuatXu, NgayTao
                    FROM SanPham
                    WHERE MaSP = @productId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@productId", productId);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Get product data
                        string tenSP = reader["TenSP"].ToString();
                        int giaGoc = Convert.ToInt32(reader["Gia"]);
                        int khuyenMaiPhanTram = Convert.ToInt32(reader["KhuyenMai"]);
                        int giaSauKhuyenMai = giaGoc;
                        string tinhTrang = reader["TinhTrang"].ToString();
                        string xuatXu = reader["XuatXu"].ToString();
                        string moTa = reader["MoTa"].ToString();
                        string hinhAnhChinh = reader["HinhAnhChinh"].ToString();
                        string hinhAnhPhu = reader["HinhAnhPhu"].ToString();
                        string hinhAnhPhu2 = reader["HinhAnhPhu2"].ToString();
                        string loaiSP = reader["LoaiSP"].ToString();

                        // Calculate discount price if applicable
                        if (khuyenMaiPhanTram > 0)
                        {
                            giaSauKhuyenMai = giaGoc - (giaGoc * khuyenMaiPhanTram / 100);
                        }

                        // Set product data to UI elements                        
                        litProductName.Text = tenSP;
                        productNameBreadcrumb.InnerText = tenSP;
                        litStatus.Text = string.IsNullOrEmpty(tinhTrang) ? "Còn hàng" : tinhTrang;
                        litOrigin.Text = string.IsNullOrEmpty(xuatXu) ? "Việt Nam" : xuatXu;
                        litPrice.Text = giaSauKhuyenMai.ToString("N0").Replace(",", ".");
                        litDescription.Text = string.IsNullOrEmpty(moTa) ?
                            "Chưa có mô tả chi tiết cho sản phẩm này." : moTa;                        // Set category link in breadcrumb
                        if (!string.IsNullOrEmpty(loaiSP))
                        {
                            // Use the LoaiSP value directly as it contains the category name in your database schema
                            string categoryName = loaiSP;
                            
                            categoryLink.InnerText = categoryName;
                            categoryLink.HRef = $"/src/home/home.aspx?loai={loaiSP}";
                        }
                        else
                        {
                            categoryLink.InnerText = "Sản phẩm";
                            categoryLink.HRef = "/src/home/home.aspx";
                        }// Set images
                        imgMainProduct.ImageUrl = hinhAnhChinh;
                        imgThumbnail1.ImageUrl = hinhAnhChinh;
                        imgThumbnail1.CssClass = "product-thumb active"; // Set first thumbnail as active
                        imgThumbnail2.ImageUrl = string.IsNullOrEmpty(hinhAnhPhu) ? hinhAnhChinh : hinhAnhPhu;
                        imgThumbnail3.ImageUrl = string.IsNullOrEmpty(hinhAnhPhu2) ? hinhAnhChinh : hinhAnhPhu2;

                        // Show discount info if applicable
                        if (khuyenMaiPhanTram > 0)
                        {
                            pnlDiscount.Visible = true;
                            litOriginalPrice.Text = giaGoc.ToString("N0").Replace(",", ".");
                            litDiscountPercent.Text = khuyenMaiPhanTram.ToString();
                        }
                    }
                    else
                    {
                        // Product not found
                        Response.Redirect("/src/home/home.aspx");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Log or display error
                    Response.Write($"<div class='alert alert-danger' style='padding: 15px; margin: 20px 0; color: #721c24; background-color: #f8d7da; border: 1px solid #f5c6cb; border-radius: 3px;'>Đã xảy ra lỗi: {ex.Message}</div>");
                }
            }
        }

        private void LoadRelatedProducts(int currentProductId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // First, get the category of the current product
                string categoryQuery = "SELECT LoaiSP FROM SanPham WHERE MaSP = @productId";
                string loaiSP = string.Empty;

                SqlCommand cmdCategory = new SqlCommand(categoryQuery, conn);
                cmdCategory.Parameters.AddWithValue("@productId", currentProductId);

                conn.Open();
                object categoryResult = cmdCategory.ExecuteScalar();

                if (categoryResult != null)
                {
                    loaiSP = categoryResult.ToString();
                }                // Now get related products (same category, excluding current product)
                string query = @"
                    SELECT TOP 4 MaSP, TenSP, Gia, KhuyenMai, HinhAnhChinh
                    FROM SanPham
                    WHERE LoaiSP = @loaiSP AND MaSP != @currentProductId
                    ORDER BY NEWID()";  // Using NEWID() for random selection

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@loaiSP", loaiSP);
                cmd.Parameters.AddWithValue("@currentProductId", currentProductId);

                SqlDataReader reader = cmd.ExecuteReader();
                List<modelItems> relatedProducts = new List<modelItems>();

                while (reader.Read())
                {
                    int giaGoc = Convert.ToInt32(reader["Gia"]);
                    int khuyenMaiPhanTram = Convert.ToInt32(reader["KhuyenMai"]);
                    int giaSauKhuyenMai = giaGoc;

                    if (khuyenMaiPhanTram > 0)
                    {
                        giaSauKhuyenMai = giaGoc - (giaGoc * khuyenMaiPhanTram / 100);
                    }

                    modelItems item = new modelItems
                    {
                        MaSP = Convert.ToInt32(reader["MaSP"]),
                        TenSP = reader["TenSP"].ToString(),
                        Gia = giaSauKhuyenMai,
                        KhuyenMai = khuyenMaiPhanTram,
                        HinhAnhChinh = reader["HinhAnhChinh"].ToString()
                    };

                    relatedProducts.Add(item);
                }

                rptRelatedProducts.DataSource = relatedProducts;
                rptRelatedProducts.DataBind();
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            AddToCart(false);
        }

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
            AddToCart(true);
        }

        private void AddToCart(bool redirectToCheckout)
        {
            string userId = GetUserIdFromCookie();
            if (userId == null)
            {
                Response.Redirect("/src/dangnhap/login.aspx");
                return;
            }

            string productIdString = Request.QueryString["id"];
            if (!int.TryParse(productIdString, out int productId))
            {
                return;
            }

            int quantity = 1;
            if (int.TryParse(txtQuantity.Text, out int parsedQuantity) && parsedQuantity > 0)
            {
                quantity = parsedQuantity;
            }

            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO DonHang (SoLuong, MaND, MaSP) VALUES (@soLuong, @userId, @maSP)";
                SqlCommand cmd = new SqlCommand(query, conn); cmd.Parameters.AddWithValue("@soLuong", quantity);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@maSP", productId); try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();                    // Header cart count refresh removed

                    if (redirectToCheckout)
                    {
                        Response.Redirect("/src/Thanhtoan/Thanhtoan.aspx");
                    }
                    else
                    {
                        // Show success message using Response.Write to avoid JavaScript
                        Response.Write("<div class='alert alert-success' style='padding: 15px; margin: 20px 0; color: #155724; background-color: #d4edda; border: 1px solid #c3e6cb; border-radius: 3px;'>Sản phẩm đã được thêm vào giỏ hàng!</div>");
                    }
                }
                catch (Exception ex)
                {
                    // Log or display error
                    Response.Write($"<div class='alert alert-danger' style='padding: 15px; margin: 20px 0; color: #721c24; background-color: #f8d7da; border: 1px solid #f5c6cb; border-radius: 3px;'>Đã xảy ra lỗi khi thêm vào giỏ hàng: {ex.Message}</div>");
                }
            }
        }

        protected void rptRelatedProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                string userId = GetUserIdFromCookie();
                if (userId == null)
                {
                    Response.Redirect("/src/dangnhap/login.aspx");
                    return;
                }

                int productId = Convert.ToInt32(e.CommandArgument);

                string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO DonHang (SoLuong, MaND, MaSP) VALUES (@soLuong, @userId, @maSP)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@soLuong", 1);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@maSP", productId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();                        // Header cart count refresh removed
                        // Show success message
                        Response.Write("<div class='alert alert-success' style='padding: 15px; margin: 20px 0; color: #155724; background-color: #d4edda; border: 1px solid #c3e6cb; border-radius: 3px;'>Sản phẩm đã được thêm vào giỏ hàng!</div>");
                    }
                    catch (Exception ex)
                    {
                        // Log or display error
                        Response.Write($"<div class='alert alert-danger' style='padding: 15px; margin: 20px 0; color: #721c24; background-color: #f8d7da; border: 1px solid #f5c6cb; border-radius: 3px;'>Đã xảy ra lỗi khi thêm vào giỏ hàng: {ex.Message}</div>");
                    }
                }
            }
        }

        protected void btnDecrease_Click(object sender, EventArgs e)
        {
            int quantity = Convert.ToInt32(txtQuantity.Text);
            if (quantity > 1)
            {
                txtQuantity.Text = (quantity - 1).ToString();
            }
        }

        protected void btnIncrease_Click(object sender, EventArgs e)
        {
            int quantity = Convert.ToInt32(txtQuantity.Text);
            txtQuantity.Text = (quantity + 1).ToString();
        }
        protected void imgThumbnail_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton clickedImage = (ImageButton)sender;
            string imageUrl = clickedImage.ImageUrl;
            string commandArg = clickedImage.CommandArgument;

            // Update the main image with the clicked thumbnail
            imgMainProduct.ImageUrl = imageUrl;
            imgMainProduct.CssClass = "product-main-img image-transition";

            // Mark the current thumbnail as active
            imgThumbnail1.CssClass = "product-thumb";
            imgThumbnail2.CssClass = "product-thumb";
            imgThumbnail3.CssClass = "product-thumb";
            clickedImage.CssClass = "product-thumb active";
        }

        // Product model class
        public class modelItems
        {
            public int MaSP { get; set; }
            public string TenSP { get; set; }
            public int Gia { get; set; }
            public int KhuyenMai { get; set; }
            public int TongSoLuong { get; set; }
            public string LoaiSP { get; set; }
            public string MoTa { get; set; }
            public string TinhTrang { get; set; }
            public string HinhAnhChinh { get; set; }
            public string HinhAnhPhu { get; set; }
            public string HinhAnhPhu2 { get; set; }
            public string XuatXu { get; set; }
            public DateTime NgayTao { get; set; }
            public int SoLuongBan { get; set; }
            public int SoLuong { get; set; }
        }
    }
}
