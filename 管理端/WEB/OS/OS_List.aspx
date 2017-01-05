<%@ Page Language="C#"  ValidateRequest="false"  AutoEventWireup="true" CodeBehind="OS_List.aspx.cs" Inherits="WEB.OS.OS_List" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/base.css" rel="stylesheet" />
    <link href="/css/datagrid.css?v=1" rel="stylesheet" />
    <%if (IsOnlineServices == 1)
      { 
    %>
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/jquery-page.js"></script>
    <script src="/js/common.js"></script>
    <script src="/js/tc-list.js"></script>
    <%  } %>
    <style>
          <%if (IsOnlineServices == 1)
            { 
     %>
        .editbotton { position: absolute; top: 9px; left: 9px; }
        <%  }
            else
            {
                %>
                    .ed-title { background: #308ad2; height: 35px; line-height: 35px; padding-left: 10px; color: #FFF; }
                    .ed-title h3 { float: left; color: #FFF; font-weight: bold;}
                    <%
            } %>
    </style>
</head>
<body>
    <div class="g-div-e">
        <%if (IsOnlineServices == 1)
          { 
        %>

        <div class="t-tool">
            <div class="left" style="margin-left: 125px;">
                <a class="l-btn" target="_blank" href="OS_Add.aspx">
                    <span class="l-btn-icon  icon-add"></span>
                    <span class="l-btn-text">新增</span>
                </a>
                <a class="l-btn btn-del" onclick="Del_ChecdList()">
                    <span class="l-btn-icon  icon-remove"></span>
                    <span class="l-btn-text">删除</span>
                </a>
            </div>
            <div class="right" style="margin-right: 20px; padding: 0px 10px;">
                <div class="searchbox clearfix">
                    <input type="text" class="inpt-search" value="<%=KeyWords%>" onkeydown="OnEnter(this)" id="txtSearch" />
                    <a class="btn-search" onclick="Search()">
                        <span class="s-icon"></span>
                        <span class="s-text">搜索</span>
                    </a>
                </div>
            </div>
        </div>

        <%
          } %>
        <form id="Form1" runat="server">
            <%if (IsOnlineServices != 1)
              { 
            %>
            <div class="clearfix ed-title">
                <h3>自定义在线客服
                </h3>
            </div>
            <div>
                <textarea style="width: 500px; height: 150px; margin-bottom: 10px; margin-top: 10px;" runat="server" id="txtOSJsCode"></textarea>
            </div>
            <%
              } %>
            <div>
                <asp:Button Text="保存自定义在线客服" runat="server" ID="btnSaveOnline" CssClass="editbotton btn btn-primary" OnClick="btnSaveOnline_Click" />

                <asp:Button Text="关闭在线客服" runat="server" ID="btnSave" CssClass="editbotton btn btn-warning" OnClick="btnSave_Click" />
            </div>

            <%if (IsOnlineServices == 1)
              { 
            %>
            <asp:GridView ID="gv_List" DataKeyNames="ID" CssClass="m-table" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gv_List_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="全选">
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                        <ItemTemplate>
                            <%#PageSize*(PageIndex-1)+Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input id="chkCheckAll" runat="server" name="controlAll" type="checkbox" class="list_checkall" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                        <ItemTemplate>
                            <input id="chkSelect" value='<%#Eval("ID")%>' runat="server" name="checkitems" type="checkbox" class="list_checkitems" />
                            <input type="hidden" class="hide_id" value='<%#Eval("ID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="类型">
                        <ItemStyle HorizontalAlign="Center" Width="80" />
                        <ItemTemplate>
                            <asp:Literal ID="ltOSType" runat="server" Text='<%#Eval("OSType")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="名称" FooterText="名称">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Literal ID="Title" runat="server" Text='<%#Eval("Title")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="值">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Literal ID="ValueNum" runat="server" Text='<%#Eval("ValueNum")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建时间" FooterText="创建时间">
                        <ItemStyle HorizontalAlign="Center" Width="120" />
                        <ItemTemplate>
                            <asp:Literal ID="CreateTS" runat="server" Text='<%#Eval("CreateTS","{0:yyyy-MM-dd HH:mm}")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="操作" FooterText="操作">
                        <ItemStyle HorizontalAlign="Center" Width="100" />
                        <ItemTemplate>
                            <a class="td-btn" href="OS_Modify.aspx?id=<%#Eval("ID")%>" target="_blank">
                                <span class="td-btn-icon  icon-edit"></span>
                                <span class="td-btn-text">修改</span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%
              } %>
        </form>
        <%if (IsOnlineServices == 1)
          { 
        %>
        <!--分页控件-->
        <div class="page" data-page="<%=PageIndex%>" data-total="<%=TotalCount%>" data-size="<%=PageSize%>" style="float: left"></div>
        <%
          } %>
    </div>

</body>
</html>
