using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Support
{
    public partial class SupportGetRecord_List : UICommon.BasePage_PM
    {
        public static int UserInfoID = 0;
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
            UserInfoID = userInfo.ID;
            if (!IsPostBack)
            {
                BindData();
            }
        }
        #region 绑定数据

        private void BindData()
        {
            System.Text.StringBuilder sqlWhere = new System.Text.StringBuilder();
            //sqlWhere.Append(" UserID=" + userInfo.ID);
            sqlWhere.Append(" 1>0");
            if (!string.IsNullOrEmpty(KeyWords))
            {
                sqlWhere.Append(" AND (GetContent Like '%" + KeyWords + "%' or GetIP like '%" + KeyWords + "%' or GetBrowserInfo like '%" + KeyWords + "%')");
            }
            TotalCount = DAL.SupportGetRecordDAL.GetRecordCount(sqlWhere.ToString());
            List<Model.SupportGetRecordEntity> productList = DAL.SupportGetRecordDAL.GetPageList<Model.SupportGetRecordEntity>(PageIndex, PageSize, "*", sqlWhere.ToString(), "KeyID");
            gv_List.DataSource = productList;
            gv_List.DataBind();
        }

        #endregion
    }
}