// link to Kendo documentation http://demos.kendoui.com/web/menu/index.html 
(function ($) {
    $(document).ready(function () {
        $('.some-toggle.sfNavToggle').unbind('click').bind('click', function () {
              
            $(this).closest(".sfNavWrp").find(".sfNavList").toggleClass("sfShown");
        });

        jQuery(window).resize(function () {
            var windowWidth = jQuery(window).width();

            if (windowWidth <= 959) {
                jQuery(".district-nav ul.sfNavList li").css('width', '');
                return;
            }



            var totalLength = jQuery(".district-nav ul.sfNavList li a").text().length;

            var totalChars = jQuery(".district-nav ul.sfNavList li a").text().length;


            var totalItems = jQuery(".district-nav ul.sfNavList li").length;

            var totalWidth = jQuery(".district-nav ul.sfNavList").width();

            var elementWidth = totalWidth / (totalItems <= 0 ? 1 : totalItems);

            //console.log(totalChars);

            if (totalChars >= 45) {
                jQuery(".district-nav ul.sfNavList li a").css({ 'padding': '8px' });
            } else {
                jQuery(".district-nav ul.sfNavList li a").css({ 'padding': '19px' });
            }



            jQuery(".district-nav ul.sfNavList li").css('width', elementWidth + 'px');
        });
        jQuery(window).resize();

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
    });
})(jQuery);
