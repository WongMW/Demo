<%@ Control Language="VB" Debug="true" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/AbatmentsFormData__c.ascx.vb"
    Inherits="AbatmentsFormData__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="../Aptify_General/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc2" %>
<%@ Register Src="organisationAddress__c.ascx" TagName="organisationAddress__c" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>


<div>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <span class="label-title label-title-inline">Status : </span>
    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblABTName" class="label-title" runat="server"></asp:Label>

    <asp:Label ID="lblcriteria" class="label-title" runat="server"></asp:Label>

    <asp:Label ID="lblAtrributevalue" class="label-title" runat="server"></asp:Label>

    <asp:Label ID="lbldesc" class="label-title" runat="server"></asp:Label>

    <asp:Label ID="lblDescriptionText" class="label-title" runat="server"></asp:Label>

    <div runat="Server" id="MaternityDes" visible="false">
        <asp:Label ID="lblMoreDiscription" runat="server"></asp:Label>
    </div>
    <div class="actions">
        <input type="button" id="btnPrint" onclick="window.print();" class="submitBtn" value="Print" runat="server" />
    </div>


    <asp:Panel ID="Panel1" runat="server" CssClass="cai-form">
        <div class="form-title">
            <asp:Label ID="lblIwish" runat="server"></asp:Label>
        </div>

        <div id="idAbatementType" runat="server" visible="false">
            <b>Type :</b>
            <asp:Label ID="Lblabatetypecheck" runat="server"></asp:Label>
        </div>

        <div class="cai-form-content">
            <div class="field-group">
                <span class="label-title">Reason :</span>
                <asp:TextBox ID="txtReason" runat="server"></asp:TextBox>
            </div>


            <div class="field-group">
                <asp:Label ID="LbllevelIncome" runat="server" class="label-title"><span class="RequiredField">*</span>Level Of Income : </asp:Label>

                <asp:DropDownList ID="ddlstatus" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select Level Of Income"
                    ForeColor="Red" ControlToValidate="ddlstatus" InitialValue="--Select--"></asp:RequiredFieldValidator>
            </div>


            <div class="field-group">
                <span class="label-title">Annual Income :</span>
                <div class="currency-input">
                    <asp:Label ID="lblPrefeeredCurrency" runat="server" Text=""></asp:Label>
                    <asp:TextBox ID="txtAnnualIncome" runat="server"></asp:TextBox>
                </div>
                <asp:Label ID="lblAbatementYear" runat="server"></asp:Label>
            </div>


            <div class="field-group">
                <span class="label-title">Members name :</span>
                <asp:Label ID="lblfname" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblsname" runat="server" Visible="false"></asp:Label>
                Dear <asp:Label ID="lbllname" runat="server"></asp:Label>
            </div>


            <div class="field-group" runat="server" visible="false">
                <asp:Label ID="lblemail1" runat="server" class="label-title"><span class="RequiredField">*</span>Email Address :</asp:Label>
                <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtEmail"
                    Display="Dynamic" ErrorMessage="Email Required" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtEmail"
                    Display="Dynamic" ErrorMessage="Invalid  Email " ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
            </div>


            <div class="field-group">
                <span class="label-title">Date :</span>
                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
            </div>

            <div class="field-group" runat="server" visible="false">
                <asp:Label ID="lblphn" runat="server" class="label-title"> Phone Number :</asp:Label>
                <asp:TextBox ID="txtPhoneAreaCode" runat="server" CssClass="txtUserProfileAreaCodeSmall"
                    MaxLength="3" Width="100px"></asp:TextBox>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="txtUserProfileAreaCode" MaxLength="11" Width="300px"></asp:TextBox>
            </div>


            <div id="trRecordAttachment" runat="server" visible="false" class="field-group">
                <span class="label-title">UPLOAD DOCUMENT :</span>
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
            </div>

            <div id="trRejectMessage" runat="server" visible="false" class="field-group">
                <span class="label-title">Reject Message :</span>
                <asp:Label ID="lblRejectedMessage" runat="server" Text=""></asp:Label>
            </div>

            <div id="trAbatementStatusReason" runat="server" visible="false">
                <span class="label-title">Abatement Status Reasons :</span>

                <asp:GridView ID="grdAbatmentStatusReason" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Status Reason">
                            <ItemTemplate>

                                <asp:Label ID="lblStatusReason" runat="server" Text='<%# Eval("StatusReason")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Completed">
                            <ItemTemplate>
                                <asp:Label ID="lblIsCompleted" runat="server" Text='<%# Eval("IsCompleted")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Abatement Status">
                            <ItemTemplate>
                                <asp:Label ID="lblAbatementStatusID" runat="server" Text='<%# Eval("AbatementStatusID")%>' Visible="false" />
                                <asp:Label ID="lblAbatementStatus" runat="server" Text='<%# Eval("AbatementStatus")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle />
                </asp:GridView>
            </div>


            <div class="field-group">
                <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblPartTimeEmp" runat="server" Text=""></asp:Label>
                <asp:CheckBox ID="chkPartTimeEmp" runat="server" Visible="false" />
            </div>


            <div class="field-group">
                <asp:Label ID="lblBottomText" runat="server" Text=""></asp:Label>
                <asp:CheckBox ID="chkAbatementBottom" runat="server" Visible="false" />
            </div>


            <div class="field-group">
                <asp:Label ID="lblBottomLastText" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblTick" runat="server" Text=" please tick box"></asp:Label>
                <asp:CheckBox ID="Chkinfo" runat="server" Text="" />
            </div>

            <div class="field-group">
                <span class="label-title">Please return completed form by post to: </span>
                <uc1:organisationAddress__c ID="organisationAddress__c1" runat="server" />
                <asp:Label ID="lblAbtementFormBottomText" runat="server" Text=""></asp:Label>
            </div>

            <div class="field-group actions">
                <asp:Button ID="btnsubmit" runat="server" CssClass="submitBtn" Text="Save" />
                <asp:Button ID="btnSave" runat="server" CssClass="submitBtn" Text="Submit" />
            </div>
        </div>
    </asp:Panel>
</div>
<cc3:User ID="AptifyEbusinessUser1" runat="server" />
