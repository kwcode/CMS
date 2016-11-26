using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Product
{
    public partial class ProductClass2_Add : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int maxOrder = DAL.ProductClass2DAL.GetSingle("MAX(OrderNum)");
                txtOrderNum.Value = UICommon.Util.ConvertToString(maxOrder + 1);

                //一类
                SqlParameter[] pramsWhere =
                    { 
                        DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                    };
                List<ProductClass1Entity> ProductClass1List = DAL.ProductClass1DAL.GetList<ProductClass1Entity>("*", pramsWhere, "OrderNum");
                ddlProductClass1.DataSource = ProductClass1List;
                ddlProductClass1.DataTextField = "Title";
                ddlProductClass1.DataValueField = "ID";
                ddlProductClass1.DataBind();
                ddlProductClass1.Items.Insert(0, new ListItem("请选择", ""));

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ProductClass1_ID = UICommon.Util.ConvertToInt32(ddlProductClass1.Value);
                string title = txtTitle.Value;
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value);
                SqlParameter[] pramsAdd =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title),
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),
                    DAL.DALUtil.MakeInParam("@ProductClass1_ID",System.Data.SqlDbType.Int,4,ProductClass1_ID),
                    DAL.DALUtil.MakeInParam("@AddDate",System.Data.SqlDbType.DateTime,8,DateTime.Now), 
                };
                int row_Add = DAL.ProductClass2DAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    ltMsg.Visible = true;
                    ltMsg.Text = title + ",保存成功！";
                    txtTitle.Value = string.Empty;
                    txtOrderNum.Value = UICommon.Util.ConvertToString(OrderNum + 1);
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