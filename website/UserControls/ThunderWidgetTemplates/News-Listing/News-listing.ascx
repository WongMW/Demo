<%@ Control Language="C#" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.Comments" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit" Assembly="Telerik.Sitefinity" %>

<sf:SitefinityLabel ID="title" runat="server" WrapperTagName="div" HideIfNoText="true" HideIfNoTextMode="Server" />

<!-- Filter options
<div class="sf_cols latest-news-header ">
    <div runat="server" class="sf_colsOut sf_2cols_1_50">
        <div runat="server" class="sf_colsIn sf_2cols_1in_50">
            <div class="sort-by">
                <select>
                    <option value="All Categories">All Categories</option>
                </select>
            </div>           
        </div>
    </div>
    <div runat="server" class="sf_colsOut sf_2cols_2_50">
        <div runat="server" class="sf_colsIn sf_2cols_2in_50">
            <div class="order-by">
                 <select>
                     <option value="Newsest first"> Newest first</option>
                </select>
            </div>
           
        </div>
    </div>
</div>
-->

<div class="sf_cols news-listing news-2">
    <telerik:RadListView ID="NewsList" ItemPlaceholderID="ItemsContainer" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
        <LayoutTemplate>
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <div id="Div1" runat="server" class="sf_colsOut sf_3cols_1_33">
                <div id="Div2" runat="server" class="sf_colsIn sf_3cols_1in_33 style-1">
                    <span class="latest-news-category">
                      <sitefinity:TextField runat="server" DisplayMode="Read" Value='<%# SoftwareDesign.Helper.GetCategoryTitle(Eval("Category")) %>' />
                      
                    </span>
                    <div class="latest-news-single-block">
                      <div class="cta-image sd-cta-image" style="background-image: url(<%# SoftwareDesign.Helper.GetNewsArticleImage(Eval("Image"),Eval("Category")) %>);">
                          <asp:Image CssClass="sd-latest-news-image" runat="server" ImageUrl=<%# SoftwareDesign.Helper.GetNewsArticleImage(Eval("Image"),Eval("Category")) %> ID="Image2" />
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
                                    <div id="Div3" runat="server" class="sf_colsOut sf_2cols_1_50">
                                        <div id="Div4" runat="server" class="sf_colsIn sf_2cols_1in_50">
                                            <div class="date">
                                                <sf:FieldListView ID="PublicationDate" runat="server" Format="{PublicationDate.ToLocal():MMM dd, yyyy}" />
                                            </div>
                                        </div>
                                    </div>
                                    <div id="Div5" runat="server" class="sf_colsOut sf_2cols_2_50">
                                        <div id="Div6" runat="server" class="sf_colsIn sf_2cols_2in_50">
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


