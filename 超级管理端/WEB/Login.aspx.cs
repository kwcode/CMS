
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string name = txtName.Value.Trim();
            string pwd = txtPwd.Value.Trim();
            string yzm = txtYzm.Value.Trim();
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pwd) && !string.IsNullOrEmpty(yzm))
            {

                string ValidateCode = UICommon.Util.ConvertToString(Session["ValidateCode"]);
                if (yzm == ValidateCode)
                {
                    Session["ValidateCodeError"] = 0;
                    Session["ValidateCodeLength"] = 2;
                    string password = UICommon.SecurityHelper.MD5Encrypt(pwd);
                    SuperAdministratorEntity entity = DAL.SuperAdministratorDAL.Login(name, password);
                    if (entity.Result == 1)
                    {
                        UICommon.ScriptHelper.Alert("账号不存在！");
                    }
                    else if (entity.Result == 2)
                    {
                        UICommon.ScriptHelper.Alert("密码不正确！");
                    }
                    else if (entity.Result == 8)
                    {
                        Session["Administrator"] = entity;
                        UICommon.ScriptHelper.ShowAndRedirect("登录成功！", "/Admin/Index.aspx");
                    }
                    else
                    {
                        UICommon.ScriptHelper.Alert("登录异常，请刷新重试！");
                    }
                }
                else
                {
                    int ValidateCodeError = UICommon.Util.ConvertToInt32(Session["ValidateCodeError"]);
                    Session["ValidateCodeError"] = ValidateCodeError + 1;
                    if (ValidateCodeError >= 5)
                    {
                        Session["ValidateCodeLength"] = 5;//验证码错误4次
                    }
                    UICommon.ScriptHelper.Alert("验证码不正确！");
                }
            }
            else
            {
                UICommon.ScriptHelper.Alert("请填写完整的登录信息！");

            }
        }
    }
}