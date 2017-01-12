using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using UICommon;

namespace WEB.Support
{
    public partial class Support_List : UICommon.BasePage_PM
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
        [WebMethod]
        public static object DoDelete(string jsonlist)
        {
            //检测是否登录
            List<int> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(jsonlist);
            int index = 0;
            foreach (int item in list)
            {
                Model.SupportEntity Support = DAL.SupportDAL.Get_99(item, "*");
                if (Support != null && Support.KeyID > 0)
                {
                    int row_Del = DAL.SupportDAL.Delete_1(Support.KeyID);
                    if (row_Del > 0)
                    {
                        index++;
                        DAL.SupportOperateRecordDAL.Add(UserInfoID,2,Support);
                    }
                }
            }
            ReturnCode code = ReturnCode.error;
            string msg = "删除失败";
            object data = "";
            if (index > 0)
            {
                code = ReturnCode.success;
                msg = "删除成功,影响行数(" + index + ")";
            }
            object obj = UICommon.ResponseData.GetResponseData(code, msg, data);
            return obj;
        }
        #region 绑定数据

        private void BindData()
        {
            System.Text.StringBuilder sqlWhere = new System.Text.StringBuilder();
            //sqlWhere.Append(" UserID=" + userInfo.ID);
            sqlWhere.Append(" 1>0");
            if (!string.IsNullOrEmpty(KeyWords))
            {
                sqlWhere.Append(" AND (Title Like '%" + KeyWords + "%' or Content Like '%" + KeyWords + "%' or Keysword Like '%" + KeyWords + "%')");
            }
            TotalCount = DAL.SupportDAL.GetRecordCount(sqlWhere.ToString());
            List<Model.SupportEntity> productList = DAL.SupportDAL.GetPageList<Model.SupportEntity>(PageIndex, PageSize, "*", sqlWhere.ToString(), "KeyID");
            gv_List.DataSource = productList;
            gv_List.DataBind();
        }

        #endregion
    }
}