using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Admin.User
{
    public partial class Password_Modify : UICommon.BasePage_Admin
    {
        public int ID
        {
            get
            {
                return UICommon.Util.ConvertToInt32(Request["ID"]);
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
                string password = txtPassword.Value.Trim();
                string md5Password = UICommon.SecurityHelper.MD5Encrypt(password);
                SqlParameter[] pramsModify =
                    {
                        //DAL.DALUtil.MakeInParam("@UserName",System.Data.SqlDbType.NVarChar,100,UserName),   
                    };
                //int row_Mod = DAL.UserInfoDAL.Modify(pramsModify, ID);
                //if (row_Mod > 0)
                //{
                //    txtUserName.Value = "";
                //    txtMaturityTime.Value = "";
                //    UICommon.ScriptHelper.CloseAndAlert(UserName + ",修改成功！");
                //}
                //else
                //{
                //    UICommon.ScriptHelper.Alert("保存失败");
                //}
            }
            catch (Exception ex)
            {
                UICommon.ScriptHelper.Alert("保存失败," + ex.Message);
            }
        }

    }
}