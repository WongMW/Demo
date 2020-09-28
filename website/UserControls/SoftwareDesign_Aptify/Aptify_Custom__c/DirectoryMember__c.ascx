<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/DirectoryMember__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.DirectoryMember__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>



<!-- Bootstrap CSS -->
 <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet"/>
<!-- Jquery UI  CSS -->
<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<!-- jQuery and Bootstrap JS -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

<!--'BEGIN:loboJ updated for #19626  -->
<telerik:radcodeblock id="RadCodeBlock1" runat="server">
<script  type="text/javascript" >
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
        	var coid = $('#<%=cmbSubsidarisCompney.ClientID %>').val();
        	//alert(coid);
        	BindControls();
			function BindControls() {
										$("#ts").autocomplete({
											source: function (request, response) {
												$.ajax({
													url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetMemberNames") %>',
												    data: JSON.stringify({
												        slookup : request.term,
												        cid : coid
												    }),
													dataType: "json",
													type: "POST",
													contentType: "application/json; charset=utf-8",
													dataFilter: function (data) { return data; },
													success: function (data) {
														response($.map(data.d, function (item) {
															return { value: item }
														}))
													},
													error: function (XMLHttpRequest, textStatus, errorThrown) {
														alert("error" + errorThrown);
													}
												});
											},
											minLength: 1    // MINIMUM 1 CHARACTER TO START WITH.
										});
							}
	
						
        });
 </script>
<script type="text/javascript">
	$(document).ready(function () {
		BindControls();
		var coid = $('#<%=cmbSubsidarisCompney.ClientID %>').val();
		function BindControls() {
										$("#ts").autocomplete({
											source: function (request, response) {
												$.ajax({
													url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetMemberNames") %>',
												    data: JSON.stringify({
												        slookup : request.term,
												        cid : coid
												    }),
													dataType: "json",
													type: "POST",
													contentType: "application/json; charset=utf-8",
													dataFilter: function (data) { return data; },
													success: function (data) {
														response($.map(data.d, function (item) {
															return { value: item }
														}))
													},
													error: function (XMLHttpRequest, textStatus, errorThrown) {
														alert("error" + errorThrown);
													}
												});
											},
											minLength: 1    // MINIMUM 1 CHARACTER TO START WITH.
										});
							}
	
	});



	

		
	//});

						
</script>
</telerik:radcodeblock>
<!--'END:loboJ updated for #19626-->


<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1200px;">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<%-- Susan Wong 23/08/2017 #17887 start --%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" class="cai-table">
<%--Susan Wong 23/08/2017 #17887 end --%>
    <ContentTemplate>
        <div>
            <%-- Susan Wong 23/08/2017 #17887 start --%>
            <table id="tblmember" runat="server" width="100%" class="data-form company-directory">
            <%-- Susan Wong 23/08/2017 #17887 end --%>
                <tr>
                    <td>
                        <asp:DropDownList ID="cmbSubsidarisCompney" runat="server" Width="300px" AutoPostBack="true">
                        </asp:DropDownList>
						<%--Hid the Add Person button as part of #18494 --%> 
                        <asp:LinkButton ID="lnkAddPerson" runat="server" Visible="false">Add Persons</asp:LinkButton>
						</td>
                </tr>
