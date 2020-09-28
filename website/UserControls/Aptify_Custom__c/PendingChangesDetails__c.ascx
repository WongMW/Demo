<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PendingChangesDetails__c.ascx.vb" Debug="true"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.PendingChangesDetails__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix" id="divTop" runat="server">
   
   <table width="100%">
      <tr>
          <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <rad:RadGrid ID="rgvPendingchange"  runat="server" AutoGenerateColumns="False" 
                       allowpaging="true" sortingsettings-sortedasctooltip="Sorted Ascending"  pagesize="5" pagerstyle-pagesizelabeltext="Records Per Page" CssClass="cai-table">
                                    <mastertableview>
                                           <Columns>
                                              
                                              <rad:GridBoundColumn DataField="FieldName" HeaderText="Field" />
                                              <rad:GridBoundColumn DataField="Changes" HeaderText="Current Value" /> 
                                              <rad:GridBoundColumn DataField="NewValue" HeaderText="Proposed Value" />
                                              <rad:GridBoundColumn DataField="ScheduledDate" HeaderText="Scheduled Date" Visible="false"/>
                                      <%--    <rad:GridBoundColumn DataField="Type" HeaderText="Specific Type" /> --%>
                                                           
                                          </Columns>
                             </mastertableview>
                       </rad:RadGrid>  
                       
                       
                                                                                              
                   </ContentTemplate>
                                          
               </asp:UpdatePanel > 
      
          </td>
      </tr>
    
   </table>
          
       
    
      
</div>


