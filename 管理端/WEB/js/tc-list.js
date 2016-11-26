
function PopShow(url, options) {
    $.tc.showFrame(url, options);
}

//获取选中的集合ID列表
function GetCheckList_ID() {
    var list = [];
    $('.list_checkitems').each(function () {
        var $that = $(this);
        if (this.checked) {
            var val = $that.val();
            list.push(val);
        }
    })
    return list;
}
//移除选中的项
function RemoverCheckItems(id) {
    $('.list_checkitems').each(function () {
        var $that = $(this);
        if (this.checked) {
            var val = $that.val();
            if (val == id) {
                $that.closest("tr").remove();
            }
        }
    })
}
/*删除选中*/
function Del_ChecdList() {
    var list = GetCheckList_ID();
    if (list.length <= 0) {
        alert("请选择需要删除的数据！");
        return;
    }
    $.tc.confirm("是否永久删除选中吗？").done(function () {
        var data = JSON.stringify({ "jsonlist": JSON.stringify(list) });
        $.tc.gopost({
            url: window.location.pathname + "/DoDelete",
            contentType: "application/json; charset=utf-8",
            data: data
        }).done(function (result) {
            var data = result.d;
            if (data.code == 1) {
                for (var i = 0; i < list.length; i++) {
                    RemoverCheckItems(list[i]);
                }
                alert(data.msg);
            }
            else {
                alert(data.msg);
            }
        })
    })
}

function OnEnter(obj) {
    var e = event || window.event || arguments.callee.caller.arguments[0];
    if (e && e.keyCode == 13) { // enter 键
        //要做的事情
        Search();
    }
}

function Search() {
    var keywords = $("#txtSearch").val();
    window.location.href = window.location.pathname + "?keywords=" + encodeURIComponent(keywords);
}

$(function () {
    //全选
    $(".list_checkall").click(function () {
        if (this.checked) {                 //如果当前点击的多选框被选中
            $('.list_checkitems').prop("checked", true);
        } else {
            $('.list_checkitems').prop("checked", false);
        }
    });
    //取消单选
    $('.list_checkitems').click(function () {
        var flag = true;
        $('.list_checkitems').each(function () {
            if (!this.checked) {
                flag = false;
            }
        });
        if (flag) {
            $('.list_checkall').prop('checked', true);
        } else {
            $('.list_checkall').prop('checked', false);
        }
    });
})
