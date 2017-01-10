$(function () {
    var _layer, isclose = true;
    if (layer.index == 1) {
        isclose = false;
    }
    else {
        _layer = $.tc.showWaiting("请稍等...", 0, 6);
    }
    var htmlContent = document.getElementById("hide_Content").value;
    //正确的初始化方式  阻止复制的div标签自动转换为p标签
    var ue = UE.getEditor('editor', {
        allowDivTransToP: false
    });
    //正确的初始化方式
    ue.ready(function () {
        //this是当前创建的编辑器实例
        this.setContent(htmlContent);
        if (isclose) {
            layer.close(_layer);
        }
    })

    //给图片赋值
    var imgPath = document.getElementById("hide_ImgPath").value;
    $("#img").attr("src", imgPath);
    //
    UE.registerUI('插入图片', function (editor, uiName) {
        //注册按钮执行时的command命令，使用命令默认就会带有回退操作 
        //创建一个button
        var btn = new UE.ui.Button({
            //按钮的名字
            name: uiName,
            //提示
            title: uiName,
            //添加额外样式，指定icon图标，这里默认使用一个重复的icon
            cssRules: 'background-position: -381px 0px;',
            //点击时执行的命令
            onclick: function () {
                //这里可以不用执行命令,做你自己的操作也可
                //editor.execCommand(uiName);
                $.tw.albumpopup({ autoClose: false, single: false }).done(function (result) {
                    var list = result;
                    var html = "";
                    for (var i = 0; i < list.length; i++) {
                        var orig = list[i].orig;
                        html += '<p><img  src="' + orig + '"  alt="' + list[i].title + '"  title="' + list[i].title + '"/></p>';

                    }

                    layer.closeAll();//这里 ue和layer有个bug 
                    editor.execCommand("insertHtml", html);
                });
            }
        });
        //因为你是添加button,所以需要返回这个button
        return btn;
    });
})