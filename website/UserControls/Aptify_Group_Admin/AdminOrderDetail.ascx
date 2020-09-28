<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdminOrderDetail.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.AdminOrderDetail" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>
<script language="javascript" type="text/javascript">
 
    function validatePage() {
       var isValid = Page_ClientValidate();
    var button = document.getElementById('<%= cmdMakePayment.ClientID %>');
      var buttonText = button.value;
    button.disabled = true;
     button.value = 'Submitting...';

    button.disabled = isValid;

        if (isValid == false) {
          button.value = buttonText;
     }
    }

    function UpdateItemCountField(sender, args) {
        //set the footer text
        sender.get_dropDownElement().lastChild.innerHTML = "A total of " + sender.get_items().get_count() + " items";
    }

     function SubmittingSave() 
     {  
         var isValid = Page_ClientValidate();  
         var button = document.getElementById('<%= BtnSave.ClientID %>');
         var hdnTemp = document.getElementById('<%= Hidden.ClientID %>');
         button.disabled = true;
         button.value = 'Submitting...';
         button.style.cursor="auto";  
         button.disabled = isValid;
         
         if (hdnTemp.value=="true")
         { 
            hdnTemp.value="false"
            button.click();  
            hdnTemp.value="true"      
         }     
    }

    <%--Anil Issue 6640--%>   
    window.history.forward(1);
   
</script>

<div class="dvUpdateProgress"  style="overflow:visible;"> 
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server"
        DisplayAfter="0"> 
        <ProgressTemplate> 
            <div class="dvProcessing" style="height:1000px;"> 
                <table class="tblFullHeightWidth"> 
                    <tr> 
                        <td class="tdProcessing" style="vertical-align:middle" > 
                            Please wait... 
                        </td> 
                    </tr> 
                </table> 
            </div> 
        </ProgressTemplate> 
    </asp:UpdateProgress> 
</div>



