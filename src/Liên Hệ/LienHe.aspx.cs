using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace doan.src.Liên_Hệ
{
    public partial class LienHe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateCaptcha();
            }
        }
        protected void btnTaoLaiCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }
        private void GenerateCaptcha()
        {
            // Tạo mã captcha ngẫu nhiên 5 ký tự
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rand = new Random();
            string captcha = new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[rand.Next(s.Length)])
                          .ToArray()
            );

            lblCapcha.Text = captcha;
            // Lưu vào Session để so sánh khi gửi
            Session["Captcha"] = captcha;
        }

        private bool KiemTraSoDienThoaiTrung(string sdt)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT COUNT(*) FROM LienHe WHERE DienThoai = @sdt";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        private bool KiemTraEmailTrung(string email)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT COUNT(*) FROM NguoiDung WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        protected void btnGui_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var saved = Session["Captcha"] as string;
                if (!string.IsNullOrEmpty(saved) && txtcaptcha.Text.Trim().Equals(saved, StringComparison.OrdinalIgnoreCase))
                {
                    string sodienthoai = txtSDT.Text.Trim();
                    string email = txtEmail.Text.Trim();

                    // Kiểm tra số điện thoại trùng
                    if (KiemTraSoDienThoaiTrung(sodienthoai))
                    {
                        lblLoiSDT.Text = "Số điện thoại đã tồn tại. Vui lòng nhập số khác!";
                        lblLoiSDT.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else
                    {
                        lblLoiSDT.Text = "";
                    }

                    // Kiểm tra email trùng
                    if (KiemTraEmailTrung(email))
                    {
                        lblLoiEmail.Text = "Email đã được đăng ký. Vui lòng dùng email khác!";
                        lblLoiEmail.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else
                    {
                        lblLoiEmail.Text = "";
                    }

                    // Lấy dữ liệu từ người dùng nhập
                    string hoTen = txtHoTen.Text;
                    string Diachi = txtDiachi.Text;
                    string Noidung = txtNoidung.Text;

                    // Chuỗi kết nối từ Web.Config
                    string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        try
                        {
                            string sql = @"INSERT INTO lienhe (HoTen, Email, DienThoai, MoTa, DiaChi, MaND, UserName, NgayGui)
                                   VALUES (@HoTen, @Email, @DienThoai, @MoTa, @Diachi, @MaND, @UserName, @NgayGui)";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@HoTen", hoTen);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@DienThoai", sodienthoai);
                            cmd.Parameters.AddWithValue("@MoTa", Noidung);
                            cmd.Parameters.AddWithValue("@Diachi", Diachi);
                            cmd.Parameters.AddWithValue("@MaND", 2);

                            string userName = "Khách";
                            if (Session["UserName"] != null)
                            {
                                userName = Session["UserName"].ToString();
                            }
                            cmd.Parameters.AddWithValue("@UserName", userName);
                            cmd.Parameters.AddWithValue("@NgayGui", DateTime.Now);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            lblMessage.Text = "<div class='success'>✅ Bạn đã gửi thành công!</div>";
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = $"<div class='fail'>❌ Lỗi lưu dữ liệu: {ex.Message}</div>";
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "<div class='fail'>❌ Mã bảo mật không đúng!</div>";
                    GenerateCaptcha();
                }
            }
            else
            {
                lblMessage.Text = "<div class='fail'>❌ Bạn đã gửi liên hệ thất bại</div>";
            }
        }


        protected void btnNhaplai_Click(object sender, EventArgs e)
        {
            // Xoá form
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtDiachi.Text = "";
            txtSDT.Text = "";
            txtNoidung.Text = "";
            txtcaptcha.Text = "";
            lblLoiSDT.Text = "";
            lblLoiEmail.Text = "";
            GenerateCaptcha();
        }
    }
}