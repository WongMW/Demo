<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Customer_Service/DownloadControl.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.DownloadControl" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="EBusinessShoppingCart" Namespace="Aptify.Framework.Web.eBusiness"
    TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
<div class="alert alert-info">
    <p style="font-size:17px;"><strong>Info!</strong> Please, Before clicking on a course link, make sure you are enabled to use the pop-up in your current browser.</p>
  </div>
<asp:label id="e1" runat="server" forecolor="Red"></asp:label>
<div class="content-container clearfix" id="divTop" runat="server">
    <script language="javascript" type="text/javascript">
        function GetClientUTC() {
            var now = new Date()
            var offset = now.getTimezoneOffset();
            document.getElementById('<%= hdOffset.ClientID%>').value = offset
        }
    </script>
    <asp:hiddenfield id="hdOffset" runat="server" />
    <table id="Table1" width="100%" class="cai-table">
        <tbody>
            <tr>
                <td>
                    &nbsp; &nbsp;
                </td>
                <td>
                    <script language="javascript" type="text/javascript">
                        GetClientUTC();
                    </script>
                    <asp:label id="lblError" runat="server" forecolor="Red"></asp:label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:panel id="pnlShowDownload" class="content-container clearfix" runat="server"
                        visible="true" width="100%">
                        <rad:RadGrid ID="grdDownload" runat="server" AutoGenerateColumns="False" onrowcommand="grdDownload_RowCommand"
                            Width="100%" AllowPaging="false" GridLines="Horizontal" alternatingrowstyle-backcolor="White" >
                            <MasterTableView>
                              <%-- 'Suraj Issue 15287 4/9/13, if the grid dont have any record then grid should visible and it should show "No recors " msg--%>
                                <NoRecordsTemplate>
                                    No downloads available.
                                </NoRecordsTemplate>
                                <%-- Susan Wong #19139 | Commented out do to issue for #20008--%> 
                                <%--<SortExpressions>
                                    <telerik:GridSortExpression FieldName="OrderDate" SortOrder="Descending" />
                                </SortExpressions>--%>
                            <Columns>
                                    <rad:GridBoundColumn DataField="ProductName" HeaderText="Product" />
                                    <rad:GridBoundColumn DataField="FileName" HeaderText="File Name" />
                                       <rad:GridTemplateColumn HeaderText="URL" DataField="URL">
                                        <ItemTemplate>
                                            <%-- Task #21239 --%>
					                         <asp:Button  id="bcourselink" CssClass="submitBtn" runat="server" Text="Course link"   CommandArgument ='<%# Eval("URL") & "," & Eval("ProductID") & "," & Eval("DownloadItemID") & "," & Eval("OrderID") %>' CommandName ="courselink" ></asp:Button>
                                             <%-- Task #21239--%>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <%-- Susan Wong #19139 --%> 
                                    <rad:GridBoundColumn DataField="OrderDate" HeaderText="Order date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <rad:GridTemplateColumn HeaderText="Order ID" DataField="OrderID">
                                        <ItemTemplate>
                                            <asp:hyperlink id="hypOrderID" runat="server" text='<%# Eval("OrderID") %>'>
                                            </asp:hyperlink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Download" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Button CssClass="submitBtn" id="btnDownload" runat="server" text="Download" commandargument='<%# Eval("AttachmentID") & "," & Eval("ProductID") & "," & Eval("DownloadItemID") & "," & Eval("OrderID")%>'
                                                commandname="Download" />
                                            <asp:label id="lblDMessage" runat="server" visible="false" CssClass="download-link"></asp:label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                        <%--<asp:gridview id="grdDownload" runat="server" autogeneratecolumns="False" onrowcommand="grdDownload_RowCommand"
                            width="95%" allowpaging="false" skinid="test" gridlines="Horizontal" bordercolor="#CCCCCC"
                            borderwidth="1px" alternatingrowstyle-backcolor="White">
                            <headerstyle cssclass="GridViewHeader" height="28px" horizontalalign="Center" font-bold="true" />
                            <rowstyle cssclass="GridItemStyle" backcolor="#e5e2dd" />
                            <columns>
                                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:TemplateField HeaderText="Order ID">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hypOrderID" runat="server" Text='<%# Eval("OrderID") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Button ID="btnDownload" runat="server" Text="Download" CommandArgument='<%# Eval("AttachmentID") & "," & Eval("ProductID") & "," & Eval("DownloadItemID") & "," & Eval("OrderID")%>'
                                            CommandName="Download" />
                                        <asp:Label ID="lblDMessage" runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </columns>
                        </asp:gridview>--%>
                    </asp:panel>
                </td>
            </tr>
        </tbody>
    </table>
</div>
<cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
<cc1:User ID="User1" runat="server"></cc1:User>
<script>
     //Susan Wong #19139
    $('.download-link').each(function () {
        if ($(this).text().trim() === "Download Expired") {
            $(this).parent().parent().css('display', 'none');
        }
    });
    $('.url-link').each(function () {
        //if url is not empty
        if ($(this).text().length !== 0) {
            //alert("empty link");
            $(this).text("Course link");
            $(this).addClass("submitBtn");
            $(this)
                .css('height', 'auto')
                .css('margin','12px 0px');
        }
    });
</script>
