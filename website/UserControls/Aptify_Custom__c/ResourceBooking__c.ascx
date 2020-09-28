<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ResourceBooking__c.ascx.vb" Debug="true"
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
    <table runat="server" id="tblMain" class="data-form" width="100%">
        <tr>
            <td>
                <table id="tblDropdown" runat="server" class="auto-style1">
                    <tr>
                        <td align="right" style="text-align: right;">
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Resource :  "></asp:Label>
                        </td>
                        <td align="left" style="text-align: left;">
                            <asp:DropDownList ID="cmbResource" runat="server" Width="150px" Height="26px" AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:DropDownList runat="server" ID="cmbStatus" AutoPostBack="true" Height="26px" >
                                <asp:ListItem Text="Upcoming"></asp:ListItem>
                                <asp:ListItem Text="Past"></asp:ListItem>                                
                                <asp:ListItem Text="All"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                     <td align="right" style="text-align: right;">
                            <asp:Label ID="lblstartDate" runat="server" Font-Bold="true" Text="Start Date :  "></asp:Label>
                        </td>
                         <td align="left" style="text-align: left;">
                          <telerik:RadDatePicker CssClass="rcCalPopup" ID="txtstartdate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                MinDate="01/01/1777" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                            </telerik:RadDatePicker>
                          <asp:Label ID="lblEndDate" runat="server" Font-Bold="true" Text="End Date :  "></asp:Label>
                           <telerik:RadDatePicker CssClass="rcCalPopup" ID="txtenddate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                MinDate="01/01/1777" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                            </telerik:RadDatePicker>
                            <asp:Button ID="btnGo" runat="server" Text="Search" CssClass="submitBtn" />
                         </td>
                         
                    </tr>
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
                    PagerStyle-PageSizeLabelText="Records Per Page"><%--Skin="Sunset"--%>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowSorting="true" AllowNaturalSort="false" DataKeyNames="ID" EnableNoRecordsTemplate="true" AllowFilteringByColumn="true"
                        ShowHeadersWhenNoRecords="false" >
                        <NoRecordsTemplate>
                            <div>
                                No Data to Display
                            </div>
                        </NoRecordsTemplate>
                        <Columns>
                        <rad:GridTemplateColumn HeaderText="Event Date" ItemStyle-Width="90px" DataField="EventDateTime" SortExpression="EventDateTime" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEventDateTime" runat="server" Text='<%# Eval("EventDateTime") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Requested Time" ItemStyle-Width="70px" DataField="RequestedTime" SortExpression="RequestedTime" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestedTime" runat="server" Text='<%# Eval("RequestedTime") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Resource Name" ItemStyle-Width="50px" DataField="ResourceName" SortExpression="ResourceName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <asp:Label ID="lblResourceName" runat="server" Text='<%# Eval("ResourceName") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                             <rad:GridTemplateColumn HeaderText="Quantity" ItemStyle-Width="70px" DataField="Quantity"
                                SortExpression="Quantity" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                             <rad:GridTemplateColumn HeaderText="Comments" ItemStyle-Width="70px" DataField="Comments" SortExpression="Comments" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70" />
                                <ItemTemplate>
                                    <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Room" ItemStyle-Width="70px" DataField="Room" SortExpression="Room" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70"  />
                                <ItemTemplate>
                                    <asp:Label ID="lblRoom" runat="server" Text='<%# Eval("Room") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                             <rad:GridTemplateColumn HeaderText="Status" ItemStyle-Width="70px" DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                           
                              <rad:GridTemplateColumn HeaderText="Resource Status" ItemStyle-Width="70px" DataField="ResourceStatus" SortExpression="ResourceStatus" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRecordStatus" runat="server" Text='<%# Eval("ResourceStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Room Booking Type" ItemStyle-Width="50px" DataField="RoomBookingType" SortExpression="RoomBookingType" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRoomBookingType" runat="server" Text='<%# Eval("RoomBookingType") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>

                            <rad:GridTemplateColumn HeaderText="Requester" ItemStyle-Width="50px" DataField="Requester" SortExpression="Requester" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("Requester") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            
                             <rad:GridTemplateColumn HeaderText="Primary Company" ItemStyle-Width="50px" DataField="PrimaryCompany" SortExpression="PrimaryCompany" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPrimaryCompany" runat="server" Text='<%# Eval("PrimaryCompany") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>

                             <rad:GridTemplateColumn HeaderText="Primary Address" ItemStyle-Width="50px" DataField="PrimaryAddress" SortExpression="PrimaryAddress" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPrimaryAddress" runat="server" Text='<%# Eval("PrimaryAddress") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>

                             <rad:GridTemplateColumn HeaderText="Booking Reference No" ItemStyle-Width="50px" DataField="BookingReferenceNo" SortExpression="BookingReferenceNo" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="50" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBookingReferenceNo" runat="server" Text='<%# Eval("BookingReferenceNo") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            
                            <rad:GridTemplateColumn HeaderText="Meeting Title" ItemStyle-Width="70px" DataField="MeetingTitle" SortExpression="MeetingTitle" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <HeaderStyle HorizontalAlign="Center" Width="70" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMeetingTitle" runat="server" Text='<%# Eval("MeetingTitle") %>'></asp:Label>
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
