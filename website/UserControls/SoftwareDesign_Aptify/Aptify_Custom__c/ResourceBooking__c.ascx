<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/ResourceBooking__c.ascx.vb"
    Inherits="ResourceBooking__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .auto-style1
    {
        width: 100%;
    }
</style>
<div class="content-container clearfix">
    <table runat="server" id="tblMain" class="cai-table mobile-table" width="100%">
        <tr>
            <td>
                <table id="tblDropdown" runat="server" class="auto-style1 cai-table">
                    <tr>
                        <td align="right" style="text-align: right;">
                            <span class="mobile-label">Resource:</span>
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" height="40px" Text="Resource :  " CssClass="cai-table-data no-mob"></asp:Label>
                        </td>
                        <td align="left" style="text-align: left;">
                            <asp:DropDownList ID="cmbResource" runat="server" Width="230px" Height="40px" AutoPostBack="true" CssClass="cai-table-data">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:DropDownList runat="server" Width="150px" ID="cmbStatus" AutoPostBack="true" Height="40px" CssClass="cai-table-data">
                                <asp:ListItem Text="Upcoming"></asp:ListItem>
                                <asp:ListItem Text="Past"></asp:ListItem>                                
                                <asp:ListItem Text="All"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
<tr>
                     <td align="right" style="text-align: right;">
                         <span class="mobile-label">Start date:</span>
                            <asp:Label ID="lblstartDate" runat="server" Font-Bold="true" Text="Start date :  " CssClass="cai-table-data no-mob"></asp:Label>
                        </td>
                         <td align="left" style="text-align: left;">
                          <telerik:RadDatePicker CssClass="rcCalPopup cai-table-data" ID="txtstartdate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                MinDate="01/01/1777" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                            </telerik:RadDatePicker>
                             <span class="mobile-label">End date:</span>
                          <asp:Label ID="lblEndDate" runat="server" Font-Bold="true" Text="End date :  " CssClass="cai-table-data no-mob"></asp:Label>
                           <telerik:RadDatePicker CssClass="rcCalPopup cai-table-data" ID="txtenddate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                MinDate="01/01/1777" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                            </telerik:RadDatePicker>
                            <asp:Button ID="btnGo" runat="server" Text="Search" CssClass="submitBtn" />
                         </td>
                         
                    </tr>
