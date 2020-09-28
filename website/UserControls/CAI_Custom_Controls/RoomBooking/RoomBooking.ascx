<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RoomBooking.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.RoomBooking.RoomBooking" %>
<script type="text/JavaScript" src="https://www.charteredaccountants.ie/Scripts/jquery-1.7.1.min.js" ></script>

<asp:Label ID="lblVenueIndicator" Visible="false" runat="server"></asp:Label>

<asp:UpdatePanel ID="pnlRoomBooking" runat="server" Visible="false" Class="plain-table table-left" style="margin:20px 0px">
    <ContentTemplate>
        <asp:Repeater runat="server" ID="roomBookingRepeater" OnItemDataBound="roomBookingRepeater_ItemDataBound">
            <HeaderTemplate>
                <table>
                    <thead>
                        <tr>
                            <th>Start Time</th>
                            <th>End Time</th>
                            <th>Meeting Title</th>
                            <th>Assigned Room</th>
                            <th>Floor</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                        <tr>
                            <td><%# Eval("StartTime") %></td>
                            <td><%# Eval("EndTime") %></td>
                            <td><%# Eval("MeetingTitle") %></td>
                            <td><span class="room"><%# Eval("AssignedRoom") %></span></td>
                            <td><%--<%# Eval("RoomType") %>--%><span class="floor"></span></td>
                        </tr>
            </ItemTemplate>
            <FooterTemplate>
                        <tr id="no_items" runat="server" visible="false">
                            <td colspan="5">No upcoming Room Bookings.</td> 
                        </tr>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div style="display: none"><asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

<script type="text/javascript">
    $(function () {
        function refreshTable(avoidRefresh) {
            var btnRefresh = "#<%= btnRefresh.ClientID %>";
            var refreshInterval = <%= RefreshInterval %>;

            // put default of 15 minutes
            if(!refreshInterval) refreshInterval = 15;

            refreshInterval *= 60 * 1000;

            if(!avoidRefresh) {
                $(btnRefresh).click();
                checkFloor();
            }

            setTimeout(refreshTable, refreshInterval)
        }
        refreshTable(true);
    });
    function checkFloor() {
        $(".room").each(function() {
            //if($(this).text() ==  "Purple" || $(this).text() ==  "Red" || $(this).text() ==  "Green" || $(this).text() ==  "Gold"){
            if($(this).text().includes("Purple") || $(this).text().includes("Red") || $(this).text().includes("Gold") || $(this).text().includes("Green")){
                $(this).parent().next('td').children('.floor').text("-1");
                $(this).parent().next('td').children('.floor').addClass("floor-1");
            }
            else if($(this).text().includes("Lambay") || $(this).text().includes("Rathlin") || $(this).text().includes("Tory") || $(this).text().includes("Valentia") || $(this).text().includes("Aran") || $(this).text().includes("Gola")){
                $(this).parent().next('td').children('.floor').text("1");
                $(this).parent().next('td').children('.floor').addClass("floor1");
            }
            else if($(this).text().includes("Achill") || $(this).text().includes("Blaskett") || $(this).text().includes("Leinster") || $(this).text().includes("Munster") || $(this).text().includes("Western") || $(this).text().includes("Ulster")){
                $(this).parent().next('td').children('.floor').text("2");
                $(this).parent().next('td').children('.floor').addClass("floor2");
            }
            else if($(this).text().includes("Main BoardRoom")){
                $(this).parent().next('td').children('.floor').text("4");
                $(this).parent().next('td').children('.floor').addClass("floor4");
            }
        });
    }
    checkFloor();
    function pageLoad() {		
        var delay=10;
        setTimeout(function() {
            checkFloor();
        }, delay);
    };
</script>
