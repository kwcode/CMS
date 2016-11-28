using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UICommon
{
    public class BasePage_Admin : System.Web.UI.Page
    {
        public SuperAdministratorEntity superAdministrator = null;
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
            }
            catch (Exception)
            {

                throw;
            }

            base.OnInit(e);
        }
    }
}
