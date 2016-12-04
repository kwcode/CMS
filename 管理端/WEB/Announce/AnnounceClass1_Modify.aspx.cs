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

namespace WEB.Announce
{
    public partial class AnnounceClass1_Modify : BasePage_PM
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
                Model.AnnounceClass1Entity entity = AnnounceClass1DAL.Get_99(ID, "*");
                txtTitle.Value = entity.Title;
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Value.Trim();
            int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value.Trim());
            SqlParameter[] pramsModify =
            {
                DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title), 
                DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
            };
            int row_Add = DAL.AnnounceClass1DAL.Modify(pramsModify, ID);
            if (row_Add > 0)
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