using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UICommon;

namespace WEB.Support
{
    public partial class Support_Modify : BasePage_PM
    {
        public int KeyID
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
                if (KeyID > 0)
                {
                    Model.SupportEntity entity = DAL.SupportDAL.Get_99(KeyID, "*");
                    txtTitle.Value = entity.Title;
                    txtKeysword.Value = entity.Keysword;
                    txtContent.Value = entity.Content;
                    txtLookCount.Value = entity.LookCount.ToString();
                    selStatus.Value = entity.Status.ToString();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.SupportEntity Support = new Model.SupportEntity();
            Support.KeyID = KeyID;
            Support.Title = txtTitle.Value.Trim();
            Support.Keysword = txtKeysword.Value.Trim();
            Support.Content = txtContent.Value.Trim();
            Support.LookCount = UICommon.Util.ConvertToInt32(txtLookCount.Value.Trim());
            Support.Status = UICommon.Util.ConvertToInt32(selStatus.Value.Trim());
            int row_Add = 0;
            if (KeyID > 0)
                row_Add = DAL.SupportDAL.Modify(Support);
            else
            {
                row_Add = DAL.SupportDAL.Add(Support);
                Support.KeyID = row_Add;
            }
            if (row_Add > 0)
            {
                ltMsg.Visible = true;
                ltMsg.Text = (KeyID > 0 ? "修改" : "新增") + "成功！";
                DAL.SupportOperateRecordDAL.Add(userInfo.ID, KeyID > 0 ? 1 : 0, Support);
            }
            else
            {
                UICommon.ScriptHelper.Alert("保存失败");
            }
        }
    }
}