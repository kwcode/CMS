using DAL;
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

namespace WEB.OS
{
    public partial class OS_List : UICommon.BasePage_PM
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
        public int IsOnlineServices { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Model.UserInfoExtraEntity entity = DAL.UserInfoExtraDAL.Get_99(userInfo.ID, "IsOnlineServices,OSJsCode");
                IsOnlineServices = UICommon.Util.ConvertToInt32(entity.IsOnlineServices);
                txtOSJsCode.Value = entity.OSJsCode;
                if (IsOnlineServices == 0)
                {
                    btnSave.Text = "开启在线客服";
                    txtOSJsCode.Visible = true;
                }
                else
                {
                    btnSaveOnline.Visible = false;
                    txtOSJsCode.Visible = false;
                }
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
                int row_Del = DAL.OnlineServicesDAL.Delete_1(item);
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
            sqlWhere.Append(" UserID=" + userInfo.ID);
            if (!string.IsNullOrEmpty(KeyWords))
            {
                sqlWhere.Append(" AND Title Like '%" + KeyWords + "%'");
            }
            TotalCount = DAL.OnlineServicesDAL.GetRecordCount(sqlWhere.ToString());
            List<Model.OnlineServicesEntity> entityList = DAL.OnlineServicesDAL.GetPageList<Model.OnlineServicesEntity>(PageIndex, PageSize, "*", sqlWhere.ToString());
            gv_List.DataSource = entityList;
            gv_List.DataBind();
        }

        #endregion

        #region 处理数据
        protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Literal ltOSType = e.Row.FindControl("ltOSType") as Literal;
                if (ltOSType != null)
                {
                    int OSType = Util.ConvertToInt32(ltOSType.Text);
                    ltOSType.Text = UICommon.Em.EnumUtil.GetOSTitle(OSType);
                }
            }
            catch { }
        }
        #endregion

        #region 是否开启
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.UserInfoExtraEntity entity = DAL.UserInfoExtraDAL.Get_99(userInfo.ID, "IsOnlineServices,OSJsCode");
            int IsOnlineServices = UICommon.Util.ConvertToInt32(entity.IsOnlineServices);
            int status = 1;
            if (IsOnlineServices == 1)
            {
                status = 0;
            }
            SqlParameter[] pramsModify =
			{
				DALUtil.MakeInParam("@IsOnlineServices",SqlDbType.Int,4,status) 
			};
            int row_mod = DAL.UserInfoExtraDAL.Modify(pramsModify, userInfo.ID);
            if (row_mod > 0)
            {
                Response.Redirect("OS_List.aspx");
            }
        }
        protected void btnSaveOnline_Click(object sender, EventArgs e)
        {
            string OSJsCode = txtOSJsCode.Value;
            SqlParameter[] pramsModify =
			{ 
                DALUtil.MakeInParam("@OSJsCode",SqlDbType.NText,OSJsCode.Length,OSJsCode),
			};
            int row_mod = DAL.UserInfoExtraDAL.Modify(pramsModify, userInfo.ID);
            if (row_mod > 0)
            {
                UICommon.ScriptHelper.ShowAndRedirect("保存成功", "OS_List.aspx"); 
            }
        }

        #endregion
    }
}