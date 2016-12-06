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
    public partial class Banner_Add : BasePage_PM
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
                int OrderNum = Util.ConvertToInt32(DAL.BannerDAL.GetSingle(" Max(OrderNum) ", " UserID=" + userInfo.ID));
                txtOrderNum.Value = Util.ConvertToString(OrderNum + 1);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Value.Trim();
            string url = txtUrl.Value.Trim();
            int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value.Trim());
            string Description = txtDescription.Value.Trim();
            string ImagePath = UICommon.Util.ConvertToString(Request["hide_ImgPath"]);
            SqlParameter[] pramsAdd =
            {
                DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title), 
                DAL.DALUtil.MakeInParam("@ImagePath",System.Data.SqlDbType.NVarChar,200,ImagePath), 
                DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
                DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID), 
                DAL.DALUtil.MakeInParam("@Url",System.Data.SqlDbType.NVarChar,250,url),
                DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,250,Description),
            };
            int row_Add = DAL.BannerDAL.Add(pramsAdd);
            if (row_Add > 0)
            {
                txtTitle.Value = "";
                txtOrderNum.Value = UICommon.Util.ConvertToString(OrderNum + 1);
                UICommon.ScriptHelper.Alert(title + ",新增成功");
            }
            else
            {
                UICommon.ScriptHelper.Alert("保存失败");
            }
        }
    }
}