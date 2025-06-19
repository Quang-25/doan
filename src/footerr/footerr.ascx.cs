using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace doan.src.footerr
{
    public partial class footerr : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie userCookie = Request.Cookies["UserInfo"];
            if (userCookie != null && !string.IsNullOrEmpty(userCookie["TenDangNhap"]))
            {
                int soLanTruyCap = LaySoLanTruyCap(userCookie["TenDangNhap"]);
                lblnguoidung.Text = "Số lần truy cập: " + soLanTruyCap;
            }
            else
            {
                lblnguoidung.Text = "Cookie không tồn tại";
            }
        }
        

        
        public int LaySoLanTruyCap(string tenDangNhap)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT SoLanTruyCap FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                //return result != null ? Convert.ToInt32(result) : 0; Code cũ

            }
        }
    }

}