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

namespace WEB.Product
{
    public partial class ProductClass2_Modify : BasePage_PM
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

                ProductClass2Entity entity = ProductClass2DAL.Get_99(ID, "*");
                txtTitle.Value = entity.Title;
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
                ddlProductClass1.Value = UICommon.Util.ConvertToString(entity.ProductClass1_ID);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ProductClass1_ID = UICommon.Util.ConvertToInt32(ddlProductClass1.Value);
            string title = txtTitle.Value.Trim();
            int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value.Trim());
            SqlParameter[] pramsModify =
            {
                DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title), 
                DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
                DAL.DALUtil.MakeInParam("@ProductClass1_ID",System.Data.SqlDbType.Int,4,ProductClass1_ID),
            };
            int row_Mod = DAL.ProductClass2DAL.Modify(pramsModify, ID);
            if (row_Mod > 0)
            {
                ltMsg.Visible = true;
                ltMsg.Text = title + ",修改成功！";
            }
            else
            {
                UICommon.ScriptHelper.Alert("保存失败");
            }
        }
    }
}