<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>
<ol>
    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="Message" CssClass="sfTxtLbl">Title</asp:Label>
        <asp:TextBox ID="Title" runat="server" CssClass="sfTxt" />
        <div class="sfExample">Title of the box</div>
    </li>
    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="Message" CssClass="sfTxtLbl ">Subtitle</asp:Label>
        <asp:TextBox ID="Subtitle" runat="server" CssClass="sfTxt" />
        <div class="sfExample">Subtitle of the box</div>
    </li>
    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="Message" CssClass="sfTxtLbl">Message</asp:Label>
        <asp:TextBox ID="Message" runat="server" CssClass="sfTxt" />
        <div class="sfExample">Message of the box</div>
    </li>
    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="Message" CssClass="sfTxtLbl">Style</asp:Label>
        <asp:DropDownList SkinID="Sitefinity" ID="Style" runat="server">
            <asp:ListItem Value="style-1">Red Style</asp:ListItem>
            <asp:ListItem Value="style-2">Blue Style</asp:ListItem>
            <asp:ListItem Value="style-3">Cyan Style</asp:ListItem>
        </asp:DropDownList>
        <div class="sfExample">Style of the box in colour</div>
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
    
    <li class="sfFormCtrl">
        <asp:Label runat="server" CssClass="sfTxtLbl">Image Size</asp:Label>
        <telerik:RadTextBox InputType="Number" runat="server" CssClass="sfTxt" ID="ImageSize"></telerik:RadTextBox>
        <div class="sfExample">Image Size (a number)</div>
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
        <label id="sqpreviewSelectedDocumentID" CssClass="sfTxtLbl" style="display:none;"></label>
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
