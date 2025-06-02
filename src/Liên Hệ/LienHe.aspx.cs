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

        protected void btnGui_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var saved = Session["Captcha"] as string;
                if (!string.IsNullOrEmpty(saved) && txtcaptcha.Text.Trim().Equals(saved, StringComparison.OrdinalIgnoreCase))
                {
                    //Lấy dữ liệu từ người dùng nhập
                    string hoTen = txtHoTen.Text;
                    string Email = txtEmail.Text;
                    string Diachi = txtDiachi.Text;
                    string sodienthoai = txtSDT.Text;
                    string Noidung = txtNoidung.Text;

                    //Chuỗi kết nối từ Web.Config
                    string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    
                    try
                        {
                            
                            {
                                string sql = @"INSERT INTO lienhe (HoTen, Email, DienThoai, MoTa, DiaChi,MaND, UserName, NgayGui) VALUES(@HoTen, @Email, @DienThoai, @MoTa, @Diachi, @MaND, @UserName, @NgayGui)";
                                SqlCommand cmd = new SqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                                cmd.Parameters.AddWithValue("@Email", Email);
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
                            }
                            lblMessage.Text = "<div class='success' > ✅ Bạn đã gửi thành công! </div>";
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = $"<div class='fail'> ❌ Lỗi lưu dữ liệu: {ex.Message}</div>";
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
        private bool IsValidEmail(string email)
        {
            try { var addr = new MailAddress(email); return true; }
            catch { return false; }
        }

        private bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{10,11}$");
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
            GenerateCaptcha();
        }
    }
}