using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Admin.Domain
{
    public partial class Domain_Add : UICommon.BasePage_Admin
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int UserID = UICommon.Util.ConvertToInt32(txtUserID.Value.Trim());
                if (UserID == 0)
                {
                    UICommon.ScriptHelper.Alert("异常");
                    return;
                }
                string DomainName = txtDomainName.Value.Trim();
                //一个域名只能绑定一次
                //int IsExist = DAL.AdminAccountDAL.GetSingle("count(0)", "DomainName=" + DAL.DALUtil.ConverToSqlTxt(DomainName) + " AND UserID=" + UserID);
                //if (IsExist > 0)
                //{
                //    UICommon.ScriptHelper.Alert(DomainName + ",已经存在！");
                //    return;
                //} 
                DateTime? endDate = UICommon.Util.ConvertToDateTime(txtEndDate.Value.Trim());
                SqlParameter[] pramsAdd =
                    {
                        DAL.DALUtil.MakeInParam("@DomainName",System.Data.SqlDbType.NVarChar,100,DomainName),  
                        DAL.DALUtil.MakeInParam("@EndDate",System.Data.SqlDbType.DateTime,8,endDate), 
                        DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,UserID),  
                    };
                int row_Add = DAL.DomainDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    ltMsg.Visible = true;
                    ltMsg.Text = DomainName + ",添加成功！";
                    txtDomainName.Value = "";
                    UICommon.ScriptHelper.Alert(DomainName + ",新增成功！");
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