﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WEB.Admin.SuperAdministrator
{
    public partial class SuperAdministrator_Add : UICommon.BasePage_Admin
    {
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
                string NickName = txtNickName.Value.Trim();
                string LoginName = txtLoginName.Value.Trim();
                //判断账号不存在
                int IsExist = DAL.SuperAdministratorDAL.GetSingle("count(0)", "LoginName=" + DAL.DALUtil.ConverToSqlTxt(LoginName));
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
                        DAL.DALUtil.MakeInParam("@NickName",System.Data.SqlDbType.NVarChar,50,NickName),  
                    };
                int row_Add = DAL.SuperAdministratorDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    ltMsg.Visible = true;
                    string message = "超级账号：" + LoginName + "<br/>密码：" + Password;
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