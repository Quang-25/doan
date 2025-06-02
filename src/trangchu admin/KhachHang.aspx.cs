using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace doan.src.trangchu_admin
{
    public partial class KhachHang : Page
    {
        private string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadKhachHang();
            }
        }

        private void LoadKhachHang(string keyword = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM NguoiDung";
                if (!string.IsNullOrEmpty(keyword))
                {
                    query += " WHERE TenND LIKE @keyword";
                }

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                if (!string.IsNullOrEmpty(keyword))
                {
                    da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                }
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvKhachHang.DataSource = dt;
                gvKhachHang.DataBind();
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            LoadKhachHang(keyword);
        }

        protected void btnLuuMoi_Click(object sender, EventArgs e)
        {
            string tenND = txtTenMoi.Text.Trim();
            string diaChi = txtDiaChiMoi.Text.Trim();
            string sdt = txtSdtMoi.Text.Trim();
            string email = txtEmailMoi.Text.Trim();
            string chucVu = txtChucVuMoi.Text.Trim();
            decimal soDu = 0;
            decimal.TryParse(txtSoDuMoi.Text.Trim(), out soDu);
            string matKhau = txtMatKhauMoi.Text.Trim();

            if (string.IsNullOrEmpty(tenND) || string.IsNullOrEmpty(matKhau))
            {
                // Hiển thị lỗi nếu cần thiết
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO NguoiDung (TenND, DiaChi, Sdt, Email, ChucVu, SoDu, MatKhau) " +
                               "VALUES (@TenND, @DiaChi, @Sdt, @Email, @ChucVu, @SoDu, @MatKhau)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenND", tenND);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@Sdt", sdt);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                cmd.Parameters.AddWithValue("@SoDu", soDu);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau); // Mật khẩu không được để null

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Làm sạch form và tải lại dữ liệu
            ClearThemMoiForm();
            LoadKhachHang();
        }
        protected void btnThemMoi_Click(object sender, EventArgs e)
        {
            // Ví dụ: xóa các textbox để nhập mới
            txtTenMoi.Text = "";
            txtDiaChiMoi.Text = "";
            txtSdtMoi.Text = "";
            txtEmailMoi.Text = "";
            txtChucVuMoi.Text = "";
            txtSoDuMoi.Text = "";
            txtMatKhauMoi.Text = "";

            // Nếu bạn có một khối form cần ẩn/hiện, có thể dùng Visible = true;
            // formThemMoi.Visible = true;
        }
        private void ClearThemMoiForm()
        {
            txtTenMoi.Text = "";
            txtDiaChiMoi.Text = "";
            txtSdtMoi.Text = "";
            txtEmailMoi.Text = "";
            txtChucVuMoi.Text = "";
            txtSoDuMoi.Text = "";
            txtMatKhauMoi.Text = "";
        }



        protected void btnLuu_Click(object sender, EventArgs e)
        {
            string tenND = txtTenMoi.Text.Trim();
            string diaChi = txtDiaChiMoi.Text.Trim();
            string sdt = txtSdtMoi.Text.Trim();
            string email = txtEmailMoi.Text.Trim();
            string chucVu = txtChucVuMoi.Text.Trim();
            string matkhau = txtMatKhauMoi.Text.Trim();
            decimal soDu = 0;

            if (!decimal.TryParse(txtSoDuMoi.Text.Trim(), out soDu))
            {
                lblMessage.Text = "Số dư phải là số hợp lệ.";
                return;
            }

            if (string.IsNullOrEmpty(tenND))
            {
                lblMessage.Text = "Vui lòng nhập họ tên.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO NguoiDung (TenND, DiaChi, Sdt, Email, ChucVu, SoDu) 
                                VALUES (@TenND, @DiaChi, @Sdt, @Email, @ChucVu, @SoDu)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenND", tenND);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@Sdt", sdt);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                cmd.Parameters.AddWithValue("@SoDu", soDu);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            pnlThemMoi.Visible = false;
            LoadKhachHang();
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            ClearThemMoiForm();
        }

        protected void gvKhachHang_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvKhachHang.EditIndex = e.NewEditIndex;
            LoadKhachHang(txtTimKiem.Text.Trim());
        }

        protected void gvKhachHang_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvKhachHang.EditIndex = -1;
            LoadKhachHang(txtTimKiem.Text.Trim());
        }

        protected void gvKhachHang_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvKhachHang.Rows[e.RowIndex];
            int maND = Convert.ToInt32(gvKhachHang.DataKeys[e.RowIndex].Value);

            string tenND = (row.FindControl("txtTenND") as TextBox).Text.Trim();
            string diaChi = (row.FindControl("txtDiaChi") as TextBox).Text.Trim();
            string sdt = (row.FindControl("txtSdt") as TextBox).Text.Trim();
            string email = (row.FindControl("txtEmail") as TextBox).Text.Trim();
            string chucVu = (row.FindControl("txtChucVu") as TextBox).Text.Trim();
            string soDuStr = (row.FindControl("txtSoDu") as TextBox).Text.Trim();

            decimal soDu;
            if (!decimal.TryParse(soDuStr, out soDu))
            {
                // Có thể hiện thông báo lỗi cho người dùng (nâng cao)
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"UPDATE NguoiDung SET TenND=@TenND, DiaChi=@DiaChi, Sdt=@Sdt, Email=@Email, ChucVu=@ChucVu, SoDu=@SoDu WHERE MaND=@MaND";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenND", tenND);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@Sdt", sdt);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                cmd.Parameters.AddWithValue("@SoDu", soDu);
                cmd.Parameters.AddWithValue("@MaND", maND);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            gvKhachHang.EditIndex = -1;
            LoadKhachHang(txtTimKiem.Text.Trim());
        }

        protected void gvKhachHang_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int maND = Convert.ToInt32(gvKhachHang.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM NguoiDung WHERE MaND=@MaND";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaND", maND);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadKhachHang(txtTimKiem.Text.Trim());
        }
    }
}
