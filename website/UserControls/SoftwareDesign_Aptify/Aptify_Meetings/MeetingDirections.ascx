<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Meetings/MeetingDirections.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MeetingDirections" %>
<%@ Register Src="GoogleMapsDirectionsAPI.ascx" TagName="GoogleMapsDirectionsAPI"
    TagPrefix="ucGMD" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div>
    <div id="Addresses" class="DirAddressDiv">
        <div class="cai-form directions-box">
            <h3 class="form-title">Origin Address</h3>

            <div class="cai-form-content">
                <div class="field-group">
                    <span class="label-title">Address</span>
                    <asp:TextBox runat="server" ID="txtfromStreet" Width="238px" />
              
                    <span class="label-title">City, State, Zip</span>
                    <asp:TextBox runat="server" ID="txtfromCityStateZip" Width="238px" />
                    <asp:RequiredFieldValidator ID="rvCityStateZip" runat="server" ControlToValidate="txtfromCityStateZip"
                        ErrorMessage="Required" />
                </div>
                <div class="field-group">
                    <asp:Button runat="server" ID="btnGetDirections" Text="Get Directions" CssClass="GetDirectionButton btn" />
                </div>
            </div>
        </div>

        <div class="cai-form directions-box">
            <h3 class="form-title">Meeting Address</h3>
            <div class="cai-form-content">
                <div class="field-group">
                    <asp:Label ID="lblMeetingAddress" runat="server" />
                    <asp:Label ID="lblMeetingCityStateZip" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div id="MapDirection" style="width: 99%; float: left;">
        <%--<div class="Directions" style="width:37%; float:left; border:1px solid #c7baa6;"></div>        --%>
        <div class="GoogleMap" id="DirectionMap" style="width: 100%; float: left; border: 1px solid #c7baa6; height: 500px;">
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
