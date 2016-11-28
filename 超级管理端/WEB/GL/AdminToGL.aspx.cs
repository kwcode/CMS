using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.GL
{
    public partial class AdminToGL : UICommon.BasePage_Admin
    {
        public int UserID
        {
            get
            {
                return UICommon.Util.ConvertToInt32(Request["UserID"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            UserInfoEntity userInfo = DAL.UserInfoDAL.Get_99(UserID, "id,TC_Name,UserName,LastLoginTime");
            if (userInfo != null && userInfo.ID > 0)
            {
                Session["UserInfo"] = userInfo;
                Response.Redirect("/GL/Index.aspx");
            }
            else
            {
                Response.Write("获取用户信息失败");
            }

        }
    }
}