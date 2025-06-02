<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="YourNamespace.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />
    <link href="thietke.css" rel="stylesheet" />
    <title>Đăng Nhập</title>
    <style>
        body {
            font-family: Arial;
            background: #f0f0f0;
        }

        .login-container {
            width: 300px;
            margin: 50px auto;
            padding: 20px;
            background: #f9f9f9;
            opacity:0.9;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }

        .login-container h2 {
            text-align: center;
            color: #2a8a29;
            border-bottom: 2px solid #2a8a29;
            padding-bottom: 5px;
        }

        .login-container input[type="text"],
        .login-container input[type="password"] {
            width: 100%;
            padding: 10px;
            margin-top: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .login-container .btn-login {
            background: #2a8a29;
            color: white;
            padding: 10px;
            width: 100%;
            border: none;
            margin-top: 15px;
            cursor: pointer;
            border-radius: 4px;
        }

        .login-container a {
            font-size: 13px;
            text-decoration: none;
        }

        .social-btn {
            margin-top: 15px;
            display: flex;
            flex-direction: column;
            gap: 10px;
        }

        .social-btn a {
            display: block;
            text-align: center;
            padding: 10px;
            border-radius: 4px;
            color: white;
            font-weight: bold;
        }
        
        .facebook { background-color: #3b5998; }
        .google { background-color: #dd4b39; }
        .zalo { background-color: #0084ff; }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div class="backgroud">
        <div class="login-container">
            <h2>ĐĂNG NHẬP</h2>
            <asp:TextBox ID="txtUsername" runat="server" Placeholder="Tên truy cập" CssClass="form-control" />
            

            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Mật khẩu" CssClass="form-control" />
            

            <asp:Button ID="btnLogin" runat="server" Text="ĐĂNG NHẬP" CssClass="btn-login" OnClick="btnLogin_Click" />
            <asp:Label runat="server" ID="lblthongbao" ></asp:Label>
                    

            <div style="margin-top:10px;">
                <a href="#">- Bạn quên mật khẩu?</a><br />
                <a href="https://localhost:44322/src/DANGKY/dangky">- Bạn chưa có tài khoản? <span style="color: #2a8a29">Đăng ký ngay</span></a>
                
            </div>
            <div class="social-btn">
                <div class="social-btn">
    <a href="https://www.facebook.com" target="_blank" class="btn-social facebook">
        <i class="fab fa-facebook-f"></i> Facebook
    </a>
    <a href="https://accounts.google.com" target="_blank" class="btn-social google">
        <i class="fab fa-google"></i> Google
    </a>
    <a href="https://zalo.me" target="_blank" class="btn-social zalo">
        <i class="fas fa-comment-dots"></i> Zalo
    </a>
</div>

            </div>
        </div>
        </div>
    </form>
</body>
</html>
