<%@ Control Language="C#" %>
<asp:Label ID="MessageLabel" Text="Text" runat="server" Visible="false" />

<div class="sidebar-quote-holder" id="Holder" runat="server">
    <div class="sidebar-outer">
        <div class="sidebar-inner">
            <div class="sidebar-message">
                <asp:Label ID="QuoteLabel" Text="Text" runat="server" CssClass="" />
            </div>
            <div class="sidebar-name">
                <asp:Label ID="AuthorLabel" Text="Text" runat="server" CssClass="" />
            </div>
        </div>
    </div>
</div>
