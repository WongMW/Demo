<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/CTCDesignationApp__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CTCDesignationApp__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1000px;">
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

<div class="info-data cai-form">
    <div class="form-title">CTC Designation</div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="True">
        <ContentTemplate>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            <div class="row-div clearfix">
                <div class="label-div w30">
                    &nbsp;
                </div>
                <div class="field-div1 w100">
                    <div class="info-data" style="margin: 0 5% 0 6% !important;">
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>Student Number:</b> 
                            </div>
                            <div class="field-div1 w60">
                               <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>Student Name:</b>
                            </div>
                            <div class="field-div1 w60">
                           <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>Email:</b>
                            </div>
                            <div class="field-div1 w60">
                               <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                          <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                  <b>Request:</b>
                            </div>
                            <div class="field-div1 w60">
                                <asp:Label ID="lblRequest" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                          <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>Status:</b> 
                            </div>
                            <div class="field-div1 w60">
                              <asp:Label ID="lblStatus" runat="server" Text="In Progress"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>CTC Designation Product Details:</b> 
                            </div>
                            <div class="field-div1 w60">
                              <%-- &nbsp;<asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>--%>
                                <telerik:RadGrid ID="radCTCDesignationSummery" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AllowFilteringByColumn="false" AllowSorting="false" Width="150px">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Product" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Quantity" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%# String.Format("{0:f2}",DataBinder.Eval(Container.DataItem,"Qty")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Price" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Price") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>CTC Designation Product Price:</b> 
                            </div>
                            <div class="field-div1 w60">
                                &nbsp;<asp:Label ID="lblCurrency" runat="server"
                                    Text=""> </asp:Label><asp:Label ID="lblProductPrice" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                         <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20" id="idProfessionalBodyDiv" runat="server" visible="false">
                                &nbsp;
                            </div>
                            <div class="field-div1 w60">
                                 <asp:CheckBox ID="chkProfessionalBody" runat="server" Text="Is Member of a recognized professional body" />
                            </div>
                        </div>

                         <div class="row-div clearfix" id="divTermsCondition" runat="server" visible="false">
                            <div style="text-align: left;" class="field-div1 w20" >
                                &nbsp;
                            </div>
                            <div class="field-div1 w60">
                                  <span style="color: Red;">*</span>
                                <asp:CheckBox ID="chkTermsAndConditions" runat="server" />
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Terms and Conditions Required"
                                    Display="Dynamic" CssClass="required-label" ClientValidationFunction="ValidateCheckBox"></asp:CustomValidator><br />
                                &nbsp;<asp:Label ID="lblWebDesc" runat="server"></asp:Label>
                            </div>
                        </div>
                      
                       <%--    <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                <b>Comments:</b> 
                            </div>
                            <div class="field-div1 w60">
                              <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="100px"
                                        Style="resize: none" width="300px"></asp:TextBox>
                            </div>
                        </div>--%>
                          <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w10">
                                &nbsp;
                            </div>
                            <div class="field-div1 w60">
                                <uc1:CreditCard ID="CreditCard" runat="server" />
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w20">
                                &nbsp;
                            </div>
                            <div class="field-div1 w60 cai-form-content">
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="32px" CausesValidation="false" CssClass="submitBtn"/>
                                 
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"   Height="32px" CssClass="submitBtn"/>
                            </div>
                        </div>
                          <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w10">
                                &nbsp;
                            </div>
                            <div class="field-div1 w60">
                               <asp:Label ID="lblScheduleText" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <telerik:RadWindow ID="radWindow" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="CTC Designation Application" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div>
                                    <br />
                                </div>
                                <div>
                                    <asp:Button ID="btnOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow ID="radAcceptTerms" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="CTC Designation Application" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblAcceptTermsCondition" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div>
                                    <br />
                                </div>
                                <div>
                                    <asp:Button ID="btnAcceptTerms" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
            <cc3:User ID="User1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
