<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.MembershipApplication__c" CodeFile="~/UserControls/Aptify_Custom__c/MembershipApplication__c.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<style type="text/css">
    .auto-style1 {
    }
    #tblMain {
        width: 858px;
    }
    .auto-style2 {
        width: 406px;
        height: 54px;
    }
    .auto-style3 {
        height: 54px;
    }
    .auto-style4 {
        width: 99%;
    }
    .auto-style5 {
        width: 150px;
    }
    .auto-style6 {
        width: 243px;
    }
    .auto-style7 {
        width: 141px;
    }
    .auto-style8 {
        width: 508px;
    }
    .auto-style9 {
        width: 141px;
        height: 23px;
    }
    .auto-style10 {
        height: 23px;
        text-align: center;
    }
    .auto-style11 {
        width: 141px;
        height: 26px;
    }
    .auto-style12 {
        height: 26px;
    }
    .auto-style13 {
    }
    .auto-style14 {
        width: 100%;
    }
    .auto-style15 {
        height: 23px;
    }
    .auto-style16 {
        width: 175px;
    }
    .auto-style17 {
        text-decoration: underline;
    }
    .auto-style18 {
        width: 159px;
        height: 77px;
         text-align: center;
    }
    .auto-style19 {
        width: 680px;
        height: 30px;
         text-align: center;
    }
    .auto-style20 {
        height: 7px;
        text-align: left;
    }
    .auto-style21 {
        text-align: center;
    }
</style>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="auto-style10" colspan="3">
               <img class="auto-style18"  runat="server" id="companyLogo" src="" /></td>
      </tr>
      <tr>
           <td class="auto-style13" colspan="3">
               <img class="auto-style19"  runat="server" id="ApplicationHeadings" src="" /></td>
      </tr>
      <tr>
           <td class="auto-style2">To the Council of&nbsp; Chartered Accountants Ireland:


               <br />
               <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
               <asp:TextBox ID="TextBox14" runat="server" Width="71px"></asp:TextBox>
               <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
               <br />
               <asp:TextBox ID="TxtPerson" runat="server" Width="305px" Visible="False"></asp:TextBox>
               <br />
               of,<br />
               <br />
               <table class="auto-style4">
                   <tr>
                       <td>Telephone No:</td>
                       <td>
                           <asp:TextBox ID="Txttelephone" runat="server"></asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td>Date Of Birth</td>
                       <td>

                          <%-- <asp:TextBox ID="Txtbdate" runat="server"></asp:TextBox>--%>
                  
                        <telerik:RadDatePicker ID="rdpBirthdate" runat="server" Calendar-ShowOtherMonthsDays="false" DateInput-DateFormat="MM/dd/yyyy" MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false" AutoPostBack="false" />

                       </td>
                   </tr>
               </table>
               <br />
           </td>
           <td class="auto-style3" colspan="2">
               <table class="auto-style4">
                   <tr>
                       <td>FULL DETAILS IF INCORRECT:</td>
                   </tr>
                   <tr>
                       <td>(Block Capitals Please)</td>
                   </tr>
                   <tr>
                       <td>
                           <asp:TextBox ID="TxtLine1" runat="server" Width="423px"></asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td>
                           <asp:TextBox ID="TxtLine2" runat="server" Width="423px"></asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td>
                           <asp:TextBox ID="TxtLine3" runat="server" Width="423px"></asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td>
                           <asp:TextBox ID="TxtLine4" runat="server" Width="423px"></asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td>
                           <asp:TextBox ID="txtcitystae" runat="server" Width="423px"></asp:TextBox>
                       </td>
                   </tr>
               </table>
               <table class="auto-style4">
                   <tr>
                       <td class="auto-style5">Preferred Mobile No:</td>
                       <td>
                           <asp:TextBox ID="Txtpmobileno" runat="server" Width="273px"></asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td class="auto-style5">Preferred Landline No:</td>
                       <td>
                           <asp:TextBox ID="TxtLandlineNo" runat="server" Width="273px"></asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td class="auto-style5">Preferred Email:</td>
                       <td>
                           <asp:TextBox ID="TxtEmail" runat="server" Width="273px"></asp:TextBox>
                       </td>
                   </tr>
               </table>


           </td>
      </tr>
      <tr>
           <td class="auto-style13">&nbsp;</td>
           <td class="RightColumn" colspan="2">
               <table class="auto-style4">
                   <tr>
                       <td rowspan="2" class="auto-style6">Please Send all correspondence to my</td>
                       <td>
                           <asp:CheckBox ID="ChkOffice" runat="server" Text="Office" AutoPostBack="true" />
                       </td>
                   </tr>
                   <tr>
                       <td>
                           <asp:CheckBox ID="ChkHome" runat="server" Text="Home" AutoPostBack="true" />
                       </td>
                   </tr>
               </table>
           </td>
      </tr>
      <tr>
           <td class="auto-style1" colspan="3">hereby apply to be admitted as an Associate Member of Institute.<br />
               I hereby undertake that , If admitted as an associate Member of Institute,I will be bound by my provisions of Royal Charter and by
               <br />
