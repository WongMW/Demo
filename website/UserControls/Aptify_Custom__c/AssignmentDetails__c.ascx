<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AssignmentDetails__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.AssignmentDetails__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        CheckGrid();
    });

    function CheckGrid() {
        $("[id*=chkAllStudent]").live("click", function () {
            var chkAllStudent = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkAllStudent.is(":checked")) {
                    $(this).attr("checked", "checked");
                    var td = $("td", $(this).closest("tr"));

                    $("[id*='txtScore']", td).attr('disabled', false);
                    $("[id*='ddlscore']", td).attr('disabled', false);
                } else {
                    $(this).removeAttr("checked");
                    var td = $("td", $(this).closest("tr"));

                    $("[id*='txtScore']", td).attr('disabled', true);
                    $("[id*='ddlscore']", td).attr('disabled', true);
                }
            });
        });
        $("[id*=chkSelect]").live("click", function () {
            var grid = $(this).closest("table");
            var chkAllStudent = $("[id*=chkAllStudent]", grid);
            if (!$(this).is(":checked")) {
                var td = $("td", $(this).closest("tr"));
                
                chkAllStudent.removeAttr("checked");
                $("[id*='txtScore']", td).attr('disabled', true);
                $("[id*='ddlscore']",td).attr('disabled', true);
            } else {
                var td = $("td", $(this).closest("tr"));

                $("[id*='txtScore']", td).attr('disabled', false);
                $("[id*='ddlscore']",td).attr('disabled', false);
                if ($("[id*=chkSelect]", grid).length == $("[id*=chkSelect]:checked", grid).length) {
                    chkAllStudent.attr("checked", "checked");
                }
            }
        });

    }
