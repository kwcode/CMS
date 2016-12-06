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

namespace WEB.GL.Fun
{
    public partial class CopyTC : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        List<OperateLog> OperateLogList = new List<OperateLog>();
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                int FromUserID = UICommon.Util.ConvertToInt32(txtFromUserID.Text);
                if (FromUserID == userInfo.ID)
                {
                    UICommon.ScriptHelper.Alert("自己不能复制给自己");
                    return;
                }
                if (cb_BackgroundMenu.Checked)
                {
                    CopyBackgroundMenu(FromUserID);
                }
                if (cb_NavigationBar.Checked)
                {
                    CopyNavigationBar(FromUserID);
                } 

                Rep1.DataSource = OperateLogList;
                Rep1.DataBind();
            }
            catch (Exception ex)
            {
                UICommon.ScriptHelper.Alert(ex.Message);
            }
        }

        #region 复制后台栏目
        private void CopyBackgroundMenu(int FromUserID)
        {
            SqlParameter[] pramsWhere =
				{
					DALUtil.MakeInParam("@UserID", SqlDbType.Int, 4, FromUserID)
				};
            #region 复制后台一类
            List<BackgroundMenuClass1Entity> entityClass1List = DAL.BackgroundMenuClass1DAL.GetList<Model.BackgroundMenuClass1Entity>("*", pramsWhere, "OrderNum");
            foreach (BackgroundMenuClass1Entity item in entityClass1List)
            {
                SqlParameter[] pramsAdd = DAL.DALUtil.GetParams<BackgroundMenuClass1Entity>(item, userInfo.ID);
                int row_1 = DAL.BackgroundMenuClass1DAL.Add(pramsAdd);
                OperateLogList.Add(new OperateLog("后台一类,ID=" + row_1));
            }
            #endregion

            #region 复制后台栏目
            List<BackgroundMenuEntity> entityList = DAL.BackgroundMenuDAL.GetList<Model.BackgroundMenuEntity>("*", pramsWhere, "OrderNum");
            foreach (BackgroundMenuEntity item in entityList)
            {
                SqlParameter[] pramsAdd = DAL.DALUtil.GetParams<BackgroundMenuEntity>(item, userInfo.ID);
                int row_2 = DAL.BackgroundMenuDAL.Add(pramsAdd);
                OperateLogList.Add(new OperateLog("后台栏目,ID=" + row_2));
            }
            #endregion

        }
        #endregion

        #region 复制前台栏目
        private void CopyNavigationBar(int FromUserID)
        {
            SqlParameter[] pramsWhere =
				{
					DALUtil.MakeInParam("@UserID", SqlDbType.Int, 4, FromUserID)
				};
            List<NavigationBarEntity> entityClass1List = DAL.NavigationBarDAL.GetList<Model.NavigationBarEntity>("*", pramsWhere, "OrderNum");
            foreach (NavigationBarEntity item in entityClass1List)
            {
                SqlParameter[] pramsAdd = DAL.DALUtil.GetParams<NavigationBarEntity>(item, userInfo.ID);
                int row_1 = DAL.NavigationBarDAL.Add(pramsAdd);
                OperateLogList.Add(new OperateLog("前台栏目,ID=" + row_1));
            }
        }
        #endregion
        /// <summary>
        /// 操作日志
        /// </summary>
        private class OperateLog
        {
            private string _txtContent = "";
            public string TxtContent
            {
                get { return _txtContent; }
                set { _txtContent = value; }
            }
            public OperateLog(string txtContent)
            {
                this.TxtContent = txtContent;
            }
        }
    }
}