<%@ Control Language="C#" %>
<%@ Import Namespace="Telerik.Sitefinity.Web.UI.NavigationControls.Extensions.LightNavigationControlTemplate" %>
<%@ Import Namespace="Telerik.Sitefinity.Web.UI.NavigationControls" %>

<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="navigation" Namespace="Telerik.Sitefinity.Web.UI.NavigationControls" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>

<sf:ResourceLinks runat="server" UseEmbeddedThemes="true" Theme="Basic">
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Themes.Basic.Styles.nav.widget.css" Static="true" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css" Static="true" />
    <sf:ResourceFile JavaScriptLibrary="JQuery" />
    <sf:ResourceFile JavaScriptLibrary="KendoWeb" />
</sf:ResourceLinks>

<navigation:SitefinitySiteMapDataSource runat="server" ID="dataSource" />

<sf:SitefinityLabel id="title" runat="server" WrapperTagName="div" HideIfNoText="true" HideIfNoTextMode="Server" />
<div class="sfNavWrp sfNavTreeviewWrp nav-sidebar <%= this.GetCssClass() %>">
    <a id="baseTemplatePlaceholder_content_ctl01_ctl04_ctl05_C013_ctl00_ctl00_ctl01_ctl00_ctl00_toggleAnchor" class="sfNavToggle">Menu</a>

<script type="text/javascript">
    (function ($) {
        $($get('baseTemplatePlaceholder_content_ctl01_ctl04_ctl05_C013_ctl00_ctl00_ctl01_ctl00_ctl00_toggleAnchor')).click(function () {
            $(this).closest(".sfNavWrp").find(".sfNavList").toggleClass("sfShown");
        });
    })(jQuery);
</script>
    
    <%-- responsive design section - renders templates for the responsive design--%>
    <navigation:NavTransformationTemplate runat="server" TransformationName="ToToggleMenu" TemplateName="ToggleMenu" />
    <navigation:NavTransformationTemplate runat="server" TransformationName="ToDropDown" TemplateName="Dropdown" />
    <%-- end of the responsive design section --%>

    <ul class="sfNavTreeview sfNavList" runat="server" id="navigationUl">
        <navigation:NavigationContainer runat="server" DataSourceID="dataSource">
            <Templates>
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
                </Templates>
        </navigation:NavigationContainer>
    </ul>
</div>

<%-- link to Kendo documentation http://demos.kendoui.com/web/treeview/index.html --%>

<script type="text/javascript">
    (function ($) {
        var kendoTreeView = $('.sfNavTreeview').not('div.k-treeview .sfNavTreeview').kendoTreeView({
            animation: false,
            expand: function (e) {
                if (window.DataIntelligenceSubmitScript) {
                    DataIntelligenceSubmitScript._client.sentenceClient.writeSentence({
                        predicate: "Toggle navigation",
                        object: this.dataItem(e.node).text,
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
        }).data('kendoTreeView');
        if (kendoTreeView) {
            kendoTreeView.expand(kendoTreeView.element.find(".k-item"));
        }
    })(jQuery);
</script>