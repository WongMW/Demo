<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.RoomBookingApplications__cClass" CodeFile="RoomBookingApplications__c.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="LeftColumn">Requester</td>
           <td class="RightColumn">
               <asp:TextBox id="txtRequesterName" runat="server" AptifyDataField="Requester" Width="350px"    />
           </td>
      </tr>

      <tr>
           <td class="LeftColumn">On Behalf Of</td>
           <td class="RightColumn">
               <asp:TextBox id="txtOnBehalfOf" runat="server" AptifyDataField="OnBehalfOf" Width="350px"/>
           </td>
      </tr>

      <tr>
           <td class="LeftColumn">Venue</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbVenueID" runat="server" AptifyDataField="VenueID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwVenues UNION SELECT -1 ID, '' Name" Width="165px" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Meeting Title</td>
           <td class="RightColumn">
               <asp:TextBox id="txtMeetingTitle" runat="server" AptifyDataField="MeetingTitle"   TextMode="MultiLine" Width="350px" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Start Date</td>
           <td class="RightColumn">
               <asp:TextBox id="txtStartDate" runat="server" AptifyDataField="StartDate" Width="165px"  />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">End Date</td>
           <td class="RightColumn">
               <asp:TextBox id="txtEndDate" runat="server" AptifyDataField="EndDate" Width="165px"  />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Seats</td>
           <td class="RightColumn">
               <asp:TextBox id="txtSeats" runat="server" AptifyDataField="Seats" Width="165px"  />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Room Type</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbRoomTypeID" runat="server" AptifyDataField="RoomTypeID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwMeetingRoomTypes UNION SELECT -1 ID, '' Name" Width="165px" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Status</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbStatus" runat="server" AptifyDataField="Status" Width="165px" >
                   <asp:ListItem>Not Submitted</asp:ListItem>
                   <asp:ListItem>Submitted to CAI</asp:ListItem>
                   <asp:ListItem>Provisionally Booked</asp:ListItem>
                   <asp:ListItem>Quote Sent to Customer</asp:ListItem>
                   <asp:ListItem>Confirmed</asp:ListItem>
                   <asp:ListItem>Cancelled</asp:ListItem>
               </asp:DropDownList>
           </td>
      </tr>
         <tr>
            <td>               
            </td>
            <td> </td>
        </tr>
        <tr>
             <td class="LeftColumn">               
            </td>
            <td class="RightColumn"><uc1:CreditCard ID="CreditCard" runat="server"></uc1:CreditCard>
            </td>
        </tr>
      <tr>
        <td></td><td><asp:Button ID="cmdSave" Runat="server" Text="Save"></asp:Button>&nbsp;<asp:Button 
              ID="cmdSubmit" Runat="server" Text="Submit"></asp:Button></td>
      </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
