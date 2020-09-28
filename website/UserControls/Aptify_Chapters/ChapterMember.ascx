<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChapterMember.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Chapters.ChapterMember" %>
<table id="tblMain" runat="server" class="data-form">
    <tr>
        <td colspan="2" class="Header">
            Chapter Member
        </td>
    </tr>
    <tr>
        <td class="LeftColumn">
            <asp:Label ID="lblName" runat="server">Name</asp:Label>
        </td>
        <td class="RightColumn">
            <asp:TextBox AptifyDataField="FirstName" ID="txtFirstName" runat="server" Width="100px"></asp:TextBox>
            <asp:TextBox AptifyDataField="MiddleName" ID="txtMiddleName" Width="25px" runat="server" />
            <asp:TextBox AptifyDataField="LastName" ID="txtLastName" Width="105px" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="LeftColumn">
            <asp:Label ID="lblTitle" runat="server">Title</asp:Label>
        </td>
        <td class="RightColumn">
            <asp:TextBox AptifyDataField="Title" ID="txtTitle" Width="250px" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="LeftColumn">
            <asp:Label ID="lblAddress" runat="server">Address</asp:Label>
        </td>
        <td class="RightColumn">
            <asp:TextBox AptifyDataField="AddressLine1" Width="250px" ID="txtAddressLine1" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="txtAddressLine2" AptifyDataField="AddressLine2" Width="250px" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="txtAddressLine3" AptifyDataField="AddressLine3" Width="250px" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="LeftColumn">
            <asp:Label ID="lblCityStateZip" runat="server">City, State ZIP</asp:Label>
        </td>
        <td class="RightColumn">
            <asp:TextBox AptifyDataField="City" ID="txtCity" runat="server" Width="75px"></asp:TextBox>
            <asp:DropDownList AptifyDataField="State" Width="60px" AptifyListTextField="State"
                AptifyListValueField="State" ID="cmbState" runat="server" DataValueField="State"
                DataTextField="State" />
            <asp:TextBox AptifyDataField="ZipCode" Width="75px" ID="txtZipCode" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="LeftColumn">
            <asp:Label ID="lblCountry" runat="server">Country</asp:Label>
        </td>
        <td class="RightColumn">
            <asp:DropDownList Width="250px" AptifyDataField="CountryCodeID" AptifyListTextField="Country"
                AptifyListValueField="ID" ID="cmbCountry" runat="server" DataValueField="ID"
                DataTextField="Country" AutoPostBack="true" />
        </td>
    </tr>
    <tr>
        <td class="LeftColumn">
            <asp:Label ID="lblPhone" runat="server">Phone</asp:Label>
        </td>
        <td class="RightColumn">
            <asp:TextBox AptifyDataField="PhoneCountryCode" ID="txtPhoneCountryCode" runat="server"
                Width="15px" />
            <asp:TextBox AptifyDataField="PhoneAreaCode" ID="txtPhoneAreaCode" runat="server"
                Width="30px" />
            <asp:TextBox AptifyDataField="Phone" ID="txtPhone" runat="server" Width="60px" />
        </td>
    </tr>
    <tr>
        <td class="LeftColumn">
            <asp:Label ID="lblFax" runat="server">Fax</asp:Label>
        </td>
        <td class="RightColumn">
            <asp:TextBox AptifyDataField="FaxCountryCode" ID="txtFaxCountryCode" runat="server"
                Width="15px" />
            <asp:TextBox AptifyDataField="FaxAreaCode" ID="txtFaxAreaCode" Width="30px" runat="server" />
            <asp:TextBox AptifyDataField="FaxPhone" ID="txtFaxPhone" Width="60px" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="LeftColumn">
            <asp:Label ID="lblEmail" runat="server">Email</asp:Label>
        </td>
        <td class="RightColumn">
            <asp:TextBox AptifyDataField="Email1" ID="Email1" Width="200px" runat="server" />
            <%--Suraj Issue 15210 ,4/3/13 RegularExpressionValidator validator --%>
            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
                
                ControlToValidate="Email1" ErrorMessage="Invalid Email Format"
                ForeColor="Red"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblError" runat="server" Visible="False" />
        </td>
        <td>
            <asp:Button ID="cmdSave" Text="Save" runat="server" CssClass="submitBtn" />
        </td>
    </tr>
</table>
