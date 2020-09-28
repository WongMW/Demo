<%@ Control Language="C#" %>


<div id="designerLayoutRoot" runat="server" class="sfContentViews sfSingleContentView">
    <asp:HyperLink ID="BlockLink" runat="server">
        <asp:Panel runat="server" ID="panelHolder" CssClass="squareBoxHolder">
            <div id="Holder" runat="server" class="square-box-holder">
                <div class="squareBox">
                    <asp:Label ID="StyleLabel" runat="server" />
                    <div class="box-title">
                        <asp:Label ID="TitleLabel" Text="Text" runat="server" CssClass="" />
                    </div>
                    <div class="box-subtitle">
                        <asp:Label ID="SubtitleLabel" Text="Text" runat="server" CssClass="" />
                    </div>
                    <div class="box-message">
                        <asp:Label ID="MessageLabel" Text="Text" runat="server" CssClass="" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="empty-block-bottom">
        </div>
    </asp:HyperLink>
</div>