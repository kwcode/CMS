using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.GL.PictureText
{
    public partial class PictureTextClass1_Add : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int maxOrder = DAL.PictureTextClass1DAL.GetSingle("MAX(OrderNum)");
                txtOrderNum.Value = UICommon.Util.ConvertToString(maxOrder + 1);
                int maxValue = DAL.PictureTextClass1DAL.GetSingle("MAX(ValueNum)");
                txtValueNum.Value = UICommon.Util.ConvertToString(maxValue + 1);

                //SqlParameter[] pramsWhere =
                //    { 
                //        DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                //    };
                List<BackSectionsSetEntity> class1List = DAL.BackSectionsSetDAL.GetList<BackSectionsSetEntity>("Title,ValueNum", null, "OrderNum");
                ddlBackSectionsSet.DataSource = class1List;
                ddlBackSectionsSet.DataTextField = "Title";
                ddlBackSectionsSet.DataValueField = "ValueNum";
                ddlBackSectionsSet.DataBind();
                ddlBackSectionsSet.Items.Insert(0, new ListItem("请选择栏目分组", ""));

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ValueNum = UICommon.Util.ConvertToInt32(txtValueNum.Value);
                //是否存在
                int isExist = DAL.PictureTextClass1DAL.GetSingle("Count(0)", " ValueNum=" + ValueNum);
                if (isExist == 1)
                {
                    UICommon.ScriptHelper.Alert("值已经存在,请换一个值。");
                    return;
                }
                string Title = txtTitle.Value.Trim();
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value);
                string Description = txtDescription.Value.Trim();
                SqlParameter[] pramsAdd =
                    {
                        DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title), 
                        DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),
                        DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,userInfo.ID),
                        DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                        DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,200,Description),
                        DAL.DALUtil.MakeInParam("@CreateTS",System.Data.SqlDbType.DateTime,8,DateTime.Now), 
                    };
                int row_Add = DAL.PictureTextClass1DAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    //ltMsg.Visible = true;
                    //ltMsg.Text = title + ",保存成功！";
                    txtTitle.Value = string.Empty;
                    txtOrderNum.Value = UICommon.Util.ConvertToString(OrderNum + 1);
                    txtValueNum.Value = UICommon.Util.ConvertToString(ValueNum + 1);
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

        protected void ddlBackSectionsSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem item = ddlBackSectionsSet.SelectedItem;
            string title = item.Text;
            string valueNum = item.Value;
            txtTitle.Value = title;
            txtValueNum.Value = valueNum;
            txtDescription.Value = title;

        }
    }
}