&nbsp;the Bye-laws that are now in force or may hereafter from time to time be made. I hereby give the Information required by council.</td>
      </tr>
      <tr>
           <td class="auto-style1" colspan="3"><strong>SIGNATURE OF STUDENT: </strong>
               <asp:TextBox ID="TextBox12" runat="server" Width="316px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp; <strong>&nbsp; Date:<telerik:RadDatePicker ID="rdpformentrydate" runat="server" Calendar-ShowOtherMonthsDays="false" DateInput-DateFormat="MM/dd/yyyy" MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false" AutoPostBack="false" Enabled="False" />
               </strong></td>
      </tr>
      <tr>
           <td class="auto-style13">
               <table class="auto-style4">
                   <tr>
                       <td colspan="2"><strong>TRAINING<br />
                           <br />
                           <br />
                           </strong>I completed my training contract on:</td>
                   </tr>
                   <tr>
                       <td class="auto-style11">Expiry Date:</td>
                       <td class="auto-style12">
                          <telerik:RadDatePicker ID="RdpExpirydate" runat="server" Calendar-ShowOtherMonthsDays="false" DateInput-DateFormat="MM/dd/yyyy" MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false" AutoPostBack="false" /></td>
                   </tr>
                   <tr>
                       <td class="auto-style9">with,</td>
                       <td class="auto-style10"></td>
                   </tr>
                   <tr>
                       <td class="auto-style7">Firm Name:</td>
                       <td>
                           <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack ="true">
                           </asp:DropDownList>
                       </td>
                   </tr>
                   <tr>
                       <td class="auto-style7">Telephone No:</td>
                       <td>
                           <asp:TextBox ID="TxtCompphn" runat="server"></asp:TextBox>
                       </td>
                   </tr>
               </table>
           </td>
           <td class="RightColumn" colspan="2">
               <table class="auto-style4">
                   <tr>
                       <td colspan="2"><strong>CA DIARY OF PROFFESSIONAL DEVELOPMENT<br />
                           <br />
                           </strong></td>
                   </tr>
                   <tr>
                       <td class="auto-style8">
                           <asp:CheckBox ID="ChkFTR" runat="server" AutoPostBack=" true" Text="I enclose Final Training Reviews of Proffessional Development </br>and Declaration by Chartered Accountants of Ireland" />
                       </td>
                       <td>&nbsp;</td>
                   </tr>
                   <tr>
                       <td colspan="2"><strong>OR</strong></td>
                   </tr>
                   <tr>
                       <td class="auto-style8">
                           <asp:CheckBox ID="Chksummaryform" runat="server" AutoPostBack=" true" Text="I enclose the summary Form of record of experience" />
                       </td>
                       <td>&nbsp;</td>
                   </tr>
                   <tr>
                       <td colspan="2">(Note: The above form must be signed off by chartered Accountants Ireland <br />
                           Member (overhaed) at the Recognised Training Firm)</td>
                   </tr>
               </table>
           </td>
      </tr>
        <tr>
        <td class="auto-style13">
            <table class="auto-style14">
                <tr>
                    <td><strong>I.T. PROGRAMME</strong></td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Personal computing for Accountants(PCA)" />
                        ,or</td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Accounting in a Computer Environment " />
                        , or</td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Microcomputers for Accountants " />
                    </td>
                </tr>
                <tr>
                    <td>I passed this Programme on
                        <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>(Do not submit results)</td>
                </tr>
            </table>
            </td>
            <td colspan="2">
                <table class="auto-style14">
                    <tr>
                        <td><strong>COMPANY LAW MODULE<br />
                            </strong></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="CheckBox4" runat="server" Text="Exempt from module" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="CheckBox5" runat="server" Text="I passed company law module" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>(tick appropriate box)</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        <td class="auto-style13"><strong>NAME fOR&nbsp; CERTIFICATE<br />
            </strong>
            <asp:TextBox ID="TextBox17" runat="server" Width="344px"></asp:TextBox>
            </td><td class="auto-style16"><strong>OR</strong></td>
            <td>Please issue my certificate&nbsp; the following name:<br />
                <asp:TextBox ID="TextBox18" runat="server" Width="265px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td class="auto-style15" colspan="3">FINAL ADMITTING EXAMINATIONS<br />
            <br />
            I passed my FAE in
            <asp:TextBox ID="TextBox19" runat="server" Width="214px"></asp:TextBox>
            (Do not submit results)</td>
        </tr>
      <tr>
        <td class="auto-style21" colspan="3">
            <br />
          <br />
          </td>
      </tr>
      <tr>
        <td class="auto-style20" colspan="3">
               <img class="auto-style18"  runat="server" id="companyLogo0" src="" /></td>
      </tr>
      <tr>
        <td class="auto-style13">&nbsp;</td><td colspan="2">&nbsp;</td>
      </tr>
      <tr>
        <td class="auto-style13" colspan="3"><strong>CERTIFICATE TO BE COMPLETED AND SIGNED OFF BY CHARTERED ACCOUNTANTS IRELAND MEMBER IN RECOGNISED TRAINING FIRM</strong></td>
      </tr>
      <tr>
        <td class="auto-style13" colspan="3">I,<asp:TextBox ID="TextBox20" runat="server" Width="377px"></asp:TextBox>
