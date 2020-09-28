<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.MeetingRoomsClass" CodeFile="MeetingRooms_Bootstrap__cai.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <form class="form">
        <div class="form-group">
            <label for="txtName">Name</label>
            <asp:TextBox id="txtName" CssClass="form-control" runat="server" AptifyDataField="Name" Width="100%" placeholder="Your Name"/>
        </div>
        <div class="form-group">
            <label for="txtDescription">Description</label>
            <asp:TextBox id="txtDescription" CssClass="form-control" runat="server" AptifyDataField="Description" Width="100%"  TextMode="MultiLine" Height="100px" placeholder="Meeting Description"/>
        </div>
        <div class="form-group">
            <label for="cmbParentID">Parent ID</label>
            <asp:DropDownList id="cmbParentID" CssClass="form-control" runat="server" AptifyDataField="ParentID" placeholder="Parent ID number" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwMeetingRooms UNION SELECT -1 ID, '' Name" />
        </div>
        <div class="form-group">
            <label for="cmbVenueID">Venue</label>
            <asp:DropDownList id="cmbVenueID" CssClass="form-control" runat="server" AptifyDataField="VenueID" placeholder="Venue" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwVenues" />
        </div>
        <div class="form-group">
            <label for="txtWidth">Width</label>
            <asp:TextBox id="txtWidth" CssClass="form-control" runat="server" AptifyDataField="Width" Width="100%" placeholder="Room Width"/>
        </div>
        <div class="form-group">
            <label for="txtDepth">Depth</label>
            <asp:TextBox id="txtDepth" CssClass="form-control" runat="server" AptifyDataField="Depth" Width="100%" placeholder="Room Depth"/>
        </div>
        <div class="form-group">
            <label for="txtCeilingHeight">Ceiling Height</label>
            <asp:TextBox id="txtCeilingHeight" CssClass="form-control" runat="server" AptifyDataField="CeilingHeight" Width="100%" placeholder="Ceiling Height"/>
        </div>
        <div class="form-group">
            <label for="txtLat">Lat</label>
            <asp:TextBox id="txtLat" CssClass="form-control" runat="server" AptifyDataField="Lat" Width="100%" placeholder="Latitude"/>
        </div>
        <div class="form-group">
            <label for="txtLong">Long</label>
            <asp:TextBox id="txtLong" CssClass="form-control" runat="server" AptifyDataField="Long" Width="100%" placeholder="Longitude"/>
        </div>
        <div class="form-group">
            <label for="txtFloorWeightCapacity">Floor Weight Capacity</label>
            <asp:TextBox id="txtFloorWeightCapacity" CssClass="form-control" runat="server" AptifyDataField="FloorWeightCapacity" Width="100%" placeholder="Floor Weight Capacity required"/>
        </div>
        <div class="form-group">
            <label for="txtObstructions">Obstructions</label>
            <asp:TextBox id="txtObstructions" CssClass="form-control" runat="server" AptifyDataField="Obstructions" Width="100%" TextMode="MultiLine" Height="100px" placeholder="Obstructions"/>
        </div>
        <div class="form-group">
            <label for="cmbHandicapAccess">Handicap Access</label>
            <asp:DropDownList id="cmbHandicapAccess" CssClass="form-control" runat="server" AptifyDataField="HandicapAccess"  placeholder="Handicap Access">
                <asp:ListItem>None</asp:ListItem>
                <asp:ListItem>Partial</asp:ListItem>
                <asp:ListItem>Full</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="cmbMeetingTypesAllowed">Meeting Types Allowed</label>
            <asp:DropDownList id="cmbMeetingTypesAllowed" CssClass="form-control" runat="server" AptifyDataField="MeetingTypesAllowed"  placeholder="Meeting Types Allowed">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Restricted</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="txtComments">Comments</label>
            <asp:TextBox id="txtComments" CssClass="form-control" runat="server" AptifyDataField="Comments" Width="100%" TextMode="MultiLine" Height="100px" placeholder="Your Comments"/>
        </div>
        <div class="form-group">
            <label for="cmbChargeableProductID">Chargeable Product</label>
            <asp:DropDownList id="cmbChargeableProductID" CssClass="form-control" runat="server" AptifyDataField="ChargeableProductID" placeholder="Chargeable Product" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwProducts UNION SELECT -1 ID, '' Name" />
        </div>
        <asp:Button ID="cmdSave" class="btn btn-default" Runat="server" Text="Save Record"></asp:Button>
    </form>
    <asp:Label id="lblError" ForeColor="Red" runat="server" CssClass="label label-default" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
