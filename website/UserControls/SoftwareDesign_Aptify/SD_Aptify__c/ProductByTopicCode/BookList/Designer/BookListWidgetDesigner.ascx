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
            <asp:Label runat="server" AssociatedControlID="TopicCodes" CssClass="sfTxtLbl">Topic Codes</asp:Label>
            <asp:ListBox ID="TopicCodes" runat="server" SelectionMode="Multiple" Width="150px" Rows="10"></asp:ListBox>
            <div class="sfExample">Select the list of topic codes</div>
            <asp:HiddenField ID="HdnTopicCodes" runat="server" />
        </li>
        <li class="sfFormCtrl">
            <asp:Label runat="server" CssClass="sfTxtLbl">Number of products to show</asp:Label>
            <telerik:RadTextBox InputType="Number" runat="server" CssClass="sfTxt" ID="ProductCount"></telerik:RadTextBox>
            <div class="sfExample">Select the number of items to show</div>
        </li>
    </ol>
</div>
