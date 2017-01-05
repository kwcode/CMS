using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.SEO
{
    public partial class Seo_Set : UICommon.BasePage_PM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //检测是否第一次进来设置 如果是则需要新建一条数据
                Model.SEOEntity entity = DAL.SEODAL.Get_98(userInfo.ID, "*");
                if (entity != null && entity.ID > 0)
                {
                    txtTitle.Value = entity.Title;
                    txtKeyWords.Value = entity.KeyWords;
                    txtDescription.Value = entity.Description;
                }
                else
                {
                    SqlParameter[] pramsAdd = 
                    { 
                       DALUtil.MakeInParam("@UserID",SqlDbType.Int,4,userInfo.ID)                       
                    };
                    DAL.SEODAL.Add(pramsAdd);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Value;
            string keywords = txtKeyWords.Value;
            string description = txtDescription.Value;
            SqlParameter[] pramsModify = 
                    { 
                       DALUtil.MakeInParam("@Title",SqlDbType.NText,title.Length,title),
                       DALUtil.MakeInParam("@KeyWords",SqlDbType.NText,keywords.Length,keywords),
                       DALUtil.MakeInParam("@Description",SqlDbType.NText,description.Length,description),
                    };
            SqlParameter[] pramsWhere = 
                    { 
                       DALUtil.MakeInParam("@UserID",SqlDbType.Int,4,userInfo.ID)                       
                    };
            int row = DAL.SEODAL.Modify(pramsModify, pramsWhere);
            if (row > 0)
            {
                UICommon.ScriptHelper.Alert("保存成功");
            }
            else
            {
                UICommon.ScriptHelper.Alert("保存失败");
            }
        }
    }
}