<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ListingEdit__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MarketPlace.ListingEdit" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<table id="tblListingInfo" class="data-form">
    <tr>
        <td class="LeftColumnMarketPlace">
            Company Name:
        </td>
        <td class="RightColumn">
            <asp:Label ID="lblCompanyName" runat="server" CssClass="txtfontfamily"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="LeftColumnMarketPlace">
            Company Contact:
        </td>
        <td class="RightColumn">
            <asp:Label ID="lblCompanyContact" runat="server" CssClass="txtfontfamily"></asp:Label>
            <br />
        </td>
    </tr>
    <tr>
        <%--Aparna issue no.12966 for adding red star mark for compulsary field--%>
        <td class="LeftColumnMarketPlace">
            <em class="red">*</em>Name
        </td>
        <td class="RightColumn">
            <asp:TextBox ID="txtListingName" runat="server" CssClass="txtfontfamily"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please specify the MarketPlace Listing Name."
                Display="Dynamic" ControlToValidate="txtListingName" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="LeftColumnMarketPlace">
            Listing Type
        </td>
        <td class="RightColumn">
            <asp:DropDownList ID="cboListingType" runat="server" AutoPostBack="True" CssClass="txtfontfamily">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="LeftColumnMarketPlace">
            Category
        </td>
        <td class="RightColumn">
            <asp:DropDownList ID="cboCategories" runat="server" CssClass="txtfontfamily">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="LeftColumnMarketPlace">
            Offering Type
        </td>
        <td class="RightColumn">
            <asp:DropDownList ID="cboOfferingType" runat="server" CssClass="txtfontfamily">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <%--Aparna issue no.12966 for adding red star mark for compulsary field--%>
        <td class="LeftColumnMarketPlace">
            <em class="red">*</em>Plain Text Description
        </td>
        <td class="RightColumn">
            <asp:TextBox ID="txtDescription" runat="server" Width="300px" Height="150px" TextMode="MultiLine"
                CssClass="txtfontfamily txtRestrictResize"></asp:TextBox>
        </td>
    </tr>
    <%--	    <tr>
		    <td class="LeftColumnMarketPlace" >HTML Description</td>
		    <td class="RightColumn">
				    <asp:textbox id="txtHTMLDescription"  runat="server" width="300px" Height="150px"
					     TextMode="MultiLine"></asp:textbox>
				    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Please specify the HTML Description."
					    Display="Dynamic" ControlToValidate="txtHTMLDescription"></asp:RequiredFieldValidator></td>
	    </tr>--%>
    <tr>
        <td class="LeftColumnMarketPlace">
            Vendor Product Information URL
        </td>
        <td class="RightColumn">
            <asp:TextBox ID="txtVendorURL" runat="server" CssClass="txtfontfamily"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="LeftColumnMarketPlace">
            Request Information Email
        </td>
        <td class="RightColumn">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtfontfamily"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <%--td class="RightColumn">--%>
        <td>
        </td>
        <td class="RightColumn">
            <asp:CheckBox ID="chkAti" runat="server" Width ="80px" Text="ATI?"></asp:CheckBox>
            <asp:CheckBox ID="chkGraduate" runat="server" Width ="100px" Text="Graduate?" CssClass="cb"></asp:CheckBox>
            <asp:CheckBox ID="chkSchoolLeaver" runat="server" Width ="100px" Text="School Leaver?" CssClass="cb">
            </asp:CheckBox>
        </td>
        <%--</td>--%>
    </tr>
</table>
<p>
    <cc3:User ID="User1" runat="server" />
    <cc1:AptifyShoppingCart ID="ShoppingCart1" runat="server" />
</p>
