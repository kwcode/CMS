using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UICommon;

namespace WEB.Case
{
    public partial class Case_Modify : UICommon.BasePage_PM
    {
        public int ID
        {
            get
            {
                return UICommon.Util.ConvertToInt32(Request["ID"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UICommon.Util.No_Back();

                #region 一类
                SqlParameter[] pramsWhere =
                { 
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                };
                List<CaseClass1Entity> class1List = DAL.CaseClass1DAL.GetList<Model.CaseClass1Entity>("*", pramsWhere, "OrderNum");
                ddlCaseClass1.DataSource = class1List;
                ddlCaseClass1.DataTextField = "Title";
                ddlCaseClass1.DataValueField = "ID";
                ddlCaseClass1.DataBind();
                ddlCaseClass1.Items.Insert(0, new ListItem("请选择", "0"));

                #endregion

                #region 获取信息赋值
                Model.CaseEntity entity = DAL.CaseDAL.Get_99(ID, "*");
                txtTitle.Value = UICommon.Util.ConvertToString(entity.Title);
                txtSummay.Value = UICommon.Util.ConvertToString(entity.Summay);
                hide_Content.Value = entity.TxtContent;//这个是ueditor.all.js 里面默认的值 
                hide_ImgPath.Value = entity.TitlePictures;

                #endregion


                ddlCaseClass1.SelectedValue = UICommon.Util.ConvertToString(entity.CaseClass1_ID);


            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Title = UICommon.Util.ConvertToString(txtTitle.Value).Trim();
                int CaseClass1_ID = Util.ConvertToInt32(ddlCaseClass1.SelectedValue);
                string Summay = UICommon.Util.ConvertToString(txtSummay.Value).Trim();
                string TxtContent = hide_Content.Value = Server.HtmlDecode(UICommon.Util.ConvertToString(Request["content"]));//这个是ueditor.all.js 里面默认的值 
                string TitlePictures = hide_ImgPath.Value = UICommon.Util.ConvertToString(Request["hide_ImgPath"]);
                SqlParameter[] pramsModify =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title),
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),  
                    DAL.DALUtil.MakeInParam("@MaturityDate",System.Data.SqlDbType.DateTime,8,DateTime.Now),  
                    DAL.DALUtil.MakeInParam("@CaseClass1_ID",System.Data.SqlDbType.Int,4,CaseClass1_ID),  

                    DAL.DALUtil.MakeInParam("@Summay",System.Data.SqlDbType.NText,Summay.Length,Summay), 
                    DAL.DALUtil.MakeInParam("@TxtContent",System.Data.SqlDbType.NText,TxtContent.Length,TxtContent),
                    DAL.DALUtil.MakeInParam("@TitlePictures",System.Data.SqlDbType.NVarChar,250,TitlePictures),
                };
                int row_Mod = DAL.CaseDAL.Modify(pramsModify, ID);
                if (row_Mod > 0)
                {
                    //ltMsg.Visible = true;
                    //ltMsg.Text = Title + ",修改成功！";
                    UICommon.ScriptHelper.Alert(Title + ",修改成功 ");

                }
                else
                {
                    UICommon.ScriptHelper.Alert("保存失败");
                }
            }
            catch (Exception ex)
            {
                UICommon.ScriptHelper.Alert("保存失败," + ex.Message);
            }
        }

    }
}