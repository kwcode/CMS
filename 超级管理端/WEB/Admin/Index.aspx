<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WEB.Admin.Index" %>


<%@ Import Namespace="Model" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/base.css" rel="stylesheet" />
    <link href="/css/datagrid.css" rel="stylesheet" />
    <link href="/css/admin_index.css" rel="stylesheet" />
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
                <h1 class="logo">超级管理后台
                </h1>
                <div class="login-info ue-clear">
                    <div class="welcome ue-clear"><span>欢迎您,</span><a href="javascript:;" class="user-name"><%=superAdministrator.NickName %></a></div>
                    <%--<div class="login-msg ue-clear">
                        <a href="javascript:;" class="msg-txt">消息</a>
                        <a href="javascript:;" class="msg-num">10</a>
                    </div>--%>
                </div>
                <div class="toolbar ue-clear">
                    <%--<a href="javascript:;" data-href="ProjectConfiguration_List.aspx" class="btn-set">基础配置</a>
                    <a href="index.aspx" class="home-btn">首页</a>--%>
                    <a href="Exit.aspx" class="quit-btn exit"></a>
                </div>
            </div>
        </div>
    </div>
    <!--中间-->
    <div data-options="region:'west',split:true,title:'功能导航'" style="width: 190px;">
        <ul class="nav">
            <%-- <li class="office current">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear" date-src="/Admin/User/UserInfo_List.aspx"><span>用户管理</span><i class="icon"></i></a>
                </div>
            </li>--%>
            <li class="office">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>用户管理</span><i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="/Admin/User/UserInfo_List.aspx">用户信息</a></li>
                    <li><a href="javascript:;" date-src="/Admin/Domain/Domain_List.aspx">域名配置</a></li>
                </ul>
            </li>
            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear">
                        <span>配置</span><i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="Template/Templates_List.aspx">模板配置</a></li> 
                    <li><a href="javascript:;" date-src="BackSectionsSet/BackSectionsSet_List.aspx">基础栏目配置</a></li>
                </ul>
            </li>

            <%-- <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear">
                        <span>产品管理</span><i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="/Product/Product_List.aspx">产品管理</a></li>
                </ul>
            </li>--%>

            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>登录管理</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="SuperAdministrator/SuperAdministrator_List.aspx">账号管理</a></li>
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
                <span>超级管理后台</span>
                <em>SuperAdministrator&nbsp;System</em>
            </div>
            <div class="ft-right">
                <span>Automation</span>
                <em>V1.0&nbsp;2015</em>
            </div>
        </div>
    </div>
</body>
</html>
