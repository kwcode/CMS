using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Admin.Template
{
    public partial class Templates_Add : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int maxOrder = DAL.TemplatesDAL.GetSingle("MAX(OrderNum)");
                txtOrderNum.Value = UICommon.Util.ConvertToString(maxOrder + 1);
                int maxValue = DAL.TemplatesDAL.GetSingle("MAX(ValueNum)");
                txtValueNum.Value = UICommon.Util.ConvertToString(maxValue + 1);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ValueNum = UICommon.Util.ConvertToInt32(txtValueNum.Value);
                //是否存在
                int isExist = DAL.TemplatesDAL.GetSingle("Count(0)", " AND ValueNum=" + ValueNum);
                if (isExist == 1)
                {
                    UICommon.ScriptHelper.Alert("值已经存在,请换一个值。");
                    return;
                }
                string Name = txtName.Value.Trim();
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value);
                string Description = txtDescription.Value.Trim();
                SqlParameter[] pramsAdd =
                    {
                        DAL.DALUtil.MakeInParam("@Name",System.Data.SqlDbType.NVarChar,100,Name), 
                        DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),
                        DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                        DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,200,Description),
                        DAL.DALUtil.MakeInParam("@CreateTS",System.Data.SqlDbType.DateTime,8,DateTime.Now), 
                    };
                int row_Add = DAL.TemplatesDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    //ltMsg.Visible = true;
                    //ltMsg.Text = title + ",保存成功！";
                    txtName.Value = string.Empty;
                    txtOrderNum.Value = UICommon.Util.ConvertToString(OrderNum + 1);
                    txtValueNum.Value = UICommon.Util.ConvertToString(ValueNum + 1);
                    UICommon.ScriptHelper.Alert(Name + ",保存成功！");
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