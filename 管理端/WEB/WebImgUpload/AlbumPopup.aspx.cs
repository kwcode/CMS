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
    public partial class AlbumPopup : UICommon.BasePage_PM
    {
        public int PageSize = 21;
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
        /// <summary>
        /// 单选=1
        /// </summary>
        public int Single
        {
            get
            {
                int single = UICommon.Util.ConvertToInt32(Request["single"]);
                return single;
            }

        }
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
                BindData();
            }
        }
        #region 绑定数据

        private void BindData()
        {
            System.Text.StringBuilder sqlWhere = new System.Text.StringBuilder();
            sqlWhere.Append(" UserID=" + userInfo.ID+" AND IsDel=0");
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