</script>--%>
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
<asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <%-- <script type="text/javascript">
        Sys.Application.add_load(CheckGrid);
    </script>--%>
        <div id="divContent" runat="server">
            <asp:Panel ID="pnlData" runat="server">
                <div class="info-data">
                    <asp:Label ID="lblExemptionsNotFound" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    <div class="row-div clearfix" align="right">
                        <asp:HyperLink ID="lnkHelp" Text="Help" runat="server" Target="_blank"></asp:HyperLink>
                    </div>
                    <div class="row-div clearfix" align="center">
                        <div class="row-div clearfix">
                            <div class="label-div w40" align="right">
                                <asp:Label ID="lblCurriculum" runat="server"><span class="RequiredField">*</span>Curriculum:</asp:Label>
                            </div>
                            <div class="field-div1 w50" align="left">
                                <asp:DropDownList ID="ddlCurriculumList" runat="server" AutoPostBack="true" Width="50%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w40" align="right">
                                <asp:Label ID="lblCourse" runat="server"><span class="RequiredField">*</span>Course:</asp:Label>
                            </div>
                            <div class="field-div1 w50" align="left">
                                <asp:DropDownList ID="ddlCourseList" runat="server" AutoPostBack="true" Width="50%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w40" align="right">
                                <asp:Label ID="lblAssignment" runat="server"><span class="RequiredField">*</span>Assignment Name:</asp:Label>
                            </div>
                            <div class="field-div1 w50" align="left">
                                <asp:DropDownList ID="ddlAssignmentList" runat="server" AutoPostBack="true" Width="50%">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row-div clearfix" align="center">
                        <asp:Button ID="btnDisplay" runat="server" Text="Display" Height="25px" />
                    </div>
                </div>
        </asp:Panel>
        <asp:Panel ID="pnlDetails" runat="server" Visible="true">
            <div class="row-div clearfix">
                <telerik:RadGrid ID="gvAssignmentDetails" runat="server" AllowPaging="false" AllowSorting="True"
                    AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AutoGenerateColumns="false"
                    Width="99%" Visible="true" Style="margin-top: 13px; overflow: auto" Height="200px">
                    <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                        </Scrolling>
                    </ClientSettings>
                    <MasterTableView AllowSorting="true" AllowNaturalSort="false" DataKeyNames="StudentID"
                        EnableNoRecordsTemplate="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true"
                        FilterItemStyle-HorizontalAlign="Center">
                        <NoRecordsTemplate>
                            <div>
                                No Data to Display
                            </div>
                        </NoRecordsTemplate>
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column"
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderStyle-Width="5%" HeaderText="" ItemStyle-HorizontalAlign="Left"
                                ShowFilterIcon="false" AllowFiltering="false">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAllStudent" runat="server" AutoPostBack="true" OnCheckedChanged="ToggleSelectedState" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                    <asp:HiddenField runat="server" ID="hdnPartStatusID" Value='<%# Eval("StudentID")%>' />
                                    
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                DataField="StudentID" FilterControlWidth="100%" HeaderStyle-Width="15%" HeaderText="Student#"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="StudentID"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="FirstName" FilterControlWidth="100%" HeaderStyle-Width="20%" HeaderText="First Name"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="FirstName"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="LastName" FilterControlWidth="100%" HeaderStyle-Width="20%" HeaderText="Last Name"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="LastName"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Lesson" FilterControlWidth="100%" HeaderStyle-Width="20%" HeaderText="Lesson"
                                ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Lesson"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="Type" FilterControlWidth="100%" HeaderStyle-Width="30%" HeaderText="Type"
                                ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false" SortExpression="Type"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="30%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderText="Score" ItemStyle-HorizontalAlign="Left"
                                ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtScore" runat="server" ReadOnly="true" Visible="true"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="REV1" ControlToValidate="txtScore" ValidationExpression="\d+"
                                        Display="Static" EnableClientScript="true" ErrorMessage="Please enter numbers only"
                                        ForeColor="Red" runat="server" />
                                    <asp:DropDownList ID="ddlscore" runat="server" Enabled="false" Visible="false"  Width="90%">
                                        <asp:ListItem>NA (Not addressed)</asp:ListItem>
                                        <asp:ListItem>NC (Nominal Competence)</asp:ListItem>
                                        <asp:ListItem>RC (Reaching Competence)</asp:ListItem>
                                        <asp:ListItem>C (Competent)</asp:ListItem>
                                        <asp:ListItem>HC (Highly Competent)</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid><br />
            </div>
            <div class="row-div clearfix" align="center">
                <asp:Button ID="cmdAdd" runat="server" Text="Add" Width="63px"></asp:Button>
            </div>
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="pnlAssignmnetDetails" runat="server" Visible="false">
            <div class="info-data">
                <div class="row-div clearfix">
                    <telerik:RadGrid ID="gvAssignment" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="10" AllowFilteringByColumn="False" CellSpacing="0" GridLines="None"
                        AutoGenerateColumns="false" Width="99%" Visible="true" Style="overflow: auto"
                        Height="100px">
                        <MasterTableView AllowSorting="true" AllowNaturalSort="false" DataKeyNames="StudentID"
                            EnableNoRecordsTemplate="true" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                            <NoRecordsTemplate>
                                <div>
                                    No Data to Display
                                </div>
                            </NoRecordsTemplate>
                            <Columns>
                                <rad:GridBoundColumn DataField="StudentID" HeaderStyle-Width="15%" HeaderText="Student#"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="StudentID"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="FirstName" HeaderStyle-Width="20%" HeaderText="First Name"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="FirstName"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="LastName" HeaderStyle-Width="20%" HeaderText="Last Name"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="LastName"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="Lesson" HeaderStyle-Width="20%" HeaderText="Lesson"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="Lesson"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="Type" HeaderStyle-Width="20%" HeaderText="Type" ItemStyle-HorizontalAlign="Left"
                                    ShowFilterIcon="false" SortExpression="Type" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </rad:GridBoundColumn>
                                <rad:GridBoundColumn DataField="score" HeaderStyle-Width="20%" HeaderText="Score"
                                    ItemStyle-HorizontalAlign="Left" ShowFilterIcon="false" SortExpression="score"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </rad:GridBoundColumn>
                                <rad:GridTemplateColumn HeaderStyle-Width="10%" HeaderText="Remove" ItemStyle-HorizontalAlign="Left"
                                    ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="Remove"
                                            CommandArgument='<%# Eval("ClassRegPartStatusID")%>' OnClick="btnRemove_Click"></asp:LinkButton>
                                              <asp:HiddenField runat="server" ID="hdnStudent" Value='<%# Eval("StudentID")%>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid><br />
                </div>
            </div>
            <div class="row-div clearfix" align="center">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Button ID="cmdSubmit" runat="server" Text="Submit"></asp:Button>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--<asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
    <ProgressTemplate>   
        <div class="modal">
            <div class="center">
                <img alt="Please Wait" src="../../Images/ajax-loader.gif" />
            </div>
        </div>                  
    </ProgressTemplate>
</asp:UpdateProgress> --%>

        </asp:Panel>
        &nbsp; </div>
        <div>
            <cc1:User ID="User1" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
