<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/LLLPostEnrolmentRequest__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLPostEnrolmentRequest__c" %>
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
    &nbsp;
<div class="info-data cai-form">
  <div class="form-title"  id="HeadEducationRoute" >Post Enrolment Changes Request Form</div>
                
               
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode ="Always" >
                       <ContentTemplate>
                <asp:Panel ID="pnlDetails" runat="server">
                 
                    
  
            <div id="Eligibility" class="cai-form-content" >
                        
                      
                      

                        <div class="row-div clearfix field-content">
                <div class="label-div w30">
                    &nbsp;
                </div>
              
                <div class="field-div1 w100">
                     <div class="row-div clearfix field-content" id="div5" runat="server" >
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Type :</b>
                            </div>
                            <div class="field-div1 w17" style="text-align: left;">
                             <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" Width="99%">
                                                </asp:DropDownList>
                                         
                                                
                            </div>
                                &nbsp &nbsp<asp:Label ID="lbltypecomp" runat="server" ForeColor="Red"></asp:Label>   
                        </div>
                        <div class="row-div clearfix field-content" id="divFirstName" runat="server" >
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Student Number :</b>
                            </div>
                            <div class="field-div1 w17" style="text-align: left;">
                              <asp:Label ID="lblStudentno" runat="server" Text=""></asp:Label>
                                 <asp:Label ID="lblStudentID" runat="server" Visible="false" Text=""></asp:Label>
                            </div>

                        </div>
                        <div class="row-div clearfix field-content" id="div2" runat="server" >
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Name:</b>
                            </div>
                            <div class="field-div1 w17" style="text-align: left;">
                              <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                            </div>

                          
                        </div>
                   <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode ="Always" >
                       <ContentTemplate >--%>
                         
         
                        <div class="row-div clearfix field-content" id="divLastName" runat="server">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Qualification:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:Label ID="lblQualification" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="lblQualificationID" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix field-content">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Comments:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                           <asp:TextBox ID="txtcomment" Width="350px" MaxLength="50" runat="server" Height="150"
                                    TextMode="MultiLine" Style="resize: none; margin-left: 50px"></asp:TextBox>
                            </div>
                        </div>
                        
                        

                        

                         <div class="row-div clearfix">
                           
                           
                            
                        </div>


                        
                    </div>
                </div>
                 
                
            </div>


            


            <div class="info-data cai-form-content">
                        <div class="row-div clearfix">
                        </div>
                        <div class="row-div clearfix field-content">
                            <div class="label-div-left-align w80">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="submitBtn"/>&nbsp; <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="submitBtn" />&nbsp<asp:Label ID="lblsubmit" runat="server" ForeColor="Red"></asp:Label>
                         </div>
                               
                               
                            </div>
                        </div>
                    
                 <rad1:RadWindow ID="RadAlert" runat="server" Width="250px" Height="150px" Modal="True"
    Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
    ForeColor="#BDA797"  Title="Success" Behavior="None"><ContentTemplate>
  
       
    <table width="100%">
     
    <tr ><td align="center" height="25">Post Enrolment Request submitted successfully! </td></tr><tr ><td align="center" height="25"><asp:Button ID="btnOK" runat="server" Text="Ok" CssClass="submitBtn"
                        CausesValidation="false" Height="23px" Width="60px" /></td></tr></table></ContentTemplate></rad1:RadWindow>

    

     
   <div>
      </asp:Panel>
   </ContentTemplate> 
   </asp:UpdatePanel>
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
 </div> 
 



 
 


    
