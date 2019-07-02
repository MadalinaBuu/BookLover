<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Verto.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Opticron</title>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/foundation.css" />
    <link rel="stylesheet" href="css/app.css" />
    <link type="text/css" rel="stylesheet" href="css/lightslider.css" />
</head>
<body>
    <form runat="server">
        <div class="content-container mobile-container">
            <div class="top-bar">
                <asp:Image runat="server" class="logo" src="img/header-logo.png" alt="logo" />
                <div data-responsive-toggle="responsive-menu">
                    <div class="title-bar-title">Menu</div>
                    <button class="menu-icon" type="button" data-toggle="responsive-menu"></button>
                </div>
            </div>
            <div class="top-bar">
                <div class="top-bar-left" id="responsive-menu">
                    <ul class="dropdown menu" data-dropdown-menu="">
                        <li>
                            <asp:LinkButton runat="server" href="#">Our Products</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton runat="server" href="#">Where to Buy</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton runat="server" href="#">News & Reviews</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton runat="server" href="#">Help & Support</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton runat="server" href="#">My Opticron</asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div class="top-bar-right">
                    <div class="input-group">
                        <asp:TextBox runat="server" class="input-group-field" type="search" placeholder="My Opticron..." />
                        <div class="input-group-button">
                            <button class="button secondary"><i class="fa fa-search"></i></button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="orbit" role="region" aria-label="Favorite Space Pictures" data-orbit>
                <div class="orbit-wrapper">
                    <div class="orbit-controls">
                        <button class="orbit-previous"><span class="show-for-sr">Previous Slide</span>&#9664;&#xFE0E;</button>
                        <button class="orbit-next"><span class="show-for-sr">Next Slide</span>&#9654;&#xFE0E;</button>
                    </div>
                    <ul class="orbit-container">
                        <li class="is-active orbit-slide">
                            <figure class="orbit-figure">
                                <asp:Image runat="server" class="orbit-image slide-image" src="img/slide1.jpg" alt="Image" />
                            </figure>
                        </li>
                        <li class="orbit-slide">
                            <figure class="orbit-figure">
                                <asp:Image runat="server" class="orbit-image slide-image" src="img/slide2.jpg" alt="Image" />
                            </figure>
                        </li>
                        <li class="orbit-slide">
                            <figure class="orbit-figure">
                                <asp:Image runat="server" class="orbit-image slide-image" src="img/slide3.jpg" alt="Image" />
                            </figure>
                        </li>
                        <li class="orbit-slide">
                            <figure class="orbit-figure">
                                <asp:Image runat="server" class="orbit-image slide-image" src="img/slide4.jpg" alt="Image" />
                            </figure>
                        </li>
                    </ul>
                </div>
            </div>
            <section>
                <div class="grid-container">
                    <div class="grid-x grid-padding-x small-up-2 medium-up-4">
                        <asp:ListView ID="lvTabs" runat="server">
                            <ItemTemplate>
                                <div class="cell">
                                    <div class="card">
                                        <h4><%#Eval("Name").ToString()%></h4>
                                        <asp:Image runat="server" src='<%#Eval("Source")%>' />
                                        <div class="card-section">
                                            <p><%#Eval("Description").ToString()%></p>
                                            <asp:LinkButton runat="server" href="#" class="button buttonExt">View More</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </section>
            <section>
                <div class="grid-container" style="background: lightgrey; padding-top: 2%;">
                    <h4 class="text-center">Special Offers</h4>
                    <div class="grid-x grid-padding-x small-up-2 medium-up-3" style="padding-top: 2%;">
                        <asp:ListView ID="lvProducts" runat="server">
                            <ItemTemplate>
                                <div class="cell">
                                    <div class="card">
                                        <asp:Image runat="server" src='<%#Eval("Source")%>' Style="height: 15vw;" />
                                        <div class="card-section text-center">
                                            <div><%#Eval("Name").ToString()%></div>
                                            <div><strong><%#Eval("Offer").ToString()%></strong></div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <div class="text-center"><a href="#" class="button buttonExt">View All Offers</a></div>
                </div>
            </section>
            <div class="grid-container" style="padding-top: 2%;">
                <h4 class="text-center">Product Categories</h4>
                <ul id="lightSlider" style="padding-top: 2%;">
                    <asp:Repeater ID="rptCategories" runat="server">
                        <ItemTemplate>
                            <li>
                                <asp:Image runat="server" src='<%#Eval("Source")%>' Style="height: 15vw;" />
                                <p><%#Eval("Name")%></p>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <footer>
                <div class="callout large secondary">
                    <div class="grid-container">
                        <div class="grid-x grid-padding-x small-up-2 medium-up-2">
                            <div class="cell">Opticron Unit 21, Titan Court, Laporte Way, Luton, Bedfordshire, LU4 8EF, UK</div>
                            <div class="cell">call 01582 726522</div>
                            <div class="cell"></div>
                            <div class="cell">email us sales@opticron.co.uk</div>
                            <div class="cell"></div>
                            <div class="cell"></div>
                            <div class="cell">Site map/ Terms / Privacy Policy / Site By Verto</div>
                            <div class="cell">ICONS</div>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
    </form>
    <script src="js/vendor/jquery.js"></script>
    <script src="js/vendor/foundation.js"></script>
    <script src="js/app.js"></script>
    <script src="js/lightslider.js"></script>
</body>
</html>

<script>
    $(document).ready(function () {
        $("#lightSlider").lightSlider({
            item: 4,
            responsive: [],
        });
    });
</script>
