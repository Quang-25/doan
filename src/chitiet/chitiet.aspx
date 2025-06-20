<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chitiet.aspx.cs" Inherits="doan.src.chitiet.chitiet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chi tiết sản phẩm</title>
    <link rel="stylesheet" href="chitiet.css" />
    <link rel="icon" type="image/png" href="https://demo037102.web30s.vn/datafiles/34058/upload/images/logo.png" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- Banner section -->
        <div class="banner">
            <h1>Chi tiết sản phẩm</h1>
        </div>        <!-- Breadcrumb section -->
        <div class="breadcrumb">
            <div class="container">
                <a href="/src/home/home.aspx">Trang chủ</a>
                <span class="separator"><i class="fa fa-angle-right"></i></span>
                <a href="/src/loaisanpham/loaisp.aspx" runat="server" id="categoryLink"></a>
                <span class="separator"><i class="fa fa-angle-right"></i></span>
                <span class="active" runat="server" id="productNameBreadcrumb"></span>
            </div>
        </div>

        <!-- Product detail section -->
        <div class="product-detail">
            <div class="container">
                <div class="product-container">
                    <!-- Left column: Product images -->
                    <div class="product-images">
                <div class="main-image">
                            <asp:Image ID="imgMainProduct" runat="server" CssClass="product-main-img" />
                        </div>
                        <div class="thumbnail-images">
                            <asp:ImageButton ID="imgThumbnail1" runat="server" CssClass="product-thumb" OnClick="imgThumbnail_Click" CommandArgument="1" />
                            <asp:ImageButton ID="imgThumbnail2" runat="server" CssClass="product-thumb" OnClick="imgThumbnail_Click" CommandArgument="2" />
                            <asp:ImageButton ID="imgThumbnail3" runat="server" CssClass="product-thumb" OnClick="imgThumbnail_Click" CommandArgument="3" />
                        </div>
                    </div>

                    <!-- Right column: Product details -->
                    <div class="product-info">
                        <h1 class="product-title">
                            <asp:Literal ID="litProductName" runat="server"></asp:Literal>
                        </h1>
                        <div class="product-meta">
                            <div class="product-status">
                                <span>Tình trạng: </span>
                                <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                            </div>
                            <div class="product-origin">
                                <span>Xuất xứ: </span>
                                <asp:Literal ID="litOrigin" runat="server"></asp:Literal>
                            </div>
                            <div class="product-rating">
                                <span class="stars">★ ★ ★ ★ ★</span>
                            </div>
                        </div>

                        <div class="product-price">
                            <asp:Panel ID="pnlDiscount" runat="server" CssClass="discount-info" Visible="false">
                                <span class="original-price">
                                    <asp:Literal ID="litOriginalPrice" runat="server"></asp:Literal> đ
                                </span>
                                <span class="discount-badge">
                                    <asp:Literal ID="litDiscountPercent" runat="server"></asp:Literal>%
                                </span>
                            </asp:Panel>
                            <div class="current-price">
                                <asp:Literal ID="litPrice" runat="server"></asp:Literal> đ
                            </div>
                        </div>

                        <div class="product-actions">                            <div class="quantity-selector">
                                <span>Số lượng:</span>
                                <div class="quantity-control">
                                    <asp:Button ID="btnDecrease" runat="server" Text="-" CssClass="quantity-btn minus" OnClick="btnDecrease_Click" />
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="quantity-input" Text="1" min="1" max="100"></asp:TextBox>
                                    <asp:Button ID="btnIncrease" runat="server" Text="+" CssClass="quantity-btn plus" OnClick="btnIncrease_Click" />
                                </div>
                            </div>
                            
                            <div class="action-buttons">
                                <asp:Button ID="btnAddToCart" runat="server" Text="Thêm vào giỏ" CssClass="btn-add-to-cart" OnClick="btnAddToCart_Click" />
                                <asp:Button ID="btnBuyNow" runat="server" Text="Mua ngay" CssClass="btn-buy-now" OnClick="btnBuyNow_Click" />
                            </div>
                        </div>
                        
                        <div class="product-share">
                            <span>Chia sẻ:</span>
                            <a href="#" class="share-link facebook"><i class="fab fa-facebook-f"></i></a>
                            <a href="#" class="share-link twitter"><i class="fab fa-twitter"></i></a>
                            <a href="#" class="share-link pinterest"><i class="fab fa-pinterest-p"></i></a>
                        </div>
                    </div>
                </div>
                
                <!-- Product description -->
                <div class="product-description">
                    <h2>Mô tả sản phẩm</h2>
                    <div class="description-content">
                        <asp:Literal ID="litDescription" runat="server"></asp:Literal>
                    </div>
                </div>
                
                <!-- Related products -->
                <div class="related-products">
                    <h2>Sản phẩm liên quan</h2>
                    <div class="related-products-container">
                        <asp:Repeater ID="rptRelatedProducts" runat="server" OnItemCommand="rptRelatedProducts_ItemCommand">
                            <ItemTemplate>
                                <div class="related-product-item">
                                    <div class="product-image">
                                        <a href="/src/chitiet/chitiet.aspx?id=<%# Eval("MaSP") %>">
                                            <img src='<%# Eval("HinhAnhChinh") %>' alt='<%# Eval("TenSP") %>' />
                                        </a>
                                        <div class="product-overlay">
                                            <asp:LinkButton ID="btnAddToCartRelated" runat="server"
                                                CommandName="AddToCart"
                                                CommandArgument='<%# Eval("MaSP") %>'
                                                CssClass="btn-overlay">
                                                <i class="fa fa-shopping-cart"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="product-details">
                                        <h3 class="product-name">
                                            <a href="/src/chitiet/chitiet.aspx?id=<%# Eval("MaSP") %>">
                                                <%# Eval("TenSP") %>
                                            </a>
                                        </h3>
                                        <div class="product-price">
                                            <span class="current-price"><%# Eval("Gia") %> đ</span>
                                            <%# Convert.ToInt32(Eval("KhuyenMai")) > 0 ? 
                                                "<span class='original-price'>" + (Convert.ToInt32(Eval("Gia")) * 100 / (100 - Convert.ToInt32(Eval("KhuyenMai")))) + " đ</span>" : "" %>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>        </div>
    </form>    <!-- No JavaScript as requested -->
</body>
</html>
