<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>
<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView" style="max-height: 400px; overflow: auto;">
    <ol>
        <li class="sfFormCtrl">
            <asp:Label runat="server" AssociatedControlID="AppOneTitle" CssClass="sfTxtLbl">Application 1 Title</asp:Label>
            <asp:TextBox ID="AppOneTitle" runat="server" CssClass="sfTxt" />
            <div class="sfExample">Title of the box</div>
        </li>
        <li>
            <asp:Label runat="server" AssociatedControlID="AppOneDate" CssClass="sfTxtLbl">Application 1 Closing Date</asp:Label>
            <asp:TextBox ID="AppOneDate" runat="server" CssClass="sfTxt" />
            <div class="sfExample">Title of the box</div>
        </li>

        <li class="sfFormCtrl">
            <asp:Label runat="server" AssociatedControlID="AppTwoTitle" CssClass="sfTxtLbl">Application 2 Title</asp:Label>
            <asp:TextBox ID="AppTwoTitle" runat="server" CssClass="sfTxt" />
            <div class="sfExample">Title of the box</div>
        </li>
        <li>
            <asp:Label runat="server" AssociatedControlID="AppTwoDate" CssClass="sfTxtLbl">Application 2 Closing Date</asp:Label>
            <asp:TextBox ID="AppTwoDate" runat="server" CssClass="sfTxt" />
            <div class="sfExample">Title of the box</div>
        </li>

        <li class="sfFormCtrl">
            <asp:Label runat="server" AssociatedControlID="AppThreeTitle" CssClass="sfTxtLbl">Application 3 Title</asp:Label>
            <asp:TextBox ID="AppThreeTitle" runat="server" CssClass="sfTxt" />
            <div class="sfExample">Title of the box</div>
        </li>
        <li>
            <asp:Label runat="server" AssociatedControlID="AppThreeDate" CssClass="sfTxtLbl">Application 3 Closing Date</asp:Label>
            <asp:TextBox ID="AppThreeDate" runat="server" CssClass="sfTxt" />
            <div class="sfExample">Title of the box</div>
        </li>

    </ol>
</div>
