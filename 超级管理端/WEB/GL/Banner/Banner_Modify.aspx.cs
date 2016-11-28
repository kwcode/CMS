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

namespace WEB.GL.Banner
{
    public partial class Banner_Modify : BasePage_PM
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
                Model.BannerEntity entity = BannerDAL.Get_99(ID, "*");
                txtTitle.Value = entity.Title;
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
                txtUrl.Value = UICommon.Util.ConvertToString(entity.Url);
                txtDescription.Value = entity.Description;
                hide_ImgPath.Value = entity.ImagePath;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Value.Trim();
            string url = txtUrl.Value.Trim();
            int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value.Trim());
            string Description = txtDescription.Value.Trim();
            string ImagePath = UICommon.Util.ConvertToString(Request["hide_ImgPath"]);
            SqlParameter[] pramsModify =
            {
                DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title), 
                DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
                DAL.DALUtil.MakeInParam("@Url",System.Data.SqlDbType.NVarChar,250,url),
                DAL.DALUtil.MakeInParam("@ImagePath",System.Data.SqlDbType.NVarChar,250,ImagePath), 
                DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,250,Description),
            };
            int row_Add = DAL.BannerDAL.Modify(pramsModify, ID);
            if (row_Add > 0)
            {
                ltMsg.Visible = true;
                ltMsg.Text = title + ",修改成功！"; 
                UICommon.ScriptHelper.Alert(title + ",修改成功！");
            }
            else
            {
                UICommon.ScriptHelper.Alert("保存失败");
            }
        }
    }
}