using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.GL.Domain
{
    public partial class Domain_Modify : UICommon.BasePage_PM
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
                Model.DomainEntity entity = DAL.DomainDAL.Get_99(ID, "*");
                txtDomainName.Value = entity.DomainName;
                txtEndDate.Value = UICommon.Util.ConvertToString(entity.EndDate);
                lbUserID.Text = UICommon.Util.ConvertToString(entity.UserID);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string DomainName = txtDomainName.Value.Trim();
                DateTime? endDate = UICommon.Util.ConvertToDateTime(txtEndDate.Value.Trim());
                SqlParameter[] pramsModify =
                    {
                        DAL.DALUtil.MakeInParam("@DomainName",System.Data.SqlDbType.NVarChar,100,DomainName),  
                        DAL.DALUtil.MakeInParam("@EndDate",System.Data.SqlDbType.DateTime,8,endDate),   
                    };
                int row_Mod = DAL.DomainDAL.Modify(pramsModify, ID);
                if (row_Mod > 0)
                {
                    ltMsg.Visible = true;
                    ltMsg.Text = DomainName + ",修改成功！";
                    txtDomainName.Value = "";
                    UICommon.ScriptHelper.Alert(DomainName + ",修改成功！");
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