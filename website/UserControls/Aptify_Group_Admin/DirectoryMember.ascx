<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DirectoryMember.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.DirectoryMember" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   <div style=" float:left;">
        <table id="tblmember" runat="server" width="100%" class="data-form">
            <tr>
                <td colspan="2">
                    <rad:RadGrid ID="grdmember" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                         PagerStyle-PageSizeLabelText="Records Per Page" 
                        AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" OnItemCreated="grdmember_GridItemCreated">
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                                Visible="True">
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                Visible="True">
                            </ExpandCollapseColumn>
                            <Columns>
                              <%--  Amruta,Issue 14878,03/11/2013,Added UniqueName Property to acess required column when redirect on this control from group admin dashborad--%>
                                <rad:GridBoundColumn HeaderText="ID" DataField="ID" Visible="False" ItemStyle-CssClass="IdWidth " FilterControlWidth="70px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" SortExpression="ID" HeaderStyle-Width="10%" AutoPostBackOnFilter="true" UniqueName="ID">
                                    <ItemStyle CssClass="IdWidth" />
                                </rad:GridBoundColumn>
                                <rad:GridTemplateColumn HeaderText="Member" DataField="FirstLast" SortExpression="FirstLast"
                                    AutoPostBackOnFilter="true" FilterControlWidth="110px" CurrentFilterFunction="Contains" 
                                    ShowFilterIcon="false" UniqueName="DirectoryMemberName">
                                    <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign" Width="15%" ></ItemStyle>
                                    <ItemTemplate>
                                        <table>
                                            <tr> 
                                                <td style="margin-left: 73px;">
                                                <%-- Neha,issue 16001,5/07/13, added css for image heightwidth and allignment of Name,Title,Adderess--%>
                                               <div class="imgmember">
                                                <%-- Neha,issue 14810,03/09/13,added Radbinaryimage --%>
                                                     <rad:RadBinaryImage ID="imgmember" CssClass="PeopleImage" runat="server"  AutoAdjustImageControlSize="false"/>
                                                     </div>
                                                </td>
                                                <td class="memberListtd">
                                                    <asp:HyperLink ID="lblMember" CssClass="namelink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstLast") %>'
                                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminEditprofileUrl") %>'></asp:HyperLink><br />
                                                    <asp:Label ID="lblMemberTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"title") %>'></asp:Label><br />
                                                    <asp:Label ID="lbladdress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"address") %>'> </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                 <rad:GridTemplateColumn HeaderText="Member" DataField="FirstLast" SortExpression="FirstLast"
                                    AutoPostBackOnFilter="true" FilterControlWidth="110px" CurrentFilterFunction="Contains" 
                                    ShowFilterIcon="false" Visible="false" UniqueName="MemberName">
                                    <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign tdVerticalAlignMiddle" Width="15%" ></ItemStyle>
                                    <ItemTemplate>
                                      <asp:HyperLink ID="lblMemberName" CssClass="namelink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstLast") %>'
                                           NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminEditprofileUrl") %>'></asp:HyperLink><br />
                                    </ItemTemplate>
                                  </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Email" DataField="Email" SortExpression="Email"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    FilterControlWidth="110px">
                                    <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign Emailstyle tdVerticalAlignMiddle" Width="15%">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" HeaderText="Email" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'> </asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridBoundColumn HeaderText="Membership Type" DataField="MemberType" FilterControlWidth="90px"
                                    ItemStyle-CssClass="LeftAlign " SortExpression="MemberType" 
                                    AutoPostBackOnFilter="true" ItemStyle-Width="10%"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="MemberShipType" >
                                    <ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle" Width="10%" />
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn HeaderText="Start Date" DataField="JoinDate" ItemStyle-Width="10%"
                                    FilterControlWidth="70px" ItemStyle-CssClass="LeftAlign" SortExpression="JoinDate"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                                    ShowFilterIcon="false" UniqueName="StartDate" >
                                    <ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle" Width="10%" />
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn HeaderText="End Date" DataField="DuesPaidThru" ItemStyle-Width="10%"
                                    FilterControlWidth="70px" ItemStyle-CssClass="LeftAlign " SortExpression="DuesPaidThru"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                                    ShowFilterIcon="false" UniqueName="EndDate" >
                                    <ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle" Width="10%" />
                                </rad:GridBoundColumn>
                                <rad:GridTemplateColumn HeaderText="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="70px" 
                                    DataField="Status">
                                    <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign tdVerticalAlignMiddle" Width="10%"></ItemStyle>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <rad:RadBinaryImage ID="imgstaus" CssClass="imgstaus" runat="server" />
                                                </td>
                                                <td valign="top">
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'> </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Remove" AllowFiltering="false" UniqueName="Remove">
                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" CssClass="tdVerticalAlignMiddle">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPersonID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                            Visible="false"></asp:Label>
                                        <asp:CheckBox ID="chkRmvCompLink" runat="server" AutoPostBack="true" ToolTip="Remove Person From Company" />
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <PagerStyle PageSizeLabelText="Records Per Page" />
                        <FilterMenu EnableImageSprites="False">
                        </FilterMenu>
                    </rad:RadGrid>
                </td>
            </tr>
            <tr>
                <td style="width:80%">
                    <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                </td>
                <td style="width:20%" align="right">
                    <asp:Button ID="btnRmvCompLink" Visible="false" runat="server" Text="Remove From Company" CssClass="submitBtn" /> 
                </td>
            </tr>
        </table>
        </div>
        <rad:RadWindow ID="radConfirm" runat="server" Width="570px" Height="100px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                    height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblConfirm" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnConfirm" runat="server" Text="Yes" Width="50px" class="submitBtn"
                                OnClick="btnConfirm_Click" ValidationGroup="ok" />&nbsp;
                            <asp:Button ID="btnNo" runat="server" Text="No" Width="50px" class="submitBtn" OnClick="btnNo_Click"
                                ValidationGroup="ok" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>

        <rad:RadWindow ID="radRCValidation" runat="server" Width="400px" Height="100px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                    height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblRCValidation" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnradRCValidation" runat="server" Text="OK" Width="50px" class="submitBtn" OnClick="btnNo_Click"
                                ValidationGroup="ok" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
    </ContentTemplate>
</asp:UpdatePanel>
<cc2:User ID="User1" runat="server"></cc2:User>