<tr><td>&nbsp;</td> </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <rad:RadGrid ID="grdResourceBooking" AutoGenerateColumns="False" runat="server" SortingSettings-SortedDescToolTip="Sorted Descending"
                    SortingSettings-SortedAscToolTip="Sorted Ascending" PageSize="5"  AllowFilteringByColumn="true"
                    PagerStyle-PageSizeLabelText="Records Per Page" CssClass="cai-table mobile-table"><%--Skin="Sunset"--%>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowSorting="true" AllowNaturalSort="false" DataKeyNames="ID" EnableNoRecordsTemplate="true" AllowFilteringByColumn="false"
                        ShowHeadersWhenNoRecords="false" >
                        <NoRecordsTemplate>
                            <div>
                                No data to display
                            </div>
                        </NoRecordsTemplate>
                        <Columns>
                        <rad:GridTemplateColumn HeaderText="Event date" HeaderStyle-CssClass="no-mob" DataField="EventDateTime" SortExpression="EventDateTime" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <span class="mobile-label">Event date:</span>
                                    <asp:Label ID="lblEventDateTime" runat="server" Text='<%# Eval("EventDateTime") %>' CssClass="cai-table-data no-mob" Width="50px"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("EventDateTime") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Requested time" HeaderStyle-CssClass="no-mob" DataField="RequestedTime" SortExpression="RequestedTime" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70" />
                                <ItemTemplate>
                                    <span class="mobile-label">Requested time:</span>
                                    <asp:Label ID="lblRequestedTime" runat="server" Text='<%# Eval("RequestedTime") %>' CssClass="cai-table-data no-mob" Width="70"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("RequestedTime") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Resource name" HeaderStyle-CssClass="no-mob" DataField="ResourceName" SortExpression="ResourceName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="80" />
                                <ItemTemplate>
                                    <span class="mobile-label">Resource name:</span>
                                    <asp:Label ID="lblResourceName" runat="server" Text='<%# Eval("ResourceName") %>' CssClass="cai-table-data no-mob" Width="80"></asp:Label>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ResourceName") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                             <rad:GridTemplateColumn HeaderText="Quantity" HeaderStyle-CssClass="no-mob" DataField="Quantity"
                                SortExpression="Quantity" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <span class="mobile-label">Quantity:</span>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' CssClass="cai-table-data no-mob" Width="50"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("Quantity") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                             <rad:GridTemplateColumn HeaderText="Comments" HeaderStyle-CssClass="no-mob" DataField="Comments" SortExpression="Comments" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="90" />
                                <ItemTemplate>
                                    <span class="mobile-label">Comments:</span>
                                    <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>' CssClass="cai-table-data no-mob" Width="90"></asp:Label>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("Comments") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Room" HeaderStyle-CssClass="no-mob" DataField="Room" SortExpression="Room" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70"  />
                                <ItemTemplate>
                                    <span class="mobile-label">Room:</span>
                                    <asp:Label ID="lblRoom" runat="server" Text='<%# Eval("Room") %>' CssClass="cai-table-data no-mob" Width="70"></asp:Label>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("Room") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                             <rad:GridTemplateColumn HeaderText="Status" HeaderStyle-CssClass="no-mob" DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' CssClass="cai-table-data no-mob"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                           
                              <rad:GridTemplateColumn HeaderText="Resource status" HeaderStyle-CssClass="no-mob" DataField="ResourceStatus" SortExpression="ResourceStatus" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <span class="mobile-label">Resource status:</span>
                                    <asp:Label ID="lblRecordStatus" runat="server" Text='<%# Eval("ResourceStatus") %>' CssClass="cai-table-data no-mob" Width="50"></asp:Label>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("ResourceStatus") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Room booking type" HeaderStyle-CssClass="no-mob" DataField="RoomBookingType" SortExpression="RoomBookingType" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <span class="mobile-label">Room booking type:</span>
                                    <asp:Label ID="lblRoomBookingType" runat="server" Text='<%# Eval("RoomBookingType") %>' CssClass="cai-table-data no-mob"></asp:Label>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("RoomBookingType") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>

                            <rad:GridTemplateColumn HeaderText="Requester" HeaderStyle-CssClass="no-mob" DataField="Requester" SortExpression="Requester" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70" />
                                <ItemTemplate>
                                    <span class="mobile-label">Requester:</span>
                                    <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("Requester") %>' CssClass="cai-table-data no-mob" Width="70"></asp:Label>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("Requester") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            
                             <rad:GridTemplateColumn HeaderText="Primary Company" HeaderStyle-CssClass="no-mob" DataField="PrimaryCompany" SortExpression="PrimaryCompany" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPrimaryCompany" runat="server" Text='<%# Eval("PrimaryCompany") %>' CssClass="cai-table-data no-mob"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>

                             <rad:GridTemplateColumn HeaderText="Primary Address" HeaderStyle-CssClass="no-mob" DataField="PrimaryAddress" SortExpression="PrimaryAddress" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPrimaryAddress" runat="server" Text='<%# Eval("PrimaryAddress") %>' CssClass="cai-table-data no-mob"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>

                             <rad:GridTemplateColumn HeaderText="Booking reference no" HeaderStyle-CssClass="no-mob" DataField="BookingReferenceNo" SortExpression="BookingReferenceNo" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <span class="mobile-label">Booking reference no:</span>
                                    <asp:Label ID="lblBookingReferenceNo" runat="server" Text='<%# Eval("BookingReferenceNo") %>' CssClass="cai-table-data no-mob" Width="50"></asp:Label>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("BookingReferenceNo") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            
                            <rad:GridTemplateColumn HeaderText="Meeting title" HeaderStyle-CssClass="no-mob" DataField="MeetingTitle" SortExpression="MeetingTitle" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="80" />
                                <ItemTemplate>
                                    <span class="mobile-label">Meeting title:</span>
                                    <asp:Label ID="lblMeetingTitle" runat="server" Text='<%# Eval("MeetingTitle") %>' CssClass="cai-table-data no-mob" Width="80"></asp:Label>
                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("MeetingTitle") %>' CssClass="cai-table-data no-desktop"></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
     
                        </Columns>
                    </MasterTableView>
                     <ExportSettings ExportOnlyData="true" >
                                <Excel Format="Biff"/>
                             </ExportSettings>
                </rad:RadGrid>
            </td>
        </tr>
 <tr>
                    <td style="text-align: right;" colspan="2">
                         <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="submitBtn" />
                    </td>
                </tr>
    </table>
</div>
<cc1:User ID="User1" runat="server" />
