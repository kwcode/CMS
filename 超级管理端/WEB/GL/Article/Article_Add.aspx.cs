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

namespace WEB.GL.Article
{
    public partial class Article_Add : UICommon.BasePage_PM
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
                List<Model.ArticleClass1Entity> ArticleClass1List = DAL.ArticleClass1DAL.GetList<ArticleClass1Entity>("*", pramsWhere, "OrderNum");
                ddlArticleClass1.DataSource = ArticleClass1List;
                ddlArticleClass1.DataTextField = "Title";
                ddlArticleClass1.DataValueField = "ValueNum";
                ddlArticleClass1.DataBind();
                ddlArticleClass1.Items.Insert(0, new ListItem("请选择", ""));

                int OrderNum = Util.ConvertToInt32(DAL.ArticleDAL.GetSingle(" Max(OrderNum) ", "AND UserID=" + userInfo.ID));
                txtOrderNum.Value = Util.ConvertToString(OrderNum + 1);
                int ValueNum = Util.ConvertToInt32(DAL.ArticleDAL.GetSingle(" Max(ValueNum) ", "AND UserID=" + userInfo.ID));
                txtValueNum.Value = Util.ConvertToString(ValueNum + 1);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //获取最大的排序id 
            try
            {
                string TxtContent = hide_Content.Value = Server.HtmlDecode(UICommon.Util.ConvertToString(Request["content"]));//这个是ueditor.all.js 里面默认的值 
                string TitlePictures = UICommon.Util.ConvertToString(Request["hide_ImgPath"]);
                int ValueNum = Util.ConvertToInt32(txtValueNum.Value.Trim());
                int isExist = DAL.ArticleDAL.GetSingle("Count(0)", " AND UserID=" + userInfo.ID + " AND ValueNum=" + ValueNum);
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
                SqlParameter[] pramsAdd =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title),
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
                    DAL.DALUtil.MakeInParam("@MaturityDate",System.Data.SqlDbType.DateTime,8,DateTime.Now),  
                    DAL.DALUtil.MakeInParam("@ArticleClass1_ValueNum",System.Data.SqlDbType.Int,4,ArticleClass1_ValueNum),  
                    DAL.DALUtil.MakeInParam("@Summay",System.Data.SqlDbType.NText,Summay.Length,Summay), 
                    DAL.DALUtil.MakeInParam("@TxtContent",System.Data.SqlDbType.NText,TxtContent.Length,TxtContent),
                    DAL.DALUtil.MakeInParam("@TitlePictures",System.Data.SqlDbType.NVarChar,250,TitlePictures),
                    DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                    DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,250,Description),
                };
                int row_Add = DAL.ArticleDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    txtTitle.Value = string.Empty;
                    hide_Content.Value = "";
                    txtSummay.Value = "";
                    hide_ImgPath.Value = "";
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


    }
}