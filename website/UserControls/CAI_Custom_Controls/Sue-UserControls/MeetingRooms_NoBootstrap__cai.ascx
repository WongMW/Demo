<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.MeetingRoomsClass" CodeFile="MeetingRooms_NoBootstrap__cai.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="LeftColumn">Name</td>
           <td class="RightColumn">
               <asp:TextBox id="txtName" runat="server" AptifyDataField="Name" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Description</td>
           <td class="RightColumn">
               <asp:TextBox id="txtDescription" runat="server" AptifyDataField="Description" Width="100%"  TextMode="MultiLine" Height="100px" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Parent ID</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbParentID" runat="server" AptifyDataField="ParentID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwMeetingRooms UNION SELECT -1 ID, '' Name" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Venue</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbVenueID" runat="server" AptifyDataField="VenueID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwVenues" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Width</td>
           <td class="RightColumn">
               <asp:TextBox id="txtWidth" runat="server" AptifyDataField="Width" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Depth</td>
           <td class="RightColumn">
               <asp:TextBox id="txtDepth" runat="server" AptifyDataField="Depth" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Ceiling Height</td>
           <td class="RightColumn">
               <asp:TextBox id="txtCeilingHeight" runat="server" AptifyDataField="CeilingHeight" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Lat</td>
           <td class="RightColumn">
               <asp:TextBox id="txtLat" runat="server" AptifyDataField="Lat" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Long</td>
           <td class="RightColumn">
               <asp:TextBox id="txtLong" runat="server" AptifyDataField="Long" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Floor Weight Capacity</td>
           <td class="RightColumn">
               <asp:TextBox id="txtFloorWeightCapacity" runat="server" AptifyDataField="FloorWeightCapacity" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Obstructions</td>
           <td class="RightColumn">
               <asp:TextBox id="txtObstructions" runat="server" AptifyDataField="Obstructions" Width="100%"  TextMode="MultiLine" Height="100px" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Handicap Access</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbHandicapAccess" runat="server" AptifyDataField="HandicapAccess" >
                   <asp:ListItem>None</asp:ListItem>
                   <asp:ListItem>Partial</asp:ListItem>
                   <asp:ListItem>Full</asp:ListItem>
               </asp:DropDownList>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Meeting Types Allowed</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbMeetingTypesAllowed" runat="server" AptifyDataField="MeetingTypesAllowed" >
                   <asp:ListItem>All</asp:ListItem>
                   <asp:ListItem>Restricted</asp:ListItem>
               </asp:DropDownList>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Comments</td>
           <td class="RightColumn">
               <asp:TextBox id="txtComments" runat="server" AptifyDataField="Comments" Width="100%"  TextMode="MultiLine" Height="100px" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Chargeable Product</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbChargeableProductID" runat="server" AptifyDataField="ChargeableProductID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwProducts UNION SELECT -1 ID, '' Name" />
           </td>
      </tr>
      <tr>
        <td></td><td><asp:Button ID="cmdSave" Runat="server" Text="Save Record"></asp:Button></td>
      </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
