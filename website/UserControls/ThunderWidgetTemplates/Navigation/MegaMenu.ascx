<%@ Control Language="C#" %>
<%@ Import Namespace="Telerik.Sitefinity.Web.UI.NavigationControls.Extensions.LightNavigationControlTemplate" %>
<%@ Import Namespace="Telerik.Sitefinity.Web.UI.NavigationControls" %>
<%@ Import Namespace="Telerik.Sitefinity.RelatedData" %>

<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="navigation" Namespace="Telerik.Sitefinity.Web.UI.NavigationControls" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>

<sf:ResourceLinks id="resLinks" runat="server" UseEmbeddedThemes="true" Theme="Basic">
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Themes.Basic.Styles.nav.widget.css" Static="true" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css" Static="true" />
    <sf:ResourceFile JavaScriptLibrary="JQuery" />
    <sf:ResourceFile JavaScriptLibrary="KendoWeb" />
</sf:ResourceLinks>

<navigation:SitefinitySiteMapDataSource runat="server" ID="dataSource" />

<sf:SitefinityLabel id="title" runat="server" WrapperTagName="div" HideIfNoText="true" HideIfNoTextMode="Server"  />
<div class="sfNavWrp sfNavHorizontalDropDownWrp topnav <%= this.GetCssClass() %>">
    <%-- responsive design section - renders templates for the responsive design --%>
    <navigation:NavTransformationTemplate runat="server" TransformationName="ToToggleMenu" TemplateName="ToggleMenu" />
    <navigation:NavTransformationTemplate runat="server" TransformationName="ToDropDown" TemplateName="Dropdown" />
    <%-- end of the responsive design section --%>

    <ul runat="server" id="navigationUl" class="sfNavHorizontalDropDown sfNavList">
        <navigation:NavigationContainer runat="server" DataSourceID="dataSource">
            <templates>
                <navigation:NavigationTemplate runat="server" Level="0">
                    <Template>
                        <li class="<%# (bool)Eval("HasChildNodes") ? "has-drop" : null %>" >
                            <a runat="server" href='<%# Eval("Url") %>' target='<%# NavigationUtilities.GetLinkTarget(Container.DataItem) %>'>
                                <%# Eval("Title") %>
                            </a>
                            <asp:PlaceHolder runat="server" Visible=<%# (bool)Eval("HasChildNodes") %>>
                                <ul <%# (Eval("MegaMenuImage") as Telerik.Sitefinity.Libraries.Model.Image) != null ? "class=\"level-0 mega\"" : null %>>
                                  <li class="mainMegaMenuContainer">
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="childNodesContainer" />
                                    </ul>
                                  </li>
                                  <li class="mainMegaMenuLastElement">
                                       <div class="mega-image-content style-1">
                                          <div class="mega-image-title">
                                              <h2><%# Eval("MegaMenuTitle") %></h2>
                                          </div>
                                          <div class="mega-image-message">
                                              <p><%# Eval("MegaMenuMessage") %></p>
                                          </div>
                                        <div class="mega-image-actions">
                                            <a href="<%# Eval("MegaMenuButtonLink") %>" class="btn-readMore">Read More</a>
                                        </div>
                                      </div>

                                      <div style="background-image: linear-gradient(to right, rgba(0, 61, 81, 0.7),  rgba(0, 61, 81, 0.8)), url('<%# Eval("MegaMenuImage.MediaUrl") %>');">
                                          <img src="<%# Eval("MegaMenuImage.MediaUrl") %>" />
                                      </div>
                                  </li>
                                </ul>
                            </asp:PlaceHolder>
                        </li>
                    </Template>
                    <SelectedTemplate>
                        <li class="active <%# (bool)Eval("HasChildNodes") ? "has-drop" : null %>" >
                            <a runat="server" href='<%# Eval("Url") %>' target='<%# NavigationUtilities.GetLinkTarget(Container.DataItem) %>'>
                                <%# Eval("Title") %>
                            </a>
                            <asp:PlaceHolder runat="server" Visible=<%# (bool)Eval("HasChildNodes") %>>
                                <ul class="level-0 mega">
                                  <li class="mainMegaMenuContainer">
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="childNodesContainer" />
                                    </ul>
                                  </li>
                                  <li class="mainMegaMenuLastElement">
                                       <div class="mega-image-content style-1">
                                          <div class="mega-image-title">
                                              <h2><%# Eval("MegaMenuTitle") %></h2>
                                          </div>
                                          <div class="mega-image-message">
                                              <p><%# Eval("MegaMenuMessage") %></p>
                                          </div>
                                        <div class="mega-image-actions">
                                            <a href='<%# Eval("MegaMenuButtonLink.Url") %>' class="btn-readMore">Read More</a>
                                        </div>
                                      </div>
                                   
                                      <div style="background-image: linear-gradient(to right, rgba(0, 61, 81, 0.7),  rgba(0, 61, 81, 0.8)), url('<%# Eval("MegaMenuImage.MediaUrl") %>');">
                                         <div class="mega-menu-img-caption">
                                          <h1 class="mega-caption-title"><%# Eval("MegaMenuTitle") %></h1>
                                          <p class="mega-message"><%# Eval("MegaMenuMessage") %></p>
                                          <div class="mega-menu-action style-1">
                                              <a href="#" class="btn-readMore">Read More</a>
                                          </div>
                                      </div>
                                          <img src="<%# Eval("MegaMenuImage.MediaUrl") %>" />
                                      </div>
                                  </li>
                                </ul>
                            </asp:PlaceHolder>
                        </li>
                    </SelectedTemplate>
                </navigation:NavigationTemplate>
                <navigation:NavigationTemplate runat="server" Level="1">
                    <Template>
                        <li>
                            <a runat="server" href='<%# NavigationUtilities.ResolveUrl(Container.DataItem) %>' target='<%# NavigationUtilities.GetLinkTarget(Container.DataItem) %>'><%# Eval("Title") %></a>
                            <div runat="server" id="childNodesContainer"></div>
                        </li>
                    </Template>
                    <SelectedTemplate>
                        <li>
                            <a runat="server" class="selected" href='<%# NavigationUtilities.ResolveUrl(Container.DataItem) %>' target='<%# NavigationUtilities.GetLinkTarget(Container.DataItem) %>'><%# Eval("Title") %></a>
                            <div runat="server" id="childNodesContainer"></div>
                        </li>
                    </SelectedTemplate>
                </navigation:NavigationTemplate>
                <navigation:NavigationTemplate runat="server" Level="2">
                    <Template>
                        <div class="mega-menu-child-item">
                            <a runat="server" href='<%# NavigationUtilities.ResolveUrl(Container.DataItem) %>' target='<%# NavigationUtilities.GetLinkTarget(Container.DataItem) %>'><%# Eval("Title") %></a>
                            <ul runat="server" id="childNodesContainer"></ul>
                        </div>
                    </Template>
                    <SelectedTemplate>
                        <div class="mega-menu-child-item selected">
                            <a runat="server" href='<%# NavigationUtilities.ResolveUrl(Container.DataItem) %>' class="sfSel" target='<%# NavigationUtilities.GetLinkTarget(Container.DataItem) %>'><%# Eval("Title") %></a>
                            <ul runat="server" id="childNodesContainer"></ul>
                        </div>
                    </SelectedTemplate>
                </navigation:NavigationTemplate>
                <navigation:NavigationTemplate>
                    <Template>
                        <li>
                            <a runat="server" href='<%# NavigationUtilities.ResolveUrl(Container.DataItem) %>' target='<%# NavigationUtilities.GetLinkTarget(Container.DataItem) %>'><%# Eval("Title") %></a>
                            <ul runat="server" id="childNodesContainer"></ul>
                        </li>
                    </Template>
                    <SelectedTemplate>
                        <li>
                            <a runat="server" href='<%# NavigationUtilities.ResolveUrl(Container.DataItem) %>' class="sfSel" target='<%# NavigationUtilities.GetLinkTarget(Container.DataItem) %>'><%# Eval("Title") %></a>
                            <ul runat="server" id="childNodesContainer"></ul>
                        </li>
                    </SelectedTemplate>
                </navigation:NavigationTemplate>
            </templates>
        </navigation:NavigationContainer>
    </ul>
</div>

<%-- link to Kendo documentation http://demos.kendoui.com/web/menu/index.html --%>
<script type="text/javascript">
    (function ($) {
        var whetherToOpenOnClick = true;

        var kendoMenu = $('.sfNavHorizontalDropDown').not('.k-menu').kendoMenu({
            animation: false,
            openOnClick: whetherToOpenOnClick,
            open: function (e) {
                if (window.DataIntelligenceSubmitScript) {
                    var item = $(e.item);

                    DataIntelligenceSubmitScript._client.sentenceClient.writeSentence({
                        predicate: "Toggle navigation",
                        object: item.find("a:first").text(),
                        objectMetadata: [
                                                    {
                                                        'K': 'PageTitle',
                                                        'V': document.title
                                                    },
                                                    {
                                                        'K': 'PageUrl',
                                                        'V': location.href
                                                    }
                        ]
                    });
                }
            }
        }).data('kendoMenu');

        if (whetherToOpenOnClick && kendoMenu) {
            jQuery(kendoMenu.element).find("li:has(ul) > a").attr("href", "javascript:void(0)");
        }
    })(jQuery);
</script>
