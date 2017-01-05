<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPictureBook_List.aspx.cs" Inherits="WEB.WebImgUpload.UserPictureBook_List" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>图片上传</title>
    <link href="/css/uploadpop.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/jquery-page.js"></script>
    <script src="/js/plupload/moxie.js"></script>
    <script src="/js/plupload/plupload.js"></script>
    <style>
        .m-table { width: 100%; line-height: normal; border-spacing: 0; background-color: #F6EFE7; border-collapse: collapse; }
        .m-table th, .m-table td { border: 1px solid #ddd; line-height: 24px; padding-left: 3px; }
        .m-table th { font-weight: bold; background: #f7e2ca; color: #0014ff; }
        .m-table tbody tr:nth-child(2n) { background: #fafafa; }
        .m-table tbody tr:hover { background: #f0f0f0; }

        .ed-table { margin-top: 2px; background-color: rgb(225, 225, 225); width: 100%; line-height: normal; border-spacing: 0; border-collapse: collapse; }
        .ed-table tr { border: 1px solid #ddd; height: 32px; background-color: #FFF; }
        .ed-table tr:nth-child(2n) { background: #f1f0f0; }
        .ed-table td { border: 1px solid #ddd; padding-left: 5px; }
        .ed-table td input[type='text'] { width: 300px; padding: 4px 2px; border: 1px solid #007588; margin-left: 5px; }
        .ed-table td textarea { width: 700px; padding: 4px 2px; height: 80px; border: 1px solid #007588; margin-left: 5px; }
        .ed-table td select { width: 150px; padding: 4px 2px; border: 1px solid #007588; margin-left: 5px; }
        .ed-table td.alignright { width: 150px; font-weight: bold; }

        input, button, select, textarea { outline: 0; }
        .ed-title { background: #308ad2; height: 35px; line-height: 35px; padding-left: 10px; color: #FFF; }
        .ed-title h3 { float: left; }
        .ed-title .close { float: right; padding-right: 10px; }

        /*工具栏*/
        .t-tool { height: 40px; line-height: 40px; text-align: center; }
        /*按钮*/
        .l-btn { text-decoration: none; color: #fff; background-color: #428bca; border-color: #357ebd; position: relative; overflow: hidden; display: inline-block; padding: 6px 12px; margin-bottom: 0; font-size: 14px; font-weight: 400; line-height: 1; text-align: center; white-space: nowrap; vertical-align: middle; -ms-touch-action: manipulation; touch-action: manipulation; cursor: pointer; -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; user-select: none; background-image: none; border: 1px solid transparent; border-radius: 4px; }
        .l-btn:hover { background: #2c7bbf; }
        .l-btn-icon { display: inline-block; width: 16px; height: 16px; line-height: 16px; position: absolute; top: 50%; margin-top: -8px; font-size: 1px; }
        .l-btn-text { margin-left: 20px; border: 0; background: transparent; color: #FFF; font-size: 12px; }
        .icon-add { background: url('/images/edit_add.png') no-repeat center center; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div id="tabhead" class="tabhead">
                <a class="tab " href="AlbumPopup.aspx">图片上传</a>
                <a class="tab focus">相册管理</a>
            </div>

            <div id="tabbody" class="tabbody">
                <div id="books" class="panel">
                    <div id="listbox">
                        <div class="t-tool">
                            <a class="l-btn" id="btn_Add">
                                <span class="l-btn-icon  icon-add"></span>
                                <span class="l-btn-text">新增一个相册</span>
                            </a>
                        </div>
                        <div style="padding: 1px;">
                            <table class="m-table" id="bookstable">
                                <tr>
                                    <th>名称</th>
                                    <th style="width: 50px; text-align: center;">排序</th>
                                    <th style="width: 50px; text-align: center;">操作</th>
                                </tr>
                                <%if (BookList != null && BookList.Count > 0)
                                  {
                                      foreach (Model.UserPictureBookEntity item in BookList)
                                      {
                                %>
                                <tr>
                                    <td><%=item.Name %></td>
                                    <td style="width: 50px; text-align: center;"><%=item.OrderNum %></td>
                                    <td style="width: 50px; text-align: center;">
                                        <a class="btn_Modify" data-id="<%=item.ID%>" data-name="<%=item.Name%>" data-ordernum="<%=item.OrderNum%>">编辑</a>
                                    </td>
                                </tr>
                                <%
                                      }
                                  } %>
                            </table>
                        </div>
                    </div>

                    <div style="padding: 3px 3px; display: none;" id="editbox">
                        <div class="clearfix ed-title">
                            <h3>新增
                            </h3>
                            <div class="close">
                                <a class="btnCancel">
                                    <img src="/Images/close.gif" />
                                </a>
                            </div>
                        </div>
                        <table class="ed-table">
                            <tr>
                                <td>名称：</td>
                                <td>
                                    <input type="text" id="txtName" maxlength="100" /></td>
                            </tr>
                            <tr>
                                <td>排序：</td>
                                <td>
                                    <input type="text" id="txtOrderNum" maxlength="100" /></td>
                            </tr>
                            <tr>
                                <td style="padding: 6px; text-align: center;" colspan="2">
                                    <input type="hidden" id="hide_MaxOrderNum" value="<%=MaxOrderNum%>" />
                                    <a class="btn btn-primary" id="btnSave">确定</a>
                                    <a class="btn btn-warning btnCancel">取消</a></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <script>
            $(function () {
                var MaxOrderNum = $("#hide_MaxOrderNum").val();
                $("#btn_Add").click(function () {
                    $("#txtName").val("");
                    $("#txtOrderNum").val(MaxOrderNum);
                    $("#btnSave").data().id = 0;
                    $("#editbox").fadeIn("slow");
                    $("#listbox").hide(1000);
                })
                $(".btn_Modify").click(function () {
                    var $that = $(this);
                    var $data = $that.data();
                    $("#txtName").val($data.name);
                    $("#txtOrderNum").val($data.ordernum);
                    $("#btnSave").data().id = $data.id;

                    $("#editbox").fadeIn("slow");
                    $("#listbox").hide(1000);

                });
                $("#btnSave").click(function () {
                    var $that = $(this);
                    var id = $that.data().id;
                    var name = $("#txtName").val();
                    var ordernum = $("#txtOrderNum").val();
                    $.post("/Ajax/ActionHandler.ashx", {
                        action: "SaveUserPictureBook",
                        name: name,
                        ordernum: ordernum,
                        id: id
                    }).done(function (result) {
                        if (result.code > 0) {
                            alert(result.msg);
                            window.location.reload();
                        }
                        else {
                            alert(result.msg);
                        }
                    }).fail(function (ex) { alert("保存失败,请刷新重试！"); })
                });
                $(".btnCancel").click(function () {
                    $("#editbox").hide(1000);
                    $("#listbox").fadeIn("slow");
                });
            })
        </script>
    </form>
</body>
</html>
