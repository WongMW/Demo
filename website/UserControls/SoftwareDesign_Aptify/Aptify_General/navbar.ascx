<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.NavBar"
    CodeFile="~/UserControls/Aptify_General/NavBar.ascx.vb" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="">
    <div class="sidebar-cat-nav">
            <a id="<%= ClientID %>" class="sfNavToggle">&#9776;</a>
    </div>
    <div class="ColorDivBGAdmin aptify-category-side-nav sfShown">
        <h6>Menu</h6>

        <rad:RadMenu ID="RadMenu1" runat="server" OnItemClick="RadMenu1_click" >
            <ItemTemplate>
                <a runat="server" class="aptify-category-link" href='<%# Container.NavigateUrl %>'><%# Container.Text %></a>
            </ItemTemplate>
        </rad:RadMenu>
    </div>
</div>
<cc1:User ID="User1" runat="server" />

<script>
    (function ($) {
        $('.sfNavToggle').click(function () {
            $('.aptify-category-side-nav').toggleClass("sfShown");
        });

    })(jQuery);
</script>

<script type="text/javascript">
    $(function() {
        var fullPath = location.href;
        $("div.aptify-category-side-nav > div.RadMenu > ul > li > div a.aptify-category-link").each(function () {
            if ($(this).prop("href") === fullPath) {
                $(this).parents('li').each(function () {
                    $(this).addClass("current");
                    $(this).children('div.rmText').children('.toggleBtn').addClass('open');
                    $(this).children('div.rmSlide').children('ul').addClass('showChildren');
                });
            }
        });
    });
</script>
