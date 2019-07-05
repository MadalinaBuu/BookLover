<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Verto.Home" MasterPageFile="~/SiteMaster.Master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div class="orbit" role="region" aria-label="Favorite Space Pictures" data-orbit="">
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
    <main class="content-container">
            <section class="section grid-x grid-padding-x">
                <asp:ListView ID="lvTabs" runat="server">
                    <ItemTemplate>
                        <div class="large-3 medium-6 small-12 cell">
                            <h3 class="subheader"><%#Eval("Name").ToString()%></h3>
                            <asp:Image runat="server" src='<%#Eval("Source")%>' class="full-width" />
                            <p><%#Eval("Description").ToString()%></p>
                            <asp:LinkButton runat="server" href="#" class="button buttonExt">View More</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </section>
            <section class="section-grey">
                <h3 class="subheader text-center padding-top-3">Special Offers</h3>
                <div class="grid-x grid-padding-x">
                    <asp:ListView ID="lvProducts" runat="server">
                        <ItemTemplate>
                            <div class="large-4 medium-12 small-12 cell">
                                <div class="special-offers">
                                    <asp:Image runat="server" src='<%#Eval("Source")%>' />
                                    <div class="text-center">
                                        <div><%#Eval("Name").ToString()%></div>
                                        <div><strong><%#Eval("Offer").ToString()%></strong></div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <div class="row">
                    <div class="small-6 small-centered text-center columns">
                        <a href="#" class="button">View All Offers</a>
                        <asp:Button ID="btnShowProducts" runat="server" CssClass="button" Text="View All Products" OnClick="btnShowProducts_Click" />
                    </div>
                </div>
                    <div id="divProducts" runat="server">
                        <section>
                            <asp:GridView ID="gvProducts" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowDeleting="gvProducts_RowDeleting" OnRowDataBound="gvProducts_RowDataBound" OnRowEditing="gvProducts_RowEditing" OnRowUpdating="gvProducts_RowUpdating">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                    <asp:BoundField DataField="Offer" HeaderText="Offer" />
                                    <asp:ImageField DataImageUrlField="Source" HeaderText="Image" ItemStyle-Width="10px"></asp:ImageField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlCategory" runat="server" Enabled="false"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="true" />
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDelete" runat="server" OnClientClick="return confirm('Are you sure to delete this record?')"  CommandName="Delete" Text="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </section>
                    </div>
            </section>
                <h3 class="subheader text-center padding-top-3">Product Categories</h3>
                <div class="product-categories">
                    <ul id="lightSlider">
                        <asp:Repeater ID="rptCategories" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:Image runat="server" src='<%#Eval("Source")%>' CssClass="img-resize" />
                                    <p class="text-center"><%#Eval("Name")%></p>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="row">
                    <div class="small-6 small-centered text-center columns">
                        <a href="#" id="btnAddNewCategory" data-open="addNewCategory" class="button">Add New Category</a>
                        <div id="addNewCategory" class="reveal" data-reveal>
                            <h2 id="modalTitle">Add New Category</h2>
                            <a class="close-button" data-close>&#215;</a>
                            <div class="row">
                                <div class="callout alert errorMessage" visible="false"></div>
                                <span>Name: </span>
                                <asp:TextBox ID="txtCategoryName" runat="server" placeholder="Add category name" CssClass="validate[required]"></asp:TextBox>
                                <span>Image: </span>
                                <asp:TextBox ID="txtCategoryImage" runat="server" placeholder="Add category image" CssClass="validate[required]"></asp:TextBox>
                                <asp:Button ID="btnSaveCategory" runat="server" CssClass="button" Text="Save" OnClientClick="SaveCategory(event)" />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </main>
</asp:Content>

