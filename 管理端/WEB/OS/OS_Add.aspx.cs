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

namespace WEB.OS
{
    public partial class OS_Add : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UICommon.Util.No_Back();
                ddlOSEnum.DataSource = UICommon.Em.EnumUtil.GetOSList();
                ddlOSEnum.DataTextField = "Title";
                ddlOSEnum.DataValueField = "ValueNum";
                ddlOSEnum.DataBind();
                ddlOSEnum.Items.Insert(0, new ListItem("请选择", ""));

                try
                {
                    int OrderNum = Util.ConvertToInt32(DAL.OnlineServicesDAL.GetSingle(" Max(OrderNum) ", " AND UserID=" + userInfo.ID));
                    txtOrderNum.Value = Util.ConvertToString(OrderNum + 1);
                }
                catch { }

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //获取最大的排序id

            try
            {
                int OrderNum = Util.ConvertToInt32(txtOrderNum.Value);
                string Title = UICommon.Util.ConvertToString(txtTitle.Value).Trim();
                string ValueNum = UICommon.Util.ConvertToString(txtValueNum.Value).Trim();
                string Remark = UICommon.Util.ConvertToString(txtRemark.Value);
                int OSType = Util.ConvertToInt32(ddlOSEnum.SelectedValue);
                SqlParameter[] pramsAdd =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title),
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
                    DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.NVarChar,300,ValueNum),  
                    DAL.DALUtil.MakeInParam("@OSType",System.Data.SqlDbType.Int,4,OSType),   
                    DAL.DALUtil.MakeInParam("@Remark",System.Data.SqlDbType.NText,Remark.Length,Remark),   
                };
                int row_Add = DAL.OnlineServicesDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    txtTitle.Value = string.Empty;
                    txtValueNum.Value = string.Empty;
                    txtRemark.Value = string.Empty;
                    txtOrderNum.Value = Util.ConvertToString(OrderNum + 1);
                    UICommon.ScriptHelper.Alert(Title + ",保存成功！");
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