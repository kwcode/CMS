; (function (window, $) {
    $.tw = $.tw || {};
    $(function () {
        if (typeof ($.tw.upfile) == "function" && typeof ($.tw.albumpopup) == "function") { return; }
        var lyIndex;
        var upfile = function (installOption) {
            var option = $.extend({
                type: 2,
                title: '上传图片',
                shadeClose: true,
                shade: [0.3, '#393D49'],
                maxmin: true, //开启最大化最小化按钮
                area: ['500px', '200px'],
                iframeSrc: "/WebImgUpload/UploadPop1.aspx",
                SrcParm: ""
            }, installOption || {});
            var $def = $.Deferred(), iframeSrc = option.iframeSrc + option.SrcParm,
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

        var albumpopup = function (installOption) {
            var option = $.extend({
                type: 2,
                title: '上传图片',
                shadeClose: true,
                shade: [0.3, '#393D49'],
                maxmin: true, //开启最大化最小化按钮
                area: ['720px', '560px'],
                iframeSrc: "/WebImgUpload/AlbumPopup.aspx",
                autoClose: true,//点击确定后是否自动关闭 这里兼容UE编辑器
                single: true//单选
            }, installOption || {});
            var $def = $.Deferred(), iframeSrc = option.iframeSrc + "?single=" + (option.single ? 1 : 0),
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
                if (option.autoClose) {
                    layer.close(lyIndex);
                }
            };
            return $def.promise();
        }

        $.extend($.tw, { upfile: upfile, albumpopup: albumpopup });
    })
})(window, $);