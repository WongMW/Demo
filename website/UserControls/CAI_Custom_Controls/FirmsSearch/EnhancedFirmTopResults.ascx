<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnhancedFirmTopResults.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedFirmTopResults" %>

<h2 class="h2-small float-left">Premium search results:</h2>

<div class="primary-directory-search">
    <table>
        <%-- REPEATER START for Enhanced listings, allow to show max of 3 rows at a time --%>
        <asp:Repeater runat="server" ID="resultsTable">
            <ItemTemplate>
                <tr>
                    <td class="cell-1">
                        <%--<img src='<%# DataBinder.Eval(Container.DataItem, "Image") %>' class="firm-logo" />--%>
						<asp:Image ID="Image1"  runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "Image") %>'  class="firm-logo" /> 

						
                    </td>
                    <td class="cell-2">
                        <span class="firm-name"><%# DataBinder.Eval(Container.DataItem, "Title") %></span>
                    </td>
                    <td class="cell-3">
                        <span class="mob-title">Location:</span>
                        <span class="mob-details"><%# DataBinder.Eval(Container.DataItem, "City") %></span>
                    </td>
                    <td class="cell-4">
                        <span class="mob-title">Phone:</span>
                        <span class="mob-details"><%# DataBinder.Eval(Container.DataItem, "Phone") %></span>
                    </td>
                    <td class="cell-5">
                        <span class="mob-title">Link:</span>
                        <span class="mob-details">
                            <a runat="server" visible='<%# !String.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Url").ToString()) %>' href='<%# DataBinder.Eval(Container.DataItem, "Url") %>' target="_blank">
                                <%# DataBinder.Eval(Container.DataItem, "Url").ToString().Replace("https://", "").Replace("http://", "") %>
                            </a>
                            <span runat="server" visible='<%# String.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Url").ToString()) %>'>
                                N/A
                            </span>
                        </span>
                    </td>
                    <td class="cell-6">
                        <a href="<%# DataBinder.Eval(Container.DataItem, "SinglePageUrl").ToString() %>" class="follow-arrow"></a><%-- Link should go to Firm Details page when clicked --%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>
