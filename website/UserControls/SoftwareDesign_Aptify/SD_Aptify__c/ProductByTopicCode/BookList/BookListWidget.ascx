<%@ Control Language="C#" CodeFile="BookListWidget.ascx.cs" 
Inherits="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.BookList.BookListWidget" %>

<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div style="vertical-align: text-top;">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</div>

<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView sd-booklist">
    <div class="sfContentBlock">
        <h3><asp:Label runat="server" ID="lblTitle" Text="Recommended publications"></asp:Label> </h3>
    </div>

    <telerik:RadGrid CssClass="plain-table" ID="RadGrid1" runat="server" AllowPaging="false" AllowSorting="false"
        AutoGenerateColumns="false" Skin="" GridLines="None" MasterTableView-DataKeyNames="ProductId">
        <MasterTableView DataKeyNames="Id">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            <AlternatingItemStyle HorizontalAlign="Left" />
            <Columns>
                <telerik:GridImageColumn ImageHeight="200px" HeaderText="" DataImageUrlFields="ImageUrl"></telerik:GridImageColumn>
                <telerik:GridBoundColumn HeaderText="Title" DataField="Title"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Price" DataField="Price" ></telerik:GridBoundColumn>
                <telerik:GridHyperLinkColumn HeaderText="" Text="Read more" DataNavigateUrlFields="ProductUrl"></telerik:GridHyperLinkColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>

<cc1:User ID="User1" runat="server" />
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
<script>
    $('.sd-booklist a').addClass("cai-btn cai-btn-red-inverse");
</script>

