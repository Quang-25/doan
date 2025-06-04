using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace doan.src.headerr
{
    public partial class headerr : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageload();
            }
        }
        public void pageload()
        {
            List<modelItems> gioHang = GetOder();
            int soLuong = gioHang.Count;
            int tongTien = gioHang.Sum(item => item.Gia);
                        lblgiohang.Text = $"{soLuong} sản phẩm - {tongTien:N0} đ";
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

            public int SoLuong { get; set; }
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
    }
}