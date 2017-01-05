using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.WebImgUpload
{
    public partial class WaterMarkEditPopup : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                img.Src = "WA.aspx";
                WatermarkSizePercentBox.Visible = false;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //保存
            List<SqlParameter> pramsList = new List<SqlParameter>();
            int rCount = DAL.UserWatermarkDAL.GetRecordCount("UserID=" + userInfo.ID);
            pramsList.Add(DALUtil.MakeInParam("@WmkType", SqlDbType.Int, 4, 0));
            pramsList.Add(DALUtil.MakeInParam("@WmkPosition", SqlDbType.Int, 4, DALUtil.ConvertToInt32(ddlWmkPosition.SelectedValue)));
            pramsList.Add(DALUtil.MakeInParam("@FontStyle", SqlDbType.Int, 4, DALUtil.ConvertToInt32(ddlFontStyle.SelectedValue)));
            pramsList.Add(DALUtil.MakeInParam("@WmkSize", SqlDbType.Int, 4, DALUtil.ConvertToInt32(txtSize.Value)));
            pramsList.Add(DALUtil.MakeInParam("@FamilyName", SqlDbType.NVarChar, 50, ddlFamilyName.SelectedValue));
            pramsList.Add(DALUtil.MakeInParam("@WmkText ", SqlDbType.NVarChar, 100, txtWmkText.Value.Trim()));

            pramsList.Add(DALUtil.MakeInParam("@WmkColor ", SqlDbType.NVarChar, 50, txtColor.Value.Trim()));
            pramsList.Add(DALUtil.MakeInParam("@WmkSizePercent ", SqlDbType.Int, 4, DALUtil.ConvertToInt32(ddlWatermarkSizePercent.SelectedValue)));
            pramsList.Add(DALUtil.MakeInParam("@WmkAlpha ", SqlDbType.Int, 4, DALUtil.ConvertToInt32(ddlAlpha.SelectedValue)));

            int result = 0;
            if (rCount > 0)
            {
                //存在 
                result = DAL.UserWatermarkDAL.Modify(pramsList.ToArray(), userInfo.ID);
            }
            else
            {
                pramsList.Add(DALUtil.MakeInParam("@UserID", SqlDbType.Int, 4, userInfo.ID));
                result = DAL.UserWatermarkDAL.Add(pramsList.ToArray());
            }
            if (result > 0)
            {
                UICommon.ScriptHelper.Alert("保存成功！");
            }
            else
            {
                UICommon.ScriptHelper.Alert("保存失败！");
            }
        }
        #region 效果预览

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder parm = new System.Text.StringBuilder();
            int wmkType = 0;// UICommon.Util.ConvertToInt32(rbType.SelectedValue);
            parm.Append("?wmkType=" + wmkType);
            if (wmkType == 0)//文字
            {
                parm.Append("&watermarkText=" + txtWmkText.Value);
                parm.Append("&FamilyName=" + ddlFamilyName.SelectedValue);
                parm.Append("&FontStyle=" + ddlFontStyle.SelectedValue);
                parm.Append("&size=" + txtSize.Value);
                string color = txtColor.Value;
                parm.Append("&color=" + Server.UrlEncode(color));
                parm.Append("&alpha=" + ddlAlpha.SelectedValue);
            }

            parm.Append("&wmkPosition=" + ddlWmkPosition.SelectedValue);
            parm.Append("&watermarkSizePercent=" + ddlWatermarkSizePercent.SelectedValue);
            img.Src = "WA.aspx" + parm.ToString();
        }
        #endregion

        #region 水印类型

        //protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int wmkType = UICommon.Util.ConvertToInt32(rbType.SelectedValue);
        //    if (wmkType == 0)//文本
        //    {
        //        txtBox.Visible = true;
        //        WatermarkSizePercentBox.Visible = false;
        //    }
        //    else
        //    {
        //        txtBox.Visible = false;
        //        WatermarkSizePercentBox.Visible = true;
        //    }
        //}
        #endregion
    }
}