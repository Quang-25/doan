using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace doan.src.DANGKY
{
    public partial class dangky : System.Web.UI.Page

    {
        private static List<string> danhSachTaiKhoan = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 1. Lấy dữ liệu từ bảng NguoiDung (để kiểm tra trùng)
                string sqlSelect = "SELECT * FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
                SqlCommand cmdCheck = new SqlCommand(sqlSelect, conn);
                cmdCheck.Parameters.AddWithValue("@TenDangNhap", txtTenTruyCap.Text);
                SqlDataReader reader = cmdCheck.ExecuteReader();

                if (reader.HasRows)
                {
                    // Tên đăng nhập đã tồn tại
                    lblthongbao.Text = "Không hợp lệ! vui lòng đăng ký lại.";

                    reader.Close();
                    return;
                }
                reader.Close();

                // 2. Không trùng thì thêm dữ liệu mới (đăng ký)
                string sqlInsert = @"INSERT INTO NguoiDung (TenND, Sdt, Email, TenDangNhap, MatKhau, ChucVu, SoDu)
                             VALUES (@TenND, @Sdt, @Email, @TenDangNhap, @MatKhau, @ChucVu, @SoDu)";
                SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn);
                cmdInsert.Parameters.AddWithValue("@TenND", txtTenTruyCap.Text);
                cmdInsert.Parameters.AddWithValue("@Sdt", txtDienThoai.Text);
                cmdInsert.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmdInsert.Parameters.AddWithValue("@TenDangNhap", txtHoTen.Text);
                cmdInsert.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                cmdInsert.Parameters.AddWithValue("@ChucVu", "user"); // hoặc txtChucVu.Text nếu bạn có nhập

                cmdInsert.Parameters.AddWithValue("@SoDu", 0); // hoặc txtSoDu.Text nếu có

                cmdInsert.ExecuteNonQuery();

            }
            Response.Redirect("https://localhost:44322/src/DANGNHAP/login");
        }

        protected void btnLamLai_Click(object sender, EventArgs e)
        {
            txtTenTruyCap.Text = "";
            txtXacNhan.Text = "";
            txtEmail.Text = "";
            txtDienThoai.Text = "";
            txtHoTen.Text = "";
            txtMatKhau.Text = "";
            lblthongbao.Text = "";

        }
    }
}