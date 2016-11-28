using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.GL.BackgroundMenu
{
    public partial class NavigationBar_Modify : UICommon.BasePage_PM
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
                int maxOrder = DAL.NavigationBarDAL.GetSingle("MAX(OrderNum)");
                txtOrderNum.Value = UICommon.Util.ConvertToString(maxOrder + 1);
                int maxValueNum = DAL.NavigationBarDAL.GetSingle("MAX(ValueNum)");
                txtValueNum.Value = UICommon.Util.ConvertToString(maxValueNum + 1);
                Model.NavigationBarEntity entity = DAL.NavigationBarDAL.Get_99(ID, "*");
                txtTitle.Value = entity.Title;
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
                txtValueNum.Value = UICommon.Util.ConvertToString(entity.ValueNum);
                txtUrl.Value = entity.Url;
                txtDescription.Value = entity.Description;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtTitle.Value.Trim();
                string Url = txtUrl.Value.Trim();
                string Description = txtDescription.Value.Trim();
                int ValueNum = UICommon.Util.ConvertToInt32(txtValueNum.Value.Trim());
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value.Trim());
                SqlParameter[] pramsModify =
                    {
                        DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title), 
                        DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),
                        DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                        DAL.DALUtil.MakeInParam("@Url",System.Data.SqlDbType.NVarChar,500,Url),
                        DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,200,Description) 
                    };
                int row_Add = DAL.NavigationBarDAL.Modify(pramsModify, ID);
                if (row_Add > 0)
                {
                    UICommon.ScriptHelper.Alert(title + "，修改成功！");
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