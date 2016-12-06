using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using UICommon;
namespace WEB.GL.Article
{
    public partial class Article_List : UICommon.BasePage_PM
    {
        public int PageSize = 15;
        public int PageIndex
        {
            get
            {
                int page = UICommon.Util.ConvertToInt32(Request["page"]);
                return page > 0 ? page : 1;
            }
        }
        public int TotalCount = 0;
        public string KeyWords
        {
            get
            {
                return UICommon.Util.ConvertToString(Request["keywords"]).Trim();
            }
        }

        public string class1
        {
            get
            {
                return UICommon.Util.ConvertToString(Request["class1"]).Trim();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region MyRegion
                SqlParameter[] pramsWhere =
				{
					DALUtil.MakeInParam("@UserID", SqlDbType.Int, 4, userInfo.ID)
				};
                List<ArticleClass1Entity> articleClass1List = DAL.ArticleClass1DAL.GetList<Model.ArticleClass1Entity>("Title,ValueNum", pramsWhere, "OrderNum");
                ddlArticleClass1.DataSource = articleClass1List;
                ddlArticleClass1.DataTextField = "Title";
                ddlArticleClass1.DataValueField = "ValueNum";
                ddlArticleClass1.DataBind();
                ddlArticleClass1.Items.Insert(0, new ListItem("请选择分类", ""));
                ddlArticleClass1.Value = class1;
                #endregion
                BindData();
            }
        }
        [WebMethod]
        public static object DoDelete(string jsonlist)
        {
            //检测是否登录
            List<int> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(jsonlist);
            int index = 0;
            foreach (int item in list)
            {
                int row_Del = DAL.ArticleDAL.Delete_1(item);
                if (row_Del > 0)
                {
                    index++;
                }
            }
            ReturnCode code = ReturnCode.error;
            string msg = "删除失败";
            object data = "";
            if (index > 0)
            {
                code = ReturnCode.success;
                msg = "删除成功,影响行数(" + index;
            }
            object obj = UICommon.ResponseData.GetResponseData(code, msg, data);
            return obj;
        }

        #region 绑定数据

        private void BindData()
        {
            System.Text.StringBuilder sqlWhere = new System.Text.StringBuilder();
            sqlWhere.Append(" UserID=" + userInfo.ID);
            if (!string.IsNullOrEmpty(KeyWords))
            {
                sqlWhere.Append(" AND Title Like '%" + Server.UrlDecode(KeyWords) + "%'");
            }
            if (!Util.IsNull(class1))
            {
                sqlWhere.Append(" AND ArticleClass1_ValueNum =" + DAL.DALUtil.ConverToSqlTxt(class1));
            }
            TotalCount = DAL.ArticleDAL.GetRecordCount(sqlWhere.ToString());
            List<Model.ArticleEntity> entityList = DAL.ArticleDAL.GetPageList<Model.ArticleEntity>(PageIndex, PageSize, "*", sqlWhere.ToString());
            gv_List.DataSource = entityList;
            gv_List.DataBind();
        }

        #endregion

        #region 处理数据
        protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Literal ltArticleClass1_ValueNum = e.Row.FindControl("ltArticleClass1_ValueNum") as Literal;
                if (ltArticleClass1_ValueNum != null)
                {
                    int ArticleClass1_ValueNum = Util.ConvertToInt32(ltArticleClass1_ValueNum.Text);
                    SqlParameter[] pramsWhere =
                    {
                        DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                        DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ArticleClass1_ValueNum),
                    };

                    ArticleClass1Entity entity = DAL.ArticleClass1DAL.Get1<ArticleClass1Entity>("Title", pramsWhere);
                    ltArticleClass1_ValueNum.Text = entity.Title;
                }
            }
            catch { }
        }

        #endregion
    }
}