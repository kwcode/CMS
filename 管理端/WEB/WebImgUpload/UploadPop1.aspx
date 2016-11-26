<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPop1.aspx.cs" Inherits="WEB.WebImgUpload.UploadPop1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                <asp:FileUpload ID="file" runat="server" CssClass="button" size="60" Width="200" />
            </p>
            <p>
                <asp:Button ID="ibUpload" Width="100" CssClass="button" runat="server" Text="上传图片"
                    OnClick="ibUpload_Click" />
            </p>
        </div>
    </form>
</body>
</html>
