//这里封装 layer的常用用法
//
; !function (window, undefined) {
    $.tc = $.tc || {};

    //重写alert   如：alert("已经保存").done(function () {alert("第二次");});
    window.alert = $.tc.alert = function (msg, tit) {
        var def = $.Deferred()
        var _layeralertindex = layer.alert(msg,
            {
                title: tit || '信息', end: function (index) {
                    def.resolve();
                    layer.close(index);
                }
            })
        return def.promise();
    }

    // $.tc.confirm("是否确认？", "提示").done(function () { alert("OK"); }).fail(function () { alert("取消"); });
    window.confirm = $.tc.confirm = function (msg, title) {
        var def = $.Deferred()
        layer.confirm(msg,
            {
                title: title || "信息",
                // btn: ['确定', '取消'] //按钮
            }, function (index) {
                def.resolve();
                layer.close(index);
            }, function (index) {
                def.reject();//触发fail()
                layer.close(index);
            });
        return def.promise();
    }

    //不关闭的加载层
    $.tc.loadlayer = function (type) {
        var index = layer.load(type || 0, {
            shade: [0.1, '#000'] //0.1透明度的白色背景
        });
        return index;
    }
    //显示等待框请稍等...
    $.tc.showWaiting = function (msg, time, ico) {
        var def = $.Deferred()
        msg = msg || "请稍等...";
        return layer.msg(msg, {
            time: time || 0, icon: ico || 7, shade: 0.3, end: function (index) {
                def.resolve();
                layer.close(index);
            }
        });
        return def.promise();
    }

    $.tc.showFrame = function (content, options) {
        //iframe层-父子操作
        layer.open($.extend({
            type: 2,
            area: ['800px', '500px'],
            fix: false, //不固定
            maxmin: true,
            content: content
        }, options));
    }


}(window);