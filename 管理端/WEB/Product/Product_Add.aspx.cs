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

namespace WEB.Product
{
    public partial class Product_Add : UICommon.BasePage_PM
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
                List<ProductClass1Entity> ProductClass1List = DAL.ProductClass1DAL.GetList<ProductClass1Entity>("*", pramsWhere, "OrderNum");
                ddlProductClass1.DataSource = ProductClass1List;
                ddlProductClass1.DataTextField = "Title";
                ddlProductClass1.DataValueField = "ID";
                ddlProductClass1.DataBind();
                ddlProductClass1.Items.Insert(0, new ListItem("请选择", ""));

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //获取最大的排序id
            int OrderNum = 1;
            try
            {
                OrderNum = Util.ConvertToInt32(ProductDAL.GetSingle(" Max(OrderNum) ", "AND UserID=" + userInfo.ID));
                OrderNum = ++OrderNum;
            }
            catch { }
            try
            {
                string Title = UICommon.Util.ConvertToString(txtTitle.Value).Trim();
                int ProductClass1_ID = Util.ConvertToInt32(ddlProductClass1.SelectedValue);
                int ProductClass2_ID = Util.ConvertToInt32(ddlProductClass2.SelectedValue);
                int ProductClass3_ID = Util.ConvertToInt32(ddlProductClass3.SelectedValue);
                int ProductStatus = Util.ConvertToInt32(ddlProductStatus.SelectedValue);
                decimal MarketPrice = Util.ConvertToDecimal(txtMarketPrice.Value.Trim());
                decimal MemberPrice = Util.ConvertToDecimal(txtMemberPrice.Value.Trim());
                string Summay = UICommon.Util.ConvertToString(txtSummay.Value).Trim();
                string SeoTitle = UICommon.Util.ConvertToString(txtSeoTitle.Value).Trim();
                string Units = UICommon.Util.ConvertToString(txtUnits.Value).Trim();
                int ProductNum = Util.ConvertToInt32(txtProductNum.Value);
                string TxtContent = hide_Content.Value = Server.HtmlDecode(UICommon.Util.ConvertToString(Request["content"]));//这个是ueditor.all.js 里面默认的值 
                string TitlePictures = UICommon.Util.ConvertToString(Request["hide_ImgPath"]);
                SqlParameter[] pramsAdd =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title),
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),
                    DAL.DALUtil.MakeInParam("@AddDate",System.Data.SqlDbType.DateTime,8,DateTime.Now), 
                    DAL.DALUtil.MakeInParam("@MaturityDate",System.Data.SqlDbType.DateTime,8,DateTime.Now),  
                    DAL.DALUtil.MakeInParam("@ProductClass1_ID",System.Data.SqlDbType.Int,4,ProductClass1_ID),
                    DAL.DALUtil.MakeInParam("@ProductClass2_ID",System.Data.SqlDbType.Int,4,ProductClass2_ID),
                    DAL.DALUtil.MakeInParam("@ProductClass3_ID",System.Data.SqlDbType.Int,4,ProductClass3_ID),
                    DAL.DALUtil.MakeInParam("@ProductStatus",System.Data.SqlDbType.Int,4,ProductStatus),

                    DAL.DALUtil.MakeInParam("@MarketPrice",System.Data.SqlDbType.Decimal,18,MarketPrice),
                    DAL.DALUtil.MakeInParam("@MemberPrice",System.Data.SqlDbType.Decimal,18,MemberPrice),
                    DAL.DALUtil.MakeInParam("@ProductNum",System.Data.SqlDbType.Int,4,ProductNum),
                    DAL.DALUtil.MakeInParam("@Units",System.Data.SqlDbType.NVarChar,50,Units),

                    DAL.DALUtil.MakeInParam("@Summay",System.Data.SqlDbType.NText,Summay.Length,Summay),
                    DAL.DALUtil.MakeInParam("@SeoTitle",System.Data.SqlDbType.NVarChar,300,SeoTitle),
                    DAL.DALUtil.MakeInParam("@TxtContent",System.Data.SqlDbType.NText,TxtContent.Length,TxtContent),
                    DAL.DALUtil.MakeInParam("@TitlePictures",System.Data.SqlDbType.NVarChar,250,TitlePictures),
                };
                int row_Add = DAL.ProductDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    //新增成功则跳转到图片管理
                    //Response.Redirect("ProductImages_List.aspx?ID=" + row_Add);
                    UICommon.ScriptHelper.ShowAndRedirect(Title + ",保存成功 ", "ProductImages_List.aspx?ID=" + row_Add);
                    ltMsg.Visible = true;
                    ltMsg.Text = Title + ",保存成功！";
                    txtTitle.Value = string.Empty;

                    //ImgPath讲图片保存
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

        #region 选择事件

        protected void ddlProductClass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ProductClass1_ID = UICommon.Util.ConvertToInt32(ddlProductClass1.SelectedValue);
            if (ProductClass1_ID > 0)
            {
                SqlParameter[] pramsWhere =
                {
                    DAL.DALUtil.MakeInParam("@ProductClass1_ID",System.Data.SqlDbType.Int,4,ProductClass1_ID),
                };
                List<ProductClass2Entity> ProductClass2List = DAL.ProductClass2DAL.GetList<ProductClass2Entity>("*", pramsWhere, "OrderNum");
                ddlProductClass2.DataSource = ProductClass2List;
                ddlProductClass2.DataTextField = "Title";
                ddlProductClass2.DataValueField = "ID";
                ddlProductClass2.DataBind();
                ddlProductClass2.Items.Insert(0, new ListItem("请选择", ""));
            }
            else
            {
                ddlProductClass2.Items.Clear();
            }
            ddlProductClass3.Items.Clear();
        }

        protected void ddlProductClass2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ProductClass2_ID = UICommon.Util.ConvertToInt32(ddlProductClass2.SelectedValue);
            if (ProductClass2_ID > 0)
            {
                SqlParameter[] pramsWhere =
                {
                    DAL.DALUtil.MakeInParam("@ProductClass2_ID",System.Data.SqlDbType.Int,4,ProductClass2_ID),
                };
                List<ProductClass3Entity> ProductClass3List = DAL.ProductClass3DAL.GetList<ProductClass3Entity>("*", pramsWhere, "OrderNum");
                ddlProductClass3.DataSource = ProductClass3List;
                ddlProductClass3.DataTextField = "Title";
                ddlProductClass3.DataValueField = "ID";
                ddlProductClass3.DataBind();
                ddlProductClass3.Items.Insert(0, new ListItem("请选择", ""));
            }
            else
            {
                ddlProductClass3.Items.Clear();
            }
        }

        #endregion
    }
}