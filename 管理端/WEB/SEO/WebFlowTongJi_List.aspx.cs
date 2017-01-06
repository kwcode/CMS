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

namespace WEB.SEO
{
    public partial class WebFlowTongJi_List : UICommon.BasePage_PM
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
        public int IsIpTongJi { get; set; }
        //是否开启流量监控
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Model.UserInfoExtraEntity entity = DAL.UserInfoExtraDAL.Get_99(userInfo.ID, "IsIpTongJi,TJJsCode");
                IsIpTongJi = UICommon.Util.ConvertToInt32(entity.IsIpTongJi);
                txtTJJsCode.Value = entity.TJJsCode;
                if (IsIpTongJi == 0)
                {
                    btnSave.Text = "开启流量统计";
                    txtTJJsCode.Visible = true;
                }
                else
                {
                    btnSaveTJ.Visible = false;
                    btnSave.Text = "关闭统计";
                    txtTJJsCode.Visible = false;
                }



                //Model.UserInfoEntity user = DAL.UserInfoDAL.Get_99(userInfo.ID, "IsIpTongJi");
                //IsIpTongJi = UICommon.Util.ConvertToInt32(user.IsIpTongJi);
                //if (IsIpTongJi == 1)
                //{
                //    btnSave.Text = "关闭统计";
                //}
                //else
                //{
                //    btnSave.Text = "开启流量统计";
                //}
                BindData();
            }
        }
        #region 绑定数据

        private void BindData()
        {
            System.Text.StringBuilder sqlWhere = new System.Text.StringBuilder();
            sqlWhere.Append(" UserID=" + userInfo.ID);
            TotalCount = DAL.WebFlowTongJiDAL.GetRecordCount(sqlWhere.ToString());
            List<Model.WebFlowTongJiEntity> productList = DAL.WebFlowTongJiDAL.GetPageList<Model.WebFlowTongJiEntity>(PageIndex, PageSize, "*", sqlWhere.ToString(), "ID DESC");
            gv_List.DataSource = productList;
            gv_List.DataBind();
        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.UserInfoExtraEntity entity = DAL.UserInfoExtraDAL.Get_99(userInfo.ID, "IsIpTongJi,TJJsCode");
            int IsIpTongJi = UICommon.Util.ConvertToInt32(entity.IsIpTongJi);
            int status = 1;
            if (IsIpTongJi == 1)
            {
                status = 0;
            }
            SqlParameter[] pramsModify =
			{
				DALUtil.MakeInParam("@IsIpTongJi",SqlDbType.Int,4,status)
			};
            int row_mod = DAL.UserInfoExtraDAL.Modify(pramsModify, userInfo.ID);
            if (row_mod > 0)
            {
                Response.Redirect("WebFlowTongJi_List.aspx");
            }
            else
            {
                UICommon.ScriptHelper.Alert("修改失败！");
            }
        }

        protected void btnSaveTJ_Click(object sender, EventArgs e)
        {
            string TJJsCode = txtTJJsCode.Value;
            SqlParameter[] pramsModify =
			{ 
                DALUtil.MakeInParam("@TJJsCode",SqlDbType.NText,TJJsCode.Length,TJJsCode),
			};
            int row_mod = DAL.UserInfoExtraDAL.Modify(pramsModify, userInfo.ID);
            if (row_mod > 0)
            {
                UICommon.ScriptHelper.ShowAndRedirect("保存成功", "WebFlowTongJi_List.aspx");
            }
        }
    }
}