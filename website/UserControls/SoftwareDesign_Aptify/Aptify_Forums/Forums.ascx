<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Forums/Forums.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.ForumsControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="PageSecurity" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<table class="Forums">
    <tr runat="server" id="trHeaderRow">
        <td>
            <h2>Sub Forums</h2>
        </td>
    </tr>
    <tr>
        <td><br />
<asp:DataList id="lstForums" skinid="lstForums2" runat="server"  >
    <HeaderTemplate>
    <table>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
      <%="<img src=""" & Me.ForumImage%><%# CStr(DataBinder.Eval(Container.DataItem, "Type")).Trim & ".gif"">"%>
      
        <%#"<a href=" & Chr(34) & Me.ForumPage & "?ID=" & _
                    CStr(DataBinder.Eval(Container.DataItem, "ID")) & Chr(34) & ">"%>
        <%#DataBinder.Eval(Container.DataItem, "NameWCount")%>
        </a> 
      <br> 
        <%# DataBinder.Eval(Container.DataItem,"Description")%>       
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
  </asp:DataList>
        
        </td>
    </tr>
</table>

<table id="tabForums" runat="server" >
</table>

<cc2:User id="User1" runat="server"  />