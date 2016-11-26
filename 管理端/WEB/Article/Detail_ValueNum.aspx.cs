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

namespace WEB.Article
{
    public partial class Detail_ValueNum : UICommon.BasePage_PM
    {
        public int ValueNum
        {
            get
            {
                return UICommon.Util.ConvertToInt32(Request["ValueNum"]);
            }
        }
        public ArticleEntity articleEntity { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlParameter[] pramsWhere =
				{
					DALUtil.MakeInParam("@ValueNum", SqlDbType.Int, 4, ValueNum),
                    DALUtil.MakeInParam("@UserID", SqlDbType.Int, 4, userInfo.ID)
				};
            articleEntity = DAL.ArticleDAL.Get1<Model.ArticleEntity>("*", pramsWhere);
        }
    }
}