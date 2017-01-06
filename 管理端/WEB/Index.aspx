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
                    <%--<div class="login-msg ue-clear">
                        <a href="javascript:;" class="msg-txt">消息</a>
                        <a href="javascript:;" class="msg-num">10</a>
                    </div>--%>
                </div>
                <div class="toolbar ue-clear">
                    <a href="<%=PreviewAddress %>" target="_blank" class="preview-btn">预览</a>
                    <a href="/Index.aspx" class="home-btn">首页</a>
                    <a href="Exit.aspx" class="quit-btn exit"></a>
                </div>
            </div>
        </div>
    </div>
    <!--中间-->
    <div data-options="region:'west',split:true,title:'功能导航'" style="width: 190px;">
        <ul class="nav">
            <%if (BackgroundMenuClass1List != null && BackgroundMenuClass1List.Count > 0)
              {
                  foreach (BackgroundMenuClass1Entity item in BackgroundMenuClass1List)
                  {
            %>
            <li class="nav-info">
                <div class="nav-header"><a href="javascript:;" class="ue-clear"><span><%=item.Title %></span><i class="icon"></i></a></div>
                <ul class="subnav">
                    <% List<BackgroundMenuEntity> towList = GetBackgroundMenuList(item.ValueNum);
                       foreach (BackgroundMenuEntity itemTow in towList)
                       {
                    %>
                    <li><a href="javascript:;" date-src="<%=itemTow.ManageUrl %>"><%=itemTow.Title %></a></li>
                    <%
                       }
                    %>
                </ul>
            </li>
            <%
                  }
              } %>

            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>网站设置</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="/SEO/Seo_Set.aspx">全局SEO优化</a></li>
                    <li><a href="javascript:;" date-src="/Nav/NavigationBar_List.aspx">页面与SEO</a></li>
                    <li><a href="javascript:;" date-src="/SEO/WebFlowTongJi_List.aspx">流量统计</a></li>
                    <li><a href="javascript:;" date-src="/OS/OS_List.aspx">在线客服</a></li>
                </ul>
            </li>

          <%--  <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>资源库管理</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="/SEO/Seo_Set.aspx">管理文件</a></li>
                    <li><a href="javascript:;" date-src="/SEO/Seo_Set.aspx">管理文件夹</a></li>
                </ul>
            </li>--%>
            <%-- <li class="office current">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>首页管理</span><i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="/Manage/ProjectData.aspx">LOGO</a></li>
                    <li><a href="javascript:;" date-src="/Manage/ProjectTable.aspx">表</a></li>
                </ul>
            </li>
            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear">
                        <span>产品管理</span><i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="/Product/ProductClass1_List.aspx">产品一类</a></li>
                    <li><a href="javascript:;" date-src="/Product/ProductClass2_List.aspx">产品二类</a></li>
                    <li><a href="javascript:;" date-src="/Product/ProductClass3_List.aspx">产品三类</a></li>
                    <li><a href="javascript:;" date-src="/Product/Product_List.aspx">产品管理</a></li>
                </ul>
            </li>

            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>文章管理</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="/Article/ArticleClass1_List.aspx">文章分类</a></li>
                    <li><a href="javascript:;" date-src="/Article/Article_List.aspx">文章列表</a></li>
                </ul>
            </li>
            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>解决方案</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="info-reg.html">信息录入22</a></li>
                </ul>
            </li>
            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>成功案例</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="info-reg.html">信息录入22</a></li>
                </ul>
            </li>
            <li class="nav-info">
                <div class="nav-header">
                    <a href="javascript:;" class="ue-clear"><span>通用管理</span>
                        <i class="icon"></i></a>
                </div>
                <ul class="subnav">
                    <li><a href="javascript:;" date-src="/Other/OnlineMessage_List.aspx">留言管理</a></li>
                    <li><a href="javascript:;" date-src="">插图管理</a></li>
                </ul>
            </li>--%>
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
