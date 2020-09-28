<%@ Control Language="C#" %>

<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView">

    <div id="Holder" runat="server" class="link-box-block">
        <asp:Image class="single-block-image" ID="image" runat="server"></asp:Image>
        <h2>
            <asp:Label ID="TitleLabel" Text="Text" runat="server" CssClass="" />
        </h2>
        <asp:HyperLink ID="PageLink" runat="server" class="btn"></asp:HyperLink>
    </div>
</div>
