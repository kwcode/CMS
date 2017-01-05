<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumPopup.aspx.cs" Inherits="WEB.WebImgUpload.AlbumPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>图片上传</title>
    <link href="/css/uploadpop.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/jquery-page.js"></script>
    <script src="/js/plupload/moxie.js"></script>
    <script src="/js/plupload/plupload.js"></script>
    <script src="/js/json2.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script>
        //关闭窗口
        function CloseWindow(sx) {
            var winname = window.name;
            if (sx) {
                parent.window.location.reload();
            }
            window.opener = null;
            window.open('', '_self');
            window.close();
            if (winname) {
                var index = parent.layer.getFrameIndex(window.name);
                parent.layer.close(index);
            }
        }
        $(function () {

            var $list = $(".uploader-images-list");
            var uploader = new plupload.Uploader({ //实例化一个plupload上 传对象
                browse_button: 'imgupload',
                runtimes: 'html5,flash,silverlight,html4',
                url: '/WebImgUpload/UploadHandler.ashx',
                flash_swf_url: '/js/plupload/Moxie.swf',
                silverlight_xap_url: '/js/plupload/Moxie.xap',
                multipart_params: { 'BookID': '<%=BookID%>', 'IsWatermark': 0 },//注意和里面内置的不要重复
                multi_selection: true,//多个文件
                filters: {
                    mime_types: [ //只允许上传图片文件
                      { title: "图片文件", extensions: "jpg,gif,png" }
                    ]
                }
                , max_file_size: '10mb'
                //, chunk_size: '1mb'//小片上传一定要注意压缩的大小
                //, resize: { width: 320, height: 240, quality: 90 }
                , init:
                {
                    PostInit: function (a) {
                        //console.log("初始化完毕");
                    },
                    FilesAdded: function (uder, files) {
                        //console.log("添加进队列");
                        for (var i = 0; i < files.length; i++) {
                            var file = files[i];
                            appendimg(file.id, file.name);
                        }
                        uder.settings.multipart_params.ModuleID = 9999;
                        uder.start();
                    },
                    BeforeUpload: function (uder, files) {
                        //console.log("开始上传");
                        //multipart_params
                        //, 'IsWatermark': $("#cbWatermark").prop("checked")
                        var isWatermark = $("#cbWatermark").prop("checked");
                        uder.settings.multipart_params.IsWatermark = isWatermark ? 1 : 0;
                    },
                    UploadProgress: function (uder, file) {
                        console.log("进度：[百分比:" + file.percent + "，状态：" + file.status + ",原始大小：" + file.origSize + ",已传：" + file.loaded + "]");
                        progress(file.id, file.percent);
                    },
                    UploadFile: function (uder) {
                        //console.log(uder.id + "开始上传");
                    },
                    FileUploaded: function (uder, file, resObject) {
                        var result = resObject.response;
                        console.log("上传完成" + result);
                        var $fileitem = $("." + file.id)
                        var data = JSON.parse(result).data;
                        $fileitem.data().tn = data.Tn;
                        $fileitem.data().show = data.Show;
                        $fileitem.data().orig = data.Orig;
                        $fileitem.data().id = data.ID;
                        $fileitem.data().title = data.Title;

                        $fileitem.find("img").attr("src", data.Tn);
                        //移除进度条
                        $fileitem.find(".progress").remove();
                    },
                    ChunkUploaded: function (a, b, c) {
                        //console.log("小片上传完成后");
                    },
                    UploadComplete: function (uder, files) {
                        //console.log("上传完毕");
                    },
                    Error: function () {
                        alert("ERROR");
                    }

                }

            });
            uploader.init(); //初始化

            function appendimg(id, name) {
                var html = '';
                html += '<div class="pictureFrame">';
                html += ' <div  class="' + id + ' file-item"><div class="fancybox"><a class="imgbox"> <img src="" /> </a> <span class="icon"></span> </div> </div>';
                html += '';
                html += '<div class="file-title"> <a>' + name + '</a></div>';
                html += '';
                html += '';
                html += '</div>';
                $(".uploader-images-list").prepend(html);
            }
            function progress(id, percent) {
                //var html = '<p class="progress" style=""><span style=" width: 24%; "></span></p>';
                var c = $list.find("." + id);
                var d = c.find(".progress span");
                d.length || (d = $('<p class="progress"><span></span></p>').appendTo(c).find("span"));
                d.css("width", percent + "%")
            }

        })
        //
        $(document).on("click", ".uploader-images-list .file-item .fancybox", function () {
            var $that = $(this);
            var single = '<%=Single%>';
            if (single == "0") {
                var isCur = $that.find(".icon").hasClass("icon-cur");
                if (isCur) {
                    $that.find(".icon").removeClass("icon-cur");
                }
                else {
                    $that.find(".icon").addClass("icon-cur");
                }
            }
            else {
                $(".uploader-images-list .file-item").find(".icon").removeClass("icon-cur");
                $that.find(".icon").addClass("icon-cur");
            }
        })

        $(document).on("change", "#selBook", function () {
            var $that = $(this);
            var bookid = $that.val();
            var single = '<%=Single%>';
            window.location.href = window.location.pathname + "?bookid=" + bookid + "&single=" + single;
        })
        function getSelected() {
            var $seleced = $("#dndArea").find(".file-item .icon-cur");
            var list = [];
            $seleced.each(function () {
                var $item = $(this).closest(".file-item");
                list.push($item.data());
            })
            //if ($seleced.length > 0) {
            //    var $item = $seleced.closest(".file-item");
            //    return $item.data();
            //}
            return list;
        }
        $(document).on("click", "#btnOK", function () {
            var list = getSelected();
            if (list.length > 0) {
                parent.window.tw.confirm(list);
            }
            else {
                alert("请选择图片！");
            }
        })

        $(document).on("click", "#watermarkEdit", function () {
            //设置水印
            var lyIndex = layer.open({
                type: 2,
                title: '设置水印',
                shadeClose: true,
                shade: [0.3, '#393D49'],
                //maxmin: true, //开启最大化最小化按钮
                area: ['350px', '500px'],
                content: "/WebImgUpload/WaterMarkEditPopup.aspx"
            });
        })
        $(document).on("click", ".operationBar .del", function (event) {
            event.preventDefault();
            var $that = $(this);
            var b = confirm("删除会影响使用该文件的地方,是否继续删除?");
            if (b) {
                $.post("/Ajax/ActionHandler.ashx", {
                    action: "DelUserPicture",
                    id: $that.data().id
                }).done(function (result) {
                    if (result.code > 0) {
                        $that.closest(".pictureFrame").remove();
                    }
                });
            }
        })

        /*修改标题*/
        function editTitle(obj, name) {
            var $that = $(obj);
            $that.removeAttr('onclick').unbind('click'); // 移除点击事件 
            var input = $('<input />').val(name).blur(function () {
                var $input = $(this);
                $that.unbind('click').click(function () {
                    editTitle(obj, name); // 补回点击事件
                });                $(this).remove();                var value = $.trim($(this).val());                if (value == "") {
                    value = name;
                }                $that.html('<a>' + value + '</a>');
                if (value != name) {
                    $.post("/Ajax/ActionHandler.ashx", {
                        action: "SetUserPictureTitle",
                        id: $that.data().id,
                        name: $input.val()
                    }).done(function (result) {

                    });
                }
            });
            $that.html(input);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div id="tabhead" class="tabhead">
                <a class="tab focus">图片上传</a>
                <a class="tab" href="UserPictureBook_List.aspx">相册管理</a>
            </div>

            <div id="tabbody" class="tabbody">

                <div id="upload" class="panel focus">
                    <div class="trip-uploader" style="height: 160px;">
                        <div class="uploadbtnbox">
                            <div class="webuploader-container">
                                <div id="imgupload" class="upload-btn">上传图片</div>
                            </div>
                            <div class="left" style="margin-left: 15px;">
                                <input type="checkbox" id="cbWatermark" />
                                <label for="cbWatermark" class="lbWatermark">添加水印</label>
                                <a class="watermarkEdit" id="watermarkEdit">[设置]</a>
                            </div>
                            <div class="picbooks" style="margin-left: 15px;">
                                上传到:
                                <select id="selBook">
                                    <option <%=BookID==0?"selected='selected'":""%> value="0">全部</option>
                                    <%if (BookList != null && BookList.Count > 0)
                                      {
                                          foreach (Model.UserPictureBookEntity item in BookList)
                                          {
                                    %>
                                    <option <%=BookID==item.ID?"selected='selected'":""%> value="<%=item.ID%>"><%=item.Name%></option>
                                    <%
                                          }
                                      } %>
                                </select>
                            </div>
                            <div style="float: right; color: green; font-weight: bold; margin-right: 7px;">
                                <a href="javascript:window.location.reload()">[刷新]</a>
                            </div>
                        </div>

                        <div id="dndArea" class="uploader-images-list">
                            <%if (UserPictureList != null && UserPictureList.Count > 0)
                              {
                                  foreach (Model.UserPictureEntity item in UserPictureList)
                                  {
                            %>
                            <div class="pictureFrame">
                                <div class="file-item"
                                    data-id="<%=item.ID%>"
                                    data-tn="<%=item.Tn%>"
                                    data-show="<%=item.Show%>"
                                    data-orig="<%=item.Orig%>"
                                    data-title="<%=item.Title%>">
                                    <div class="fancybox">
                                        <a class="imgbox">
                                            <img src="<%=item.Tn%>" />
                                        </a>
                                        <span class="icon"></span>
                                    </div>
                                    <div class="tjnum">
                                        <a href="#<%=item.ID%>" title="图片使用次数"><%=item.UseCount%></a>
                                    </div>
                                    <div class="operationBar">
                                        <a class="del" data-id="<%=item.ID%>" title="删除图片"></a>
                                    </div>
                                </div>
                                <div class="file-title picTitle" data-id="<%=item.ID%>" onclick="editTitle(this,'<%=item.Title%>')">
                                    <a><%=item.Title%></a>
                                </div>
                            </div>
                            <%
                                  }
                              } %>
                        </div>
                    </div>
                </div>
            </div>

            <div id="tabbtn" class="tabbtn">
                <div class="page" data-page="<%=PageIndex%>" data-total="<%=TotalCount%>" data-size="<%=PageSize%>" style="float: left"></div>

                <a class="btn btn-primary" id="btnOK">确定</a>
                <a class="btn btn-warning" onclick="CloseWindow()">取消</a>
            </div>
        </div>


    </form>
</body>
</html>
