(function ($) {
 
    $(function () {
        $(".nav").on("click", "li", function () {
            $(this).siblings().removeClass("current");
            var hasChild = !!$(this).find(".subnav").size();
            if (hasChild) {
                $(this).toggleClass("hasChild");
            }
            $(this).addClass("current");
        });

        $(".nav>li").css({ "borderColor": "#dbe9f1" });
        $(".nav>.current").prev().css({ "borderColor": "#7ac47f" });
        $(".nav").on("click", "li", function (e) {
            var aurl = $(this).find("a").attr("date-src");
            if (aurl) { 
                addpanel($(this).find("a").text(), aurl);
            }
            //$("#iframe").attr("src", aurl);
            $(".nav>li").css({ "borderColor": "#dbe9f1" });
            $(".nav>.current").prev().css({ "borderColor": "#7ac47f" });
            return false;
        });

        $(".toolbar").on("click", ".btn-set", function () {
            var $that = $(this);
            var href = $that.data().href;
            addpanel($that.text(), href);
        })
    })

})(jQuery);

function addpanel(title, url) {
    //$('#panel_c').panel('refresh', url);
    var html = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
    $('#panel_c').panel({
        content: html,
        title: title,
        tools: [{
            iconCls: 'icon-reload',
            handler: function () {
                addpanel(title, url);
            }
        }]
    });
    refreshUrl = url;
}