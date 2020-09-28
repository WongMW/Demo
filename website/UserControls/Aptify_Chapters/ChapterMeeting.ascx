<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChapterMeeting.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Chapters.ChapterMeetingControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc5" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="uc1" TagName="ChapterMember" Src="ChapterMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
            </td>
            <td colspan="2">
                <asp:Label ID="lblError" ForeColor="Red" Font-Size="8pt" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <asp:Label ID="lblChapterName" runat="server" CssClass="CommitteeName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblchapter" runat="server" ForeColor="Red" Font-Size="8pt">*</asp:Label>
            </td>
            <td class="LeftColumn">
                <asp:Label ID="lblName" runat="server">Name</asp:Label>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td class="LeftColumn">
                <asp:Label ID="lblDescription" runat="server">Description</asp:Label>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="txtRestrictResize"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td class="LeftColumn">
                <asp:Label ID="lblType" runat="server">Type</asp:Label>
            </td>
            <td class="RightColumn">
                <asp:DropDownList ID="cmbType" runat="server">
                    <asp:ListItem Value="One-Time">One-Time</asp:ListItem>
                    <asp:ListItem Value="Recurring">Recurring</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td class="LeftColumn">
                <asp:Label ID="lblStartDate" runat="server">Start Date/Time</asp:Label>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                <%-- suraj issue 15154 ,3/21/13 Add Css CLass CssClass="CommanValidationMessage"--%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="CommanValidationMessage" runat="server"
                    Display="Dynamic" ControlToValidate="txtStartDate" ErrorMessage="Please enter a valid Start Date/Time."></asp:RequiredFieldValidator>
                   <%-- suraj issue 15154 ,3/21/13 Add Css CLass CssClass="CommanValidationMessage"--%>
                <asp:CustomValidator ID="vldStartDate" CssClass="CommanValidationMessage"  runat="server" Display="Dynamic"
                    ControlToValidate="txtStartDate" ErrorMessage="Please enter a valid Start Date/Time."></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td class="LeftColumn">
                <asp:Label ID="lblEndDate" runat="server">End Date/Time</asp:Label>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                <%-- suraj issue 15154, 3/21/13 Add Css CLass CssClass="CommanValidationMessage"--%>
                <asp:CustomValidator ID="vldEndDate" runat="server" CssClass="CommanValidationMessage"  Display="Dynamic"
                    ControlToValidate="txtEndDate" ErrorMessage="Please enter a valid End Date/Time."></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td class="LeftColumn">
                <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
            </td>
            <td class="RightColumn">
                <asp:DropDownList ID="cmbStatus" runat="server">
                    <asp:ListItem Value="Planned">Planned</asp:ListItem>
                    <asp:ListItem Value="Completed">Completed</asp:ListItem>
                    <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td class="LeftColumn">
                <asp:Label ID="lblLocation" runat="server">Location</asp:Label>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td class="LeftColumn">
                <asp:Label ID="lblAddress1" runat="server">Address</asp:Label>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtAddressLine1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td class="LeftColumn">
                <asp:Label ID="lblCityStateZip" runat="server">City, State ZIP</asp:Label>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                <asp:DropDownList ID="cmbState" CssClass="ControlHeight" runat="server" DataTextField="State"
                    DataValueField="State">
                </asp:DropDownList>
                <asp:TextBox ID="txtZIP" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td class="LeftColumn">
                <asp:Label ID="lblCountry" runat="server">Country</asp:Label>
            </td>
            <td class="RightColumn">
                <asp:DropDownList ID="cmbCountry" runat="server" DataTextField="Country" DataValueField="ID"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td colspan="2">
                <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="submitBtn"></asp:Button>
                <asp:LinkButton ID="lnkChapter" runat="server">Go To Chapter</asp:LinkButton>
            </td>
        </tr>
    </table>
    <cc5:AptifyShoppingCart runat="server" ID="ShoppingCart1" />
    <cc3:User ID="User1" runat="server" />
</div>
