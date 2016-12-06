using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Admin.User
{
    public partial class AdminAccount_Add : UICommon.BasePage_Admin
    {
        public int UserID
        {
            get
            {
                int UserID = UICommon.Util.ConvertToInt32(Request["UserID"]);
                return UserID > 0 ? UserID : 1;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserID == 0)
                {
                    UICommon.ScriptHelper.Alert("异常");
                    return;
                }
                string LoginName = txtLoginName.Value.Trim();
                //判断账号不存在
                int IsExist = DAL.AdminAccountDAL.GetSingle("count(0)", "LoginName=" + DAL.DALUtil.ConverToSqlTxt(LoginName) + " AND UserID=" + UserID);
                if (IsExist > 0)
                {
                    UICommon.ScriptHelper.Alert(LoginName + ",已经存在！");
                    return;
                }
                string Password = txtPassword.Value.Trim();
                string md5Password = UICommon.SecurityHelper.MD5Encrypt(Password);

                SqlParameter[] pramsAdd =
                    {
                        DAL.DALUtil.MakeInParam("@LoginName",System.Data.SqlDbType.NVarChar,100,LoginName),  
                        DAL.DALUtil.MakeInParam("@Password",System.Data.SqlDbType.NVarChar,200,md5Password), 
                        DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,UserID),  
                    };
                int row_Add = DAL.AdminAccountDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    ltMsg.Visible = true;
                    string message = "账号：" + LoginName + "<br/>密码：" + Password;
                    ltMsg.Text = message;
                    txtLoginName.Value = "";
                    UICommon.ScriptHelper.Alert(LoginName + ",新增成功！");
                }
                else
                {
                    UICommon.ScriptHelper.Alert("保存失败");
                }
            }
            catch (Exception ex)
            {
                UICommon.ScriptHelper.Alert("保存失败," + ex.Message);
            }
        }
    }
}