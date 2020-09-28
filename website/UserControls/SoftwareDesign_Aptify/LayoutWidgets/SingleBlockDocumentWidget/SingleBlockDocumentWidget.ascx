<%@ Control Language="C#" %>

<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView">

    <div id="Holder" runat="server" class="single-block single-block-full-link">
        <asp:HyperLink ID="FullLink" class="full-link" runat="server"></asp:HyperLink>

        <div class="single-block-cta-image">
            <div class="single-block-image" id="image" runat="server">
            </div>
             <div class="cta-title">
                <h2>
                    <asp:Label ID="TitleLabel1" Text="Text" runat="server" CssClass="" /></h2>
            </div>
        </div>
        <div class="cta-content">
            <div class="cta-title">
                <h2>
                    <asp:Label ID="TitleLabel2" Text="Text" runat="server" CssClass="" /></h2>
            </div>
            <div class="cta-message">
                <p>
                    <asp:Label ID="MessageLabel" Text="Text" runat="server" CssClass="" />
                </p>
            </div>

            <div class="btn-actions">
                <asp:HyperLink ID="PageLink" runat="server" class="btn"></asp:HyperLink>
            </div>
        </div>
    </div>
</div>
