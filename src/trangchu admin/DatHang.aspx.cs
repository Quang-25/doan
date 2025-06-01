using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;

namespace doan.src.trangchu_admin
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDonHang();
            }
        }
        void LoadDonHang(string search = "")
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM DonHang";
                if (!string.IsNullOrEmpty(search))
                {
                    query += " WHERE MaDH = @search";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", search);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                gvdatHang.DataSource = dt;
                gvdatHang.DataBind();
            }
        }
       

        protected void gvDonHang_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvdatHang.EditIndex = e.NewEditIndex;
            LoadDonHang();
        }

        protected void gvDonHang_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvdatHang.EditIndex = -1;
            LoadDonHang();
        }
        protected void gvDonHang_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvdatHang.Rows[e.RowIndex];
            int maDH = Convert.ToInt32(gvdatHang.DataKeys[e.RowIndex].Value);
            int soLuong = Convert.ToInt32(((TextBox)row.Cells[1].Controls[0]).Text);
            int maND = Convert.ToInt32(((TextBox)row.Cells[2].Controls[0]).Text);
            int maSP = Convert.ToInt32(((TextBox)row.Cells[3].Controls[0]).Text);
            DateTime ngay = Convert.ToDateTime(((TextBox)row.Cells[4].Controls[0]).Text);
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE DonHang SET SoLuong=@SoLuong, MaND=@MaND, MaSP=@MaSP, NgayDatHang=@NgayDatHang WHERE MaDH=@MaDH";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@MaND", maND);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                cmd.Parameters.AddWithValue("@NgayDatHang", ngay);
                cmd.Parameters.AddWithValue("@MaDH", maDH);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            gvdatHang.EditIndex = -1;
            LoadDonHang();
        }
        protected void gvDonHang_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int maDH = Convert.ToInt32(gvdatHang.DataKeys[e.RowIndex].Value);
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))

            {
                string query = "DELETE FROM DonHang WHERE MaDH=@MaDH";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDH", maDH);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadDonHang();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int soLuong = int.Parse(txtSoLuong.Text);
            int maND = int.Parse(txtMaND.Text);
            int maSP = int.Parse(txtMaSP.Text);
            DateTime ngay = DateTime.Now;
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))

            {
                string query = "INSERT INTO DonHang (SoLuong, MaND, MaSP, NgayDatHang) VALUES (@SoLuong, @MaND, @MaSP, @NgayDatHang)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@MaND", maND);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                cmd.Parameters.AddWithValue("@NgayDatHang", ngay);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadDonHang();
        }



    }
}