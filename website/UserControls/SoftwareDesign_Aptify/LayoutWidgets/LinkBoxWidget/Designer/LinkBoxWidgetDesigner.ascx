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

    <li class="sfFormCtrl">
        <asp:Label runat="server" AssociatedControlID="LinkText" CssClass="sfTxtLbl">Or enter a URL</asp:Label>
        <asp:TextBox ID="LinkText" runat="server" CssClass="sfTxt" />
        <div class="sfExample">URL for button to open once clicked</div>
    </li>
    
    <li class="sfFormCtrl">
        <asp:Label runat="server" CssClass="sfTxtLbl">Open in new tab</asp:Label>
        <asp:CheckBox runat="server" ID="TabCheckbox" Text="Tick this box if you want the link to open in a new tab"/>
    </li>
</ol>
