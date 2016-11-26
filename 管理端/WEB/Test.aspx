<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%
                UICommon.FileHelper.GetDirectory(10, UICommon.FileNameIndex.ContentPictures);
                Model.UserInfoEntity userInfo = DAL.UserInfoDAL.Get_99(1, "id,TC_Name,UserName,LastLoginTime");
                userInfo.LoginName = "admin";
                Session["UserInfo"] = userInfo;
            %>
            <a href="Index.aspx">跳转到首页</a>
        </div>
    </form>
</body>
</html>
