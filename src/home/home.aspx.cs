﻿
using doan.src.Giohang;
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

namespace doan.src.home
{
    public partial class home : System.Web.UI.Page
    {
        public List<modelItems> items = new List<modelItems>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = GetUserIdFromCookie();
                if (userId == null)
                {
                    // Sửa đường dẫn, thêm phần mở rộng .aspx
                    Response.Redirect("/src/dangnhap/login.aspx");
                    return;
                }
                pageload();
                rptSanPhamMoi.DataSource = GetSanPhamMoi();
                rptSanPhamMoi.DataBind();
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

           
        



          
        


        public void pageload()
        {
            items = GetProducts();
            rptSanPham.DataSource = items;
            rptSanPham.DataBind();
            
        }
        

        public List<modelItems> GetProducts()
        {
            List<modelItems> products = new List<modelItems>();
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT MaSP, TenSP, Gia, KhuyenMai,TongSoLuong, LoaiSP, MoTa , TinhTrang, HinhAnhChinh,HinhAnhPhu,HinhAnhPhu2,XuatXu,NgayTao From SanPham ORDER BY NgayTao OFFSET 0 ROWS FETCH NEXT 8 ROWS ONLY ";
                string userId = GetUserIdFromCookie();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
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
                            TongSoLuong = Convert.ToInt32(reader["TongSoLuong"]),
                            LoaiSP = reader["LoaiSP"].ToString(),
                            MoTa = reader["MoTa"].ToString(),
                            TinhTrang = reader["TinhTrang"].ToString(),
                            HinhAnhChinh = reader["HinhAnhChinh"].ToString(),
                            HinhAnhPhu = reader["HinhAnhPhu"].ToString(),
                            HinhAnhPhu2 = reader["HinhAnhPhu2"].ToString(),
                            XuatXu = reader["XuatXu"].ToString(),
                            NgayTao = Convert.ToDateTime(reader["NgayTao"])
                        };
                        products.Add(item);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Lỗi: " + ex.Message);
                }

            } return products;


        }
        public void OnClickUpdateItem(object sender, CommandEventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string userId = GetUserIdFromCookie();
                string query = @"INSERT INTO DonHang (Soluong,MaND,MaSP) VALUES (@soluong,@userId,@idSp)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@soluong", 1);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@idSp", e.CommandArgument.ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            pageload();
            headerHome.pageload();

        }
        public List<modelItems> GetOder()
        {
            List<modelItems> Oder = new List<modelItems>();
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                
                string query = @"Select SanPham.MaSp, SanPham.TenSp,SanPham.Gia,SanPham.KhuyenMai,DonHang.SoLuong,SanPham.LoaiSp,SanPham.MoTa,SanPham.TinhTrang,SanPham.HinhAnhChinh,SanPham.HinhAnhPhu,SanPham.HinhAnhPhu2,SanPham.XuatXu,SanPham.NgayTao,DonHang.SoLuong
                FROM DonHang
                Join SanPham ON DonHang.MaSP = SanPham.MaSP
                Join NguoiDung ON DonHang.MaND = NguoiDung.MaND 
                WHERE DonHang.MaND = @userId
                ORDER BY SanPham.NgayTao";
                string userId = GetUserIdFromCookie();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int giaGoc = Convert.ToInt32(reader["Gia"]);
                        int khuyenMaiPhanTram = Convert.ToInt32(reader["KhuyenMai"]);
                        int giaSauKhuyenMai = giaGoc;
                        if (khuyenMaiPhanTram > 0)
                        {
                            giaSauKhuyenMai = giaGoc - (giaGoc * khuyenMaiPhanTram / 100);
                        }
                        modelItems item = new modelItems()
                        {
                            MaSP = Convert.ToInt32(reader["MaSP"]),
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
                            NgayTao = Convert.ToDateTime(reader["NgayTao"]),
                            
                        };
                        Oder.Add(item);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Lỗi: " + ex.Message);
                }
            }
            return Oder;
        }
        public List<modelItems> GetSanPhamMoi()
        {
            List<modelItems> products = new List<modelItems>();
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                
                string query = @"SELECT TOP 6 MaSP, TenSP, Gia, KhuyenMai, TongSoLuong, LoaiSP, MoTa, TinhTrang, HinhAnhChinh, XuatXu, NgayTao 
                         FROM SanPham 
                         ORDER BY NgayTao DESC";
                string userId = GetUserIdFromCookie();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
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
                            TongSoLuong = Convert.ToInt32(reader["TongSoLuong"]),
                            LoaiSP = reader["LoaiSP"].ToString(),
                            MoTa = reader["MoTa"].ToString(),
                            TinhTrang = reader["TinhTrang"].ToString(),
                            HinhAnhChinh = reader["HinhAnhChinh"].ToString(),
                            XuatXu = reader["XuatXu"].ToString(),
                            NgayTao = Convert.ToDateTime(reader["NgayTao"]),
                        };
                        products.Add(item);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Lỗi: " + ex.Message);
                }
            }
            return products;
        }
        protected void OnClickUpdateItem(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "update")
            {
                string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string userId = GetUserIdFromCookie();
                    string query = @"INSERT INTO DonHang (Soluong,MaND,MaSP) VALUES (@soluong,@userId,@idSp)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@soluong", 1);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@idSp", e.CommandArgument.ToString());
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                pageload();
                headerHome.pageload();
            }
        }
        public class modelItems
        {
            public int MaSP { get; set; }
            public string TenSP { get; set; }
            public int Gia{  get; set; }
            public int KhuyenMai {  get; set; }
            public int TongSoLuong {  get; set; }
            public string LoaiSP { get; set; }
            public string MoTa {  get; set; }
            public string TinhTrang { get; set; }
            public string HinhAnhChinh { get; set;}
            public string HinhAnhPhu { get; set; }

            public string HinhAnhPhu2 {  get; set; }
            public string XuatXu {  get; set; }
            public DateTime NgayTao { get; set; }

            public int MaND {  get; set; }

            public int SoLuongBan { get; set; }

           public int SoLuong { get; set; }
        } 
        
        
        
         protected void OnClickUpdateItem(object sender, EventArgs e)
        {
            

        }


        protected void rptSanPham_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {

        }
    }
    
}