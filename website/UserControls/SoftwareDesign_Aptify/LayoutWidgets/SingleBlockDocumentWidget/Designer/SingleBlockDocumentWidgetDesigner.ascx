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
        <asp:Label runat="server" AssociatedControlID="Message" CssClass="sfTxtLbl">Message</asp:Label>
        <asp:TextBox ID="Message" runat="server" CssClass="sfTxt" />
        <div class="sfExample">Message of the box</div>
    </li>
    
    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="ButtonLabel" CssClass="sfTxtLbl">Button Label</asp:Label>
        <asp:TextBox ID="ButtonLabel" runat="server" CssClass="sfTxt" />
        <div class="sfExample">Label for the read more button</div>
    </li>
    

    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="Style" CssClass="sfTxtLbl">Style</asp:Label>
        <asp:DropDownList SkinID="Sitefinity" ID="Style" runat="server">
            <asp:ListItem Value="style-1">Red Style</asp:ListItem>
            <asp:ListItem Value="style-2">Blue Style</asp:ListItem>
            <asp:ListItem Value="style-3">Cyan Style</asp:ListItem>
            <asp:ListItem Value="style-1-alt">Alternative Red Style</asp:ListItem>
            <asp:ListItem Value="style-2-alt">Alternative Blue Style</asp:ListItem>
            <asp:ListItem Value="style-3-alt">Alternative Cyan Style</asp:ListItem>
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
    
     <li class="sfFormCtrl documentLinkCtrl">
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
</ol>
