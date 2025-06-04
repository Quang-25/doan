using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static doan.src.home.home;

namespace doan.src.Giohang
{
    public partial class giohang : System.Web.UI.Page
    {
        public List<modelItems> items= new List<modelItems>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageload();
            }
            
        }
        private string GetUserIdFromCookie()
        {
            if (Session["idUser"] != null)
            {
                return Session["idUser"].ToString();
            }

            return null;
        }
        public void pageload()
        {
            items = GetCart();
            rptgiohang.DataSource = items;
            rptgiohang.DataBind();
            List<modelItems> gioHang = GetCart();
            int tongTien = items.Sum(item => item.Gia * item.SoLuong);
            lblTotal.Text = $"{tongTien:N0} đ";
            
        }

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

            public int MaND { get; set; }

            public int SoLuongBan { get; set; }
            public int SoLuong {get; set; }

        }
        public List<modelItems> GetCart()
        {

            List<modelItems> cart = new List<modelItems>();
            List<modelItems> products = new List<modelItems>();
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"Select SanPham.MaSp, SanPham.TenSp,SanPham.Gia,SanPham.KhuyenMai,SanPham.LoaiSp,SanPham.MoTa,SanPham.TinhTrang,SanPham.HinhAnhChinh,SanPham.HinhAnhPhu,SanPham.HinhAnhPhu2,SanPham.XuatXu,SanPham.NgayTao,DonHang.SoLuong FROM DonHang 
                Join SanPham ON DonHang.MaSP=SanPham.MaSP
                Join NguoiDung ON DonHang.MaND=NguoiDung.MaND Where NguoiDung.MaND = @iduser ORDER BY SanPham.NgayTao";
                
                string userId = GetUserIdFromCookie();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@iduser", userId);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int maSP = Convert.ToInt32(reader["MaSP"]);
                        int giaGoc = Convert.ToInt32(reader["Gia"]);
                        int khuyenMaiPhanTram = Convert.ToInt32(reader["KhuyenMai"]);

                        int giaSauKhuyenMai = giaGoc;




                        if (khuyenMaiPhanTram > 0)
                        {
                            giaSauKhuyenMai = giaGoc - (giaGoc * khuyenMaiPhanTram / 100);

                        }
                        var existingItem = cart.FirstOrDefault(x => x.MaSP == maSP);
                        if (existingItem != null)
                        {
                            // ✅ Nếu đã có, tăng số lượng
                            existingItem.SoLuong += Convert.ToInt32(reader["SoLuong"]);
                        }
                        else
                        {
                        
                            modelItems item = new modelItems
                            {
                                MaSP = maSP,
                                TenSP = reader["TenSP"].ToString(),
                                Gia = giaSauKhuyenMai,
                                KhuyenMai = khuyenMaiPhanTram,
                                SoLuong = Convert.ToInt32(reader["SoLuong"]),
                                LoaiSP = reader["LoaiSP"].ToString(),
                                MoTa = reader["MoTa"].ToString(),
                                TinhTrang = reader["TinhTrang"].ToString(),
                                HinhAnhChinh = reader["HinhAnhChinh"].ToString(),
                                HinhAnhPhu = reader["HinhAnhPhu"].ToString(),
                                HinhAnhPhu2 = reader["HinhAnhPhu2"].ToString(),
                                XuatXu = reader["XuatXu"].ToString(),
                                NgayTao = Convert.ToDateTime(reader["NgayTao"])
                            };
                            cart.Add(item);
                        }
                        
                    }
                    reader.Close();
                } 
                catch (Exception ex)
                {
                    Response.Write("Lỗi: " + ex.Message);
                }
            
            }
            return cart;
        }
        
        protected void rptgiohang_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
        }

        
        
      

       

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("/src/Thanhtoan/Thanhtoan.aspx");
        }
    }
}



        
            
        
    
