using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UICommon;

namespace WEB.Other
{
    public partial class OnlineMessage_Detail : BasePage_PM
    {
        public int ID
        {
            get
            {
                return UICommon.Util.ConvertToInt32(Request["ID"]);
            }
        }
        public OnlineMessageEntity entity { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            entity = DAL.OnlineMessageDAL.Get_99(ID, "*"); 
        }
    }
}