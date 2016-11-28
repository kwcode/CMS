using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.GL.BackgroundMenu
{
    public partial class BackgroundMenu_Modify : UICommon.BasePage_PM
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
                #region 一类
                List<BackgroundMenuClass1Entity> entityList = DAL.BackgroundMenuClass1DAL.GetList<BackgroundMenuClass1Entity>("*", null, "OrderNum");
                ddlBackgroundMenuClass1.DataSource = entityList;
                ddlBackgroundMenuClass1.DataTextField = "Title";
                ddlBackgroundMenuClass1.DataValueField = "ValueNum";
                ddlBackgroundMenuClass1.DataBind();
                ddlBackgroundMenuClass1.Items.Insert(0, new ListItem("请选择", ""));
                #endregion

                Model.BackgroundMenuEntity entity = DAL.BackgroundMenuDAL.Get_99(ID, "*");
                txtTitle.Value = entity.Title;
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
                ddlBackgroundMenuClass1.Value = UICommon.Util.ConvertToString(entity.BackgroundMenuClass1_ValueNum);
                txtValueNum.Value = UICommon.Util.ConvertToString(entity.ValueNum);
                txtManageUrl.Value = entity.ManageUrl;
                txtDescription.Value = entity.Description;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtTitle.Value.Trim();
                string ManageUrl = txtManageUrl.Value.Trim();
                string Description = txtDescription.Value.Trim();
                int ValueNum = UICommon.Util.ConvertToInt32(txtValueNum.Value.Trim());
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value.Trim());
                int BackgroundMenuClass1_ValueNum = UICommon.Util.ConvertToInt32(ddlBackgroundMenuClass1.Value);
                SqlParameter[] pramsModify =
                    {
                        DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title), 
                        DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),
                        DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                        DAL.DALUtil.MakeInParam("@ManageUrl",System.Data.SqlDbType.NVarChar,500,ManageUrl),
                        DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,200,Description),  
                        DAL.DALUtil.MakeInParam("@BackgroundMenuClass1_ValueNum",System.Data.SqlDbType.Int,4,BackgroundMenuClass1_ValueNum),
                        
                    };
                int row_Mod = DAL.BackgroundMenuDAL.Modify(pramsModify, ID);
                if (row_Mod > 0)
                {
                    UICommon.ScriptHelper.Alert("修改成功");
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