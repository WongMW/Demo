<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.EducationFailApp__c"
    CodeFile="EducationFailApp__c.ascx.vb" %>

<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagName="CreditCard" TagPrefix="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>
    <script language="javascript" type="text/javascript">
        function printDiv() {
          
            var content = "<html><body>";
            content += document.getElementById('<%= divContent.ClientID %>').innerHTML;
            content += "</body>";
            content += "</html>";


            var printWin = window.open('', '', 'left=0,top=0,width=1000,height=500,toolbar=0,scrollbars=0,status =0');
            printWin.document.write(content);
            printWin.document.close();
            printWin.focus();
            printWin.print();
            printWin.close();
        }
        function NewWindow() {
            document.forms[0].target = "_blank";
        }
</script>
<div class="content-container clearfix" id="divContent" runat="server">
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td colspan="2">
              
                  <%--  <asp:Label ID="lblError" runat="server" Text=""></asp:Label></b>--%>
            </td>
        </tr>
           <tr>
                <td colspan="2">
                    <b>Status : </b>&nbsp;   <b>   <asp:Label ID="lblStatus" runat="server" Text="In Progress"></asp:Label></b>
                </td>
               
            </tr>
             <tr>
            <td align="right">
                Exam Number
            </td>
            <td>
                <asp:Label ID="lblExamNumber" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td align="right">
                Course for Appeal
            </td>
            <td>
                <asp:TextBox ID="txtCourseAppeal" runat="server" Enabled="false" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Type of Appeal
            </td>
            <td>
                <asp:DropDownList ID="drpAppealReason" runat="server" Enabled="false" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="idAppealDescription" runat="server" >
            <td align="right">
                Please Explain Further
            </td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" 
                    Width="360px" Height="80px"></asp:TextBox>
            </td>
        </tr>
         <tr>
           
            <td>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>
        </tr>
         <tr>
            <td align="right" >
                &nbsp;</td>
            <td align="left"  >
               
               Total Cost: <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblTotalCost" runat="server" Text=""></asp:Label>
               
            </td>
        </tr>
        <%-- <tr id="trRecordAttachment" runat="server" visible="false">
                <td class="auto-style23" style="font-weight: bold" align="right">
                    <b>UPLOAD DOCUMENT :</b>&nbsp;<br />
                </td>
                <td class="auto-style4" colspan="4">
                    <asp:Panel ID="Panel2" runat="Server">
                        <table runat="server" id="Table1" class="data-form" width="100%">
                            <tr>
                                <td class="LeftColumn">
                                    <uc2:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                                        AllowAdd="True" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>--%>

            <tr>
            <td colspan="2" >
                Please note that you are required to print and sign this form and send it in with
                original documentation supporting your the claim to the following address:<br />
                Examinations Department <br />Chartered Accountants Ireland <br />47-49 Pearse Street<br /> Dublin 2
            </td>
            
        </tr>
        <tr >
        
        <td align="justify" colspan="2" >
        <CreditCard:CreditCard ID="CreditCard" runat="server"  Visible="false"/>
           <%-- <rsweb:ReportViewer ID="ReportViewer1" runat="server">
            </rsweb:ReportViewer>--%>
        </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
              
               <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" OnClientClick="NewWindow();"/>--%>
                 <asp:Label ID="lblAlreadyProductAddedError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" />
                <asp:Button ID="btnSave" runat="server" Text="Submit" class="submitBtn"  CausesValidation="false"  Visible="false"/>
                <asp:Button ID="btnPay" runat="server" Text="Save And Pay" class="submitBtn" Visible="false" />
                <asp:Button ID="btnAddToCard" runat="server" Text="Add To Cart" />
                <asp:Button ID="btnCancel" runat="server" Text="Back" class="submitBtn"  />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    <cc1:User ID="User1" runat="server" />
     <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
</div>
