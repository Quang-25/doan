using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace doan.src.trangchu_admin
{
    public partial class admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.AbsolutePath;
            if (url == "/src/trangchu%20admin/KhachHang")
            {
                p_boxNavigationBar1.CssClass = "boxNavigationChoose";
            }
            else if (url == "/src/trangchu%20admin/sanpham")
            {
                p_boxNavigationBar2.CssClass = "boxNavigationChoose";
            }
            else if (url == "/src/trangchu%20admin/DatHang")
            {
                p_boxNavigationBar3.CssClass = "boxNavigationChoose";
            }
            else if (url == "/src/trangchu%20admin/lienhe")
            {
                p_boxNavigationBar4.CssClass = "boxNavigationChoose";
            }
        }
        public void OnclickButtonNavigate(object sender, EventArgs e)
        {
            Response.Redirect("/src/trangchu%20admin/KhachHang");
        }
        public void OnclickButtonNavigate1(object sender, EventArgs e)
        {
            Response.Redirect("/src/trangchu%20admin/sanpham");
        }
        public void OnclickButtonNavigate2(object sender, EventArgs e)
        {
            Response.Redirect("/src/trangchu%20admin/DatHang");
        }
        public void OnclickButtonNavigate3(object sender, EventArgs e)
        {
            Response.Redirect("/src/trangchu%20admin/lienhe");
        }
    }
}