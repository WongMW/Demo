<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MeetingActionControl.ascx.vb" Inherits="Files_MeetingActionControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<table runat="server" id="tblMain" width="100%">
    <tr>
        <td>
            <img runat="server" id="imgGeneral" src="" alt="General Info" title="General Info" border="0"  align="middle"/>&nbsp;<asp:HyperLink runat="server" ID="lnkGeneral" Text="General" ToolTip="View general information about the meeting"   /> 
        </td>
    </tr>
    <tr>
        <td>
            <img runat="server" id="imgSchedule" src="" alt="Meeting Schedule" title="Meeting Schedule" border="0"  align="middle"/>&nbsp;<asp:HyperLink runat="server" ID="lnkSchedule" Text="Schedule" Font-Size="10pt" ToolTip="View meeting schedule"  />
        </td>
    </tr>
    <tr>
        <td>
            <img runat="server" id="imgSpeakers" src="" alt="Speakers" title="Speakers" border="0"  align="middle"/>&nbsp;<asp:HyperLink runat="server" ID="lnkSpeakers" Text="Speakers" Font-Size="10pt" ToolTip="View a list of speakers for the meeting"   />
        </td>
    </tr>
    <tr>
        <td>
            <img runat="server" id="imgTravel" src="" alt="Travel" title="Travel" border="0"  align="middle"/>&nbsp;<asp:HyperLink runat="server" ID="lnkTravel" Text="Travel" Font-Size="10pt" ToolTip="View travel information for the meeting."   /><br /> 
        </td>
    </tr>
    <tr id="trForum" runat="server">
        <td>
            <img runat="server" id="imgForum" src="" alt="Discussion Forum" title="Discussion Forum" border="0"  align="middle"/>&nbsp;<asp:HyperLink runat="server" ID="lnkForum" Text="Forum" Font-Size="10pt" ToolTip="View discussion forum for the meeting"   /><br /> 
        </td>
    </tr>
    <tr id="trPeopleYouMayKnow" runat="server">
        <td>
            <img runat="server" id="imgPeopleYouMayKnow" src="" alt="People You May Know" title="People You May Know" border="0"  align="middle"/>&nbsp;<asp:HyperLink 
                runat="server" ID="lnkPeopleYouMayKnow" Text="People at Meeting" 
                Font-Size="10pt" ToolTip="View People You May Know for the meeting"   /><br /> 
        </td>
    </tr>
    <tr id="trRegister" runat="server">
        <td>
            <!-- <img src="Images/RegisterSmall.gif" alt="Register" border="0"  align="absmiddle"/> -->
            <!-- changed img to asp:image to control visibility -->
            <asp:image  ID="imgRegister"  runat="server" tooltip="Register" AlternateText="Register"  BorderWidth="0"  ImageAlign="AbsMiddle" />  
            <!-- changed lnkRegister from <a> tag to asp:LinkButton to allow ToolTip like the above links use. -->
            &nbsp;<asp:LinkButton ID="lnkRegister" runat="server" ToolTip="Register Online for the meeting...">Register Now - 
            <asp:Label runat="server" ID="lblPrice" /></asp:LinkButton>&nbsp;<br />
              <br />
            <asp:Image ID="imgRegisterGroup" runat="server" ToolTip="Register" AlternateText="Register"
                BorderWidth="0" ImageAlign="AbsMiddle" />&nbsp;<asp:HyperLink runat="server" ID="HLGroupReg"
                    Text="Register Group"></asp:HyperLink>&nbsp;<br />
            <asp:Label runat="server" ID="lblFrimAdminLogin" Text="Firm administrators should login first to register group." />
            <asp:LinkButton ID="lnkLogin" runat="server" ToolTip="login first to register group..."
                Text="Login"></asp:LinkButton>
            <asp:Label ID="lblMemSavings" runat="server" Font-Bold="True" Font-Size="Smaller"
                ForeColor="Green" Visible="False"></asp:Label><br />
                <br />
            <!-- added lblRegistrationResult to display result of add product - primarily to display error if one occurs. -->
            <asp:label runat="server" ID="lblRegistrationResult" Visible="false"></asp:label><asp:Label ID="lblMeetingStatus" runat="server" Visible="False" Font-Bold="True" ForeColor="Black"></asp:Label><br />
            <br />
            <asp:LinkButton ID="lnkNewMeeting" runat="server" Visible="False" ToolTip="Newer event available!">Click here for the next occurence of this event.</asp:LinkButton></td></tr></table><cc1:User ID="User1" runat="server" />
<cc2:AptifyShoppingCart id="ShoppingCart1" runat="server" Width="47px" Height="14px" Visible="False"></cc2:AptifyShoppingCart>
