<%@ Control Language="C#" %>
<asp:Label ID="MessageLabel" Text="Text" runat="server" Visible="false" />

<div runat="server" class="sf_cols">
    <div runat="server" class="sf_colsOut sf_1cols_1_100">
        <div runat="server" class="sf_colsIn sf_2cols_1in_100">
            <div class="report-wrapper">
                <div class="report-content">
                    <h2>Was this article helpful?</h2>
                </div>
                <div id="HelpfulForm" runat="server" class="report-actions">
                    <asp:LinkButton ID="YesVote" runat="server" CssClass="btn-article yesVote">yes</asp:LinkButton>
                    <asp:LinkButton ID="NoVote" runat="server" class="btn-article noVote">no</asp:LinkButton>
                    <div id="CommentHolder" runat="server" Visible="false">
                        <asp:Label runat="server">Comments</asp:Label>
                        <asp:TextBox ID="CommentBox" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="SubmitButton" runat="server" class="btn-article">Submit</asp:LinkButton>
                    </div>
                </div>
                <asp:Label id="Message" runat="server" Visible="false">Thank you for your feedback</asp:Label>
            </div>
        </div>
    </div>
</div>