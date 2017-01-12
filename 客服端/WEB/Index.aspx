<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WEB.Index" %>


<%@ Import Namespace="Model" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/base.css" rel="stylesheet" />
    <link href="css/datagrid.css" rel="stylesheet" />
    <link href="css/index.css" rel="stylesheet" />
    <script src="js/jquery-1.8.3.min.js"></script>
    <link href="js/easyui/layout/css.css" rel="stylesheet" />
    <script src="js/easyui/layout/js.js"></script>
    <script src="js/index.js"></script>
</head>
<body class="easyui-layout">
    <!--头部-->
    <div data-options="region:'north',border:false" style="height: 55px;">
        <div id="hd">
            <div class="hd-wrap ue-clear">
                <div class="top-light"></div>
                <h1 class="logo"><a href="Index.aspx" style="color: #FFF;">后台管理</a>
                </h1>
                <div class="login-info ue-clear">
                    <div class="welcome ue-clear"><span>欢迎您,</span><a href="javascript:;" class="user-name"><%=userInfo.UserName %></a></div>
                </div>
                <div class="toolbar ue-clear">
                    <a href="/Index.aspx" class="home-btn">首页</a>
                    <a href="Exit.aspx" class="quit-btn exit"></a>
                </div>
            </div>
        </div>
    </div>
    <!--中间-->
    <div data-options="region:'west',split:true,title:'功能导航'" style="width: 190px;">
        <ul class="nav">
            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>答库管理</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="/Support/Support_List.aspx">答库列表</a></li>
                    <li><a href="javascript:;" date-src="/Support/SupportGetRecord_List.aspx">请求记录列表</a></li>
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
                <em>后台管理</em>
            </div>
            <div class="ft-right">
                <em>V2.0&nbsp;2015</em>
            </div>
        </div>
    </div>
</body>
</html>