<caption>
					<br />
					<%-- BEGIN:loboj 09/08/2018 #19626 --%>
					<tr>
						<td>
							<div class="form-search-box">
								<asp:TextBox ID="ts" runat="server" ClientIDMode="Static" class="form-control" palceholder ="Search by Name.." />
							
								<div id="divSearchbtn" class="collapse">
									<asp:Button ID="bsearch" runat="server" class="submitBtn" Text="Search" />
								</div>
							</div>

						</td>
					</tr>
					<%--END:Loboj 09/08/2018 #19626 --%>
					<tr>
						<td><%-- Susan Wong 23/08/2017 #17887 start --%>
							<br />
							<rad:RadGrid ID="grdmember" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellSpacing="0" CssClass="company-members" GridLines="None" OnItemCreated="grdmember_GridItemCreated" PagerStyle-PageSizeLabelText="Records Per Page">
								<PagerStyle CssClass="sd-pager" />
								<%-- Susan Wong 23/08/2017 #17887 end --%>
								<GroupingSettings CaseSensitive="false" />
								<MasterTableView>
									<CommandItemSettings ExportToPdfText="Export to PDF" />
									<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
									</RowIndicatorColumn>
									<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
									</ExpandCollapseColumn>
									<Columns>
										<%-- Susan Wong 23/08/2017 #17887 start --%><%--  Amruta,Issue 14878,03/11/2013,Added UniqueName Property to acess required column when redirect on this control from group admin dashborad--%>
										<rad:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataField="ID" FilterControlWidth="" HeaderStyle-Width="" HeaderText="ID" ItemStyle-CssClass="IdWidth " ShowFilterIcon="false" SortExpression="ID" UniqueName="ID" Visible="False">
											<%-- Susan Wong 23/08/2017 #17887 end --%>
											<ItemStyle CssClass="IdWidth" />
										</rad:GridBoundColumn>
										<%-- Susan Wong 23/08/2017 #17887 start --%>
										<rad:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="FirstLast" FilterControlWidth="" HeaderText="Staff member" ShowFilterIcon="true" SortExpression="FirstLast" UniqueName="DirectoryMemberName">
											<%-- Susan Wong 23/08/2017 #17887 end --%><%-- Susan Wong 23/08/2017 #17887 start --%>
											<ItemStyle CssClass="member-profile-td" HorizontalAlign="Center" Width="" />
											<ItemTemplate>
												<div class="imgmember">
													<asp:Label runat="server" CssClass="mobile-label no-desktop" Text="Profile Image:"></asp:Label>
													<rad:RadBinaryImage ID="imgmember" runat="server" AutoAdjustImageControlSize="false" CssClass="PeopleImage cai-table-data" />
												</div>
												<div class="member-profile-details">
													<asp:Label runat="server" CssClass="mobile-label member-name-label no-desktop" Text="Staff member:"></asp:Label>
													<asp:HyperLink ID="lblMember" runat="server" CssClass="namelink cai-table-data member-name-label" Text='<%# DataBinder.Eval(Container.DataItem, "FirstLast") %>'></asp:HyperLink><%--Remove hyperlink URL for Redmine #19626 NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "AdminEditprofileUrl") %>'--%>
													<asp:Label runat="server" CssClass="mobile-label no-desktop" Text="Order:"></asp:Label>
													<asp:Label ID="lblMemberTitle" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "title") %>'></asp:Label>
													<asp:Label runat="server" CssClass="mobile-label no-desktop" Text="Order:"></asp:Label>
													<asp:Label ID="lbladdress" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "address") %>'> </asp:Label>
												</div>
												<%-- Susan Wong 23/08/2017 #17887 end --%>
											</ItemTemplate>
										</rad:GridTemplateColumn>
										<%-- Susan Wong 23/08/2017 #17887 start --%>
										<rad:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="FirstLast" FilterControlWidth="" HeaderText="Member" ShowFilterIcon="false" SortExpression="FirstLast" UniqueName="MemberName" Visible="false">
											<ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle" HorizontalAlign="Center" Width="" />
											<%-- Susan Wong 23/08/2017 #17887 end --%>
											<ItemTemplate>
												<asp:HyperLink ID="lblMemberName" runat="server" CssClass="namelink" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "AdminEditprofileUrl") %>' Text='<%# DataBinder.Eval(Container.DataItem, "FirstLast") %>'></asp:HyperLink>
												<br />
											</ItemTemplate>
										</rad:GridTemplateColumn>
										<%-- Susan Wong 23/08/2017 #17887 start --%>
										<rad:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="Email" FilterControlWidth="" HeaderText="Email" ShowFilterIcon="false" SortExpression="Email">
											<ItemStyle CssClass="LeftAlign Emailstyle tdVerticalAlignMiddle" HorizontalAlign="Center" Width="" />
											<%-- Susan Wong 23/08/2017 #17887 end --%>
											<ItemTemplate>
												<%-- Susan Wong 24/08/2017 #17887 start --%>
												<asp:Label runat="server" CssClass="mobile-label no-desktop" Text="Email:"></asp:Label>
												<asp:Label ID="lblEmail" runat="server" CssClass="cai-table-data" HeaderText="Email" Text='<%# DataBinder.Eval(Container.DataItem, "Email") %>'> </asp:Label>
												<asp:Label runat="server" CssClass="mobile-label no-desktop" Text="Membership Type:"></asp:Label>
												<asp:Label ID="Label1" runat="server" CssClass="cai-table-data no-desktop" HeaderText="Email" Text='<%# DataBinder.Eval(Container.DataItem, "MemberType")%>'> </asp:Label>
												<asp:Label runat="server" CssClass="mobile-label no-desktop" Text="Start Date:"></asp:Label>
												<asp:Label ID="Label2" runat="server" CssClass="cai-table-data no-desktop" HeaderText="Email" Text='<%# DataBinder.Eval(Container.DataItem, "JoinDate")%>'> </asp:Label>
												<asp:Label runat="server" CssClass="mobile-label no-desktop" Text="End Date:"></asp:Label>
												<asp:Label ID="Label3" runat="server" CssClass="cai-table-data no-desktop" HeaderText="Email" Text='<%# DataBinder.Eval(Container.DataItem, "DuesPaidThru")%>'> </asp:Label>
												<%-- Susan Wong 24/08/2017 #17887 end --%>
											</ItemTemplate>
										</rad:GridTemplateColumn>
										<%-- Susan Wong 24/08/2017 #17887 start --%>
										<rad:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="MemberType" FilterControlWidth="" HeaderStyle-CssClass="no-mob" HeaderText="Membership Type" ItemStyle-CssClass="no-mob" ItemStyle-Width="" ShowFilterIcon="false" SortExpression="MemberType" UniqueName="MemberShipType">
											<ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle no-mob" />
											<%-- Susan Wong 24/08/2017 #17887 end --%>
										</rad:GridBoundColumn>
										<%-- Susan Wong 24/08/2017 #17887 start --%>
										<rad:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="JoinDate" FilterControlWidth="" HeaderStyle-CssClass="no-mob" HeaderText="Start Date" ItemStyle-CssClass="no-mob" ItemStyle-Width="" ShowFilterIcon="false" SortExpression="JoinDate" UniqueName="StartDate">
											<ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle no-mob" />
											<%-- Susan Wong 24/08/2017 #17887 end --%>
										</rad:GridBoundColumn>
										<%-- Susan Wong 24/08/2017 #17887 start --%>
										<rad:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="DuesPaidThru" FilterControlWidth="" HeaderStyle-CssClass="no-mob" HeaderText="End Date" ItemStyle-CssClass="no-mob" ItemStyle-Width="" ShowFilterIcon="false" SortExpression="DuesPaidThru" UniqueName="EndDate">
											<ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle no-mob" />
											<%-- Susan Wong 24/08/2017 #17887 end --%>
										</rad:GridBoundColumn>
										<%-- Susan Wong 24/08/2017 #17887 start --%>
										<rad:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="Status" FilterControlWidth="" HeaderText="Status" ShowFilterIcon="false" SortExpression="Status" Visible="false" >  <%--status visible false as per redmine #19626--%>
											<ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle" HorizontalAlign="Center" />
											<%-- Susan Wong 24/08/2017 #17887 end --%>
											<ItemTemplate>
												<%-- Susan Wong 24/08/2017 #17887 start --%>
												<rad:RadBinaryImage ID="imgstaus" runat="server" CssClass="imgstaus" />
												<asp:Label runat="server" CssClass="mobile-label no-desktop" Text="Status:"></asp:Label>
												<asp:Label ID="lblstatus" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'> </asp:Label>
												<%-- Susan Wong 24/08/2017 #17887 end --%>
											</ItemTemplate>
										</rad:GridTemplateColumn>
										<%-- Susan Wong 24/08/2017 #17887 start --%>
										<rad:GridTemplateColumn AllowFiltering="false" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" DataField="IsMentor__c" FilterControlWidth="" HeaderText="Make Mentor" ShowFilterIcon="false" SortExpression="IsMentor__c">
											<ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle" HorizontalAlign="Center" />
											<ItemTemplate>
												<%-- Added by Kavita ZInage #18296--%>
												<asp:CheckBox ID="chkIsMentor" runat="server" AutoPostBack="true" Checked='<%#IIf(Eval("IsMentor__c") = "1", True, False)%>' CssClass="chkBox" Enabled='<%#IIf(Eval("IsMentor__c") = 0, True, False)%>' OnCheckedChanged="OnCheckChangeEvent" />
												<%-- Commented by Kavita ZInage #18296--%><%-- <asp:Label runat="server" Text="Make mentor:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:Label ID="lblMakeMentor" HeaderText="Email" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"IsMentor__c") %>'> </asp:Label>--%>
											</ItemTemplate>
											<%-- Susan Wong 24/08/2017 #17887 end --%>
										</rad:GridTemplateColumn>
										<rad:GridTemplateColumn AllowFiltering="false" HeaderText="Remove" UniqueName="Remove" Visible="false">
											<ItemStyle CssClass="tdVerticalAlignMiddle" HorizontalAlign="Center" VerticalAlign="Middle" Width="" />
											<ItemTemplate>
												<asp:Label ID="lblPersonID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>' Visible="false"></asp:Label>
												<%-- Susan Wong 24/08/2017 #17887 start --%><%--<asp:Label runat="server" Text="Remove:" CssClass="mobile-label no-desktop"></asp:Label>--%>
												<asp:CheckBox ID="chkRmvCompLink" runat="server" AutoPostBack="true" CssClass="cai-table-data" ToolTip="Remove Person From Company" Visible="false" />
												<%-- Susan Wong 24/08/2017 #17887 end --%>
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
					<%-- Susan Wong 24/08/2017 #17887 start --%>
				</caption>
            </table>
            <div class="actions">

                <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                <asp:Button ID="btnRmvCompLink" Visible="false" runat="server" Text="Remove From Company" CssClass="submitBtn" />

            </div>
            <%-- Susan Wong 24/08/2017 #17887 end --%>
        </div>
        <rad:RadWindow ID="radConfirm" runat="server" Width="570px" Height="100px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
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
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
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
<script>
    document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_DirectoryMember__c_grdmember_ctl00_ctl02_ctl02_FilterTextBox_DirectoryMemberName").placeholder = 'Search staff member';
    document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_DirectoryMember__c_grdmember_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").placeholder = 'Search email';
    document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_DirectoryMember__c_grdmember_ctl00_ctl02_ctl02_FilterTextBox_MemberShipType").placeholder = 'Search member type';
    document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_DirectoryMember__c_grdmember_ctl00_ctl02_ctl02_FilterTextBox_StartDate").placeholder = 'Search date';
    document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_DirectoryMember__c_grdmember_ctl00_ctl02_ctl02_FilterTextBox_EndDate").placeholder = 'Search date';
    document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_DirectoryMember__c_grdmember_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn1").placeholder = 'Search status';
</script>
