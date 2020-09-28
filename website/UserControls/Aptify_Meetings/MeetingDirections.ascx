<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MeetingDirections.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MeetingDirections" %>
<%@ Register Src="GoogleMapsDirectionsAPI.ascx" TagName="GoogleMapsDirectionsAPI"
    TagPrefix="ucGMD" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div>
    <div id="Addresses" class="DirAddressDiv">
        <div  class="dirOrAddressDiv">
            <h3 class="Dirheader" style="padding-left:5px;">
                Origin Address</h3>
            <table cellpadding="2" cellspacing="2" border="0" width="98%;">
                <tr>
                    <td style="padding-left:5px;">
                        Address
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtfromStreet" Width="238px" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:5px;">
                        City, State, Zip
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtfromCityStateZip" Width="238px" />
                        <asp:RequiredFieldValidator ID="rvCityStateZip" runat="server" ControlToValidate="txtfromCityStateZip"
                            ErrorMessage="Required" />
                    </td>
                </tr>
              <%--  <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:CheckBox ID="chkDisplayDirection" Text="Display Directions" runat="server" />
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnGetDirections" Text="Get Directions" CssClass="GetDirectionButton" />
                    </td>
                </tr>
            </table>
        </div>
    <%-- dilip  issue 12721 23/01/2012 put the width & height of Div .--%>
        <div class="DirArrow" style="height: 31px; width:60px">
        </div>
        <div  class="dirOrAddressDiv">
            <h3 class="Dirheader" style="padding-left:5px;">
                Meeting Address</h3>
            &nbsp;<asp:Label ID="lblMeetingAddress" runat="server" />
            <br />
             &nbsp;<asp:Label ID="lblMeetingCityStateZip" runat="server"/>
        </div>
    </div>
    <div id="MapDirection" style="width: 99%; float: left;">
        <%--<div class="Directions" style="width:37%; float:left; border:1px solid #c7baa6;"></div>        --%>
        <div class="GoogleMap" id="DirectionMap" style=" width:100%; float:left; border:1px solid #c7baa6; height:500px;" >
        </div>
    </div>
    <ucGMD:GoogleMapsDirectionsAPI ID="GoogleMapsDirectionsAPI" MapElementId="DirectionMap"
        runat="server" />
    <cc1:User ID="User1" runat="server"></cc1:User>
</div>
<%--<fieldset title="Start Address" style="width:45%;float:left;padding:10px">
<legend>Origin Address</legend>
<label>Address :</label>
<br />
<label>City & State OR Zip Code:</label>

</fieldset>--%>
<%--<fieldset title="Destination" style="width:45%;float:right;padding:10px">
<legend>Destination</legend>
<label>Street Address [optional]:</label>
<asp:textbox runat="server" id="txttoStreet" /><br />
<label>City & State, or Zip code:</label>
<asp:textbox runat="server" id="txttoCityStateZip" /><asp:RequiredFieldValidator
    ID="rv2" ControlToValidate="txttoCityStateZip" runat="server" ErrorMessage="<< Required"></asp:RequiredFieldValidator>
</fieldset>--%>
