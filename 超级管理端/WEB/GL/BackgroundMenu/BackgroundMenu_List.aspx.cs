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

namespace WEB.GL.BackgroundMenu
{
    public partial class BackgroundMenu_List : UICommon.BasePage_PM
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
                List<BackgroundMenuClass1Entity> articleClass1List = DAL.BackgroundMenuClass1DAL.GetList<Model.BackgroundMenuClass1Entity>("Title,ValueNum", pramsWhere, "OrderNum");
                ddlArticleClass1.DataSource = articleClass1List;
                ddlArticleClass1.DataTextField = "Title";
                ddlArticleClass1.DataValueField = "ValueNum";
                ddlArticleClass1.DataBind();
                ddlArticleClass1.Items.Insert(0, new ListItem("请选一级分类", ""));
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
                int row_Del = DAL.BackgroundMenuDAL.Delete_1(item);
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
            sqlWhere.Append("UserID=" + userInfo.ID);
            if (!string.IsNullOrEmpty(KeyWords))
            {
                sqlWhere.Append(" AND Title Like '%" + KeyWords + "%'");
            }
            if (!Util.IsNull(class1))
            {
                sqlWhere.Append(" AND BackgroundMenuClass1_ValueNum =" + DAL.DALUtil.ConverToSqlTxt(class1));
            }
            TotalCount = DAL.BackgroundMenuDAL.GetRecordCount(sqlWhere.ToString());
            List<Model.BackgroundMenuEntity> productList = DAL.BackgroundMenuDAL.GetPageList<Model.BackgroundMenuEntity>(PageIndex, PageSize, "*", sqlWhere.ToString());
            gv_List.DataSource = productList;
            gv_List.DataBind();
        }

        #endregion

        #region 处理数据
        protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Literal ltBackgroundMenuClass1_ValueNum = e.Row.FindControl("ltBackgroundMenuClass1_ValueNum") as Literal;
                if (ltBackgroundMenuClass1_ValueNum != null)
                {
                    int BackgroundMenuClass1_ValueNum = Util.ConvertToInt32(ltBackgroundMenuClass1_ValueNum.Text);
                    ltBackgroundMenuClass1_ValueNum.Text = DAL.BackgroundMenuClass1DAL.Get_98(BackgroundMenuClass1_ValueNum, userInfo.ID, "Title").Title;
                }
            }
            catch { }
        }

        #endregion
    }
}