<asp:Label ID="lblrecmsg" runat="server" Visible="False" Text="Item not found" ForeColor="red"></asp:Label>
<div id="payoffdiv" runat="server" class="maindiv">
    <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server">
        <ContentTemplate>
            <table id="tblmember" runat="server" width="100%" class="data-form">
                <tr>
                    <td runat="server" style="font-weight: bold; font-size: 13px; height: 30px">
                        Pay Open Invoices for My Company
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblfilter" Text=" Currency Type:" runat="server" Font-Bold="true"></asp:Label>
                        <%--<rad:RadComboBox ID="radcurrency" runat="server" Height="60px" Width="220px" AllowCustomText="false"
                            EmptyMessage="Select a Currency" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                            OnClientItemsRequested="UpdateItemCountField" OnDataBound="radcurrency_DataBound"
                            OnItemDataBound="radcurrency_ItemDataBound" OnItemsRequested="radcurrency_ItemsRequested"
                            AutoPostBack="true" CausesValidation="false">
                            <HeaderTemplate>
                                <ul>
                                    <li class="col1">Currency Type</li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <ul>
                                    <li class="col1">
                                        <%# DataBinder.Eval(Container.DataItem, "CurrencyType")%></li>
                                </ul>
                            </ItemTemplate>
                            <FooterTemplate>
                                A total of
                                <asp:Literal runat="server" ID="RadComboItemsCount" />
                                items
                            </FooterTemplate>
                        </rad:RadComboBox>--%>
                        <asp:DropDownList ID="radcurrency" AutoPostBack="true" DataTextField="CurrencyType" style="width: 160px;" DataValueField="CurrencyTypeID" runat="server">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rad:RadGrid ID="grdOrderDetails" runat="server" AutoGenerateColumns="False" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending" PagerStyle-PageSizeLabelText="Records Per Page"
                            AllowPaging="True">
                            <MasterTableView  AllowFilteringByColumn="true">
                                <Columns>
                                    <rad:GridTemplateColumn AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Image ID="ImgFlag" runat="server" Visible="false" ToolTip="Flagged for Review" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                     <%--Neha Changes for Issue:14972,04/29/13--%>
                                    <rad:GridTemplateColumn AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="gridAlign"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="center" Width="30px" />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllMakePayment" Width="30px" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                AutoPostBack="True" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkMakePayment" OnCheckedChanged="chkMakePayment_CheckedChanged"
                                                AutoPostBack="true"></asp:CheckBox>
                                            <asp:Label ID="ID" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Name" DataField="Name" SortExpression="Name"
                                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="120px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign gridAlign" Width="120px">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemberName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn HeaderText="City" SortExpression="City" AutoPostBackOnFilter="true"
                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        DataField="City" ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-CssClass="LeftAlign gridAlign" />
                                    <rad:GridTemplateColumn HeaderText="Order #" DataField="ID" SortExpression="ID" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="60px" CssClass="LeftAlign gridAlign" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblOrderNo" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"OrderConfirmationURL") %>'
                                                runat="server" CssClass="namelink" Target="_blank" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridDateTimeColumn DataField="OrderDate" UniqueName="GridDateTimeColumnOrderDate"
                                        HeaderText="Date" FilterControlWidth="100px" HeaderStyle-Width="100px" SortExpression="OrderDate"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                                        ShowFilterIcon="false" EnableTimeIndependentFiltering="true" ItemStyle-Width="100px"
                                        ItemStyle-CssClass="LeftAlign gridAlign" />
                                    <rad:GridBoundColumn HeaderText="Product(s)" HeaderStyle-Width="100px" ItemStyle-Wrap="true"
                                        FilterControlWidth="80%" ItemStyle-Width="100px" DataField="Product" SortExpression="Product"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        ItemStyle-CssClass="LeftAlign gridAlign" />
                                    <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="Total" SortExpression="GrandTotal"
                                        FilterControlWidth="80%" DataField="GrandTotal" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemStyle CssClass="rightAlign gridAlign" Width="60px" />
                                        <HeaderStyle Width="60px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrandTotal" runat="server" Text='<%#GetFormattedCurrency(Container, "GrandTotal")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="Balance"
                                        FilterControlWidth="80%" SortExpression="Balance" DataField="Balance" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle CssClass="rightAlign gridAlign" Width="60px" />
                                        <HeaderStyle Width="90px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#GetFormattedCurrency(Container, "Balance")%>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Currency Symbol" Visible="false" DataField="CurrencySymbol"
                                        FilterControlWidth="80%">
                                        <ItemStyle Width="10px" />
                                        <HeaderStyle Width="10px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrencySymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Comments" AllowFiltering="false" FilterControlWidth="80%">
                                        <ItemStyle HorizontalAlign="Center" CssClass="gridAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAddComments" runat="server" Text="Add/Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                CommandName="ADDEditComments" CausesValidation="false" Visible="true"></asp:LinkButton>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Pay Amount" HeaderStyle-CssClass="rightAlign"
                                        FilterControlWidth="80%" AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="rightAlign gridAlign" Width="130px">
                                        </ItemStyle>
                                        <HeaderStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumDigitsAfterDecimal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NumDigitsAfterDecimal") %>'></asp:Label>
                                            <asp:Label ID="lblCurrSymbol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CurrencySymbol") %>'></asp:Label>
                                            <asp:TextBox ID="txtPayAmt" size="15" runat="server" CssClass="rightAlign TextboxStylePayment"
                                                Text="0.00" OnTextChanged="txtPayAmt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </td>
                </tr>
                <tr class="rightAlign">
                    <td align="right">
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblTotal" Text="Total: " ForeColor="#333333" Font-Size="13px" Font-Bold="true"
                                        runat="server"></asp:Label>
                                </td>
                                <td class="Paddingrightpayment" align="right">
                                    <asp:Label ID="txtTotal" runat="server" Width="100px" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<asp:UpdatePanel runat="server" ID="UpEditBadgeInfo" UpdateMod>
                            <ContentTemplate>--%>
                                <telerik:RadWindow ID="radGAReviewComments" runat="server" Modal="True" Skin="Default" CssClass="dvPopUpOrderComments"
                                    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" Height="180px"
                                    Width="350px" ForeColor="#C59933" IconUrl="~/Images/Comments.png" Title="Review Comments">
                                    <ContentTemplate>
                                    <asp:UpdatePanel runat="server" ID="UpEditBadgeInfo" >
                                        <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; width: 100%">
                                            <tr style="padding-bottom: 5px;">
                                                <td class="lblReviewComments" colspan="2" style="padding-bottom: 10px;">
                                                    <asp:Label ID="lblOrderID" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-bottom: 5px; vertical-align: top;" class="rightAlign">
                                                    <b>Comments:</b>
                                                </td>
                                                <td style="padding-bottom: 5px;" class="width: 268px">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtGAReviewComments" runat="server" TextMode="MultiLine" CssClass="GAComments"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="padding-top: 5px; padding-right: 10px; padding-bottom: 5px;"
                                                    colspan="2">
                                                    <asp:Button ID="BtnSave" class="submitBtn" runat="server" Text="Save" CausesValidation="false"  ValidationGroup = "grpAddComments" />
                                                    &nbsp;
                                                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" class="submitBtn" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>

                              </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnSave" />     
                            </Triggers>
                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </telerik:RadWindow>
                           <%-- </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="radGAReviewComments" />     
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>
            </table>
            <table valign="top" width="46%" class="bordercolor" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdbgcolorpayment infohead" style="padding-left: 8px">
                        <strong><font size="2">Payment Information</font></strong>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 5px;">
                     <%--  'Anil B change for 10737 on 13/03/2013
                'Set Credit Card ID to load property form Navigation Config--%>
                        <uc1:CreditCard ID="CreditCard" runat="server" />
                    </td>
                </tr>
            </table>
            <rad:RadWindow ID="radpaymentmsg" runat="server" Width="260px" Height="120px" Modal="True"
                BackColor="#f4f3f1" Skin="Default" VisibleStatusbar="False" Behaviors="None"
                ForeColor="#BDA797" Title="Order Payment" Behavior="None">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblpayment" runat="server" Text=" Your payment was made successfully!"
                                    Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnok" runat="server" Text="OK" Width="70px" class="submitBtn" OnClick="btnok_Click"
                                    ValidationGroup="ok" />&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </rad:RadWindow>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radpaymentmsg" />           
        </Triggers>
    </asp:UpdatePanel>
    <table>
        <tr>
            <td valign="top" align="left" style="padding-left: 8px">
                <br />
                <asp:Button CssClass="submitBtn" ID="cmdMakePayment" TabIndex="1" runat="server"
                    Height="26px" Text="Make Payment" UseSubmitBehavior="false" OnClientClick="validatePage();">
                </asp:Button>
            </td>
        </tr>
    </table>
</div>
<cc2:User runat="Server" ID="User1" />
<asp:HiddenField  runat="server" ID="Hidden" Value="true"/>
