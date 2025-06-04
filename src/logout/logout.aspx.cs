using System;
using System.Web;

namespace YourNamespace
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Xóa cookie
            if (Request.Cookies["UserInfo"] != null)
            {
                HttpCookie userCookie = new HttpCookie("UserInfo");
                userCookie.Expires = DateTime.Now.AddDays(-1); // Thiết lập thời gian hết hạn trong quá khứ
                Response.Cookies.Add(userCookie);
            }

            // Xóa session
            Session.Clear();
            Session.Abandon();

            // Chuyển hướng về trang đăng nhập
            Response.Redirect("/src/dangnhap/login.aspx");
        }
    }
}
