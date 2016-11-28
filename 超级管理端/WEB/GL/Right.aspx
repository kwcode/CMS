<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Right.aspx.cs" Inherits="WEB.GL.Right" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/base.css" rel="stylesheet" />
    <link href="/css/home.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="g-div-e">
            <div class="article toolbar">
                <div class="title ue-clear">
                    <h2>系统信息</h2>
                </div>
                <div class="content matter-content ue-clear">

                    <div class="today">
                        <p class="year">
                            <script>
                                var d = new Date();
                                document.write(d.getFullYear() + "年" + (d.getMonth() + 1) + "月");
                            </script>
                        </p>
                        <p class="date">
                            <script>
                                var d = new Date();
                                document.write(d.getDate());
                            </script>
                        </p>
                    </div>
                    <div>
                        <p class="info">
                            <a>客户端操作系统： <span><%=Request.Browser.Platform%></span></a>
                            <a>客户端计算机名： <span><%=Request.UserHostName%></span> </a>
                            <a>客户端IP： <span><%=Request.UserHostAddress %></span></a>

                        </p>
                        <p class="info">
                            <a>浏览器： <span><%=Request.Browser.Browser%></span> </a>
                            <a>浏览器版本： <span><%=Request.Browser.Version %></span></a>
                            <a>浏览器类型： <span><%=Request.Browser.Type%></span> </a>
                        </p>
                        <p class="info">浏览器类型和版本：<%=Request.UserAgent%></p>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
