<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoticeboardWidget.ascx.cs" Inherits="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.NoticeboardWidget" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!-- Susan Wong 06-July-2017 Update for Ticket #17097: Added to freeze scroll on RadWindow open -->
<style>
    .stop-scroll {overflow:hidden;}
    .start-scroll {overflow:visible;}
</style>

<div class="noticeboard">
    <%-- WongS, Modified for #20778 Start --%>
    <%--<h1>Messages/Notices</h1>
    <p>
        Results Returned: --%>
        <asp:Label runat="server" ID="CountLabel" Visible="false">0</asp:Label>
    <%--</p>
    <div style="margin-top: 20px"><b>Filter by:</b></div>
    <div class="form-inline">
        <span style="margin-right: 30px">
            <span class="control-label">Subject</span>--%>
            <asp:DropDownList ID="SubjectDropDown" runat="Server" AutoPostBack="False" Visible="false"></asp:DropDownList>
        <%--</span>
        <span style="margin-right: 30px">
            <span class="control-label">Date From</span>--%>
            <rad:raddatepicker id="DateFromPicker" width="200px" runat="server" autopostback="false" Visible="false"></rad:raddatepicker>
        <%--</span>
        <span style="margin-right: 30px">
            <span class="control-label">Date To</span>--%>
            <rad:raddatepicker id="DateToPicker" width="200px" runat="server" autopostback="false" Visible="false"> </rad:raddatepicker>
        <%--</span>
        <button class="submitBtn">Submit</button>
    </div>
    <p style="margin-top: 10px; margin-bottom: 10px;">Items marked in red with an * are high priority</p>--%>
    <%-- WongS, Modified for #20778 End --%>
    <div class="plain-table stu-noticeboard-grid"> <%-- WongS, Modified for #20782 --%>
        <asp:GridView ID="MessagesGrid" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" OnRowCommand="MessagesGrid_OnRowCommand">
            <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
            <Columns>
                <%-- WongS, Modified for #20782 Start--%>
                <asp:TemplateField>
                    <HeaderStyle CssClass="rgHeader" HorizontalAlign="Left" />
                    <HeaderTemplate>
                        <asp:Label ID="lblFname" Text="Date posted" runat="server"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("PublicationDate", "{0:dd/MM/yyyy}") %>' CssClass='<%# (Boolean)Eval("HighPriority") ? "high-priority" : "" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- WongS, Modified for #20782 End --%>
                <asp:TemplateField>
                    <HeaderStyle CssClass="rgHeader" HorizontalAlign="Left" />
                    <HeaderTemplate>
                        <asp:Label ID="lblFname" Text="Title" runat="server"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text='<%# String.Format("{0}{1}", Eval("Title"),  (Boolean)Eval("HighPriority") ? "*" : "" ) %>' CssClass='<%# (Boolean)Eval("HighPriority") ? "high-priority" : "" %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false"> <%-- WongS, Modified for #20782 --%>
                    <HeaderStyle CssClass="rgHeader" HorizontalAlign="Left" />
                    <HeaderTemplate>
                        <asp:Label ID="lblFname" Text="Subject" runat="server"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("Subject")%>' CssClass='<%# (Boolean)Eval("HighPriority") ? "high-priority" : "" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Susan Wong 06-July-2017 Update for Ticket #17097: Added to freeze scroll on RadWindow open -->
        <rad:radwindow id="ViewMessageModal" runat="server" autosize="True"
            modal="True" skin="Default" backcolor="#f4f3f1" visiblestatusbar="False"
            forecolor="#BDA797" iconurl="~/Images/Alert.png" title="View Message" behavior="Close" OnClientClose="OnClientClose1">
            <ContentTemplate>
                <div class="noticeboard-message">
                    <div>
                        <span><b>Title: </b></span>
                        <asp:Label id="TitleLabel" runat="server"></asp:Label>
                    </div>
                    <div>
                        <span><b>Subject: </b></span>
                        <asp:Label id="SubjectLabel" runat="server"></asp:Label>
                    </div>
                    <div>
                        <span><b>Date: </b></span>
                        <asp:Label id="DateLabel" runat="server"></asp:Label>
                    </div>
                    <br/>
                    <div class="message-box">
                        <span><b>Message: </b></span>
                        <asp:Label id="MessageLabel" runat="server" ></asp:Label>
                    </div>
                    
                    <br/>
                    
                    <h4>Related Files</h4>
                    
                    <asp:GridView ID="AttachmentsGrid" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
                        EmptyDataText="There are no related files for this message" CssClass="related-files" >
                        <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" Text='<%# Eval("Name") %>'   OnClick="OnClick" CommandArgument='<%# Eval("ID") %>'  ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </rad:radwindow>
    </div>
</div>

<cc1:aptifywebuserlogin id="WebUserLogin1" runat="server" />

<!-- Susan Wong 06-July-2017 Update for Ticket #17097: Added to freeze scroll on RadWindow open -->
<script type="text/javascript">
    $(function () {
        $('body').addClass("stop-scroll");
        $('.TelerikModalOverlay').css('width', '100%');
    });

    window.onload = function () {
        if (!$('.RadWindow').is(':visible')) {
            $('body').removeClass("stop-scroll");
            $('body').addClass("start-scroll");
        }
        else if (!$('.RadWindow').is(':hidden')) {
            $('body').addClass("stop-scroll");
            $('.TelerikModalOverlay').css('width', '100%');
            $('body').removeClass("start-scroll");
        }
    };

    function OnClientClose1 (sender, args) {
            $('body').removeClass("stop-scroll");
            $('body').addClass("start-scroll");
    };
</script>
