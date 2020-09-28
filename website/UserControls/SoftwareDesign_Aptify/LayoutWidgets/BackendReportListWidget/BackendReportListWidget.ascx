<%@ Control Language="C#" %>

<div class="sfContent">
    <div class="sfWorkArea">
        <strong>
            <h1 id="MessageLabel" text="Text" runat="server"></h1>
        </strong>
        <br />
        <label>Start date</label>
        <input type="date" data-date-format="DD/MMMM/YYYY" id="startDateInput" />
        <label>End date</label>
        <input type="date" data-date-format="DD/MMMM/YYYY" id="endDateInput" />

        <asp:HiddenField runat="server" ID="startDate" />
        <asp:HiddenField runat="server" ID="endDate" />

        <br />
        <br />
        <asp:Button ID="viewNegativeVotes" runat="server" class="btn cai-btn" Text="View Latest Negative Votes"></asp:Button>
        <asp:Button ID="viewPositiveVotes" runat="server" class="btn cai-btn" Text="View Latest Positive Votes"></asp:Button>
        <asp:Button ID="viewAllVotes" runat="server" class="btn cai-btn" Text="View All Pages"></asp:Button>
        <asp:Button ID="viewTopPositiveVotes" runat="server" class="btn cai-btn" Text="View Top 10 pages with positive Votes"></asp:Button>
        <asp:Button ID="viewTopNegativeVotes" runat="server" class="btn cai-btn" Text="View Top 10 pages with negative Votes"></asp:Button>
        <br />
        <br />
        <asp:TextBox ID="searchText" runat="server" type="text" placeholder="Search..."></asp:TextBox>
        <asp:Button ID="searchBtn" runat="server" class="btn cai-btn" Text="Search"></asp:Button>
        <br />
        <br />
        <asp:GridView ID="ReportGrid" runat="server" AutoGenerateColumns="false" AllowPaging="false">
            <Columns>
                <asp:TemplateField HeaderText="Page Title">
                    <ItemTemplate>
                        <asp:LinkButton ID="pageLink" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PageTitle")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PageURL" HeaderText="Page URL" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:GridView ID="YesGridView" runat="server" AutoGenerateColumns="false" AllowPaging="false" Visible="false"
            Caption='<strong>Yes Votes</strong>' CaptionAlign="Top">
            <Columns>
                <asp:BoundField DataField="PageTitle" HeaderText="Page Ttile" />
                <asp:BoundField DataField="PageURL" HeaderText="Page URL" />
                <asp:BoundField DataField="PublicationDate" HeaderText="Date" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:GridView ID="NoGridView" runat="server" AutoGenerateColumns="false" AllowPaging="false" Visible="false"
            Caption='<strong>No Votes</strong>' CaptionAlign="Top">
            <Columns>
                <asp:BoundField DataField="PageTitle" HeaderText="Page Ttile" />
                <asp:BoundField DataField="PageURL" HeaderText="Page URL" />
                <asp:BoundField DataField="Comments" HeaderText="Comments" />
                <asp:BoundField DataField="PublicationDate" HeaderText="Date" />
            </Columns>
        </asp:GridView>
    </div>
</div>

<script src="https://code.jquery.com/jquery-1.11.3.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {

        var startDateValue = $("#<%=startDate.ClientID%>").val();
        var endDateValue = $("#<%=endDate.ClientID%>").val();
        $("#startDateInput").val(startDateValue);
        $("#endDateInput").val(endDateValue);

        $("#startDateInput").on("change", function () {
            $("#<%=startDate.ClientID%>").val($(this).val());
         })
        $("#endDateInput").on("change", function () {
            $("#<%=endDate.ClientID%>").val($(this).val());
        })
    });




</script>

