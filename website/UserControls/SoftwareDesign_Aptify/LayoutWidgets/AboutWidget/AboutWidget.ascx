
<%@ Control Language="C#" %>

<div id="designerLayoutRoot" runat="server" class="sfContentViews sfSingleContentView about-block">
    <div runat="server" class="sf_cols">
        <div runat="server" class="sf_colsOut sf_2cols_1_50">
            <div runat="server" class="sf_colsIn sf_2cols_1in_50">
                <div class="empty-block-left"></div>
                <div class="about-img" ID="caiLogo"  runat="server" >
                    
                </div>
            </div>
        </div>
        <div runat="server" class="sf_colsOut sf_2cols_1_50">
            <div runat="server" class="sf_colsIn sf_2cols_1in_50">

                <div class="about-box">
                    <h2 class="about-box-title">
                        <asp:Label ID="TitleLabel" Text="Text" runat="server" CssClass="" />
                    </h2>
                    <p class="about-box-subtitle">
                        <asp:Label ID="SubtitleLabel" Text="Text" runat="server" CssClass="" />
                    </p>
                    <p class="about-box-message">
                    <asp:Label ID="MessageLabel" Text="Text" runat="server" CssClass="" />
                    </p>
                     <div class="about-box-image">
                         <asp:Image runat="server" ImageUrl="~/Images/CAITheme/logo-about-box.png" ID="Image1" />
                      
                    </div>

                    <div class="about-box-btn-actions">
                        <asp:HyperLink ID="PageLink" runat="server" class=" btn-readMore">
                               read more
                        </asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
