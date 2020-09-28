// Susan Issue 18377 defer JS calls
// CHECKBOX FUNCTION START
jQuery(function ($) {
            $('<span class="checkbox"><i class="fa fa-check"></i></span>').insertBefore('input[type="checkbox"]');
            checkCheckboxes();
            Sys.Application.add_init(appl_init);
            handleNavMenu();
        });
        function checkCheckboxes() {
            $('.checkbox').each(function () {
                var chkbox = $(this).parent().find('input[type="checkbox"]');
                if (chkbox.attr('checked') === 'checked') {
                    $(this).addClass('checked');
                }
            });
            $('.aspNetDisabled .checkbox').each(function () {
                $(this).addClass('disabled');
            });

            $('.checkbox:not(.disabled)').on('click', function () {
                if ($(this).parent()[0].tagName !== "LABEL") {
                    $(this).toggleClass('checked');
                    var spanIsChecked = $(this).hasClass("checked");
                    var chkboxIsChecked = $(this).parent().find('input[type="checkbox"]')[0].checked;

                    if (spanIsChecked !== chkboxIsChecked) {
                        $(this).parent().find('input[type="checkbox"]')[0].checked = spanIsChecked;
                        var onclickHandler = $(this).parent().find('input[type="checkbox"]')[0].onclick;

                        if ((onclickHandler !== undefined) && (onclickHandler !== null)) {
                            onclickHandler();
                        }
                    }
                }
                else {
                    $(this).parent()[0].click();
                }
            });

            $('input[type="checkbox"]').on('click', function () {
                if ($(this).parent()[0].tagName !== "LABEL") {
                    $(this).parent().find('span.checkbox').toggleClass('checked');
                }
            });
        }
	function appl_init() {
            var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
            pgRegMgr.add_endRequest(EndHandler);
        }
        function EndHandler() {
            $('input[type="checkbox"]').each(function () {
                if (!$(this).prev().hasClass('checkbox')) {
                    $('<span class="checkbox"><i class="fa fa-check"></i></span>').insertBefore($(this));
                }
            });
            checkCheckboxes();
        }
