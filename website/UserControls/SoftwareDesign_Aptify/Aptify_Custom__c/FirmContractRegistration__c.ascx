<%--Aptify e-Business 5.5.1 SR1, June 2014--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FirmContractRegistration__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.FirmContractRegistration__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="table-div">
    <div class="raDiv" style="overflow: visible;">
        <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div class="dvProcessing">
                    <img src="/Images/CAITheme/bx_loader.gif" />
                    <span>Loading</span>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
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
        <%-- below div added and relocation control for Redmine #20351 --%>
			  <div class="row-div">
                <div class="row-div top-margin clearfix">
                    <div class="label-title">
                        Filter by registration status:
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlRegStatus" runat="server" AutoPostBack="true" Width="50%">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlRegStatus" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <%--added blank div for space for Redmine #20351, removed blank div--%>
                 <%--   <div class="row-div">
						&nbsp;
					</div>--%>
                    <%--Blank div end --%>
					 <div class="label-title">
                       Filter by office location:
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="True">
                            <ContentTemplate>
                               <asp:DropDownList runat="server" ID="cmbOfficeLocation" AutoPostBack="true" Width="50%">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cmbOfficeLocation" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
               <%--added blank div for space for Redmine #20351--%>
                    <div class="row-div">
						<hr style="border-width:thick"/><%--added line for space for Redmine #20351/Updated border style #20351--%>
					</div>
                    <%--Blank div end --%>
            <div class="row-div">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <div class="row-div top-margin clearfix">
                            <div class="label-title">
                                Start date:
                            </div>
                            <div>
                                <telerik:RadDatePicker ID="txtStartDate" runat="server">
                                    <DatePopupButton ToolTip="" />
                                </telerik:RadDatePicker>
                                <telerik:RadToolTip ID="RadToolTipStartDate" runat="server" IsClientID="true" Text=""
                                    AutoCloseDelay="20000" />
                            </div>
                            <%--below div commented by GM for relocation as per Redmine #20351--%>
                           <%-- <div>
                                Office location:
                            </div>
                            <div>
                                <asp:DropDownList runat="server" ID="cmbOfficeLocation" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>--%>
                            <div class="label-title">
                                <asp:HyperLink ID="lnkMasterSchedule" runat="server" Font-Bold="true">Contract registration schedule</asp:HyperLink><%--updated text for Redmine #20351--%>
                            </div>
                            <div>
                                <asp:DropDownList runat="server" ID="cmbMasterSchedule" Width="16.5%"><%--updated text for Redmine #20351/updated width #20351--%>
                                </asp:DropDownList>

                            </div>
                            <div class="actions">
                                <asp:Button runat="server" ID="btnSet" CssClass="submitBtn" Text="Apply" /><%--updated text for Redmine #20351--%>

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
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:CheckBox ID="chkActive" Text="Display only active registrations" AutoPostBack="true"
                                    runat="server" Visible="false" /><%--visible false for Redmine #20351--%>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chkActive" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div>
                <br />
            </div>
            <%--below div commented by GM for relocation as per Redmine #20351--%>
           <%-- <div class="row-div">
                <div class="row-div top-margin clearfix">
                    <div>
                        Filter by registration status
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlRegStatus" runat="server" AutoPostBack="true" Width="50%">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlRegStatus" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>--%>
            <div style="width: 1400px">
                <asp:UpdatePanel ID="UppanelGrid" runat="server">
                    <ContentTemplate>
                        <%-- Repositioned the Submit button by Sheela as part of Task #18788/Commented below div as per Redmine #20351 --%>
		               <%-- <div class="actions" >
		                    <asp:Button ID="btnSave" runat="server" CssClass="submitBtn" Text="Submit" style="float:right" />
                    	            <asp:Label ID="lblsubmit" runat="server" style="float:right;"></asp:Label>
                                </div>--%>
		                <br />
		                <br />
                        <telerik:RadGrid ID="grdStudentDetails" runat="server" AllowPaging="false" AllowSorting="True"
                            PageSize="10" AllowFilteringByColumn="false" CellSpacing="0" GridLines="None"
                            AutoGenerateColumns="false" CssClass="cai-table mobile-table">

                            <MasterTableView ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false" ShowFilterIcon="false">
                                        <HeaderTemplate>

                                            <asp:CheckBox ID="chkAllStudent" CssClass="cai-table-data" runat="server" AutoPostBack="True" OnCheckedChanged="ToggleSelectedState" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <span class="mobile-label">Checkbox:</span>
                                            <asp:CheckBox ID="chkStudent" runat="server" CssClass="cai-table-data" />
                                            <asp:HiddenField ID="hidECID" Value='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                runat="server" />
                                            <asp:HiddenField ID="hidAttributeValue" Value='<%# DataBinder.Eval(Container.DataItem,"AttributeValue") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Student number" DataField="OldID" SortExpression=""
                                        AutoPostBackOnFilter="true" FilterControlWidth="100%" DataType="System.String"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AllowFiltering="false"> <%--added AllowFiltering="false" for Redmine #20351--%>
                                        <ItemTemplate>
                                            <span class="mobile-label">Student#:</span>
                                            <asp:Label ID="lnkStudentNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"OldID") %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hidStudentNo" Value='<%# DataBinder.Eval(Container.DataItem,"StudentNo") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="First name" ShowFilterIcon="false" DataField="FirstName"
                                        SortExpression="FirstName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" AllowFiltering="false"> <%--added AllowFiltering="false" for Redmine #20351--%>
                                        <ItemTemplate>
                                            <span class="mobile-label">First name:</span>
                                            <asp:Label ID="lblFirstName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Last name" DataField="LastName" SortExpression="LastName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"> <%--added AllowFiltering="false" for Redmine #20351--%>
                                        <ItemTemplate>
                                            <span class="mobile-label">Last name:</span>
                                            <asp:Label ID="lblLastName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="Country" HeaderText="Student country of origin"
                                        AllowFiltering="false" SortExpression="Country" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">  <%--added AllowFiltering="false" for Redmine #20351--%>
                                        <ItemTemplate>
                                            <span class="mobile-label">Student country of origin:</span>
                                            <span class="cai-table-data"><%# DataBinder.Eval(Container.DataItem,"Country") %></span>
                                            <asp:HiddenField ID="hidCompany" Value='<%# DataBinder.Eval(Container.DataItem,"CompanyID") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Office where student will train" DataField=""
                                        SortExpression="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AllowFiltering="false"> 
                                        <ItemTemplate>
                                            <span class="mobile-label">Office where student will train:</span>
                                            <asp:DropDownList ID="cmbTrainOffice" runat="server" CssClass="cai-table-data">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Yrs.of exp. required" AllowFiltering="false"
                                        DataField="YearsOfExperience" SortExpression="YearsOfExperience" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Yrs.of exp. required:</span>
                                            <asp:Label ID="lblyrsOfExp" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"YearsOfExperience") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Start date" AllowFiltering="false" DataField="ContractStartDate"
                                        SortExpression="ContractStartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Start date:</span>
                                            <telerik:RadDatePicker CssClass="cai-table-data date" ID="txtGvStartDate" MinDate="01/01/1900" MaxDate="01/01/2900" AutoPostBack="true" OnSelectedDateChanged="txtGvStartDate_OnSelectedDateChanged"
                                                SelectedDate='<%# If((Eval("ContractStartDate") IsNot Nothing AndAlso TypeOf Eval("ContractStartDate") Is DateTime), Convert.ToDateTime(Eval("ContractStartDate")), CType(Nothing, System.Nullable(Of DateTime))) %>'
                                                runat="server">
                                                <DatePopupButton ToolTip="" />
                                            </telerik:RadDatePicker>
                                            <telerik:RadToolTip ID="RadToolTipGv" runat="server" IsClientID="true" Text="" AutoCloseDelay="20000" />
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="End date" AllowFiltering="false" DataField="ContractExpireDate"
                                        SortExpression="ContractExpireDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">End date:</span>
                                            <asp:Label ID="lblContractExpireDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractExpireDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
 <telerik:GridTemplateColumn HeaderText="Inactive date" AllowFiltering="false" DataField="InactiveDate"
                                        SortExpression="InactiveDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Inactive date:</span>
                                            <asp:Label ID="lblInactiveDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InactiveDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Registration status" DataField="Status" SortExpression="Status"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"> <%--added AllowFiltering="false" for Redmine #20351--%>
                                        <ItemTemplate>
                                            <span class="mobile-label">Registration status:</span>
                                            <asp:Label ID="lblStatus" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <%-- Updated Header Text #20351 --%>
                                    <telerik:GridTemplateColumn HeaderText="Contract registration schedule" DataField="MasterSchedule"
                                        SortExpression="MasterSchedule" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AllowFiltering="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Contract registration schedule:</span><%-- Updated Text #20351 --%>
                                             <asp:TextBox ID="cmbGvMasterSchedule" runat="server" Enabled="false"></asp:TextBox><%-- Updated'/commented Text #20634 --%>
											<%--<asp:DropDownList ID="cmbGvMasterSchedule" Visible="false" runat="server" CssClass="cai-table-data" Enabled="false" >
											</asp:DropDownList>--%><%-- Updated'/commented Text #20634 --%>
                                            <asp:Label ID="lblMasterSchedule" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"MasterSchedule") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Type" DataField="Type" SortExpression="TYPE"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        AllowFiltering="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Type:</span>
                                            <asp:Label CssClass="cai-table-data" ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <%-- added below div as per Redmine #20351 --%>
                        <div class="actions" >
		                    <asp:Button ID="btnSave" runat="server" CssClass="submitBtn" Text="Submit" style="float:right" />
                    	            <asp:Label ID="lblsubmit" runat="server" style="float:right;"></asp:Label>
                                </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>


            <div>
                <asp:UpdatePanel ID="UpApproveStudents" runat="server">
                    <ContentTemplate>
                        <telerik:RadGrid ID="grdApprovedStudents" runat="server" AllowPaging="false" AllowSorting="True"
                            PageSize="10" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None"
                            AutoGenerateColumns="false" CssClass="cai-table mobile-table">

                            <MasterTableView ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <%--added columns by GM for Redmine #20351--%>
								<telerik:GridTemplateColumn HeaderText="" DataField="OldID" SortExpression=""
										AutoPostBackOnFilter="true" FilterControlWidth="100%" DataType="System.String"
										CurrentFilterFunction="EqualTo" ShowFilterIcon="false"  >
										<ItemTemplate>
											<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
										</ItemTemplate>
										<ItemStyle />
									</telerik:GridTemplateColumn>
									<telerik:GridTemplateColumn HeaderText="" AllowFiltering="false" ShowFilterIcon="false">
										<HeaderTemplate>

											<asp:CheckBox ID="chkAllStudent" CssClass="cai-table-data" runat="server" AutoPostBack="True" OnCheckedChanged="ToggleSelectedState" Enabled="false" />
										</HeaderTemplate>
										<ItemTemplate>
											<span class="mobile-label">Checkbox:</span>
											<asp:CheckBox ID="chkStudent" runat="server" CssClass="cai-table-data" Enabled="false" />
											<asp:HiddenField ID="hidECID" Value='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
												runat="server" />
											<asp:HiddenField ID="hidAttributeValue" Value='<%# DataBinder.Eval(Container.DataItem, "AttributeValue") %>'
												runat="server" />
										</ItemTemplate>
									</telerik:GridTemplateColumn>
									<%--End Redmine #20351--%>
                                    <telerik:GridTemplateColumn HeaderText="Student number" DataField="OldID" SortExpression=""
                                        AutoPostBackOnFilter="true" FilterControlWidth="100%" DataType="System.String"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AllowFiltering="false"> <%--added AllowFiltering="false" for Redmine #20351--%>
                                        <ItemTemplate>
                                            <span class="mobile-label">Student#:</span>
                                            <asp:Label ID="lnkStudentNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"OldID") %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hidStudentNo" Value='<%# DataBinder.Eval(Container.DataItem,"StudentNo") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="First name" ShowFilterIcon="false" DataField="FirstName"
                                        SortExpression="FirstName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" AllowFiltering="false"> <%--added AllowFiltering="false" for Redmine #20351--%>
                                        <ItemTemplate>
                                            <span class="mobile-label">First name:</span>
                                            <asp:Label ID="lblFirstName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Last name" DataField="LastName" SortExpression="LastName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"> <%--added AllowFiltering="false" for Redmine #20351--%>
                                        <ItemTemplate>
                                            <span class="mobile-label">Last name:</span>
                                            <asp:Label ID="lblLastName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="Country" HeaderText="Student country of origin"
                                        AllowFiltering="false" SortExpression="Country" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">   <%--added AllowFiltering="false" for Redmine #20351--%>
                                        <ItemTemplate>
                                            <span class="mobile-label">Student country of origin:</span>
                                            <span class="cai-table-data"><%# DataBinder.Eval(Container.DataItem,"Country") %></span>
                                            <asp:HiddenField ID="hidCompany" Value='<%# DataBinder.Eval(Container.DataItem,"CompanyID") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Office where student will train" DataField=""
                                        SortExpression="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AllowFiltering="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Office where student will train:</span>
                                            <%-- <asp:DropDownList ID="cmbTrainOffice" runat="server" CssClass="cai-table-data">
                                            </asp:DropDownList>--%>
                                            <asp:Label ID="cmbTrainOffice" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "City")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Yrs.of exp. required" AllowFiltering="false"
                                        DataField="YearsOfExperience" SortExpression="YearsOfExperience" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Yrs.of exp. required:</span>
                                            <asp:Label ID="lblyrsOfExp" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"YearsOfExperience") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Start date" AllowFiltering="false" DataField="ContractStartDate"
                                        SortExpression="ContractStartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Start date:</span>
                                            <%--<telerik:RadDatePicker CssClass="cai-table-data date" ID="txtGvStartDate" MinDate="01/01/1900" MaxDate="01/01/2900" AutoPostBack="true" OnSelectedDateChanged="txtGvStartDate_OnSelectedDateChanged"
                                                SelectedDate='<%# If((Eval("ContractStartDate") IsNot Nothing AndAlso TypeOf Eval("ContractStartDate") Is DateTime), Convert.ToDateTime(Eval("ContractStartDate")), CType(Nothing, System.Nullable(Of DateTime))) %>'
                                                runat="server">
                                                <DatePopupButton ToolTip="" />
                                            </telerik:RadDatePicker>
                                            <telerik:RadToolTip ID="RadToolTipGv" runat="server" IsClientID="true" Text="" AutoCloseDelay="20000" />--%>
                                            <asp:Label ID="txtGvStartDate" runat="server" CssClass="cai-table-data" Text='<%# If((Eval("ContractStartDate") IsNot Nothing AndAlso TypeOf Eval("ContractStartDate") Is DateTime), Convert.ToDateTime(Eval("ContractStartDate")), CType(Nothing, System.Nullable(Of DateTime))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="End date" AllowFiltering="false" DataField="ContractExpireDate"
                                        SortExpression="ContractExpireDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">End date:</span>
                                            <asp:Label ID="lblContractExpireDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractExpireDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
 <telerik:GridTemplateColumn HeaderText="Inactive date" AllowFiltering="false" DataField="InactiveDate"
                                        SortExpression="InactiveDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Inactive date:</span>
                                            <asp:Label ID="lblInactiveDate" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InactiveDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Registration status" DataField="Status" SortExpression="Status"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"> <%--added AllowFiltering="false" for Redmine #20351--%>
                                        <ItemTemplate>
                                            <span class="mobile-label">Registration status:</span>
                                            <asp:Label ID="lblStatus" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <%-- Updated Header Text #20351 --%>
                                    <telerik:GridTemplateColumn HeaderText="Contract registration schedule" DataField="MasterSchedule"
                                        SortExpression="MasterSchedule" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AllowFiltering="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Contract registration schedule:</span> <%-- Updated Text #20351 --%>
                                            <asp:TextBox ID="cmbGvMasterSchedule" runat="server" Enabled="false"></asp:TextBox><%-- Updated'/commented Text #20634 --%>
											<%--<asp:DropDownList ID="cmbGvMasterSchedule" Visible="false" runat="server" CssClass="cai-table-data" Enabled="false" >
											</asp:DropDownList>--%><%-- Updated'/commented Text #20634 --%>
                                            <asp:Label ID="lblMasterSchedule" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"MasterSchedule") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Type" DataField="Type" SortExpression="TYPE"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        AllowFiltering="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Type:</span>
                                            <asp:Label CssClass="cai-table-data" ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TYPE") %>'></asp:Label>
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
                <%-- Commented by Sheela as part of Task #18788 --%>
                    <%--<div class="actions">
                        <asp:Button runat="server" ID="btnSave" CssClass="submitBtn" Text="Submit" />
                    </div>--%>
                    <div>
                        <telerik:RadWindow ID="radMockTrial" runat="server" Width="350px" Modal="True" BackColor="#f4f3f1"
                            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Firm Contract Registration"
                            Behavior="None" Height="200px">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblWarning" runat="server" Font-Bold="true" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <br />
                                            </div>
                                            <div>
                                                <center>
                                                <asp:Button ID="btnOk" runat="server" Text="Ok" class="submitBtn" />
                                                <asp:Button ID="btnOkSet" Visible="false" runat="server" Text="Ok" class="submitBtn" />
						</center>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </telerik:RadWindow>
                    </div>
                      <%-- below div and nw rad window added by GM for Redmine #20634--%>
                    <div>
                        	<telerik:RadWindow ID="radConfirmation" runat="server" Width="400px" Modal="True" BackColor="#f4f3f1"
							VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Firm Contract Registration"
							Behavior="None" Height="200px">
							<ContentTemplate>
								<table>
									<tr>
										<td>
											<asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Would you like to complete the registration process?"></asp:Label>
										</td>
									</tr>
									<tr>
										<td>
											<div>
												<br />
											</div>
											<div>
												<center>
                                                <asp:Button ID="btnYes" runat="server" Text="YES" class="submitBtn" />
                                                <asp:Button ID="btnNo"  runat="server" Text="NO" class="submitBtn" />
						</center>
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
