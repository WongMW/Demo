<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.Conferring__c" CodeFile="~/UserControls/Aptify_Custom__c/Conferring__c.ascx.vb" Debug="true" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div class="content-container clearfix">
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td colspan="2">
                <%--    <asp:Button id="cmdNewRecord" Text="New Conferring Record" Runat="server" /> --%>
            </td>
        </tr>
        <tr>
            <td colspan="2">


                <asp:GridView ID="grdMain" AutoGenerateColumns="False" runat="server" AllowPaging="false"
                    SkinID="test" Width="100%" GridLines="Horizontal" BorderColor="#CCCCCC" BorderWidth="1px"
                    AlternatingRowStyle-BackColor="White">
                    <HeaderStyle CssClass="GridViewHeader" Height="28px" HorizontalAlign="Center" Font-Bold="true" />
                    <RowStyle CssClass="GridItemStyle" BackColor="#e5e2dd" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="Lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Person">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PersonName")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="DateCreated" HeaderText="Date Created" />

                        <asp:BoundField DataField="DateOpenUntil" HeaderText="Date Open Until" />



                        <asp:TemplateField HeaderText="Meeting">
                            <ItemTemplate>
                                <asp:Label ID="Lblmeet" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Meeetingtitle")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Certificate Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txtcert" runat="server" Width="300px"/>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcert"
                                            Display="Dynamic" ErrorMessage="Certificate Name Required" Font-Size="X-Small" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                    <%--  <PagerSettings Mode="Numeric" />--%>
                </asp:GridView>
            </td>
        </tr>
        <tr>
           

            <td style="text-align: center">
                <asp:Button ID ="Submit" runat="server" Text="Submit" style="margin-left: 400px" /></td>

                 <td>&nbsp;</td>
        </tr>
    </table>

    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
</div>
