using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UICommon;

namespace WEB.GL.Article
{
    public partial class Article_Modify : UICommon.BasePage_PM
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

                #region 一类
                SqlParameter[] pramsWhere =
                { 
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                };
                List<ArticleClass1Entity> ProductClass1List = DAL.ArticleClass1DAL.GetList<ArticleClass1Entity>("*", pramsWhere, "OrderNum");
                ddlArticleClass1.DataSource = ProductClass1List;
                ddlArticleClass1.DataTextField = "Title";
                ddlArticleClass1.DataValueField = "ValueNum";
                ddlArticleClass1.DataBind();
                ddlArticleClass1.Items.Insert(0, new ListItem("请选择", "0"));

                #endregion

                #region 获取信息赋值
                Model.ArticleEntity entity = DAL.ArticleDAL.Get_99(ID, "*");
                txtTitle.Value = UICommon.Util.ConvertToString(entity.Title);
                txtSummay.Value = UICommon.Util.ConvertToString(entity.Summay);
                hide_Content.Value = entity.TxtContent;//这个是ueditor.all.js 里面默认的值 
                hide_ImgPath.Value = entity.TitlePictures;
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
                txtValueNum.Value = UICommon.Util.ConvertToString(entity.ValueNum);
                txtDescription.Value = entity.Description;
                #endregion

                ddlArticleClass1.SelectedValue = UICommon.Util.ConvertToString(entity.ArticleClass1_ValueNum);

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string TxtContent = hide_Content.Value = Server.HtmlDecode(UICommon.Util.ConvertToString(Request["content"]));//这个是ueditor.all.js 里面默认的值 
                string TitlePictures = hide_ImgPath.Value = UICommon.Util.ConvertToString(Request["hide_ImgPath"]);
                int ValueNum = Util.ConvertToInt32(txtValueNum.Value.Trim());
                int isExist = DAL.ArticleDAL.GetSingle("Count(0)", " UserID=" + userInfo.ID + " AND ValueNum=" + ValueNum + " AND ID <>" + ID);
                if (isExist == 1)
                {
                    UICommon.ScriptHelper.Alert("值存在");
                    return;
                }

                string Title = UICommon.Util.ConvertToString(txtTitle.Value).Trim();
                int ArticleClass1_ValueNum = Util.ConvertToInt32(ddlArticleClass1.SelectedValue);
                string Summay = UICommon.Util.ConvertToString(txtSummay.Value).Trim();
              
                string Description = txtDescription.Value.Trim();

                int OrderNum = Util.ConvertToInt32(txtOrderNum.Value.Trim());
                SqlParameter[] pramsModify =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title), 
                    DAL.DALUtil.MakeInParam("@MaturityDate",System.Data.SqlDbType.DateTime,8,DateTime.Now),  
                    DAL.DALUtil.MakeInParam("@ArticleClass1_ValueNum",System.Data.SqlDbType.Int,4,ArticleClass1_ValueNum),  

                    DAL.DALUtil.MakeInParam("@Summay",System.Data.SqlDbType.NText,Summay.Length,Summay), 
                    DAL.DALUtil.MakeInParam("@TxtContent",System.Data.SqlDbType.NText,TxtContent.Length,TxtContent),
                    DAL.DALUtil.MakeInParam("@TitlePictures",System.Data.SqlDbType.NVarChar,250,TitlePictures),

                    DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                    DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,250,Description),
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
                };
                int row_Mod = DAL.ArticleDAL.Modify(pramsModify, ID);
                if (row_Mod > 0)
                {
                    //ltMsg.Visible = true;
                    //ltMsg.Text = Title + ",修改成功！";
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