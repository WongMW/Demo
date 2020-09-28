<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Surveys/Surveys.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Knowledge.Surveys" %>
<%@ Register TagPrefix="cc1" Assembly="AptifyKnowledgeWebControls" Namespace="Aptify.Framework.Web.eBusiness.Knowledge.Controls" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <cc3:User runat="server" ID="User1" />
                <div style="border-collapse: separate;" />
                <cc1:QuestionTreeListControl ID="QuestionTreeList" runat="server" AllowSorting="True"
                    AutoGenerateColumns="False" PersonID="-1" AlternatingItemStyle-BackColor="White"
                    ItemStyle-BackColor="#e5e2dd" Width="100%" BorderStyle="None">
                    <HeaderStyle CssClass="GridViewHeader" Height="28px" HorizontalAlign="Center" Font-Bold="true" />
                    <FooterStyle CssClass="GridFooter" />
                    <ItemStyle CssClass="GridItemStyle" BackColor="#e5e2dd" />
                    <Columns>
					    <asp:TemplateColumn HeaderText="Category">
                        <HeaderStyle BorderStyle="None"  HorizontalAlign="Left"  />
                            <ItemStyle BorderStyle="None" CssClass="griditempaddingSurvey" />
						    <ItemTemplate>
							    <%#GetCategoryName(Container)%>
						    
</ItemTemplate>
					    </asp:TemplateColumn>
					    <asp:TemplateColumn HeaderText="Name">
                          <HeaderStyle BorderStyle="None"  HorizontalAlign="Left" />
                            <ItemStyle BorderStyle="None" Font-Bold="true" CssClass="griditempaddingSurvey" />
						    <ItemTemplate>
							    <%# GetQuestionTreeName(Container) %>
						    
</ItemTemplate>
					    </asp:TemplateColumn>
					    <asp:TemplateColumn HeaderText="Description">
                          <HeaderStyle BorderStyle="None"  HorizontalAlign="Left" />
                            <ItemStyle BorderStyle="None" CssClass="griditempaddingSurvey" />
						    <ItemTemplate>
							    <%# GetQuestionTreeDescription(Container) %>
						    
</ItemTemplate>
					    </asp:TemplateColumn>
					    <asp:TemplateColumn Visible="False" HeaderText="Tracking Type">
                          <HeaderStyle BorderStyle="None"  HorizontalAlign="Left"  />
                            <ItemStyle BorderStyle="None" CssClass="griditempaddingSurvey" />
						    <ItemTemplate>
							    <%# GetTrackingTypeName(Container) %>
						    
</ItemTemplate>
					    </asp:TemplateColumn>
					    <asp:TemplateColumn Visible="False" HeaderText="Complete">
                          <HeaderStyle BorderStyle="None"  HorizontalAlign="Left"  />
                            <ItemStyle BorderStyle="None" CssClass="griditempaddingSurvey" />
						    <ItemTemplate>
							    <%# GetSessionIsCompleted(Container) %>
						    
</ItemTemplate>
					    </asp:TemplateColumn>
					    <asp:TemplateColumn Visible="False" HeaderText="Start New">
                          <HeaderStyle BorderStyle="None"  HorizontalAlign="Left"  />
                            <ItemStyle BorderStyle="None" CssClass="griditempaddingSurvey" />
						    <ItemTemplate>
							    <%# GetNewResultLink(Container) %>
						    
</ItemTemplate>
					    </asp:TemplateColumn>
                    </Columns>
                </cc1:QuestionTreeListControl>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</div>
