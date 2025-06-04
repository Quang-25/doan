<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="doan.src.home.home" %>
<%@ Register Src="~/src/headerr/headerr.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/src/footerr/footerr.ascx" TagPrefix="ux" TagName="FooterHome" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="trang.css" />
    <link rel="icon" type="image/png" href="https://demo037102.web30s.vn/datafiles/34058/upload/images/logo.png" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

</head>
<body>
    <form id="form1" runat="server">
        <uc:Header runat="server" ID="headerHome"></uc:Header>
        <div class="anh">
            <img src="https://demo037102.web30s.vn/datafiles/34058/upload/images/banner/slide1.jpg?t=1636600660" class="meetmore" alt="anh loi" />
        </div>
        <div class="infor-bar">
            <ig class="truck">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512">
                    <path d="M48 0C21.5 0 0 21.5 0 48L0 368c0 26.5 21.5 48 48 48l16 0c0 53 43 96 96 96s96-43 96-96l128 0c0 53 43 96 96 96s96-43 96-96l32 0c17.7 0 32-14.3 32-32s-14.3-32-32-32l0-64 0-32 0-18.7c0-17-6.7-33.3-18.7-45.3L512 114.7c-12-12-28.3-18.7-45.3-18.7L416 96l0-48c0-26.5-21.5-48-48-48L48 0zM416 160l50.7 0L544 237.3l0 18.7-128 0 0-96zM112 416a48 48 0 1 1 96 0 48 48 0 1 1 -96 0zm368-48a48 48 0 1 1 0 96 48 48 0 1 1 0-96z" />
                </svg>
            </ig>
            <div class="freeship">
                <span>Miễn phí vận chuyển</span>
                <span>Đặt hàng trên 100.000đ</span>
            </div>
            <ig class="gioqua">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                    <path d="M190.5 68.8L225.3 128l-1.3 0-72 0c-22.1 0-40-17.9-40-40s17.9-40 40-40l2.2 0c14.9 0 28.8 7.9 36.3 20.8zM64 88c0 14.4 3.5 28 9.6 40L32 128c-17.7 0-32 14.3-32 32l0 64c0 17.7 14.3 32 32 32l448 0c17.7 0 32-14.3 32-32l0-64c0-17.7-14.3-32-32-32l-41.6 0c6.1-12 9.6-25.6 9.6-40c0-48.6-39.4-88-88-88l-2.2 0c-31.9 0-61.5 16.9-77.7 44.4L256 85.5l-24.1-41C215.7 16.9 186.1 0 154.2 0L152 0C103.4 0 64 39.4 64 88zm336 0c0 22.1-17.9 40-40 40l-72 0-1.3 0 34.8-59.2C329.1 55.9 342.9 48 357.8 48l2.2 0c22.1 0 40 17.9 40 40zM32 288l0 176c0 26.5 21.5 48 48 48l144 0 0-224L32 288zM288 512l144 0c26.5 0 48-21.5 48-48l0-176-192 0 0 224z" />
                </svg>
            </ig>
            <div class="Qua">
                <span>Giỏ quà đặc biệt</span>
                <span>Tặng món quà hoàn hảo</span>
            </div>
            <ig class="calender">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                    <path d="M96 32l0 32L48 64C21.5 64 0 85.5 0 112l0 48 448 0 0-48c0-26.5-21.5-48-48-48l-48 0 0-32c0-17.7-14.3-32-32-32s-32 14.3-32 32l0 32L160 64l0-32c0-17.7-14.3-32-32-32S96 14.3 96 32zM448 192L0 192 0 464c0 26.5 21.5 48 48 48l352 0c26.5 0 48-21.5 48-48l0-272z" />
                </svg>
            </ig>
            <div class="calender-1">
                <span>Khuyến mãi đặc biệt</span>
                <span>Mỗi ngày, mỗi giờ </span>
            </div>
            <ig class="comment">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512"></svg>
            </ig>
            <div class="comment-1">
                <span>Chăm sóc khách hàng 24/7</span>
                <span>Gọi ngay: 1900 9477</span>
            </div>
        </div>
        <h3 class="tieude">Đồ uống khuyến mãi</h3>
        <div class="nen"> 
            <asp:Repeater ID="rptSanPham" runat="server" OnItemCommand="rptSanPham_ItemCommand1">
            <ItemTemplate>
            <div class=" juice">
                <div class="anhsanpham">
                <img src='<%# Eval("HinhAnhChinh") %>' alt='<%# Eval("TenSP") %>' />
                    <div class="sale-badge"><%# Eval("KhuyenMai") %></div>
                    <div class="sale-badge">-10%</div>

                  <div class="overlay">
                        <asp:Button ID="btn_sua" runat="server" CssClass="order-btn"
                        Text="Đặt hàng"
                        CommandName="update"
                        CommandArgument='<%# Eval("MaSP") %>'
                        OnCommand="OnClickUpdateItem"
                        CausesValidation="false"
                        UseSubmitBehavior="false" />
                      
                    </div>
                    <div class="name"><%# Eval("TenSP") %></div>
                <div class="price">
                    <del><%# Eval("Gia") %></del>
                    <span class="new-price"><%# Eval("Gia") %></span>
                </div>
                <div class="stars">★ ★ ★ ★ ★</div>
            </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
        </div>
     <ux:FooterHome runat="server" />
    </form>
</body>
</html>
