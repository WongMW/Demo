<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>
<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView" style="max-height: 400px; overflow: auto;">
    <ol>
       <li class="sfFormCtrl">
            <label class="sfTxtLbl" for="selectedTestLabel">Testimonial Selector</label>
            <div id="ImagesSelector">
                <div class="left">
                    <sf:FlatSelector ID="ItemSelector" runat="server" ItemType="Telerik.Sitefinity.DynamicTypes.Model.Testimonials.Testimonial"
                        DataKeyNames="Id" ShowSelectedFilter="true" AllowPaging="true" PageSize="3" AllowMultipleSelection="true"
                        AllowSearching="true" ShowProvidersList="false" InclueAllProvidersOption="false" EnablePersistedSelection="true"
                        SearchBoxTitleText="Filter by Title" ShowHeader="true" ServiceUrl="~/Sitefinity/Services/DynamicModules/Data.svc/?itemType=Telerik.Sitefinity.DynamicTypes.Model.Testimonials.Testimonial">
                        <DataMembers>
                            <sf:DataMemberInfo runat="server" Name="Title" IsExtendedSearchField="true" HeaderText='Title'>
                                <strong>{{Name.Value}}</strong>
                            </sf:DataMemberInfo>
                            <sf:DataMemberInfo runat="server" Name="Quote" IsExtendedSearchField="true" HeaderText='Quote'>
                                <strong>{{Quote.Value}}</strong>
                            </sf:DataMemberInfo>
                        </DataMembers>
                    </sf:FlatSelector>
                </div>
                <asp:Panel runat="server" ID="buttonAreaPanel" class="sfButtonArea sfSelectorBtns">
                    <asp:LinkButton ID="lnkDone" runat="server" OnClientClick="return false;" CssClass="sfLinkBtn sfSave">
                <strong class="sfLinkBtnIn">
                    <asp:Literal runat="server" Text="<%$Resources:Labels, Done %>" />
                </strong>
                    </asp:LinkButton>
                    <asp:Literal runat="server" Text="<%$Resources:Labels, or%>" />
                    <asp:LinkButton ID="lnkCancel" runat="server" CssClass="sfCancel" OnClientClick="return false;">
                <asp:Literal runat="server" Text="<%$Resources:Labels, Cancel %>" />
                    </asp:LinkButton>
                </asp:Panel>
            </div>
            <ul id="selectedItemsList" runat="server" data-template="ul-template-ImagesSelector" data-bind="source: items" class="sfCategoriesList"></ul>
            <script id="ul-template-ImagesSelector" type="text/x-kendo-template">
                <li>
                    <asp:Label runat="server" CssClass="sfTxtLbl">Name</asp:Label>
                    <span data-bind="text: Name.Value"> </span>

                    <asp:Label runat="server" CssClass="sfTxtLbl">Quote</asp:Label>
                    <span data-bind="text: Quote.Value"> </span>
                    <br/>
                    <a class="remove sfRemoveBtn">Remove</a>
                </li>
            </script>
            <asp:HyperLink ID="selectButton" runat="server" NavigateUrl="javascript:void(0);" CssClass="sfLinkBtn sfChange">
    <strong class="sfLinkBtnIn">Add items...</strong>
            </asp:HyperLink>
        </li>
    </ol>
</div>
