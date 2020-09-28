<%@ Control Language="C#" %>

<div class="sf_cols latest-news-header ">
    <div runat="server" class="sf_colsOut sf_2cols_1_50">
        <div runat="server" class="sf_colsIn sf_2cols_1in_50">
            <div class="main-title">
                <h2>Latest News</h2>
            </div>
        </div>
    </div>
    <div runat="server" class="sf_colsOut sf_2cols_2_50">
        <div runat="server" class="sf_colsIn sf_2cols_2in_50">
            <div class="view-all-actions">
                <a href="#">
                    <asp:HyperLink ID="PageLink1" runat="server" class="btn view-all">view all</asp:HyperLink>
                </a>
            </div>
        </div>
    </div>
</div>

<div class="sf_cols latest-news ">
    <asp:Repeater ID="NewsList" runat="server">
        <ItemTemplate>
            <div runat="server" class="sf_colsOut sf_3cols_1_33">
                <div runat="server" class='<%# DataBinder.Eval(Container.DataItem, "Style","sf_colsIn sf_3cols_1in_33 {0}") %>'>
                    <span class="latest-news-category"><%#DataBinder.Eval(Container.DataItem,"Category")%></span>
                    <div class="latest-news-single-block">
                        <div class="cta-image">
                            <asp:Image runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "Image") %>' ID="HeaderImage" />
                        </div>
                        <div class="cta-content">
                            <div class="cta-title">
                                <h2><%#DataBinder.Eval(Container.DataItem,"Title")%></h2>
                            </div>
                            <div class="cta-message">
                                <p>
                                    <%#DataBinder.Eval(Container.DataItem,"Summary")%>
                                </p>
                            </div>
                            <div class="latest-news-actions">
                                <div class="sf_cols ">
                                    <div runat="server" class="sf_colsOut sf_2cols_1_50">
                                        <div runat="server" class="sf_colsIn sf_2cols_1in_50">
                                            <div class="date">
                                                <%#DataBinder.Eval(Container.DataItem,"PublicationDate", "{0:MMM dd, yyyy}") %>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" class="sf_colsOut sf_2cols_2_50">
                                        <div runat="server" class="sf_colsIn sf_2cols_2in_50">
                                            <div class="latest-news-btn">
                                                <a class="btn" runat="server" href='<%# DataBinder.Eval(Container.DataItem, "Url") %>'>READ MORE</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

<div class="sf_cols ">
    <div runat="server" class="sf_colsOut sf_1cols_1_100">
        <div runat="server" class="sf_colsIn sf_1cols_1in_100">
            <div class="show-more-actions">
                <asp:HyperLink ID="PageLink2" runat="server" class="btn show-more">Show Me More News </asp:HyperLink>
            </div>
        </div>
    </div>
</div>
