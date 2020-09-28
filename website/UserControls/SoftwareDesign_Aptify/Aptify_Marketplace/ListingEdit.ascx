<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Marketplace/ListingEdit.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.MarketPlace.ListingEdit" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>

    <table id="tblListingInfo" class="data-form">
	    <tr>
		    <td class="LeftColumnMarketPlace"> Company Name:</td>
		    <td  class="RightColumn">
			    <asp:label id="lblCompanyName"  runat="server" CssClass="txtfontfamily"></asp:label></td>
	    </tr>
	    <tr>
		    <td class="LeftColumnMarketPlace" >Company Contact:</td>
		    <td  class="RightColumn">
			    <asp:label id="lblCompanyContact"  runat="server" CssClass="txtfontfamily"></asp:label>
			<br />    
			</td>
	    </tr>
	    <tr>
        <%--Aparna issue no.12966 for adding red star mark for compulsary field--%>
		    <td class="LeftColumnMarketPlace" ><em class="red">*</em>Name</td>
		    <td class="RightColumn">
			    <asp:textbox id="txtListingName"  runat="server" CssClass="txtfontfamily"></asp:textbox>
			    <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Please specify the MarketPlace Listing Name."
				    Display="Dynamic" ControlToValidate="txtListingName"  ForeColor="Red"></asp:RequiredFieldValidator></td>
	    </tr>
	    <tr>
		    <td class="LeftColumnMarketPlace">Listing Type</td>
		    <td class="RightColumn"><asp:dropdownlist id="cboListingType"  runat="server" 
                    AutoPostBack="True" CssClass="txtfontfamily" ></asp:dropdownlist></td>
	    </tr>
	    <tr>
		    <td  class="LeftColumnMarketPlace">Category </td>
			    
		   
		    <td class="RightColumn">
				    <asp:dropdownlist id="cboCategories"  runat="server" CssClass="txtfontfamily" ></asp:dropdownlist></td>
	    </tr>
	    <tr>
		    <td  class="LeftColumnMarketPlace">Offering Type</td>
		    <td class="RightColumn">
			    <asp:dropdownlist id="cboOfferingType" runat="server" CssClass="txtfontfamily" ></asp:dropdownlist></td>
	    </tr>
	    <tr>   <%--Aparna issue no.12966 for adding red star mark for compulsary field--%>
		    <td  class="LeftColumnMarketPlace"><em class="red">*</em>Plain Text Description</td>
		    <td class="RightColumn">
			    <asp:textbox id="txtDescription"  runat="server" width="300px" Height="150px"
				    TextMode="MultiLine" CssClass="txtfontfamily txtRestrictResize" ></asp:textbox></td>
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
		    <td  class="LeftColumnMarketPlace">Vendor Product Information URL</td>
		    <td class="RightColumn">
				    <asp:textbox id="txtVendorURL"  runat="server" CssClass="txtfontfamily" ></asp:textbox></td>
	    </tr>
	    <tr>
		    <td  class="LeftColumnMarketPlace">Request Information Email
    				
		    </td>
		    <td class="RightColumn">
			    <asp:textbox id="txtEmail"  runat="server" CssClass="txtfontfamily" ></asp:textbox></td>
	    </tr>
    </table>
    <p>
	    <cc3:User id="User1" runat="server" />
	    <cc1:AptifyShoppingCart id="ShoppingCart1" runat="server" />
    </p>
