﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace UICommon
{
    public class BasePage_PM : System.Web.UI.Page
    {

        public SuperAdministratorEntity superAdministrator = null;
        public UserInfoEntity userInfo = null;
        protected override void OnInit(EventArgs e)
        {
            try
            {
                if (Session["Administrator"] == null || Session["Administrator"].ToString() == "")
                {
                    Response.Write("访问时间过长或未登录！");
                    Response.Write("<script >parent.location.href='/login.aspx';</script>");
                    Response.End();
                }
                superAdministrator = Session["Administrator"] as SuperAdministratorEntity;

                if (Session["UserInfo"] == null || Session["UserInfo"].ToString() == "")
                {
                    Response.Write("请选择管理用户！");
                    Response.Write("<script >parent.location.href='/Admin/User/UserInfo_List.aspx';</script>");
                    Response.End();
                }
                userInfo = Session["UserInfo"] as UserInfoEntity;
                this.Title = "[" + userInfo.UserName + "]用户配置后台";
            }
            catch (Exception)
            {

                throw;
            }

            base.OnInit(e);
        }

        /// <summary>
        /// 在这里我们给Form中的服务器控件添加客户端onkeydown脚步事件，防止服务器控件按下enter键直接回发
        /// CancelFormControlEnterKey(this.Page.Form.Controls);
        /// </summary>
        /// <param name="controls"></param>
        public static void CancelFormControlEnterKey(System.Web.UI.ControlCollection controls)
        {
            foreach (Control item in controls)
            {
                //服务器TextBox
                if (item.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    WebControl webControl = item as WebControl;
                    webControl.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {return false;}} ");
                }
                else if (item.GetType() == typeof(System.Web.UI.WebControls.GridView))
                {
                    WebControl webControl = item as WebControl;
                    webControl.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {return false;}} ");

                }
                //html控件
                else if (item.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                {
                    HtmlInputControl htmlControl = item as HtmlInputControl;
                    htmlControl.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {return false;}} ");
                }
                //用户控件
                else if (item is System.Web.UI.UserControl)
                {
                    CancelFormControlEnterKey(item.Controls); //递归调用
                }
            }
        }
    }
}