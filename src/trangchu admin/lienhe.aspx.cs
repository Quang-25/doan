using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace doan.src.trangchu_admin
{
    public partial class lienhe : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        void LoadData(string keyword = "")
        {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "SELECT * FROM LienHe";
                    if (!string.IsNullOrEmpty(keyword))
                        sql += " WHERE HoTen LIKE @kw OR Email LIKE @kw";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (!string.IsNullOrEmpty(keyword))
                        cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    try
                    {
                        conn.Open(); // <-- Thêm dòng này
                        da.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Lỗi: " + ex.Message;
                    }
                }

            }
        

        protected void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"INSERT INTO LienHe (HoTen, Email, DienThoai, DiaChi, MoTa, MaND)
                               VALUES (@HoTen, @Email, @DienThoai, @DiaChi, @MoTa, @MaND)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@DienThoai", txtSDT.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                cmd.Parameters.AddWithValue("@MaND", 1); // giả định admin

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                lblMessage.Text = "<div class='success'>✔️ Thêm thành công</div>";
                LoadData();
            }
        }

        protected void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtHoTen.Text.Trim(); // tìm theo tên hoặc email
            LoadData(keyword);
        }

        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            LoadData();
        }

        protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            string hoTen = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string email = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string dt = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string diachi = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string mota = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"UPDATE LienHe SET HoTen=@HoTen, Email=@Email, DienThoai=@DienThoai, DiaChi=@DiaChi, MoTa=@MoTa WHERE MaLH=@MaLH";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@DienThoai", dt);
                cmd.Parameters.AddWithValue("@DiaChi", diachi);
                cmd.Parameters.AddWithValue("@MoTa", mota);
                cmd.Parameters.AddWithValue("@MaLH", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            GridView1.EditIndex = -1;
            LoadData();
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "DELETE FROM LienHe WHERE MaLH=@MaLH";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaLH", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            LoadData();
        }

        protected void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT MaLH, HoTen, Email, DienThoai, DiaChi, MoTa FROM LienHe WHERE HoTen LIKE @search";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@search", "%" + txtTimKiem.Text + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {

        }
    }
}