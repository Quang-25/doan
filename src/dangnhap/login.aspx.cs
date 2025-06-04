using System;
using System.Data.SqlClient;
using System.Configuration;

namespace YourNamespace
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Nếu cần xử lý khi tải trang
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Kiểm tra dữ liệu nhập vào
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

                    string sql = "SELECT MaND FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", username);
                        cmd.Parameters.AddWithValue("@MatKhau", password); // Nếu dùng hash, phải hash ở đây trước khi so sánh

                        object count = cmd.ExecuteScalar();

                        if (count != null)
                        {
                            lblthongbao.Text = "Đăng nhập thành công!";
                            lblthongbao.ForeColor = System.Drawing.Color.Green;

                            // Lưu session hoặc cookie để nhận biết user đã đăng nhập
                            Session["idUser"] = count;

                            // Chuyển hướng đến trang chính sau khi đăng nhập thành công
                            Response.Redirect("/src/home/home.aspx"); // Thay đường dẫn theo dự án bạn
                        }
                        else
                        {
                            lblthongbao.Text = "Tên truy cập hoặc mật khẩu không đúng!";
                            lblthongbao.ForeColor = System.Drawing.Color.Red;
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