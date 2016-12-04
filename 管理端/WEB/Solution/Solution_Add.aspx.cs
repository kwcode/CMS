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


namespace WEB.Solution
{
    public partial class Solution_Add : UICommon.BasePage_PM
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
                List<Model.SolutionClass1Entity> class1List = DAL.SolutionClass1DAL.GetList<SolutionClass1Entity>("*", pramsWhere, "OrderNum");

                ddlSolutionClass1.DataSource = class1List;
                ddlSolutionClass1.DataTextField = "Title";
                ddlSolutionClass1.DataValueField = "ID";
                ddlSolutionClass1.DataBind();
                ddlSolutionClass1.Items.Insert(0, new ListItem("请选择", ""));

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //获取最大的排序id
            int OrderNum = 1;
            try
            {
                OrderNum = Util.ConvertToInt32(DAL.SolutionDAL.GetSingle(" Max(OrderNum) ", " AND UserID=" + userInfo.ID));
                OrderNum = ++OrderNum;
            }
            catch { }
            try
            {
                string Title = UICommon.Util.ConvertToString(txtTitle.Value).Trim();
                int SolutionClass1_ID = Util.ConvertToInt32(ddlSolutionClass1.SelectedValue);
                string Summay = UICommon.Util.ConvertToString(txtSummay.Value).Trim();
                string TxtContent = hide_Content.Value = Server.HtmlDecode(UICommon.Util.ConvertToString(Request["content"]));//这个是ueditor.all.js 里面默认的值 
                string TitlePictures = UICommon.Util.ConvertToString(Request["hide_ImgPath"]);
                SqlParameter[] pramsAdd =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title),
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum), 
                    DAL.DALUtil.MakeInParam("@MaturityDate",System.Data.SqlDbType.DateTime,8,DateTime.Now),  
                    DAL.DALUtil.MakeInParam("@SolutionClass1_ID",System.Data.SqlDbType.Int,4,SolutionClass1_ID),  
                    DAL.DALUtil.MakeInParam("@Summay",System.Data.SqlDbType.NText,Summay.Length,Summay), 
                    DAL.DALUtil.MakeInParam("@TxtContent",System.Data.SqlDbType.NText,TxtContent.Length,TxtContent),
                    DAL.DALUtil.MakeInParam("@TitlePictures",System.Data.SqlDbType.NVarChar,250,TitlePictures),
                };
                int row_Add = DAL.SolutionDAL.Add(pramsAdd);
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