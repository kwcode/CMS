using DAL;
using Model;
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
    public partial class UserPictureBook_List : UICommon.BasePage_PM
    {
        public int PageSize = 10;
        public int PageIndex
        {
            get
            {
                int page = UICommon.Util.ConvertToInt32(Request["page"]);
                return page > 0 ? page : 1;
            }
        }
        public int BookID
        {
            get
            {
                int bookid = UICommon.Util.ConvertToInt32(Request["bookid"]);
                return bookid;
            }
        }
        public int MaxOrderNum = 1;
        public int TotalCount = 0;

        public List<Model.UserPictureEntity> UserPictureList { get; set; }
        public List<Model.UserPictureBookEntity> BookList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 获取相册集合
                SqlParameter[] pramsWhere =
			    {
				    DALUtil.MakeInParam("@UserID",SqlDbType.Int,4,userInfo.ID)
			     };
                BookList = DAL.UserPictureBookDAL.GetList<UserPictureBookEntity>("*", pramsWhere, "OrderNum");
                #endregion

                MaxOrderNum = DAL.UserPictureBookDAL.GetSingle("Max(OrderNum)", " UserID=" + userInfo.ID);
                MaxOrderNum = MaxOrderNum + 1;
                BindData();
            }
        }
        #region 绑定数据

        private void BindData()
        {
            System.Text.StringBuilder sqlWhere = new System.Text.StringBuilder();
            sqlWhere.Append(" UserID=" + userInfo.ID);
            if (BookID > 0)
            {
                sqlWhere.Append(" AND BookID=" + BookID);
            }
            TotalCount = DAL.UserPictureDAL.GetRecordCount(sqlWhere.ToString());
            UserPictureList = DAL.UserPictureDAL.GetPageList<Model.UserPictureEntity>(PageIndex, PageSize, "*", sqlWhere.ToString(), " ID DESC");

        }

        #endregion
    }
}