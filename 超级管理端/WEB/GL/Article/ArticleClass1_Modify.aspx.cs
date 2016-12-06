using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UICommon;

namespace WEB.GL.Article
{
    public partial class ArticleClass1_Modify : BasePage_PM
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
                ArticleClass1Entity entity = ArticleClass1DAL.Get_99(ID, "*");
                txtTitle.Value = entity.Title;
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
                txtValueNum.Value = UICommon.Util.ConvertToString(entity.ValueNum);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ValueNum = Util.ConvertToInt32(txtValueNum.Value.Trim());
                int isExist = DAL.ArticleClass1DAL.GetSingle("Count(0)", " UserID=" + userInfo.ID + " AND ValueNum=" + ValueNum + " AND ID <>" + ID);
                if (isExist == 1)
                {
                    UICommon.ScriptHelper.Alert("值存在");
                    return;
                }
                string title = txtTitle.Value.Trim();
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value.Trim());

                SqlParameter[] pramsModify =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title), 
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
                    DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum), 
                };
                int row_Add = DAL.ArticleClass1DAL.Modify(pramsModify, ID);
                if (row_Add > 0)
                {
                    //ltMsg.Visible = true;
                    //ltMsg.Text = title + ",修改成功！";
                    UICommon.ScriptHelper.Alert(title + ",修改成功！");
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