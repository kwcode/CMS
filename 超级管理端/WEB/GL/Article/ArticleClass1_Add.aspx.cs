using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WEB.GL.Article
{
    public partial class ArticleClass1_Add : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int maxOrder = DAL.ArticleClass1DAL.GetSingle("MAX(OrderNum)", " AND UserID=" + userInfo.ID);
                txtOrderNum.Value = UICommon.Util.ConvertToString(maxOrder + 1);
                int maxValue = DAL.ArticleClass1DAL.GetSingle("MAX(ValueNum)", " AND UserID=" + userInfo.ID);
                txtValueNum.Value = UICommon.Util.ConvertToString(maxValue + 1);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ValueNum = UICommon.Util.ConvertToInt32(txtValueNum.Value);
                //是否存在
                int isExist = DAL.ArticleClass1DAL.GetSingle("Count(0)", " AND UserID=" + userInfo.ID + " AND ValueNum=" + ValueNum);
                if (isExist == 1)
                {
                    UICommon.ScriptHelper.Alert("值存在");
                    return;
                }
                string title = txtTitle.Value;
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value);

                SqlParameter[] pramsAdd =
                    {
                        DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title),
                        DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                        DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),
                        DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                        DAL.DALUtil.MakeInParam("@CreateTS",System.Data.SqlDbType.DateTime,8,DateTime.Now), 
                    };
                int row_Add = DAL.ArticleClass1DAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    //ltMsg.Visible = true;
                    //ltMsg.Text = title + ",保存成功！";
                    txtTitle.Value = string.Empty;
                    txtOrderNum.Value = UICommon.Util.ConvertToString(OrderNum + 1);
                    txtValueNum.Value = UICommon.Util.ConvertToString(ValueNum + 1);
                    UICommon.ScriptHelper.Alert(title + ",保存成功！");
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