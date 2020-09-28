<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JS.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.JobSearch.JS" %>
                

 <div class="container">
     
    <%--<telerik:RadScriptManager runat="server" ID="RadScriptManager1" />--%>
<%--    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />--%>
   <%-- <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />--%>
 <%--   <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />--%>
    <div class="demo-container no-bg">
        <h2>Simple Data Binding:</h2>
       <telerik:RadAjaxPanel runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGrid1" AllowPaging="True"  
            AllowFilteringByColumn="True" AllowSorting="True" Width="100%"
                ShowFooter="True" OnNeedDataSource="RadGrid1_NeedDataSource" >
          <GroupingSettings CaseSensitive="false"></GroupingSettings>
           <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True">
               <Columns>
                   <telerik:GridBoundColumn FilterControlWidth="120px" DataField="EventName" HeaderText="EventName" DataType="System.String"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                            ShowFilterIcon="false" >
                        </telerik:GridBoundColumn>
               </Columns>
           </MasterTableView>
        </telerik:RadGrid>
       </telerik:RadAjaxPanel>
        <br />

<%--        <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGrid2" AllowPaging="True" AllowSorting="true"
            OnNeedDataSource="RadGrid2_NeedDataSource">
        </telerik:RadGrid>--%>
    </div>

</div>
<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:APTIFYConnectionString %>" SelectCommand="spGetEventList__cai" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
 <script type="text/javascript">

</script>