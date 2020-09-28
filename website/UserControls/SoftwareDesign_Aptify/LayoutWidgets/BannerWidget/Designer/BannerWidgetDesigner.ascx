<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>
<ol>
    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="Message" CssClass="sfTxtLbl">Message</asp:Label>
        <asp:TextBox ID="Message" runat="server" CssClass="sfTxt" />
        <div class="sfExample">The label's message</div>
    </li>

    <li class="sfFormCtrl">
        <asp:Label runat="server" CssClass="sfTxtLbl">Image</asp:Label>
        <img id="previewSelectedImageID" src="" alt="" style="display: none;" />
        <span style="display: none;" class="sfSelectedItem" id="selectedSelectedImageID"></span>
        <div>
            <asp:LinkButton ID="selectButtonSelectedImageID" OnClientClick="return false;" runat="server" CssClass="sfLinkBtn sfChange">
        <span class="sfLinkBtnIn">
          <asp:Literal runat="server" Text="<%$Resources:Labels, SelectDotDotDot %>" />
        </span>
            </asp:LinkButton>
            <asp:LinkButton ID="deselectButtonSelectedImageID" OnClientClick="return false;" runat="server" CssClass="sfLinkBtn sfChange">
        <span class="sfLinkBtnIn">
          <asp:Literal runat="server" Text="<%$Resources:Labels, Remove %>" />
        </span>
            </asp:LinkButton>
        </div>
        <sf:EditorContentManagerDialog runat="server" ID="selectorSelectedImageID" DialogMode="Image" HostedInRadWindow="false" BodyCssClass="" />
        <div class="sfExample">Select your image...</div>
    </li>
</ol>
