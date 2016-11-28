; (function (window, $) {
    $.tw = $.tw || {};
    $(function () {
        if (typeof ($.tw.upfile) == "function") { return; }
        var lyIndex;
        var upfile = function (installOption) {
            var option = $.extend({
                type: 2,
                title: '上传图片',
                shadeClose: true,
                shade: [0.3, '#393D49'],
                maxmin: true, //开启最大化最小化按钮
                area: ['500px', '200px'],
                iframeSrc: "/WebImgUpload/UploadPop1.aspx"
            }, installOption || {});
            var $def = $.Deferred(), iframeSrc = option.iframeSrc,
            iframeSrc = iframeSrc,
             lyIndex = layer.open($.extend({
                 content: iframeSrc,
                 end: function () {
                     if ($def.state() == "pending") {
                         $def.reject();
                     }
                 },
                 success: function ($layer) { }

             }, option));
            if (lyIndex <= 0) {
                $def.reject();
            }
            window.tw = window.tw || {};
            window.tw.confirm = function (data) { 
                $def.resolve(data);
                layer.close(lyIndex);
            };
            return $def.promise();
        }
        $.extend($.tw, { upfile: upfile });
    })
})(window, $);