&nbsp;being a Member of Institute, certify that<br />
            (Block capitals)<br />
            STUDENT,
            <asp:TextBox ID="TextBox21" runat="server" Width="306px"></asp:TextBox>
&nbsp;Completed his/her service satisfactorily under&nbsp; training Contract with me<br />
            Block capitals)<br />
            from
            <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
            20<asp:TextBox ID="TextBox23" runat="server" Width="16px"></asp:TextBox>
            to<asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
            20<asp:TextBox ID="TextBox25" runat="server" Width="16px"></asp:TextBox>
            for required period of&nbsp;
            <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox>
            years<br />
            .In my opinion he/she is a fit and proper person to be admitted to Associate Membership of the Institute.<br />
            <strong>SIGNATURE OF CHARTERED ACCOUNTANTS IRELAND MEMBER:<asp:TextBox ID="TextBox27" runat="server" Width="251px"></asp:TextBox>
&nbsp;FCA ACA<br />
            DATE:
            <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Important:Student surname must correspond with surname overleaf.&nbsp;&nbsp;&nbsp; </strong></td>
      </tr>
      <tr>
        <td class="auto-style13" colspan="3">
            <br />
            <strong>TRAINING IN BUSINESS<br />
            </strong>Having trained other than in a member firm of the Institute inpublic practice and under the conditions applicable to such training,<br />
            I undertake not to practice as public accountant or to seek to obtain admission as member in practice until I have furnished such<br />
&nbsp;information and have complied with such conditions relating thereto as may be prescribed by the council from time to time.<br />
            <br />
            <strong>SIGNATURE OF STUDENT: </strong>
               <asp:TextBox ID="TextBox29" runat="server" Width="316px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp; <strong>&nbsp; Date:<telerik:RadDatePicker ID="rdpformentrydate0" runat="server" Calendar-ShowOtherMonthsDays="false" DateInput-DateFormat="MM/dd/yyyy" MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false" AutoPostBack="false" Enabled="False" />
               <br />
               </strong>(To be completed by any applicant whose training contract was served in organisation in industry ,commerce or the public sector.)<br />
            <br />
          </td>
      </tr>
      <tr>
        <td class="auto-style13" colspan="3"><b>CURRENT EMPLOYMENT </b>
            <br />
            Firm Name&nbsp;&nbsp; :<asp:TextBox ID="TextBox30" runat="server" Width="651px"></asp:TextBox>
            <br />
            Firm Address:<asp:TextBox ID="TextBox31" runat="server" Width="651px"></asp:TextBox>
            <br />
            <br />
            Telephone no:<asp:TextBox ID="TextBox32" runat="server"></asp:TextBox>
&nbsp; Fax no :<asp:TextBox ID="TextBox33" runat="server"></asp:TextBox>
            <br />
            Job title&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<asp:TextBox ID="TextBox34" runat="server" Width="301px"></asp:TextBox>
