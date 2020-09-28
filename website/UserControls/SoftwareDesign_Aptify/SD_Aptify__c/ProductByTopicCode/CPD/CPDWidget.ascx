<%@ Control Language="C#"  CodeFile="CPDWidget.ascx.cs" 
Inherits="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPD.CPDWidget" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div style="vertical-align: text-top;">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</div>

<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView sd-courselist">
    <div class="sfContentBlock">
        <h3><asp:Label runat="server" ID="lblTitle" Text="Recommended CPD courses"></asp:Label> </h3>
    </div>

    <telerik:RadGrid CssClass="plain-table" ID="RadGrid1" runat="server" AllowPaging="false" AllowSorting="false"
        AutoGenerateColumns="false" Skin="" GridLines="None" MasterTableView-DataKeyNames="ProductId">
        <MasterTableView DataKeyNames="ProductId">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            <AlternatingItemStyle HorizontalAlign="Left" />
            <Columns>
                <telerik:GridBoundColumn HeaderText="Title" DataField="Title"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Start date" DataField="StartDate" DataFormatString="{0:dd MMM yyyy}"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Price" DataField="Price" ItemStyle-CssClass="price-column" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Membership discount" DataField="MembershipDiscount" Visible="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="CPD hours" DataField="CPDHours"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Location" DataField="Location" UniqueName="Location" Visible="false"></telerik:GridBoundColumn>
                <telerik:GridHyperLinkColumn HeaderText="" Text="Read more" DataNavigateUrlFields="ProductUrl"></telerik:GridHyperLinkColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>

<cc1:User ID="User1" runat="server" />
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Width="47px" Height="14px" 
                        Visible="False"></cc2:AptifyShoppingCart>
<script>
    $('.sd-courselist a').addClass("cai-btn cai-btn-red-inverse");
    $(document).ready(function () {
        var tId = "<%= RadGrid1.ClientID %>";
        $("#" + tId + " tr td.price-column").html('<img width="30px" src="/Images/CAITheme/bx_loader.gif">');

        function getData(args, context) {
            <%= Page.ClientScript.GetCallbackEventReference(this, "args", "getResult", "context", true) %>
        }

        function getResult(result, context) {
            if (result) {
                result = result.split(';');
                if (result.length > 0) {
                    $("#" + tId + " tr td.price-column").each(function (index) {
                        $(this).html(result[index]);
                    });
                }
            }
        }

        getData();
    });
</script>

