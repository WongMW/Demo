<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncludeScriptUsingJavascript.ascx.cs" Inherits="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.IncludeScriptUsingJavascript" %>
<%@ Register TagPrefix="sf" Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.PublicControls" %>

<script type="text/javascript">

    Sys.Application.add_load(function () {
        jQuery(function () {
            if (jQuery('body script[src="<%= this.ResolveUrl(this.Url) %>"]').length == 0) {
                if (jQuery('head script[src="<%= this.ResolveUrl(this.Url) %>"]').length == 0) {
                    var ele = document.createElement('script');
                    ele.setAttribute("type", "text/javascript");
                    ele.setAttribute("src", "<%= this.ResolveUrl(this.Url) %>");
                    $('head').append(ele);
                }
            }
        });

        jQuery(function () {

            var width = $(window).width();

            jQuery.fn.swap = function (b) {
                b = jQuery(b)[0];
                var a = this[0];
                var t = a.parentNode.insertBefore(document.createTextNode(''), a);
                b.parentNode.insertBefore(a, b);
                t.parentNode.insertBefore(b, t);
                t.parentNode.removeChild(t);
                return this;
            };

            var resizeFn = function () {
                // < 1024: jobs & find a firm to main
                if ($(window).width() <= 1100) {
                    var listItems = $(".right-nav-menu ul > li");
                    if (listItems.length === 2) {
                        var lastItems = listItems.slice(listItems.length - 2);
                        lastItems.appendTo(".main-menu-nav ul:first");
                        $('.nav-utils').swap('.main-nav');
                        if ($('.district-nav').length && $('.sfBreadcrumbWrp').length) {
                            $('.sfBreadcrumbWrp').swap('.district-nav');
                        }
                    }
                }

                // <= 600: all left to main
                if ($(window).width() <= 610) {
                    $(".left-nav div > ul > li.k-item.has-drop").prependTo(".main-menu-nav ul:first");
                }
                // > 600: left items to main
                if ($(window).width() > 610) {
                    $(".main-menu-nav div > ul:first-of-type > li.k-item.has-drop").appendTo(".left-nav ul.k-menu");
                }

                // >= 1024: jobs & find a firm to right
                if ($(window).width() > 1100) {
                    var listItems = $(".main-menu-nav ul > li");
                    if (listItems.length > 6 && $(".right-nav-menu ul").children().length == 0) {
                        var lastItems = listItems.slice(listItems.length - 2);
                        lastItems.appendTo(".right-nav-menu ul:last");

                        $('.main-nav').swap('.nav-utils');

                        if ($('.district-nav').length && $('.sfBreadcrumbWrp').length) {
                            $('.district-nav').swap('.sfBreadcrumbWrp');
                        }
                    }
                }
            };

            resizeFn();

            $(window).resize(function () {
                resizeFn();
            });

        }());

        jQuery(function ($) {
            $('.aptify-category-side-nav .rmText + .rmSlide').each(function () {
                $(this).prev().append('<span class="toggleBtn"></span>');
            });

            $('.aptify-category-side-nav .toggleBtn').click(function () {
                $(this).parent().parent().find('ul.rmGroup').toggleClass("showChildren");
                $(this).toggleClass("open");
            });

            $(window).resize(function () {
                if ($('#Products').length > 0) {
                    var height = $('.featured-products-slider').height();
                    $('#Products').css('height', height + 40);
                }
            });


            if ($('.cai-form').children().hasClass('active') && $('.cai-form').children().hasClass('expand')) {

                var $active = $('.active').prev();
                $($active).toggleClass('clicked');
            }
            $('a.expand').on('click', function () {
                $('h2.expand').addClass('clicked');
            });


            $('.expand').on('click', function () {
                $(this).toggleClass('clicked');
            });
            $('.form-title input[type="image"]').hide();
            $('#divCollapsebtn img').hide();

            $('.InsertUnorderedList').on('click', function () {
                $('.reContentCell ul li').addClass('un-list');
            });
            $('.InsertOrderedList').on('click', function () {
            });

        });
    });
</script>
