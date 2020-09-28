<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChapterEdit.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Chapters.ChapterEditControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="ChapterMember" Src="ChapterMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
                <tr id="trHeader" runat="server">
                    <td colspan="2" class="Header">
          <asp:label id="lblChapterName" Runat="server">Edit Chapter</asp:label></td>
                </tr>
                <tr>
                    <td colspan="2">
          <asp:Label ID="lblError" Runat="server" Visible="False"></asp:Label></td>
                </tr>
            <tr>
              <td class="LeftColumn">
                <asp:Label ID="lblName" Runat="server">Chapter Name</asp:Label>
              </td>
              <td  class="RightColumn">
                <asp:TextBox ID="txtName" AptifyDataField="Name" Runat="server"></asp:TextBox>
              </td>
            </tr>
            <tr>
		      <td class="LeftColumn">
		        <asp:label id="lblAddress" Runat="server">Address</asp:label></td>
		      <td class="RightColumn">
		         <asp:TextBox  AptifyDataField="AddressLine1" Width="250px" id="txtAddressLine1" Runat="server"></asp:TextBox><br />
			     <asp:TextBox id="txtAddressLine2" AptifyDataField="AddressLine2" Width="250px" Runat="server"></asp:TextBox><br />
			     <asp:TextBox id="txtAddressLine3" AptifyDataField="AddressLine3" Width="250px" Runat="server"></asp:TextBox>
   		      </td>
            </tr>
            <tr>
              <td class="LeftColumn">
                <asp:Label ID="lblCityStateZip" Runat="server">City, State ZIP</asp:Label>
              </td>
              <td class="RightColumn">
                <asp:TextBox AptifyDataField="City" id="txtCity" Runat="server" Width="75px"></asp:TextBox>
                <asp:dropdownlist AptifyDataField="State" width="60px" AptifyListTextField="State" AptifyListValueField="State"  id="cmbState" Runat="server" DataValueField="State" DataTextField="State" />
                <asp:TextBox AptifyDataField="ZipCode" Width="75px" id="txtZipCode" Runat="server"/>
              </td>
            </tr>
            <tr>
              <td class="LeftColumn">
                <asp:Label ID="lblCountry" Runat="server">Country</asp:Label>
              </td>
		      <td class="RightColumn">
		        <asp:dropdownlist width="250px" AptifyDataField="CountryCodeID" AptifyListTextField="Country" AptifyListValueField="ID"  id="cmbCountry" Runat="server" DataValueField="ID" DataTextField="Country" AutoPostBack="true" />
		      </td>
            </tr>
            <tr>
              <td class="LeftColumn">
                <asp:Label ID="lblTaxID" Runat="server">Tax ID</asp:Label>
              </td>
              <td class="RightColumn">
                <asp:TextBox ID="txtTaxID" AptifyDataField="FedTaxID" Runat="server"></asp:TextBox>
              </td>
            </tr>
                <tr id="trFooter" runat="server">
                    <td colspan="2">
          <asp:Button id="cmdSave" runat="server" Text="Save" CssClass="submitBtn"></asp:Button>
          <asp:Button ID="cmdChapter" runat="server" Text="Go To Chapter" CssClass="submitBtn"  />
          </td>
                </tr>
  </table>
    <cc3:user id="User1" runat="server" />
</div>