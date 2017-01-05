using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Nav
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
                Model.NavigationBarEntity entity = DAL.NavigationBarDAL.Get_99(ID, "*");
                txtTitle.Value = entity.Title;
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
                txtDescription.Value = entity.Description;

                txtSEOTitle.Value = entity.SEOTitle;
                txtSEOKeyWords.Value = entity.SEOKeyWords;
                txtSEODescription.Value = entity.SEODescription;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtTitle.Value.Trim();
                string Description = txtDescription.Value.Trim();
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value.Trim());

                string SEOTitle = txtSEOTitle.Value.Trim();
                string SEOKeyWords = txtSEOKeyWords.Value.Trim();
                string SEODescription = txtSEODescription.Value.Trim();
                SqlParameter[] pramsModify =
                    {
                        DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title), 
                        DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),  
                        DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,200,Description),

                        DAL.DALUtil.MakeInParam("@SEOTitle",System.Data.SqlDbType.NText,SEOTitle.Length,SEOTitle),
                        DAL.DALUtil.MakeInParam("@SEOKeyWords",System.Data.SqlDbType.NText,SEOKeyWords.Length,SEOKeyWords),
                        DAL.DALUtil.MakeInParam("@SEODescription",System.Data.SqlDbType.NText,SEODescription.Length,SEODescription),
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