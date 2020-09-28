<%@ Control Language="C#" %>
<%@ Register TagPrefix="sd" TagName="IncludeScript" Src="~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/IncludeScriptUsingJavascript.ascx" %>
<asp:Label ID="Label1" Text="Text" runat="server" Visible="false" />

<style>
    a[data-slide-index] img {
    width: 30px;
    border-radius: 50%;
    height: 30px;
    position: relative;
    top: 4px;
    right: -4px;
}
</style>

<asp:Label ID="MessageLabel" Text="Text" runat="server" Visible="false" />

<div runat="server" class="sf_cols">
    <div runat="server" class="sf_colsOut sf_2cols_1_50">
        <div runat="server" class="sf_colsIn sf_2cols_1in_50">
            <div class="test-img">
                <img src="" ID="person" />
            </div>
        </div>
    </div>
    <div runat="server" class="sf_colsOut sf_2cols_2_50">
        <div runat="server" class="sf_colsIn sf_2cols_2in_50">
            <div class="testimonial">
                <ul class="testimonial-bxslider" runat="server" id="bxslider">
                    <asp:Repeater ID="TestimonialRepeater" runat="server">
                        <ItemTemplate>
                            <li><%#DataBinder.Eval(Container.DataItem,"Quote")%>
                                <div class="caption">
                                    <span class="caption-name"><%#DataBinder.Eval(Container.DataItem,"Name")%></span><span class="caption-role"><%#DataBinder.Eval(Container.DataItem,"Role")%></span>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>

                <div id="test-bx-pager" class="thumbnail-nav">
                    <ul class="thumbnail-list">                
                        <asp:Repeater ID="ImageRepeater" runat="server" >
                            <ItemTemplate>
                                <li class="thumbnail-list-item">
                                    <a data-slide-index="<%# Container.ItemIndex %>" class="thumbnail-link" >
                                        <asp:Image runat="server" ImageUrl='<%#DataBinder.Eval(Container.DataItem,"ImageUrl")%>' CssClass="thumbnail-pager-img" />
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater> 
                    </ul>                  
                </div>
                <div class="thumbnail-pager">
                     <span id="slider-prev"></span>
                    <span id="slider-next"></span>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    jQuery(function ($) {
        $('#<%= bxslider.ClientID %>').bxSlider({
            captions: true,
            nextSelector: '#slider-next',
            prevSelector: '#slider-prev',
            nextText: '<i class="fa  fa-chevron-right"></i>',
            prevText: '<i class="fa fa-chevron-left"></i>',
            pagerCustom: '#test-bx-pager'
           
        });

    });


    (function () {
        //set the main picture to be the first item in slider
        var index = 0;
        setSource();

        //change image on previous click
        $('#slider-prev').on('click', function () {
            index--;

            if (index == $('a[data-slide-index]').length - 1) {
                index = 0;
            }
            setSource();
        });

        //change image on next click
        $('#slider-next').on('click', function () {
            index++;

            if (index == $('a[data-slide-index]').length + 1) {
                index = 0;
            }
            setSource();
        });

        //change image on small image click
        $('a[data-slide-index]').on('click', function () {
            index = $(this).attr('data-slide-index');
            setSource();
        });

        //function to set the source of the big image
        function setSource() {
            var item = '#test-bx-pager a[data-slide-index="' + index + '"] img';
            var src = $(item).attr('src');
            $('#person').attr('src', src);
        }
    })();

</script>
