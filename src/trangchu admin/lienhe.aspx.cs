using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        private void LoadData(string keyword = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT * FROM LienHe";
                if (!string.IsNullOrEmpty(keyword))
                {
                    query += " WHERE HoTen LIKE @kw OR Email LIKE @kw OR UserName LIKE @kw";
                }
                query += " ORDER BY NgayGui DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(keyword))
                {
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLienHe.DataSource = dt;
                gvLienHe.DataBind();
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO LienHe (HoTen, Email, DienThoai, DiaChi, MoTa, NgayGui, UserName, MaND)
                                 VALUES (@HoTen, @Email, @DienThoai, @DiaChi, @MoTa, @NgayGui, @UserName, @MaND)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@DienThoai", txtSDT.Text.Trim());
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("@MoTa", txtNoiDung.Text.Trim());
                cmd.Parameters.AddWithValue("@NgayGui", DateTime.Now);

                string userName = txtUserName.Text.Trim();
                if (string.IsNullOrEmpty(userName)) userName = "Khách";
                cmd.Parameters.AddWithValue("@UserName", userName);

                int maND = 1; // Hoặc lấy từ Session["MaND"]
                cmd.Parameters.AddWithValue("@MaND", maND);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        lblMessage.Text = "✅ Thêm liên hệ thành công!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        ClearForm();
                        LoadData();
                        
                    }
                    else
                    {
                        lblMessage.Text = "❌ Thêm thất bại!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "❌ Lỗi: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void ClearForm()
        {
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtNoiDung.Text = "";
            txtUserName.Text = "";
            txtHoTen.Focus();
        }

        protected void gvLienHe_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLienHe.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void gvLienHe_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvLienHe.EditIndex = -1;
            LoadData();
        }

        protected void gvLienHe_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvLienHe.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvLienHe.Rows[e.RowIndex];

            string hoTen = ((TextBox)row.FindControl("txtHoTen")).Text.Trim();
            string email = ((TextBox)row.FindControl("txtEmail")).Text.Trim();
            string sdt = ((TextBox)row.FindControl("txtSDT")).Text.Trim();
            string diaChi = ((TextBox)row.FindControl("txtDiaChi")).Text.Trim();
            string moTa = ((TextBox)row.FindControl("txtNoiDung")).Text.Trim();
            string userName = ((TextBox)row.FindControl("txtUserName")).Text.Trim();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"UPDATE LienHe 
                                 SET HoTen=@HoTen, Email=@Email, DienThoai=@DienThoai, DiaChi=@DiaChi, MoTa=@MoTa, UserName=@UserName
                                 WHERE MaLH=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@DienThoai", sdt);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@MoTa", moTa);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    gvLienHe.EditIndex = -1;
                    LoadData();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "❌ Lỗi cập nhật: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void gvLienHe_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int maLH = Convert.ToInt32(gvLienHe.DataKeys[e.RowIndex].Value);
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "DELETE FROM LienHe WHERE MaLH = @MaLH";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaLH", maLH);
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        protected void gvLienHe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLienHe.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            LoadData(keyword);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadData();
        }
    }
}
