<%@ Control Language="C#" %>

<div class="main-slider">
    <ul class="main-bxslider">
        <li>

        </li>
    </ul>

</div>


  <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery('.main-bxslider').bxSlider({
                captions: true,
                nextSelector: '#slider-next',
                prevSelector: '#slider-prev',
                nextText: '<i class="fa  fa-chevron-right"></i>',
                prevText: '<i class="fa fa-chevron-left"></i>',
                pagerCustom:'#main-bx-pager'
            });
        });
    </script>