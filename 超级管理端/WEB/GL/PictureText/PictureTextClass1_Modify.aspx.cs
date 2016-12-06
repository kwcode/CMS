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
    public partial class PictureTextClass1_Modify : UICommon.BasePage_PM
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
                Model.PictureTextClass1Entity entity = DAL.PictureTextClass1DAL.Get_99(ID, "*");
                txtOrderNum.Value = UICommon.Util.ConvertToString(entity.OrderNum);
                txtValueNum.Value = UICommon.Util.ConvertToString(entity.ValueNum);
                txtTitle.Value = entity.Title;
                txtDescription.Value = entity.Description;
                #region 请选择栏目分组
                
                List<BackSectionsSetEntity> class1List = DAL.BackSectionsSetDAL.GetList<BackSectionsSetEntity>("Title,ValueNum", null, "OrderNum");
                ddlBackSectionsSet.DataSource = class1List;
                ddlBackSectionsSet.DataTextField = "Title";
                ddlBackSectionsSet.DataValueField = "ValueNum";
                ddlBackSectionsSet.DataBind();
                ddlBackSectionsSet.Items.Insert(0, new ListItem("请选择栏目分组", ""));
                #endregion 
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ValueNum = UICommon.Util.ConvertToInt32(txtValueNum.Value);
                //是否存在
                int isExist = DAL.PictureTextClass1DAL.GetSingle("Count(0)", " ValueNum=" + ValueNum + " AND ID <> " + ID);
                if (isExist == 1)
                {
                    UICommon.ScriptHelper.Alert("值已经存在,请换一个值。");
                    return;
                }
                string Title = txtTitle.Value.Trim();
                int OrderNum = UICommon.Util.ConvertToInt32(txtOrderNum.Value);
                string Description = txtDescription.Value.Trim();
                SqlParameter[] pramsModify =
                    {
                        DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,Title), 
                        DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,OrderNum),
                        DAL.DALUtil.MakeInParam("@ValueNum",System.Data.SqlDbType.Int,4,ValueNum),
                        DAL.DALUtil.MakeInParam("@Description",System.Data.SqlDbType.NVarChar,200,Description), 
                    };
                int row_Add = DAL.PictureTextClass1DAL.Modify(pramsModify, ID);
                if (row_Add > 0)
                {
                    //ltmsg.visible = true;
                    //ltmsg.text = title + ",保存成功！"; 
                    UICommon.ScriptHelper.Alert(Title + ",修改成功！");
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