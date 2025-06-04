using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace YourNamespace
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra cookie trước
            HttpCookie userCookie = Request.Cookies["UserInfo"];
            if (userCookie != null && !string.IsNullOrEmpty(userCookie["UserID"]))
            {
                // Khôi phục session từ cookie nếu chưa có
                if (Session["idUser"] == null)
                {
                    Session["idUser"] = userCookie["UserID"];
                    Session["ChucVu"] = userCookie["UserRole"];
                    Session["UserName"] = userCookie["UserName"];
                }

                // Chuyển hướng dựa trên vai trò
                string chucVu = userCookie["UserRole"].ToLower();
                if (chucVu == "admin")
                {
                    Response.Redirect("/src/trangchu admin/KhachHang.aspx");
                }
                else
                {
                    Response.Redirect("/src/home/home.aspx");
                }
            }
            // Kiểm tra session nếu không có cookie
            else if (Session["idUser"] != null && Session["ChucVu"] != null)
            {
                string chucVu = Session["ChucVu"].ToString().ToLower();
                if (chucVu == "admin")
                {
                    Response.Redirect("/src/trangchu admin/KhachHang.aspx");
                }
                else
                {
                    Response.Redirect("/src/home/home.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblthongbao.Text = "Vui lòng nhập tên truy cập và mật khẩu.";
                lblthongbao.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "SELECT MaND, ChucVu, TenND FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", username);
                        cmd.Parameters.AddWithValue("@MatKhau", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = Convert.ToInt32(reader["MaND"]);
                                string chucVu = reader["ChucVu"] != DBNull.Value ? reader["ChucVu"].ToString() : "user";
                                string tenND = reader["TenND"].ToString();

                                lblthongbao.Text = "Đăng nhập thành công!";
                                lblthongbao.ForeColor = System.Drawing.Color.Green;

                                // Lưu session
                                Session["idUser"] = userId;
                                Session["UserName"] = tenND;
                                Session["ChucVu"] = chucVu;

                                // Lưu cookie
                                HttpCookie userCookie = new HttpCookie("UserInfo");
                                userCookie["UserID"] = userId.ToString();
                                userCookie["UserName"] = tenND;
                                userCookie["UserRole"] = chucVu;
                                userCookie.Expires = DateTime.Now.AddDays(7); // Cookie có hiệu lực 7 ngày
                                Response.Cookies.Add(userCookie);

                                if (chucVu.ToLower() == "admin")
                                {
                                    Response.Redirect("/src/trangchu admin/KhachHang.aspx");
                                }
                                else
                                {
                                    Response.Redirect("/src/home/home.aspx");
                                }
                            }
                            else
                            {
                                lblthongbao.Text = "Tên truy cập hoặc mật khẩu không đúng!";
                                lblthongbao.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblthongbao.Text = "Lỗi hệ thống: " + ex.Message;
                lblthongbao.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
