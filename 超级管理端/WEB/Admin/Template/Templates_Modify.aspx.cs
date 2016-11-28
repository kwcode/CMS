using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Admin.Template
{
    public partial class Templates_Modify : UICommon.BasePage_PM
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
                Model.TemplatesEntity entity = DAL.TemplatesDAL.Get_99(ID, "*");
                txtName.Value = entity.Name;
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
                txtValueNum.Value = UICommon.Util.ConvertToString(entity.ValueNum);
                txtDescription.Value = entity.Description;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ValueNum = UICommon.Util.ConvertToInt32(txtValueNum.Value);
                //是否存在
                int isExist = DAL.TemplatesDAL.GetSingle("Count(0)", " AND ValueNum=" + ValueNum + " AND ID <> " + ID);
                if (isExist == 1)
                {
                    UICommon.ScriptHelper.Alert("值已经存在,请换一个值。");
                    return;
                }
                string Name = txtName.Value.Trim();
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value);
                string Description = txtDescription.Value.Trim();
                SqlParameter[] pramsModify =
                    {
                        DAL.DALUtil.MakeInParam("@Name",System.Data.SqlDbType.NVarChar,100,Name), 
                        DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),
                        DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                        DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,200,Description), 
                    };
                int row_Add = DAL.TemplatesDAL.Modify(pramsModify, ID);
                if (row_Add > 0)
                {
                    //ltmsg.visible = true;
                    //ltmsg.text = title + ",保存成功！"; 
                    UICommon.ScriptHelper.Alert(Name + ",修改成功！");
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