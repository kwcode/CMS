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

namespace WEB.PictureText
{
    public partial class PictureText_Add : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UICommon.Util.No_Back();
                //一类
                SqlParameter[] pramsWhere =
                { 
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                };
                List<Model.PictureTextClass1Entity> ArticleClass1List = DAL.PictureTextClass1DAL.GetList<PictureTextClass1Entity>("Title,ValueNum", pramsWhere, "OrderNum");
                ddlPictureTextClass1.DataSource = ArticleClass1List;
                ddlPictureTextClass1.DataTextField = "Title";
                ddlPictureTextClass1.DataValueField = "ValueNum";
                ddlPictureTextClass1.DataBind();
                ddlPictureTextClass1.Items.Insert(0, new ListItem("请选择", ""));
                int OrderNum = Util.ConvertToInt32(DAL.PictureTextDAL.GetSingle(" Max(OrderNum) ", " UserID=" + userInfo.ID));
                txtOrderNum.Value = Util.ConvertToString(OrderNum + 1);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //获取最大的排序id 
            try
            { 
                int PictureTextClass1_ValueNum = Util.ConvertToInt32(ddlPictureTextClass1.SelectedValue);
                string TxtContent = hide_Content.Value = Server.HtmlDecode(UICommon.Util.ConvertToString(Request["content"]));//这个是ueditor.all.js 里面默认的值 
                string ImagePath = UICommon.Util.ConvertToString(Request["hide_ImgPath"]);
                int ValueNum = Util.ConvertToInt32(txtValueNum.Value.Trim());
                int isExist = DAL.PictureTextDAL.GetSingle("Count(0)", " UserID=" + userInfo.ID + " AND ValueNum=" + ValueNum);
                if (isExist == 1)
                {
                    UICommon.ScriptHelper.Alert("值存在");
                    return;
                }
                string Title = UICommon.Util.ConvertToString(txtTitle.Value).Trim();
                string Explanation = UICommon.Util.ConvertToString(txtExplanation.Value).Trim();
                string Description = txtDescription.Value.Trim();
                int OrderNum = Util.ConvertToInt32(txtOrderNum.Value.Trim());
                string Url = txtUrl.Value.Trim();
                int Width = Util.ConvertToInt32(txtWidth.Value);
                int Height = Util.ConvertToInt32(txtHeight.Value);
                SqlParameter[] pramsAdd =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title),
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),      
                    DAL.DALUtil.MakeInParam("@Explanation",System.Data.SqlDbType.NVarChar,250,Explanation), 
                    DAL.DALUtil.MakeInParam("@TxtContent",System.Data.SqlDbType.NText,TxtContent.Length,TxtContent),
                    DAL.DALUtil.MakeInParam("@ImagePath",System.Data.SqlDbType.NVarChar,250,ImagePath),
                    DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                    DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,250,Description),
                    DAL.DALUtil.MakeInParam("@Url",System.Data.SqlDbType.NVarChar,500,Url),
                    
                    DAL.DALUtil.MakeInParam("@Type",System.Data.SqlDbType.Int,4,1), 
                    DAL.DALUtil.MakeInParam("@PictureTextClass1_ValueNum",System.Data.SqlDbType.Int,4,PictureTextClass1_ValueNum), 
                    DAL.DALUtil.MakeInParam("@Width",System.Data.SqlDbType.Int,4,Width), 
                    DAL.DALUtil.MakeInParam("@Height",System.Data.SqlDbType.Int,4,Height), 
                    
                };
                int row_Add = DAL.PictureTextDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    txtTitle.Value = string.Empty;
                    hide_Content.Value = "";
                    txtExplanation.Value = "";
                    hide_ImgPath.Value = "";
                    txtDescription.Value = "";
                    txtOrderNum.Value = UICommon.Util.ConvertToString(OrderNum + 1);
                    txtValueNum.Value = UICommon.Util.ConvertToString(ValueNum + 1);

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

        #region 选择插图分组

        protected void ddlPictureTextClass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _valueNum = Util.ConvertToInt32(ddlPictureTextClass1.SelectedValue);
            if (_valueNum > 0)
            {

                int ValueNum = Util.ConvertToInt32(DAL.PictureTextDAL.GetSingle(" Max(ValueNum) ", " UserID=" + userInfo.ID + " AND PictureTextClass1_ValueNum=" + _valueNum));
                if (ValueNum == 0)
                {
                    txtValueNum.Value = Util.ConvertToString((_valueNum * 100) + 1);
                }
                else
                {
                    txtValueNum.Value = Util.ConvertToString(ValueNum + 1);
                }
            }
        }
        #endregion
    }
}