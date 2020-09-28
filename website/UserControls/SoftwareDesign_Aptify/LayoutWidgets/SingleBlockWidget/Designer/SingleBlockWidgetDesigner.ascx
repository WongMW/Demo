<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:resourcelinks id="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:resourcelinks>
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
        <sf:editorcontentmanagerdialog runat="server" id="selectorSelectedImageID" dialogmode="Image" hostedinradwindow="false" bodycssclass="" />
        <div class="sfExample">Select your image...</div>
    </li>
    
    <li class="sfFormCtrl">
        <asp:Label runat="server" CssClass="sfTxtLbl">Image Size</asp:Label>
        <telerik:RadTextBox InputType="Number" runat="server" CssClass="sfTxt" ID="ImageSize"></telerik:RadTextBox>
        <div class="sfExample">Image Size (a number)</div>
    </li>

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
            <sf:pagesselector runat="server" id="pageSelectorSelectedPageID"
                allowexternalpagesselection="false" allowmultipleselection="false" />
        </div>
        <div class="sfExample">Select your page...</div>
    </li>

    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="LinkText" CssClass="sfTxtLbl">Or enter a URL</asp:Label>
        <asp:TextBox ID="LinkText" runat="server" CssClass="sfTxt" />
        <div class="sfExample">URL for button to open once clicked</div>
    </li>
</ol>
