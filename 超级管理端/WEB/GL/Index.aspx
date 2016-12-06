<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WEB.GL.Index" %>


<%@ Import Namespace="Model" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/base.css" rel="stylesheet" />
    <link href="/css/datagrid.css" rel="stylesheet" />
    <link href="/css/index.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <link href="/js/easyui/layout/css.css" rel="stylesheet" />
    <script src="/js/easyui/layout/js.js"></script>
    <script src="/js/index.js"></script>
</head>
<body class="easyui-layout">
    <!--头部-->
    <div data-options="region:'north',border:false" style="height: 55px;">
        <div id="hd">
            <div class="hd-wrap ue-clear">
                <div class="top-light"></div>
                <h1 class="logo">用户配置后台
                </h1>
                <div class="login-info ue-clear">
                    <div class="welcome ue-clear"><span>欢迎您,</span><a href="javascript:;" class="user-name"><%=userInfo.UserName %></a></div>
                    <%--<div class="login-msg ue-clear">
                        <a href="javascript:;" class="msg-txt">消息</a>
                        <a href="javascript:;" class="msg-num">10</a>
                    </div>--%>
                </div>
                <div class="toolbar ue-clear">
                    <a href="/Admin/index.aspx" class="btn-set">基础配置</a>
                    <a href="/GL/index.aspx" class="home-btn">首页</a>
                    <a href="Exit.aspx" class="quit-btn exit"></a>
                </div>
            </div>
        </div>
    </div>
    <!--中间-->
    <div data-options="region:'west',split:true,title:'功能导航'" style="width: 190px;">
        <ul class="nav">
            <li class="office current">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>功能配置</span><i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="Fun/CopyTC.aspx">复制</a></li>
                    <li><a href="javascript:;" date-src="Fun/ImageSizeSet.aspx">图片尺寸</a></li>
                </ul>
            </li>

            <li class="office ">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>栏目配置</span><i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="BackgroundMenu/BackgroundMenuClass1_List.aspx">后台栏目一类</a></li>
                    <li><a href="javascript:;" date-src="BackgroundMenu/BackgroundMenu_List.aspx">后台栏目</a></li>
                    <li><a href="javascript:;" date-src="BackgroundMenu/NavigationBar_List.aspx">前台Link栏目</a></li>
                </ul>
            </li>
            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>图文配置</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="PictureText/PictureTextClass1_List.aspx">插图分类</a></li>
                    <li><a href="javascript:;" date-src="PictureText/PictureText_List.aspx">插图配置</a></li>
                    <li><a href="javascript:;" date-src="Banner/Banner_List.aspx">百叶窗</a></li>
                </ul>
            </li>
            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>文章配置</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="Article/ArticleClass1_List.aspx">文章分类</a></li>
                    <li><a href="javascript:;" date-src="Article/Article_List.aspx">文章列表</a></li>
                </ul>
            </li>
            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>其他管理</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="Domain/Domain_List.aspx">域名管理</a></li>
                    <li><a href="javascript:;" date-src="">客户管理</a></li>
                </ul>
            </li>
        </ul>
    </div>
    <div data-options="region:'center',title:'首页',iconCls:'icon-brk',tools:[{
					iconCls:'icon-reload',
					handler:function(){
						addpanel('首页', 'Right.aspx');
					}
				}]"
        id="panel_c">
        <iframe src="Right.aspx" id="iframe" width="100%" height="100%" frameborder="0"></iframe>
    </div>
    <div data-options="region:'south',border:false" style="height: 30px;">
        <div id="ft" class="ue-clear">
            <div class="ft-left">
                <span>用户配置后台</span>
                <em>GLAdministrator&nbsp;System</em>
            </div>
            <div class="ft-right">
                <span>Automation</span>
                <em>V1.0&nbsp;2015</em>
            </div>
        </div>
    </div>
</body>
</html>

