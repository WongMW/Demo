<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/LLLExemptionRequests__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLExemptionRequests__c" %>
<%@ Register TagPrefix="rad1" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

    <%@ Register Src="~/UserControls/Aptify_Custom__c/BeforeSaveRecordAttachments__c.ascx" TagName="SMAARecordAttachments"

    TagPrefix="uc2" %>
    <style type="text/css">
    .active
    {
        display: block;
    }
    .inactive
    {
        display: none;
    }
    .collapse
    {
        display: none;
    }
    .expand
    {
        cursor: pointer;
    }
    .ui-draggable .ui-dialog-titlebar
    {
        background-image: none;
        background-color: rgb(231, 210, 182);
        color: Black;
        border: 1px solid #F4F3F1;
    }
    .ui-widget-content
    {
        border: 1px solid #F4F3F1;
    }
    .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default
    {
        background-image: none;
        background-color: blue;
        color: white;
        border: none;
    }
    .ui-state-default:hover
    {
        background-image: none;
        background-color: blue;
        color: white;
        border: none;
        font-weight: bolder;
    }
    .ui-dialog-content ui-widget-content
    {
        border: 1px solid #F4F3F1;
    }
    
    .ui-icon:hover
    {
        background-color: Blue;
    }
    
    .ui-dialog-buttonset
    {
        margin-right: 50%;
    }
</style>

<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 600px;">
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
<div class="info-data">
  <h2  id="HeadEducationRoute" >
                        </h2>
                        <br />
                  
  
