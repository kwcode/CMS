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

namespace WEB.SEO
{
    public partial class WebFlow_Charts : UICommon.BasePage_PM
    {
        public string beginTime
        {
            get
            {
                string begin = Request["begin"] ?? "";
                if (string.IsNullOrWhiteSpace(Request["begin"]))
                {
                    begin = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                }
                return begin;
            }
        }
        public string endTime
        {

            get
            {
                string end = Request["end"] ?? "";
                if (string.IsNullOrWhiteSpace(Request["end"]))
                {
                    end = DateTime.Now.ToString("yyyy-MM-dd");
                }
                return end;
            }
        }
        public string DateList { get; set; }
        public string IPCountList { get; set; }
        public string PVCountList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sqlText = "SELECT * FROM dbo.WebFlowCharts WHERE UserID=" + userInfo.ID + " AND DTime>='" + beginTime + "' AND DTime<='" + endTime + "' ORDER BY DTime ";
                List<WebFlowChartsEntity> entityList = DAL.WebFlowChartsDAL.GetList<Model.WebFlowChartsEntity>(sqlText);
                foreach (WebFlowChartsEntity item in entityList)
                {
                    DateList += "['" + item.DTime + "'],";
                    IPCountList += item.IPCount + ",";
                    PVCountList += item.PVCount + ",";

                }
            }
        }
    }
}