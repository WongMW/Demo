<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>
<ol>
     <li class="sfFormCtrl">
        <label class="sfTxtLbl" for="selectedSelectedPageIDLabel">Page</label>
        <span style="display: none;" class="sfSelectedItem" id="selectedSelectedPageIDLabel">
            <asp:Literal runat="server" Text="" />
        </span>
        <span class="sfLinkBtn sfChange">
            <a href="javascript: void(0)" class="sfLinkBtnIn" id="pageSelectButtonSelectedPageID">
                <asp:Literal runat="server" Text="<%$Resources:Labels, SelectDotDotDot %>" />
            </a>
        </span>
        <div id="selectorTagSelectedPageID" runat="server" style="display: none;">
            <sf:PagesSelector runat="server" ID="pageSelectorSelectedPageID"
                AllowExternalPagesSelection="false" AllowMultipleSelection="false" />
        </div>
        <div class="sfExample">Select your page...</div>
    </li>

</ol>
