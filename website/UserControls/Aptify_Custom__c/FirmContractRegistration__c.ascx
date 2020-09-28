<%--Aptify e-Business 5.5.1 SR1, June 2014--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FirmContractRegistration__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.FirmContractRegistration__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="table-div">
    <div>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
    </div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="True">
                <ContentTemplate>
                    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div runat="server" id="divDetails">
            <div class="row-div">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <div class="row-div top-margin clearfix">
                            <div class="label-div1">
                                Start Date:
                            </div>
                            <div class="field-div2">
                                <telerik:RadDatePicker ID="txtStartDate" runat="server">
                                    <DatePopupButton ToolTip="" />
                                </telerik:RadDatePicker>
                                <telerik:RadToolTip ID="RadToolTipStartDate" runat="server" IsClientID="true" Text=""
                                    AutoCloseDelay="20000" />
                            </div>
                            <div class="label-div1">
                                Office Location:
                            </div>
                            <div class="field-div1">
                                <asp:DropDownList runat="server" ID="cmbOfficeLocation" Width="90px" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                            <div class="label-div1">
                                <asp:HyperLink ID="lnkMasterSchedule" runat="server">Master Schedule</asp:HyperLink>
                            </div>
                            <div class="field-div2">
                                <asp:DropDownList runat="server" ID="cmbMasterSchedule"  Width="90px">
                                </asp:DropDownList>
                            </div>
                            <div class="label-div1">
                                <asp:Button runat="server" ID="btnSet" CssClass="submitBtn" Width="70px" Text="Set" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSet" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="row-div">
                <div class="row-div top-margin clearfix">
                    <div class="label-div1">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:CheckBox ID="chkActive" Text="Display only Active Registrations" AutoPostBack="true"
                                    runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chkActive" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div>
                <asp:UpdatePanel ID="UppanelGrid" runat="server">
                    <ContentTemplate>
                        <telerik:RadGrid ID="grdStudentDetails" runat="server" AllowPaging="false" AllowSorting="True"
                            PageSize="10" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None"
                            AutoGenerateColumns="false" Width="600px" Visible="true" Style="margin-top: 13px;
                            overflow: auto" Height="500px">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                                </Scrolling>
                            </ClientSettings>
                            <MasterTableView ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false" ShowFilterIcon="false"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllStudent" runat="server" AutoPostBack="True" OnCheckedChanged="ToggleSelectedState" />
                                            <%-- OnCheckedChanged="ToggleSelectedState"--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkStudent" runat="server" />
                                            <asp:HiddenField ID="hidECID" Value='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                runat="server" />
                                            <asp:HiddenField ID="hidAttributeValue" Value='<%# DataBinder.Eval(Container.DataItem,"AttributeValue") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Student Number" DataField="OldID" SortExpression=""
                                        AutoPostBackOnFilter="true" FilterControlWidth="100%" DataType="System.String"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                        <ItemTemplate>
                                             <asp:Label ID="lnkStudentNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"OldID") %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hidStudentNo" Value='<%# DataBinder.Eval(Container.DataItem,"StudentNo") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="First Name" ShowFilterIcon="false" DataField="FirstName"
                                        SortExpression="FirstName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Last Name" DataField="LastName" SortExpression="LastName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Country" HeaderText="Student Country of origin"
                                        AllowFiltering="true" SortExpression="Country" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn HeaderText="Office Where Student Will Train" DataField=""
                                        SortExpression="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbTrainOffice" runat="server">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hidCompany" Value='<%# DataBinder.Eval(Container.DataItem,"CompanyID") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Yrs.Of Exp. Required" AllowFiltering="false"
                                        DataField="YearsOfExperience" SortExpression="YearsOfExperience" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblyrsOfExp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"YearsOfExperience") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Start Date" AllowFiltering="false" DataField="ContractStartDate"
                                        SortExpression="ContractStartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <telerik:RadDatePicker ID="txtGvStartDate" AutoPostBack="true" OnSelectedDateChanged="txtGvStartDate_OnSelectedDateChanged"
                                                SelectedDate='<%# If((Eval("ContractStartDate") IsNot Nothing AndAlso TypeOf Eval("ContractStartDate") Is DateTime), Convert.ToDateTime(Eval("ContractStartDate")), CType(Nothing, System.Nullable(Of DateTime))) %>'
                                                runat="server">
                                                <DatePopupButton ToolTip="" />
                                            </telerik:RadDatePicker>
                                            <telerik:RadToolTip ID="RadToolTipGv" runat="server" IsClientID="true" Text="" AutoCloseDelay="20000" />
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="End Date" AllowFiltering="false" DataField="ContractExpireDate"
                                        SortExpression="ContractExpireDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContractExpireDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractExpireDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Registration Status" DataField="Status" SortExpression="Status"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Master Schedule" DataField="MasterSchedule"
                                        SortExpression="MasterSchedule" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbGvMasterSchedule" Visible="false" runat="server">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblMasterSchedule" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MasterSchedule") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Type" DataField="Type" SortExpression="TYPE"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div>
            <br />
        </div>
        <div style="text-align: right;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="True">
                <ContentTemplate>
                    <asp:Button runat="server" ID="btnSave" CssClass="submitBtn" Text="Submit" />
                    <div>
                        <telerik:RadWindow ID="radMockTrial" runat="server" Width="350px" Modal="True" BackColor="#f4f3f1"
                            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Firm Contract Registration"
                            Behavior="None" Height="150px">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                                    padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblWarning" runat="server" Font-Bold="true" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div>
                                                <br />
                                            </div>
                                            <div>
                                                <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                                <asp:Button ID="btnOkSet" Visible="false" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </telerik:RadWindow>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>
            <cc1:User runat="server" ID="User1" />
        </div>
    </telerik:RadAjaxPanel>
</div>
