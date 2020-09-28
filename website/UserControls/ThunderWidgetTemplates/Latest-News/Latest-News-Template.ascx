<%@ Control Language="C#" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.Comments" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit" Assembly="Telerik.Sitefinity" %>

<sf:SitefinityLabel ID="title" runat="server" WrapperTagName="div" HideIfNoText="true" HideIfNoTextMode="Server" />

<div class="sf_cols news-listing news-2">
    <telerik:RadListView ID="NewsList" ItemPlaceholderID="ItemsContainer" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
        <LayoutTemplate>
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <div runat="server" class="sf_colsOut sf_3cols_1_33">
                <div runat="server" class="sf_colsIn sf_3cols_1in_33 style-1">
                    <div class="latest-news-single-block">
                        <div class="cta-image 111">
                            <asp:Image runat="server" ImageUrl="~/Images/CAITheme/Taxation-Audits.png" ID="Image2" />
                        </div>
                        <div class="cta-content">
                            <div class="cta-title">
                                <h2>
                                    <sf:DetailsViewHyperLink TextDataField="Title" ToolTipDataField="Description" data-sf-field="Title" data-sf-ftype="ShortText" runat="server" /></h2>

                            </div>
                            <div class="cta-message summary">
                                <p>
                                     <div class="sfnewsContent sfcontent" data-sf-field="Content" data-sf-ftype="LongText">
                                            <%# Regex.Replace(Eval("Content").ToString(), "<.*?>", string.Empty) %>
                                     </div>
                                </p>
                            </div>
                            <div class="latest-news-actions">
                                <div class="sf_cols ">
                                    <div runat="server" class="sf_colsOut sf_2cols_1_50">
                                        <div runat="server" class="sf_colsIn sf_2cols_1in_50">
                                            <div class="date">
                                                <sf:FieldListView ID="PublicationDate" runat="server" Format="{PublicationDate.ToLocal():MMM dd, yyyy}" />
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" class="sf_colsOut sf_2cols_2_50">
                                        <div runat="server" class="sf_colsIn sf_2cols_2in_50">
                                            <div class="latest-news-btn">
                                                <sf:DetailsViewHyperLink ID="FullStory" Text="READ MORE" runat="server" CssClass="sfnewsFullStory sffullstory btn" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </telerik:RadListView>
</div>

<sf:Pager ID="pager" runat="server"></sf:Pager>
<asp:PlaceHolder ID="socialOptionsContainer" runat="server" />
<asp:Label ID="MessageLabel" Text="Text" runat="server" Visible="false" />

<div class="sf_cols ">
    <div runat="server" class="sf_colsOut sf_1cols_1_100">
        <div runat="server" class="sf_colsIn sf_1cols_1in_100">
            <div class="show-more-actions">
                <div class="btn show-more">
                    Show Me More News
                </div>
            </div>
        </div>
    </div>
</div>
