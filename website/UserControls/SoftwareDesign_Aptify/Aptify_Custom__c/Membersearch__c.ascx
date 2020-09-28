<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/Membersearch__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Membersearch__c" %>
<div  id="tblMain"  class="cai-form">
    <p>
        This members' directory may not be used for purposes of circulation and no part
        may be reproduced or transmitted in any form or by any means, electronic or mechanical,
        or through any information storage retrieval system, whether temporary or permanent.
        Use of this material is bound by data protection legislation.
    </p>
    <p>
        The Directory is not be used as a basis for commercial approaches to other members.
        Any unauthorized use by members or third parties is strictly forbidden and may result
        in legal action. Chartered Accountants Ireland accepts no liability in this regard.
    </p>
<div>
    <table>
        <tr>
            <td>
                <asp:DropDownList ID="ddlyear" runat="server"  Width="300px">
                </asp:DropDownList>
                <asp:Button ID="btnsearchmember" runat="server" Text="Search Members" class="submitBtn" />
            </td>
        </tr>
        <tr>
            <td>
                OR Search By Surname
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtlastname" runat="server" Width="300px"></asp:TextBox>
                <asp:Button ID="btnsearchbyname" runat="server" Text="Search Members" class="submitBtn"/>
            </td>
        </tr>
        <tr>
        <td> Search Results
        </td>
        </tr>
        <tr>
        <td>
        <asp:Repeater ID="rptSearchDetails" runat="server">
        <HeaderTemplate>
            <table cellspacing="0" rules="all" border="1">
                <tr>
                    <th scope="col" style="width: 1000px">
                        Member Name
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%-- <asp:Label ID="lblmembers" text="<%# DataBinder.Eval(Container.DataItem, "type") %>" runat="server"/>--%>
                    <%# DataBinder.Eval(Container.DataItem, "FirstLast")%>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <asp:LinkButton ID="lnkBtnPrev" runat="server" Font-Underline="False" OnClick="lnkBtnPrev_Click"
        Font-Bold="True"><< Prev </asp:LinkButton>

    <input id="txtHidden" style="width: 28px" type="hidden" value="1" runat="server" />
    <asp:LinkButton ID="lnkBtnNext" runat="server" Font-Underline="False" OnClick="lnkBtnNext_Click"
        Font-Bold="True">Next >> </asp:LinkButton>
        </td>
        </tr>
    </table>
<br />
      </div> 
</div>