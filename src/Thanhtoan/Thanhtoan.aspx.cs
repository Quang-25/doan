using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace doan.src.Thanhtoan
{
    public partial class Thanhtoan : System.Web.UI.Page
    {
        public List<modelItems> ListOrderProduct = new List<modelItems>();
        public class modelItems
        {
            public string idItem { get; set; }
            public string nameItem { get; set; }
          
            public string img0 { get; set; }
            
            public int price { get; set; }
            public int promotion { get; set; }
            
            public int quantity { get; set; }
           
            


        }
        string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = GetUserIdFromCookie();
                if (userId == null)
                {
                    Response.Redirect("/src/dangnhap/login");
                    return;
                }
                LoadDonHang();
                LoadOrderList();

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
        public void LoadOrderList()
        {
            ListOrderProduct = GetOrderProduct();
            rptListoder.DataSource = ListOrderProduct;
            rptListoder.DataBind();

            if (ListOrderProduct.Count > 0)
            {
                int tongTien = 0;

                foreach (modelItems item in ListOrderProduct)
                {
                    if (item.promotion > 0)
                    {
                        // Giá đã khuyến mãi đã tính theo số lượng trong GetOrderProduct
                        tongTien += item.promotion;
                    }
                    else
                    {
                        // Giá gốc nhân số lượng
                        tongTien += item.price * item.quantity;
                    }
                }

                int phiVanChuyen = 20000;
                int tongCong = tongTien + phiVanChuyen;

                lblTienHang.Text = tongTien.ToString("N0") + "đ";
                lblTongCong.Text = tongCong.ToString("N0") + "đ";
            }
            else
            {
                lblTienHang.Text = "0đ";
                lblTongCong.Text = "0đ";
            }
        }

        public List<modelItems> GetOrderProduct()
        {
            List<modelItems> modelItens = new List<modelItems>();

            string contro = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(contro))
            {

                string query = @"SELECT SanPham.MaSP, SanPham.HinhAnhChinh, SanPham.KhuyenMai, SanPham.TenSP, SanPham.Gia, SoLuong FROM DonHang

                            JOIN SanPham ON DonHang.MaSP = SanPham.MaSP
                            JOIN NguoiDung ON DonHang.MaND = NguoiDung.MaND
                            WHERE DonHang.MaND = @userId
                            ORDER BY SanPham.NgayTao";

                SqlCommand cmd = new SqlCommand(query, con);
                string userId = GetUserIdFromCookie();
                cmd.Parameters.AddWithValue("@userId", userId);
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();



                    while (reader.Read())
                    {

                        int pri = Convert.ToInt32(reader["Gia"]);

                        int pro = Convert.ToInt32(reader["KhuyenMai"]);

                        string[] name = reader["TenSP"].ToString().Trim().Split(' ');

                        int quant = Convert.ToInt32(reader["SoLuong"]);
                        string displayName = name.Length >= 3

                                ? $"{name[0]} {name[1]} {name[2]}..."

                                 : reader["TenSP"].ToString();

                        int pricePromotion = pro > 0 ? (pri - ((pri * pro) / 100)) * quant : 0;
                        modelItems item = new modelItems
                        {

                            idItem = reader["MaSP"].ToString(),

                            nameItem = displayName,

                            price = pri,

                            promotion = pricePromotion,
                            img0 = reader["HinhAnhChinh"].ToString(),

                            quantity = quant
                        };
                        modelItens.Add(item);
                    }
                }
                catch (Exception ex)

                {
                    Response.Write("Lỗi: " + ex.Message);
                }
            }
            return modelItens;
        }
        private void LoadDonHang()
        {
            int maND = 1; // tạm cố định, bạn có thể lấy từ session

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT dh.SoLuong, sp.TenSP, sp.Gia, (dh.SoLuong * sp.Gia) AS ThanhTien
                             FROM DonHang dh
                             JOIN SanPham sp ON dh.MaSP = sp.MaSP
                             WHERE dh.MaND = @MaND";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaND", maND);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptDonHang.DataSource = dt;
                rptDonHang.DataBind();

                int tongTien = 0;
                foreach (DataRow row in dt.Rows)
                {
                    tongTien += Convert.ToInt32(row["ThanhTien"]);
                }

                lblTienHang.Text = tongTien.ToString("N0") + " đ";
                int tongCong = tongTien + 20000;
                lblTongCong.Text = tongCong.ToString("N0") + " đ";
            }
        }

        protected void btnThanhToan_Click(object sender, EventArgs e)
        {

            string DefaultConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            ListOrderProduct = GetOrderProduct();
            int tongTien = 0;

            foreach (modelItems item in ListOrderProduct)

            {
                if (item.promotion > 0)

                {
                    tongTien += item.promotion;
                }
                else

                {
                    tongTien += item.price * item.quantity;
                }
            }
            using (SqlConnection conn = new SqlConnection(DefaultConnection))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand getMoneyCmd = new SqlCommand("SELECT SoDu FROM NguoiDung WHERE MaND = @idUsers", conn, transaction);
                    string userId = GetUserIdFromCookie();
                    getMoneyCmd.Parameters.AddWithValue("@idUsers", userId);
                    object result = getMoneyCmd.ExecuteScalar();
                    int soDu = Convert.ToInt32(result);
                    if (soDu < tongTien)
                    {
                        Response.Write("<script>alert('Bạn không đủ tiền để thanh toán.');</script>");

                        return;
                    }
                    int togn = soDu - tongTien;
                    SqlCommand updateMoneyCmd = new SqlCommand("UPDATE NguoiDung SET SoDu = @total WHERE MaND = @idUsers", conn, transaction);
                    updateMoneyCmd.Parameters.AddWithValue("@total", togn);
                    updateMoneyCmd.Parameters.AddWithValue("@idUsers", userId);
                    updateMoneyCmd.ExecuteNonQuery();
                    SqlCommand deleteOrdersCmd = new SqlCommand("DELETE FROM DonHang WHERE MaND = @idUsers", conn, transaction);
                    deleteOrdersCmd.Parameters.AddWithValue("@idUsers", userId);
                    deleteOrdersCmd.ExecuteNonQuery();
                    transaction.Commit();
                    LoadOrderList();
                    Response.Write("<script>alert('Thanh toán thành công!');</script>");
                    Response.Redirect("/src/home/home");
                }
                catch (Exception ex)
                {



                    Response.Write("<script>alert('Lỗi: " + ex.Message + " ');</script>");
                }
            }
        }
    }
}
        