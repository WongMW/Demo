<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PersonCompanyInformation__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.PersonCompanyInformation__c" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%--<style type="text/css">

     .watermarked
{
    background-color: #FFFFFF;
    border: 1px solid #A9A9A9;
    color: #BEBEBE;
    padding: 2px 0 0 2px;
    -moz-box-sizing: border-box;
    -webkit-box-sizing: border-box;
    box-sizing: border-box;
}

</style>--%>

<script type="text/javascript">


   
    function OnClientCompleted(sender, e) {
        sender._element.className = "";
    }

    function ClientItemSelected(sender, e) {
        document.getElementById("<%=hfCompanyID.ClientID %>").value = e.get_value();
        
    }

    function companyitemselecting(sender, e) {
        sender._element.className = "loading";
        document.getElementById('<%=hfCompanyID.ClientID%>').value = "-1"
    }

    function OnClientPopulated(sender, e) {
        sender._element.className = "txtBoxEditProfile";
    }

    function myShowFunction() {
                     
            var radwindow = $find('<%=RadAddNew.ClientID%>');
        
            radwindow.show();
       
    }
    
  </script>


<asp:HiddenField id="hfCompanyID" runat="server" Value="-1" />
<div class="clearfix" >
    
           <div >
                 <div>
                    <div>
                        <asp:Label ID="Label6" runat="server">Company Name :</asp:Label>
                    </div>
                    <div>                    
                           <asp:TextBox ID="txtCompany11" CssClass="txtBoxEditProfile" runat="server" onchange="getAddress();"></asp:TextBox>
                                    <Ajax:AutoCompleteExtender ID="AutoCompleteExtender11" runat="server" BehaviorID="autoComplete"
                                        CompletionInterval="10" CompletionListElementID="divwidth" CompletionSetCount="12"
                                        EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetCompanyList" ServicePath="~/GetCompanyList__c.asmx"
                                        TargetControlID="txtCompany11" OnClientPopulating="companyitemselecting" OnClientItemSelected="ClientItemSelected">
                                    </Ajax:AutoCompleteExtender>
                           <Ajax:TextBoxWatermarkExtender ID="WatermarkExtender1" runat="server"
                  TargetControlID="txtCompany11" WatermarkText="Type Company Name Here" WatermarkCssClass="watermarked" />
					 <asp:Button ID="btnAddNew" Text="Add" runat="server" ValidationGroup="ColleagueInfo" OnClientClick="myShowFunction()" CssClass="submitBtn"/>
                    </div>
                </div>
                 <div>
                    <div>
                       <asp:Label ID="Label1" runat="server">Job Title :</asp:Label>
                    </div>
                    <div>
                         <asp:DropDownList ID="cmbJobTitle" CssClass="txtBoxEditProfile" runat="server"></asp:DropDownList>
                      
                    </div>
                </div>
                 <div>
                     <div>&nbsp;</div>
                    <div>
                        <asp:Label ID="lblMessage" runat="server" class="RequiredField"></asp:Label>
                    </div>
                 </div>
                 <div>
                     <div>&nbsp;</div>
                    <div>
                        <asp:Button ID="btnUpdate" Text="Update" ValidationGroup="ColleagueInfo" runat="server" CssClass="submitBtn"
                            Visible="false" />
                       
                        <asp:Button ID="btnAdd" Text="Add" runat="server" ValidationGroup="ColleagueInfo"  CssClass="submitBtn"
                             />
                        <asp:Label ID="lblDateError" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="lblLanguageText" runat="server">Click <b> Add </b> to record your Additional Companies information</asp:Label>
                    </div>
                </div>

                 <div>
                     <div>&nbsp;</div>
                    <div>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>

                 <div>
                    <div>
                       
                      <asp:GridView ID="grvCompany" runat="server" datakeynames="ID" AllowSorting="False" AutoGenerateColumns="False" Width="100%"   GridLines="Horizontal" AllowPaging="true" BorderColor="#CCCCCC"  BorderWidth="1px" PageSize="10">
              <HeaderStyle backcolor="#f58844"  forecolor="black" HorizontalAlign="Left"  />
                <alternatingrowstyle backcolor="#fce2d2"  forecolor="black" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                            CssClass="submitBtn" Text="Delete" CommandArgument='<%# Eval("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID" HeaderText="" ReadOnly="true" Visible="false" />
                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ReadOnly="true" />
                                 <asp:BoundField DataField="JobTitle" HeaderText="Job Title" ReadOnly="true" />
                                <asp:BoundField DataField="EntID" HeaderText="" ReadOnly="true" Visible="false" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                 <div>
                    <div>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
            </div>
       
</div>
<rad:radwindow id="RadAddNew" runat="server" width="350px" height="160px" modal="True"
    skin="Default" backcolor="#f4f3f1" visiblestatusbar="False" behaviors="None"
    forecolor="#BDA797" iconurl="~/Images/Alert.png" title="Add New Colleague" behavior="None">
    <ContentTemplate>
    <div>
        <div> &nbsp; </div>
    <div>
    <div height="35">
    Your Colleague : <asp:TextBox ID="txtAddNew" onkeydown="javascript:return false" style="background-color: #d8d8bc;" runat="server"></asp:TextBox> 
        <asp:RequiredFieldValidator ValidationGroup="AddNew" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAddNew"
                             ErrorMessage="*" Display="Dynamic"
                            ForeColor="Red"></asp:RequiredFieldValidator>
    </div></div>
         <div> &nbsp; </div>
   <div >
   <div style="margin-left: 100px;">
   <asp:Button ID="Button1" runat="server" Text="Add" CssClass="submitBtn"
                        CausesValidation="false" ValidationGroup="AddNew" Height="23px" Width="60px" /> <span width="5"></span>
       <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="submitBtn"
                        CausesValidation="false" Height="23px" Width="60px" />
    </div></div>
    </div>
    </ContentTemplate>
</rad:radwindow>
<cc1:user id="User1" runat="server"></cc1:user>
<cc3:aptifywebuserlogin id="WebUserLogin1" runat="server"></cc3:aptifywebuserlogin>
