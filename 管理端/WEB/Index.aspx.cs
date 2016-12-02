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
using UICommon;

namespace WEB
{
    public partial class Index : BasePage_PM
    {
        public List<BackgroundMenuClass1Entity> BackgroundMenuClass1List { get; set; }
        public string PreviewAddress { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlParameter[] pramsWhere =
				{
					DALUtil.MakeInParam("@UserID", SqlDbType.Int, 4, userInfo.ID)
				};
            BackgroundMenuClass1List = DAL.BackgroundMenuClass1DAL.GetList<Model.BackgroundMenuClass1Entity>("*", pramsWhere, "OrderNum");

            try
            {
                string domain = DAL.DomainDAL.Get_97(userInfo.ID, " TOP 1 DomainName,EndDate ", "EndDate DESC").DomainName;
                PreviewAddress = "http://" + domain;
            }
            catch { }
        }

        public List<BackgroundMenuEntity> GetBackgroundMenuList(int class1_ValueNum)
        {
            SqlParameter[] pramsWhere =
				{
					DALUtil.MakeInParam("@UserID", SqlDbType.Int, 4, userInfo.ID),
                    DALUtil.MakeInParam("@BackgroundMenuClass1_ValueNum", SqlDbType.Int, 4, class1_ValueNum)
				};
            List<BackgroundMenuEntity> list = DAL.BackgroundMenuDAL.GetList<Model.BackgroundMenuEntity>("*", pramsWhere, "OrderNum");
            return list;
        }
    }
}