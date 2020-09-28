<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdminOrderDetail__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.AdminOrderDetail__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<script language="javascript" type="text/javascript">
 
  
    function validatePageMove() {
       //var isValid = Page_ClientValidate();
       alert(isValid);
    var button = document.getElementById('<%= btnMove.ClientID %>');
      var buttonText = button.value;
    //button.disabled = true;
     button.value = 'Submitting...';

    //button.disabled = isValid;

//        if (isValid == false) {
//          button.value = buttonText;
//        }
        button.click();
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


    function fnCheckPayment() {
//        var grid = document.getElementById('<%=grdOrderDetails.ClientID%>');  //Retrieve the grid    
//        var inputs = grid.getElementsByTagName("input"); //Retrieve all the input elements from the grid
//        var isValid = false;
//        for (var i = 0; i < inputs.length; i += 1) { //Iterate over every input element retrieved
//            if (inputs[i].type === "checkbox") { //If the current element's type is checkbox, then it is wat we need
//                if (inputs[i].checked === true) { //If the current checkbox is true, then atleast one checkbox is ticked, so break the loop
//                    isValid = true;
//                    break;
//                }
//            }
//        }
//        if (!isValid) {
//          alert('Select atlest one Record.');
//            return false;
//        }
//        else {
            var button = document.getElementById('<%= btnMove.ClientID %>');
            button.click();
            return true;
//        }
    }
      function validatenumerics(obj,e) {
        var keycode = (e.which) ? e.which : e.keyCode;
        var fieldval = (obj.value);
        var dots = fieldval.split(".").length;
        if (keycode == 46) {
            if (dots > 1) {
                return false;
            } else {
                return true;
            }
        }
        if (keycode == 8 || keycode == 9 || keycode == 46 || keycode == 13 || keycode == 37 || keycode == 39) // back space, tab, delete, enter 
        {
            return true;
        }
        if ((keycode >= 32 && keycode <= 45) || keycode == 47 || (keycode >= 58 && keycode <= 127)) {
            return false;
        }
  
        else return true;
    }

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
                    <span id="spnNote" runat="server"><asp:Label ID="lblNote" runat="server" Text="Benevolent donation amount is not editable if order is shipped."></asp:Label></span>
                        <rad:RadGrid ID="grdOrderDetails" runat="server" AutoGenerateColumns="False" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending" PagerStyle-PageSizeLabelText="Records Per Page"
                            AllowPaging="True">
                            <MasterTableView  AllowFilteringByColumn="true"  NoMasterRecordsText="No Open Invoices Available.">
                                <Columns>
                                    <rad:GridTemplateColumn AllowFiltering="false" >
                                        <ItemStyle HorizontalAlign="Center"  CssClass="gridAlign"></ItemStyle>
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
                                            <asp:Label ID="OrderLineID" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineID") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="CategoryID" Text='<%# DataBinder.Eval(Container.DataItem,"CategoryID") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="OrderLineNumber" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLineNumber") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="OrderLine" Text='<%# DataBinder.Eval(Container.DataItem,"OrderLine") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="IsDonation" Text='<%# DataBinder.Eval(Container.DataItem,"IsDonation") %>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="OrderStatus" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>' runat="server" Visible="false"></asp:Label>
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
                                    <rad:GridBoundColumn HeaderText="City" SortExpression="City" AutoPostBackOnFilter="true" UniqueName="City"
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
                                    <rad:GridBoundColumn HeaderText="Category" HeaderStyle-Width="100px" ItemStyle-Wrap="true"
                                        FilterControlWidth="80%" ItemStyle-Width="100px" DataField="ProductCategory" SortExpression="ProductCategory"
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
                                    <rad:GridTemplateColumn HeaderStyle-HorizontalAlign="right" HeaderText="CompanyTotal" SortExpression="CompanyTotal"
                                        FilterControlWidth="80%" DataField="CompanyTotal" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemStyle CssClass="rightAlign gridAlign" Width="60px" />
                                        <HeaderStyle Width="60px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyTotal" runat="server" Text='<%#GetFormattedCurrency(Container, "CompanyTotal")%>'></asp:Label>
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
                                            <asp:LinkButton ID="lnkAddComments" runat="server" Text="Add/Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"OrderLine") %>'
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
                                                Text='<%# GetFormattedCurrencyNoSymbol(Container,"Balance")%>' OnTextChanged="txtPayAmt_TextChanged" AutoPostBack="true" Enabled="false" onKeyPress="return validatenumerics(this, event);"></asp:TextBox>
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
                        <asp:UpdatePanel runat="server" ID="UpEditBadgeInfo" UpdateMod>
                            <ContentTemplate>
                                <telerik:RadWindow ID="radGAReviewComments" runat="server" Modal="True" Skin="Default" CssClass="dvPopUpOrderComments"
                                    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" Height="180px"
                                    Width="350px" ForeColor="#C59933" IconUrl="~/Images/Comments.png" Title="Review Comments">
                                    <ContentTemplate>
                                   <%-- <asp:UpdatePanel runat="server" ID="UpEditBadgeInfo" >
                                        <ContentTemplate>--%>
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

                             <%-- </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnSave" />     
                            </Triggers>
                        </asp:UpdatePanel>--%>
                                    </ContentTemplate>
                                </telerik:RadWindow>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="radGAReviewComments" />     
                            </Triggers>
                        </asp:UpdatePanel>
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
                <table>
        <tr style="float:right">
           <%-- <td valign="top" align="left" style="padding-left: 8px;display:none;">
                <br />
                <asp:Button CssClass="submitBtn" ID="cmdMakePayment" TabIndex="1" runat="server"
                    Height="26px" Text="Make Payment" UseSubmitBehavior="false" OnClientClick="validatePage();">
                </asp:Button>
            </td>--%>
            <td valign="top" align="right" style="float:right;">
                <%--  'Anil B change for 10737 on 13/03/2013
                'Set Credit Card ID to load property form Navigation Config--%>
                <asp:Button ID="btnMove" runat="server" Text="Next" OnClientClick="return fnCheckPayment();" CssClass="submitBtn" />
            </td>
        </tr>
    </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radpaymentmsg" />           
            <asp:PostBackTrigger ControlID="btnMove" />           
        </Triggers>
    </asp:UpdatePanel>

    
</div>
<cc2:User runat="Server" ID="User1" />
<asp:HiddenField  runat="server" ID="Hidden" Value="true"/>
