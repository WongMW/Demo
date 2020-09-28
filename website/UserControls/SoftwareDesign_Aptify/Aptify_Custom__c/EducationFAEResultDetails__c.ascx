<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.EducationFAEResultDetails__c"
    CodeFile="~/UserControls/Aptify_Custom__c/EducationFAEResultDetails__c.ascx.vb" %>


 
    <span class="form-title">
        <asp:Label ID="lblHeading" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label></span>

    <table width="50%">
        <tr>
            <td width="25%">
                <span class="label-title-inline">FAE Status:</span>
            </td>
            <td>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td width="25%">
                <span class="label-title-inline">Decile:</span>
            </td>
            <td>
                <asp:Label ID="lblDecile" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td width="25%">
                <span class="label-title-inline">Sufficiency:</span>
            </td>
            <td>
                <asp:Label ID="lblSufficiency" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr runat="server" id="Div1">
            <td width="25%">
                <span class="label-title-inline">Business Leadership:</span>
            </td>
            <td>
                <div id="Div11" runat="server" style="width: 3%;">
                    &nbsp;
                </div>
            </td>
        </tr>

         

          <tr runat="server" id="Div2">
            <td width="25%">
                <span class="label-title-inline">Financial Accounting & Reporting:</span>
            </td>
            <td>
                 <div id="Div22" runat="server" style="width: 3%;">
                  &nbsp;
                </div>
            </td>
        </tr>
         <tr runat="server" id="Div3">
            <td width="25%">
                <span class="label-title-inline">Audit & Assurance:</span>
            </td>
            <td>
                 <div id="Div33" runat="server" style="width: 3%;">
                   &nbsp;
                </div>
            </td>
        </tr>
         <tr runat="server" id="Div4">
            <td width="25%">
                <span class="label-title-inline">Finance:</span>
            </td>
            <td>
                 <div id="Div44" runat="server" style="width: 3%;">
                  &nbsp;
                </div>
            </td>
        </tr>
         <tr runat="server" id="Div5">
            <td width="25%">
                <span class="label-title-inline">Management Accounting:</span>
            </td>
            <td>
                 <div id="Div55" runat="server" style="width: 3%;">
                   &nbsp;
                </div>
            </td>
        </tr>
         <tr runat="server" id="Div6">
            <td width="25%">
                <span class="label-title-inline">Corporate and Individual Tax Planning:</span>
            </td>
            <td>
                 <div id="Div66" runat="server" style="width: 3%;">
                     &nbsp;
                </div>
            </td>
        </tr>
        <tr  >
            <td width="25">
              &nbsp;
            </td>
            <td>
                <asp:Button ID="BtnBack" runat="server" Text="Back" CssClass="submitBtn"/>
            </td>
        </tr>
    </table>



    

    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />

 
