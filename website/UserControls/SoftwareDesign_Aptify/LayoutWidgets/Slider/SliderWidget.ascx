<%@ Control Language="C#" %>
<%@ Register TagPrefix="sd" TagName="IncludeScript" Src="~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/IncludeScriptUsingJavascript.ascx" %>

<div class="main-slider">
    <ul class="bxslider" runat="server" id="bxslider">

        <asp:Repeater ID="ImageRepeater" runat="server">
            <ItemTemplate>
                <li class="slide">
                    <div class="main-caption">
                        <div class="main-caption-inner">
                            <h1><%#DataBinder.Eval(Container.DataItem,"Title")%></h1>
                            <div class="sub-caption">
                                <h4><%#DataBinder.Eval(Container.DataItem,"Subtitle")%></h4>
                            </div>
                        </div>
                        <div class="btn-actions style-1">
                            <a href="<%#DataBinder.Eval(Container.DataItem,"Link")%>" class=" btn-readMore">Read More</a>
                        </div>
                    </div>

                    <img src='<%#DataBinder.Eval(Container.DataItem,"Url")%>' alt="Alternate Text" />
                </li>

            </ItemTemplate>
        </asp:Repeater>
    </ul>

    <script type="text/javascript">
        jQuery(function () {
            jQuery('#<%= bxslider.ClientID %>').bxSlider();
        });
    </script>
</div>
