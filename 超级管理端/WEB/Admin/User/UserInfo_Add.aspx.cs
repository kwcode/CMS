using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Admin.User
{
    public partial class UserInfo_Add : UICommon.BasePage_Admin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //模板集合 
                List<Model.TemplatesEntity> TemplatesList = DAL.TemplatesDAL.GetList<Model.TemplatesEntity>("*", null, "OrderNum");
                ddlTemplates.DataSource = TemplatesList;
                ddlTemplates.DataTextField = "Name";
                ddlTemplates.DataValueField = "ValueNum";
                ddlTemplates.DataBind();
                ddlTemplates.Items.Insert(0, new ListItem("请选择", ""));
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Templates_ValueNum = UICommon.Util.ConvertToInt32(ddlTemplates.SelectedValue);
                string UserName = txtUserName.Value.Trim();
                DateTime? MaturityTime = UICommon.Util.ConvertToDateTime(txtMaturityTime.Value.Trim());
                SqlParameter[] pramsAdd =
                    {
                        DAL.DALUtil.MakeInParam("@UserName",System.Data.SqlDbType.NVarChar,100,UserName),  
                        DAL.DALUtil.MakeInParam("@MaturityTime",System.Data.SqlDbType.DateTime,8,MaturityTime),
                        DAL.DALUtil.MakeInParam("@CreateTS",System.Data.SqlDbType.DateTime,8,DateTime.Now), 
                        DAL.DALUtil.MakeInParam("@Templates_ValueNum",System.Data.SqlDbType.Int,4,Templates_ValueNum),
                    };
                int row_Add = DAL.UserInfoDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    txtUserName.Value = "";
                    txtMaturityTime.Value = "";
                    UICommon.ScriptHelper.CloseAndAlert(UserName + ",新增成功！");
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