&nbsp;Job Start Date:<asp:TextBox ID="TextBox35" runat="server"></asp:TextBox>
            <br />
            <asp:CheckBox ID="CheckBox6" runat="server" Text="Practicising Office" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox7" runat="server" Text="In Business" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Bussiness Category:<asp:TextBox ID="TextBox36" runat="server" Width="139px"></asp:TextBox>
            e.g. Banking,Manufacturing ,etc.</td>
      </tr>
              

    </table>
    <table runat="server" id="tblMain1" class="data-form">
      <tr>
        <td class="auto-style13"><strong>Failure to submit the required <span class="auto-style17">documentation duly 
            <br />
            signed together with appropriate fees</span> may delay your application being&nbsp; approved by officer Group.<br />
            This checklist will assist you in submitting your application.<br />
            CHECK LIST FOR COMPLETION OF THIS FORM </strong></td><td rowspan="8"><strong>OFFICE USE<br />
              <br />
              </strong>
              <asp:TextBox ID="TextBox37" runat="server"></asp:TextBox>
&nbsp;Date acknowledgement sent<br />
              <asp:TextBox ID="TextBox38" runat="server"></asp:TextBox>
              Council Approveal date<br />
              <br />
              Amount Receivbed<br />
              Membership Joining fee&nbsp; Euro<asp:TextBox ID="TextBox39" runat="server"></asp:TextBox>
              sig<asp:TextBox ID="TextBox40" runat="server"></asp:TextBox>
              <br />
              <br />
              Price year(s)<br />
              subscription(s)&nbsp; Euro<asp:TextBox ID="TextBox41" runat="server"></asp:TextBox>
              sig<asp:TextBox ID="TextBox42" runat="server"></asp:TextBox>
              <br />
              (if applicable)<br />
              <br />
              Chartered Accountants Ireland Reviewer:
              <br />
              <asp:TextBox ID="TextBox43" runat="server" Width="384px"></asp:TextBox>
              <br />
              Date:<asp:TextBox ID="TextBox44" runat="server"></asp:TextBox>
              <br />
          </td>
      </tr>
      <tr>
        <td class="auto-style13">
            <asp:CheckBox ID="CheckBox8" runat="server" Text="Student signature for Officer Group" />
          </td>
      </tr>
      <tr>
        <td class="auto-style13">
            <asp:CheckBox ID="CheckBox9" runat="server" Text="Final Review CA/Summary Form of Record of Experience" />
          </td>
      </tr>
      <tr>
        <td class="auto-style13">
            <asp:CheckBox ID="CheckBox10" runat="server" Text="change of name" />
          </td>
      </tr>
      <tr>
        <td class="auto-style13">
            <asp:CheckBox ID="CheckBox11" runat="server" Text="Current Employer" />
          </td>
      </tr>
      <tr>
        <td class="auto-style13">
            <asp:CheckBox ID="CheckBox12" runat="server" Text="Signature of chartered accountants Ireland Member" />
            <br />
            (Recognised Training Firm)</td>
      </tr>
      <tr>
        <td class="auto-style13">
            <asp:CheckBox ID="CheckBox13" runat="server" Text="TIB:Signature of student " />
&nbsp;(if applicable)</td>
      </tr>
      <tr>
        <td class="auto-style13">&nbsp;</td>
      </tr>
 </table>
    <table runat="server" style="width: 853px" >
        <tr>
            <td>                    <strong>MEMBERSHIP JOINING FEE (MJF)</strong><br />
                A remittance for the amount of MJF: Euro<asp:TextBox ID="TextBox45" runat="server"></asp:TextBox>
                stg<asp:TextBox ID="TextBox46" runat="server"></asp:TextBox>
&nbsp;together with prior year(s) subscriptions
                <br />
                (if applicable) Euro<asp:TextBox ID="TextBox47" runat="server"></asp:TextBox>
                stg<asp:TextBox ID="TextBox48" runat="server"></asp:TextBox>
                must accompany this application. Enclosed is cheque/money order/credit card number for the TOTAL AMOUNT of Euro<asp:TextBox ID="TextBox49" runat="server"></asp:TextBox>
                stg<asp:TextBox ID="TextBox50" runat="server"></asp:TextBox>
                .Please charge my Master/visa
                <br />
                Euro<asp:TextBox ID="TextBox51" runat="server"></asp:TextBox>
                stg<asp:TextBox ID="TextBox52" runat="server"></asp:TextBox>
&nbsp;to Credit Card No.<asp:TextBox ID="TextBox53" runat="server" Width="241px"></asp:TextBox>
                <br />
                Expiry Date:<asp:TextBox ID="TextBox54" runat="server"></asp:TextBox>
            </td>
            </tr>
  
        
        </table>
    <table><tr><td><asp:Button ID="cmdSave" Runat="server" Text="Save Record" style="text-align: center"></asp:Button>
            </td></tr></table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