// CHECKBOX FUNCTION END
// CONTENTBLOCK SHOW MORE OR LESS TEXT START
$(document).ready(function () {
    var x = 0;
    $('.show-more-content').each(function () {
        $(this).attr('id', 'content-box_' + x);
        $('<a class="show-all-hidden" id="show-more-btn_' + x + '" onclick = "showMore(this)">show me more</a>').insertAfter($(this));
        $('<a class="hide-all-showing" id="show-less-btn_' + x + '" onclick = "showLess(this)">show me less</a>').insertAfter($(this));
        $(this).parent().children('#show-less-btn_' + x).css("display", "none");
        $('<div class="gradient-bg-content" id="grad-box-bg_' + x + '"></div>').insertAfter($(this));
        x += 1;
    });


    $(".chkAllCheckBoxes").click(function () {
        //console.log('checkbox checked');
        let ckdValue = $(this).children('input:checkbox')[0].checked;
        //console.log(ckdValue);
        if (ckdValue) {
            $('input:checkbox').prop('checked', ckdValue).parent().children('span').addClass('checked');
        }
        else {
            $('input:checkbox').prop('checked', ckdValue).parent().children('span').removeClass('checked');
        }
    });

    //FIXING BREADCRUMB WITH FONT AWESOME - SITEFINITY 12
    $(".sfNoBreadcrumbNavigation > .rsmLink").html($(".sfNoBreadcrumbNavigation > .rsmLink").text().replace('"', ''));
});
function showMore(showcontent) {
    var showbtnid = showcontent.getAttribute('id'); //Get show-more-btn id
    var a = showbtnid.split("_");
    var b = a[0];
    var c = a[1];
    var hidebtnid = document.getElementById("show-less-btn_" + c); //Get IDs
    var contentboxid = document.getElementById("content-box_" + c); 
    var gradboxid = document.getElementById("grad-box-bg_" + c); 
    $("#" + showbtnid).css("display", "none"); //hide show more btn
    $(hidebtnid).css("display", "block"); //show show less btn
    $(gradboxid).css("display", "none"); 
    $(contentboxid).toggleClass('transform-active');        
}
function showLess(hidecontent) {
    var hidebtnid = hidecontent.getAttribute('id') //Get show-less-btn id
    var a = hidebtnid.split("_");
    var b = a[0];
    var c = a[1];
    var showbtnid = document.getElementById("show-more-btn_" + c); //Get IDs
    var contentboxid = document.getElementById("content-box_" + c); 
    var gradboxid = document.getElementById("grad-box-bg_" + c);
    $("#" + hidebtnid).css("display", "none"); //hide show less btn
    $(showbtnid).css("display", "block"); //show show more btn
    $(gradboxid).css("display", "block"); 
    $(contentboxid).toggleClass('transform-active');
}
// CONTENTBLOCK SHOW MORE OR LESS TEXT END
// BUTTON WIDGET LEFT AND RIGHT START
$(".left-button .btn-full-width").prepend( '<span class="left-arrow lrg-btn-arrow"></span>' );
$(".right-button .btn-full-width").prepend( '<span class="right-arrow lrg-btn-arrow"></span>' );
$(".left-button .btn-full-width").each(function () {
	var x = $(this);
	CheckButtonHeight(x);
});
$(".right-button .btn-full-width").each(function () {
	var x = $(this);
	CheckButtonHeight(x);
});
function CheckButtonHeight(x) {
	if (x.height() <= 30) {x.children('.lrg-btn-arrow').css("margin-top", "0px");} else 
	if (x.height() <= 60) {x.children('.lrg-btn-arrow').css("margin-top", "15px");} else
	if (x.height() <= 90) {x.children('.lrg-btn-arrow').css("margin-top", "30px");} else
	if (x.height() <= 120) {x.children('.lrg-btn-arrow').css("margin-top", "45px");} else
	if (x.height() <= 150) {x.children('.lrg-btn-arrow').css("margin-top", "60px");} else
	if (x.height() <= 180) {x.children('.lrg-btn-arrow').css("margin-top", "75px");} else
	if (x.height() <= 210) {x.children('.lrg-btn-arrow').css("margin-top", "90px");} else
	if (x.height() <= 240) {x.children('.lrg-btn-arrow').css("margin-top", "105px");} else
	if (x.height() <= 270) {x.children('.lrg-btn-arrow').css("margin-top", "120px");} else
	if (x.height() <= 300) {x.children('.lrg-btn-arrow').css("margin-top", "135px");}
}
// BUTTON WIDGET LEFT AND RIGHT END
// ICON WIDGET START
$(".icon-widget").prepend( '<span class="icon"></span>' );
// ICON WIDGET END
// PLAIN TABLE HORIZONTAL LINE START
//$(".plain-table table").before( '<hr/>' );
// PLAIN TABLE HORIZONTAL LINE END
// TOOLTIP ON CLICK START
$('.whats-this-icon').click(function() {
  if ($(this).children('.tooltip-click-box').is(':visible') )
  {$(this).children('.tooltip-click-box').css("display", "none");}
  else{$(this).children('.tooltip-click-box').css("display", "inline-block");}
});
// TOOLTIP ON CLICK END
// BUTTON ICONS START
$(".btn-icon a").prepend( '<span class="icon"></span>' );
$("a.btn-icon").prepend( '<span class="icon"></span>' );
// BUTTON ICONS END
// SITE NOTICES START
$('.info-note').each(function () {
	var Myinfotxt = $(this).html();
	$(this).html('<span class="info-help-text">' + Myinfotxt + '</span>');
});
$('.info-success').each(function () {
	var Myinfotxt = $(this).html();
	$(this).html('<span class="info-help-text">' + Myinfotxt + '</span>');
});
$('.info-error').each(function () {
	var Myinfotxt = $(this).html();
	$(this).html('<span class="info-help-text">' + Myinfotxt + '</span>');
});
$('.info-tip').each(function () {
	var Myinfotxt = $(this).html();
	$(this).html('<span class="info-help-text">' + Myinfotxt + '</span>');
});
$('.info-warning').each(function () {
	var Myinfotxt = $(this).html();
	$(this).html('<span class="info-help-text">' + Myinfotxt + '</span>');
});
$('.info-info').each(function () {
	var Myinfotxt = $(this).html();
	$(this).html('<span class="info-help-text">' + Myinfotxt + '</span>');
});
// SITE NOTICES END
// TRIGGER TITLE AND TRIGGER SECTION START
$('.trigger-section').addClass("hide-it-transition");
$(".trigger-title ").on('click', function () {
	$(this).next('.trigger-section').toggleClass("show-it-transition")
	if ($(this).next().hasClass("show-it-transition")) {
		$(this).children().children('.plus')
		.addClass("minus")
		.removeClass("plus");
		$(this).children('.form-title').css("margin-bottom", "20px");
	}
	else {
		$(this).children().children('.minus')
		.addClass("plus")
		.removeClass("minus");
		$(this).children('.form-title').css("margin-bottom", "1px");
	}
});
// TRIGGER TITLE AND TRIGGER SECTION END
// INCREMENT NUMBER START
var newIncNum = 0
$(".increment-num").each(function () {
	newIncNum += 1;
	$(this).text(newIncNum);
});
// INCREMENT NUMBER END
// STATUS MESSAGE COLOURS START
$(".status-msg-color").each(function () {
        if ($(this).text().trim() === "Unedited") {
            $(this).css("color", "red");
        }
        else if ($(this).text().trim() === "Saved as draft") {
            $(this).css("color", "orange");
        }
        else if ($(this).text().trim() === "Pending approval") {
            $(this).css("color", "blue");
        }
        else if ($(this).text().trim() === "Approved") {
            $(this).css("color", "green");
        }
    });
// STATUS MESSAGE COLOURS END
