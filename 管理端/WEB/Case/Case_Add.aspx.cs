using DAL;
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
    public partial class Case_Add : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UICommon.Util.No_Back();
                //一类
                SqlParameter[] pramsWhere =
                { 
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                };
                List<Model.CaseClass1Entity> class1List = DAL.CaseClass1DAL.GetList<CaseClass1Entity>("*", pramsWhere, "OrderNum");

                ddlCaseClass1.DataSource = class1List;
                ddlCaseClass1.DataTextField = "Title";
                ddlCaseClass1.DataValueField = "ID";
                ddlCaseClass1.DataBind();
                ddlCaseClass1.Items.Insert(0, new ListItem("请选择", ""));

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //获取最大的排序id
            int OrderNum = 1;
            try
            {
                OrderNum = Util.ConvertToInt32(DAL.CaseDAL.GetSingle(" Max(OrderNum) ", " AND UserID=" + userInfo.ID));
                OrderNum = ++OrderNum;
            }
            catch { }
            try
            {
                string Title = UICommon.Util.ConvertToString(txtTitle.Value).Trim();
                int CaseClass1_ID = Util.ConvertToInt32(ddlCaseClass1.SelectedValue);
                string Summay = UICommon.Util.ConvertToString(txtSummay.Value).Trim();
                string TxtContent = hide_Content.Value = Server.HtmlDecode(UICommon.Util.ConvertToString(Request["content"]));//这个是ueditor.all.js 里面默认的值 
                string TitlePictures = UICommon.Util.ConvertToString(Request["hide_ImgPath"]);
                SqlParameter[] pramsAdd =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title),
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
                    DAL.DALUtil.MakeInParam("@MaturityDate",System.Data.SqlDbType.DateTime,8,DateTime.Now),  
                    DAL.DALUtil.MakeInParam("@CaseClass1_ID",System.Data.SqlDbType.Int,4,CaseClass1_ID),  
                    DAL.DALUtil.MakeInParam("@Summay",System.Data.SqlDbType.NText,Summay.Length,Summay), 
                    DAL.DALUtil.MakeInParam("@TxtContent",System.Data.SqlDbType.NText,TxtContent.Length,TxtContent),
                    DAL.DALUtil.MakeInParam("@TitlePictures",System.Data.SqlDbType.NVarChar,250,TitlePictures),
                };
                int row_Add = DAL.CaseDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    txtTitle.Value = string.Empty;
                    hide_Content.Value = "";
                    txtSummay.Value = "";
                    hide_ImgPath.Value = "";
                    UICommon.ScriptHelper.Alert(Title + ",保存成功！");
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