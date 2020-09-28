<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>
<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView" style="max-height: 400px; overflow: auto;">
    <ol>
        <li>
            <label class="sfTxtLbl">Select Categories:</label>
            <sitefinity:HierarchicalTaxonField ID="CategoriesSelector" runat="server" DisplayMode="Write" Expanded="false" ExpandText="ClickToAddCategories"
                ShowDoneSelectingButton="true" AllowMultipleSelection="true" BindOnServer="false" TaxonomyMetafieldName="Category" WebServiceUrl="~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc" />
        </li>

        <li class="sfFormCtrl">
            <asp:Label runat="server" AssociatedControlID="Limit" CssClass="sfTxtLbl">Limit:</asp:Label>
            <asp:TextBox ID="Limit" runat="server" CssClass="sfTxt" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="Limit" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
            <div class="sfExample">Number of News Articles</div>
        </li>
        
        <li class="sfFormCtrl">
            <asp:Label runat="server" CssClass="sfTxtLbl">Image Size</asp:Label>
            <telerik:RadTextBox InputType="Number" runat="server" CssClass="sfTxt" ID="ImageSize"></telerik:RadTextBox>
            <div class="sfExample">Image Size (a number)</div>
        </li>

        <li class="sfFormCtrl">
        <label class="sfTxtLbl" for="selectedSelectedPageIDLabel">View More Button Link</label>
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
</div>
