<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>
<ol>
    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="Title" CssClass="sfTxtLbl">Title</asp:Label>
        <asp:TextBox ID="Title" runat="server" CssClass="sfTxt" />
        <div class="sfExample">Title of the box</div>
    </li>
    
    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="CourseName" CssClass="sfTxtLbl">Course Name</asp:Label>
        <asp:TextBox ID="CourseName" runat="server" CssClass="sfTxt" />
        <div class="sfExample">Name of the Course and if left blank, page title will be used instead</div>
    </li>
   <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="InteractionType" CssClass="sfTxtLbl">Interaction Type</asp:Label>
        <asp:DropDownList SkinID="Sitefinity" ID="InteractionType" runat="server">
            <asp:ListItem Value="Brochure">Brochure</asp:ListItem>
            <asp:ListItem Value="Syllabus">Syllabus</asp:ListItem>
        </asp:DropDownList>
        <div class="sfExample">Type of the interaction that will be recorded</div>
    </li>

   <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="Style" CssClass="sfTxtLbl">Style</asp:Label>
        <asp:DropDownList SkinID="Sitefinity" ID="Style" runat="server">
            <asp:ListItem Value="style-1">Red Style</asp:ListItem>
            <asp:ListItem Value="style-2">Blue Style</asp:ListItem>
            <asp:ListItem Value="style-3">Cyan Style</asp:ListItem>
        </asp:DropDownList>
        <div class="sfExample">Style of the box in colour</div>
    </li>

    <li class="sfFormCtrl">
        <asp:RadioButtonList ID="LinkType" runat="server" RepeatLayout="Flow">
            <asp:ListItem Value="Page">Page Link</asp:ListItem>
            <asp:ListItem Value="External">External Link</asp:ListItem>
            <asp:ListItem Value="Document">Document Link</asp:ListItem>
        </asp:RadioButtonList>
    </li>

    <li class="sfFormCtrl externalLinkCtrl" style="display: none;">
        <asp:Label runat="server" AssociatedControlID="ExternalLink" CssClass="sfTxtLbl ">External</asp:Label>
        <asp:TextBox ID="ExternalLink" runat="server" CssClass="sfTxt" />
        <div class="sfExample">External Link</div>
    </li>

    <li class="sfFormCtrl documentLinkCtrl" style="display: none;">
        <asp:Label runat="server" CssClass="sfTxtLbl">Document</asp:Label>
        <label id="selectedSelectedDocumentTitle" CssClass="sfTxtLbl" style="display:none;"></label>
        <span style="display: none;" class="sfSelectedItem" id="selectedSelectedDocumentID"></span>
        <div>
            <asp:LinkButton ID="selectButtonSelectedDocumentID" OnClientClick="return false;" runat="server" CssClass="sfLinkBtn sfChange">
                <span class="sfLinkBtnIn">
                  <asp:Literal runat="server" Text="<%$Resources:Labels, SelectDotDotDot %>" />
                </span>
            </asp:LinkButton>
            <asp:LinkButton ID="deselectButtonSelectedDocumentID" OnClientClick="return false;" runat="server" CssClass="sfLinkBtn sfChange">
                <span class="sfLinkBtnIn">
                  <asp:Literal runat="server" Text="<%$Resources:Labels, Remove %>" />
                </span>
            </asp:LinkButton>
        </div>
        <sf:EditorContentManagerDialog runat="server" ID="selectorSelectedDocumentID" DialogMode="Document" HostedInRadWindow="false" BodyCssClass="" />
        <div class="sfExample">Select your document...</div>
    </li>

    <li class="sfFormCtrl pageLinkCtrl" style="display: none;">
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
