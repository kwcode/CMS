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
    public partial class OS_Modify : UICommon.BasePage_PM
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
                UICommon.Util.No_Back();

                ddlOSEnum.DataSource = UICommon.Em.EnumUtil.GetOSList();
                ddlOSEnum.DataTextField = "Title";
                ddlOSEnum.DataValueField = "ValueNum";
                ddlOSEnum.DataBind();
                ddlOSEnum.Items.Insert(0, new ListItem("请选择", ""));


                #region 获取信息赋值
                Model.OnlineServicesEntity entity = DAL.OnlineServicesDAL.Get_99(ID, "*");
                txtTitle.Value = UICommon.Util.ConvertToString(entity.Title);
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
                txtValueNum.Value = entity.ValueNum;
                txtRemark.Value = entity.Remark;
                ddlOSEnum.SelectedValue = UICommon.Util.ConvertToString(entity.OSType);
                #endregion


            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int OrderNum = Util.ConvertToInt32(txtOrderNum.Value);
                string Title = UICommon.Util.ConvertToString(txtTitle.Value).Trim();
                string ValueNum = UICommon.Util.ConvertToString(txtValueNum.Value).Trim();
                string Remark = UICommon.Util.ConvertToString(txtRemark.Value);
                int OSType = Util.ConvertToInt32(ddlOSEnum.SelectedValue);
                SqlParameter[] pramsModify =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title), 
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
                    DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.NVarChar,300,ValueNum),  
                    DAL.DALUtil.MakeInParam("@OSType",System.Data.SqlDbType.Int,4,OSType),   
                    DAL.DALUtil.MakeInParam("@Remark",System.Data.SqlDbType.NText,Remark.Length,Remark), 
                };
                int row_Mod = DAL.OnlineServicesDAL.Modify(pramsModify, ID);
                if (row_Mod > 0)
                { 
                    UICommon.ScriptHelper.Alert(Title + ",修改成功 ");

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