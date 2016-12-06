<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEB.Login" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登录</title>

    <link href="css/A_login.css" rel="stylesheet" />
    <script src="js/jquery-1.8.3.min.js"></script>
    <%--<script src="js/layer/layer.js"></script>--%>
    <%--<script src="js/common.js"></script>--%>
    <script src="js/A_Login.js"></script>
    <script type="text/javascript">
        $(function () {
            $(this).keyup(function (event) {
                if (event.keyCode == 13) {
                    $("<%=btnLogin.ClientID%>").click();
                }
            })
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="top_div"></div>
        <div class="login_content">
            <div style="width: 165px; height: 96px; position: absolute;">
                <div class="tou"></div>
                <div class="initial_left_hand" id="left_hand"></div>
                <div class="initial_right_hand" id="right_hand"></div>
            </div>
            <p style="padding: 30px 0px 0px; position: relative;">
                <span class="u_logo"></span>
                <input id="txtName" class="input_text" type="text" placeholder="请输入用户名" runat="server" />
            </p>
            <p style="position: relative;">
                <span class="p_logo"></span>
                <input id="txtPwd" class="input_text" type="password" placeholder="请输入密码" runat="server" />
            </p>
            <p style="position: relative;">
                <span class="y_logo"></span>
                <input id="txtYzm" maxlength="6" class="input_text" type="text" placeholder="请输入验证码" style="position: relative; width: 140px; top: -12px;" runat="server" />&nbsp;
                <img id="imgcode" class="img-rounded yzmImg" src="ValidateCode.aspx" onclick="ckcode();" title="看不清，点击更换" />
            </p>
            <div style="height: 50px; line-height: 50px; margin-top: 20px; border-top-color: rgb(231, 231, 231); border-top-width: 1px; border-top-style: solid;">
                <p style="margin-top: 10px;">
                    <asp:Button ID="btnLogin" runat="server" Text="登录" CssClass="btn-primary" Style="position: relative; width: 100px; margin-left: 70px; height: 35px; top: -10px; background-color: #286090; color: #ffffff; border-radius: 10%;" OnClick="btnLogin_Click" />
                    <input type="reset" id="btnReset" value="重置" class="btn-primary" style="position: relative; width: 100px; margin-left: 70px; height: 35px; top: -10px; background-color: rgb(128, 128, 128); color: #ffffff; border-radius: 10%;" />
                </p>
            </div>
        </div>
    </form>
</body>
</html>
