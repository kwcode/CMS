using Model;
using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            sqlWhere.Append(" AND UserID=" + userInfo.ID);
            if (!string.IsNullOrEmpty(KeyWords))
            {
                sqlWhere.Append(" AND Title Like '%" + KeyWords + "%'");
            }
            TotalCount = DAL.ArticleClass1DAL.GetRecordCount(sqlWhere.ToString());
            List<Model.ArticleEntity> productList = DAL.ArticleDAL.GetPageList<Model.ArticleEntity>(PageIndex, PageSize, "*", sqlWhere.ToString());
            gv_List.DataSource = productList;
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
                    
                  ArticleClass1Entity entity=  DAL.ArticleClass1DAL.Get1<ArticleClass1Entity>("Title",pramsWhere);
                  ltArticleClass1_ValueNum.Text = entity.Title;
                }
            }
            catch { }
        }

        #endregion
    }
}