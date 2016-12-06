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

namespace WEB.GL.BackgroundMenu
{
    public partial class BackgroundMenu_Add : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int maxOrder = DAL.BackgroundMenuDAL.GetSingle("MAX(OrderNum)", "UserID=" + userInfo.ID);
                txtOrderNum.Value = UICommon.Util.ConvertToString(maxOrder + 1);
                int maxValueNum = DAL.BackgroundMenuDAL.GetSingle("MAX(ValueNum)", "UserID=" + userInfo.ID);
                txtValueNum.Value = UICommon.Util.ConvertToString(maxValueNum + 1); ;
                #region 一类
                SqlParameter[] pramsWhere =
				{
					DALUtil.MakeInParam("@UserID", SqlDbType.Int, 4, userInfo.ID)
				};
                List<BackgroundMenuClass1Entity> entityList = DAL.BackgroundMenuClass1DAL.GetList<BackgroundMenuClass1Entity>("Title,ValueNum", pramsWhere, "OrderNum");
                ddlBackgroundMenuClass1.DataSource = entityList;
                ddlBackgroundMenuClass1.DataTextField = "Title";
                ddlBackgroundMenuClass1.DataValueField = "ValueNum";
                ddlBackgroundMenuClass1.DataBind();
                ddlBackgroundMenuClass1.Items.Insert(0, new ListItem("请选择", ""));
                #endregion

                #region ddlBackSectionsSet
                List<BackSectionsSetEntity> BackSectionsSetList = DAL.BackSectionsSetDAL.GetList<Model.BackSectionsSetEntity>("Title,ManageUrl,ValueNum", null, "OrderNum");
                ddlBackSectionsSet.DataSource = BackSectionsSetList;
                ddlBackSectionsSet.DataTextField = "ManageUrl";
                ddlBackSectionsSet.DataValueField = "ID";
                ddlBackSectionsSet.DataBind();
                ddlBackSectionsSet.Items.Insert(0, new ListItem("请选择管理地址", ""));
                #endregion
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtTitle.Value.Trim();
                string ManageUrl = txtManageUrl.Value.Trim();
                string Description = txtDescription.Value.Trim();
                int ValueNum = UICommon.Util.ConvertToInt32(txtValueNum.Value.Trim());
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value.Trim());
                int BackgroundMenuClass1_ValueNum = UICommon.Util.ConvertToInt32(ddlBackgroundMenuClass1.Value);
                SqlParameter[] pramsAdd =
                    {
                        DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,title),
                        DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                        DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),
                        DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                        DAL.DALUtil.MakeInParam("@ManageUrl",System.Data.SqlDbType.NVarChar,500,ManageUrl),
                        DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,200,Description),  
                        DAL.DALUtil.MakeInParam("@BackgroundMenuClass1_ValueNum",System.Data.SqlDbType.Int,4,BackgroundMenuClass1_ValueNum),
                        DAL.DALUtil.MakeInParam("@CreateTS",System.Data.SqlDbType.DateTime,8,DateTime.Now),  
                    };
                int row_Add = DAL.BackgroundMenuDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    txtTitle.Value = string.Empty;
                    txtOrderNum.Value = UICommon.Util.ConvertToString(OrderNum + 1);
                    txtValueNum.Value = UICommon.Util.ConvertToString(ValueNum + 1);
                    UICommon.ScriptHelper.Alert(title + "，保存成功！");
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

        protected void ddlBackSectionsSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBackSectionsSet.SelectedValue != "")
            {
                ListItem item = ddlBackSectionsSet.SelectedItem;
                txtManageUrl.Value = item.Text;
            }
        }

    }
}