<asp:Panel ID="pnl" runat="server">
                 <div class="row-div clearfix">
                       <div>
                            <asp:Label ID="lblDesc" runat="server" ></asp:Label>   

                        </div>
               
                        <div class="row-div clearfix" id="div4" runat="server">
                            <div style="text-align: left;" class="field-div1 w17"   >
                                <b>Exemption Reqeust :</b>
                            </div>
                            <div class="field-div1 w12" style="text-align: left;">
                              <asp:RadioButton ID="rdException" runat="server" AutoPostBack="true" GroupName="grp2"  >
                                                </asp:RadioButton>
                            </div>
                            <div class="row-div clearfix" id="div6" runat="server">
                            <div style="text-align: left;" class="field-div1 w17">
                                <b>Enrollment Request:</b>
                            </div>
                            <div class="field-div1 w12" style="text-align: left;">
                              <asp:RadioButton ID="rdEnroll" runat="server" AutoPostBack="true" GroupName="grp2" >
                                                </asp:RadioButton>
                            </div>
                  
                        
                  </div>
                  </div>
                   </div>
                </asp:Panel>
             
              

 <asp:Panel ID="PnlExemption" runat="server">

 <div class="row-div clearfix" id="div5"  >
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Type :</b>
                            </div>
                            <div class="field-div1 w17" style="text-align: left;">
                             <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" Width="102%">
                                                </asp:DropDownList>
                                                
                                         
                                                
                            </div>
                                &nbsp &nbsp<asp:Label ID="lbltypecomp" runat="server" ForeColor="Red"></asp:Label>   
                        </div>

                        <div class="row-div clearfix" id="div7" runat="server" visible="false">

                            <div style="text-align: left;" class="field-div1 w17">
                                <b>CTC Full Exemption:</b>
                            </div>
                            <div class="field-div1 w12" style="text-align: left;">
                              <asp:RadioButton ID="rdFullExemption" runat="server" AutoPostBack="true" GroupName="grp1" >
                                                </asp:RadioButton>
                            </div>
                            <div class="row-div clearfix" id="div8" runat="server">
                            <div style="text-align: left;" class="field-div1 w17">
                                <b>CTC Partial  Exemption:</b>
                            </div>
                            <div class="field-div1 w12" style="text-align: left;">
                            <asp:RadioButton ID="rdPartialExemption" runat="server" AutoPostBack="true" GroupName="grp1"  >
                                                </asp:RadioButton>
                            </div>
                        </div>
                        
                  </div>
              

                </asp:Panel>
               
               

                <asp:Panel ID="pnlDetails" runat="server">
                 
                    
  
            <div id="Eligibility" runat="server">
                        
                      
                      

                        <div class="row-div clearfix">
                <div class="label-div w30">
                    &nbsp;
                </div>
                  
            
                <div class="field-div1 w100">
                   
                        <div class="row-div clearfix" id="divFirstName" runat="server" >
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Student Number :</b>
                            </div>
                            <div class="field-div1 w17" style="text-align: left;">
                              <asp:Label ID="lblStudentno" runat="server" Text=""></asp:Label>
                                 <asp:Label ID="lblStudentID" runat="server" Visible="false" Text=""></asp:Label>
                            </div>

                        </div>
                        <div class="row-div clearfix" id="div2" runat="server" >
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Name:</b>
                            </div>
                            <div class="field-div1 w17" style="text-align: left;">
                              <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                            </div>

                          
                        </div>
             
                       
                        <div class="row-div clearfix" id="divLastName" runat="server">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Qualification:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:Label ID="lblQualification" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="lblQualificationID" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Comments:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                           <asp:TextBox ID="txtcomment" Width="350px" MaxLength="50" runat="server" Height="150"
                                    TextMode="MultiLine" Style="resize: none; margin-left: -10px"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="row-div clearfix" id="divIsmember" runat="server">
                            <div style="text-align: left;" class="field-div1 w12">
                              
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                               
                             <asp:CheckBox ID="chkIsOtherTaxationBodyMember" runat="server"></asp:CheckBox>&nbsp;  <b>Are you Member of other taxation bodies</b>
                           
                            </div>

                        </div>

                        <div class="row-div clearfix" id="divIsnonsmember" runat="server">
                            <div style="text-align: left;" class="field-div1 w12">
                                
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                               <asp:CheckBox ID="chkIsOtherTaxationBodyMemberFull_Non" runat="server" Visible="false"></asp:CheckBox>
                             
                            </div>
                             <div class="row-div clearfix" id="div3" runat="server">
                             <div style="text-align: left;" class="field-div1 w12">
                                <b>Accountancy Body:</b>
                            </div>
                             <div class="field-div1 w17" style="text-align: left;">
                                <asp:DropDownList ID="drpAccountancy" runat="server" AutoPostBack="true" Width="102%">
                                                </asp:DropDownList>
                         
                            </div>
                            </div>
                        </div>

                         <div class="row-div clearfix">
                           
                           
                            
                        </div>


                        
                    </div>
                </div>
                 
                
            </div>


            <div class="row-div clearfix" id="divupload" runat="server">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Upload:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                 <div runat="server" id="divAttachments" style="font-size: 8pt; color: Black;
                                font-style: normal; font-weight: normal; width: 80%;">
                                <div>
                                    <span class="Error">
                                        <asp:Label ID="lblErrorFile" Visible="false" runat="server">Error</asp:Label></span></div>
                                <div>
                                    <uc2:SMAARecordAttachments ID="raSupportLetter" Visible="true" runat="server"></uc2:SMAARecordAttachments>
                                </div>
                            </div>
                        </div>
                         </div>



            <div class="info-data">
                        <div class="row-div clearfix">
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div-left-align w80">
                                <asp:Button ID="btnBack" runat="server" Text="Back" />&nbsp; <asp:Button ID="btnSubmit" runat="server" Text="Submit" />&nbsp<asp:Label ID="lblsubmit" runat="server" ForeColor="Red"></asp:Label>
                         </div>
                           
                               
                            </div>
                        </div>
                    
                 <telerik:RadWindow ID="RadAlert" runat="server" Width="350px" Modal="True" BackColor="#f4f3f1"
            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Life Long Learning Request Application"
            Behavior="None" Height="150px">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                    padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblsuccess" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div>
                                <br />
                            </div>
                            <div>
                                <asp:Button ID="btnOK" runat="server" Text="Ok" Width="70px" class="submitBtn"  CausesValidation="false"/>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </telerik:RadWindow>

    

     
   <div>
      </asp:Panel>
   
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
    
</div>


 
 


    